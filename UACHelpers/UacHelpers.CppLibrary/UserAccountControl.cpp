#include "UserAccountControl.h"

#include <windows.h>
#pragma comment (lib, "kernel32.lib")
#pragma comment (lib, "advapi32.lib")

#include <vcclr.h>
#include <msclr\marshal.h>
#include <msclr\marshal_windows.h>

using namespace msclr::interop;

using namespace System::ComponentModel;
using namespace Microsoft::Win32;

namespace UacHelpers {

	Process^ UserAccountControl::CreateProcessAsAdmin(System::String^ exePath, System::String^ arguments)
    {
        ProcessStartInfo^ psi = gcnew ProcessStartInfo(exePath, arguments);
        psi->UseShellExecute = true;
        psi->Verb = "runas";
		return Process::Start(psi);
    }

	Process^ UserAccountControl::CreateProcessAsStandardUser(System::String^ exePath, System::String^ arguments)
	{
		marshal_context context;

		//If the current process is not elevated, then there's no reason to go through the hassle --
		//just use the standard System.Diagnostics.Process facilities.
		if (!IsCurrentProcessElevated)
		{
			return Process::Start(exePath, arguments);
		}

		//The following implementation is roughly based on Aaron Margosis' post:
		//http://blogs.msdn.com/aaron_margosis/archive/2009/06/06/faq-how-do-i-start-a-program-as-the-desktop-user-from-an-elevated-app.aspx

		//Enable SeIncreaseQuotaPrivilege in this process.  (This requires administrative privileges.)
		HANDLE hProcessToken = NULL;
		if (!OpenProcessToken(GetCurrentProcess(), TOKEN_ADJUST_PRIVILEGES, &hProcessToken))
		{
			throw gcnew Win32Exception(GetLastError());
		}
		else
		{
			TOKEN_PRIVILEGES tkp;
			tkp.PrivilegeCount = 1;
			LookupPrivilegeValueW(NULL, SE_INCREASE_QUOTA_NAME, &tkp.Privileges[0].Luid);
			tkp.Privileges[0].Attributes = SE_PRIVILEGE_ENABLED;
			AdjustTokenPrivileges(hProcessToken, FALSE, &tkp, 0, NULL, NULL);
			DWORD dwLastErr = GetLastError();
			CloseHandle(hProcessToken);
			if (ERROR_SUCCESS != dwLastErr)
			{
				throw gcnew Win32Exception(dwLastErr);
			}
		}

		//Get window handle representing the desktop shell.  This might not work if there is no shell window, or when
		//using a custom shell.  Also note that we're assuming that the shell is not running elevated.
		HWND hShellWnd = GetShellWindow();
		if (hShellWnd == NULL)
		{
			throw gcnew System::InvalidOperationException("Unable to locate shell window; you might be using a custom shell");
		}

		//Get the ID of the desktop shell process.
		DWORD dwShellPID;
		GetWindowThreadProcessId(hShellWnd, &dwShellPID);
		if (dwShellPID == 0)
		{
			throw gcnew Win32Exception(GetLastError());
		}

		//Open the desktop shell process in order to get the process token.
		HANDLE hShellProcess = OpenProcess(PROCESS_QUERY_INFORMATION, FALSE, dwShellPID);
		if (hShellProcess == NULL)
		{
			throw gcnew Win32Exception(GetLastError());
		}

		HANDLE hShellProcessToken = NULL;
		HANDLE hPrimaryToken = NULL;
		try
		{
			//Get the process token of the desktop shell.
			if (!OpenProcessToken(hShellProcess, TOKEN_DUPLICATE, &hShellProcessToken))
			{
				throw gcnew Win32Exception(GetLastError());
			}

			//Duplicate the shell's process token to get a primary token.
			const DWORD dwTokenRights = TOKEN_QUERY | TOKEN_ASSIGN_PRIMARY | TOKEN_DUPLICATE | TOKEN_ADJUST_DEFAULT | TOKEN_ADJUST_SESSIONID;
			if (!DuplicateTokenEx(hShellProcessToken, dwTokenRights, NULL, SecurityImpersonation, TokenPrimary, &hPrimaryToken))
			{
				throw gcnew Win32Exception(GetLastError());
			}

			//Start the target process with the new token.
			STARTUPINFO si = {0}; si.cb = sizeof(si);
			PROCESS_INFORMATION pi = {0};
			if (!CreateProcessWithTokenW(hPrimaryToken, 0,
				context.marshal_as<LPCWSTR>(exePath), context.marshal_as<LPWSTR>(exePath + " " + arguments),
				0, NULL, NULL, &si, &pi))
			{
				throw gcnew Win32Exception(GetLastError());
			}
			CloseHandle(pi.hProcess);
			CloseHandle(pi.hThread);

			return Process::GetProcessById(pi.dwProcessId);
		}
		finally
		{
			if (hShellProcessToken != NULL)
				CloseHandle(hShellProcessToken);

			if (hPrimaryToken != NULL)
				CloseHandle(hPrimaryToken);

			if (hShellProcess != NULL)
				CloseHandle(hShellProcess);
		}
	}

	bool UserAccountControl::IsUserAdmin::get()
	{
		if (UserAccountControl::IsUacEnabled)
			return GetProcessTokenElevationType() != TokenElevationTypeDefault;	//split token

		//If UAC is off, we can't rely on the token; check for Admin group.
		return WindowsPrincipal(WindowsIdentity::GetCurrent()).IsInRole("Administrators");
	}

	bool UserAccountControl::IsUacEnabled::get()
	{
		//Check the HKLM\Software\Microsoft\Windows\CurrentVersion\Policies\System\EnableLUA registry value.
		RegistryKey^ key = Registry::LocalMachine->OpenSubKey(UacRegistryKey, false);
		return key->GetValue(UacRegistryValue)->Equals(1);
    }

	void UserAccountControl::DisableUac()
	{
		SetUacRegistryValue(false);
	}

	void UserAccountControl::DisableUacAndRestartWindows()
	{
		DisableUac();
		RestartWindows();
	}

	void UserAccountControl::EnableUac()
	{
		SetUacRegistryValue(true);
	}

	void UserAccountControl::EnableUacAndRestartWindows()
	{
		EnableUac();
		RestartWindows();
	}

	bool UserAccountControl::IsCurrentProcessElevated::get()
	{
		return GetProcessTokenElevationType() == TokenElevationTypeFull;	//elevated
	}

	bool UserAccountControl::IsCurrentProcessVirtualized::get()
	{
		HANDLE hToken;
        try
        {
            if (!OpenProcessToken(GetCurrentProcess(), TOKEN_QUERY, &hToken))
				throw gcnew Win32Exception(GetLastError());

            DWORD virtualizationEnabled;
			DWORD dwSize;
			if (!GetTokenInformation(hToken, TokenVirtualizationEnabled, &virtualizationEnabled, sizeof(virtualizationEnabled), &dwSize))
                throw gcnew Win32Exception(GetLastError());

			return virtualizationEnabled != 0;
        }
        finally
        {
            CloseHandle(hToken);
        }
	}

	int UserAccountControl::GetProcessTokenElevationType()
	{
		HANDLE hToken;
        try
        {
            if (!OpenProcessToken(GetCurrentProcess(), TOKEN_QUERY, &hToken))
				throw gcnew Win32Exception(GetLastError());

            TOKEN_ELEVATION_TYPE elevationType;
			DWORD dwSize;
            if (!GetTokenInformation(hToken, TokenElevationType, &elevationType, sizeof(elevationType), &dwSize))
                throw gcnew Win32Exception(GetLastError());

			return elevationType;
        }
        finally
        {
            CloseHandle(hToken);
        }
	}

	void UserAccountControl::SetUacRegistryValue(bool enabled)
	{
		RegistryKey^ key = Registry::LocalMachine->OpenSubKey(UacRegistryKey, true);
		key->SetValue(UacRegistryValue, enabled ? 1 : 0);
	}

	void UserAccountControl::RestartWindows()
	{
		InitiateSystemShutdownEx(NULL, NULL, 0/*Timeout*/,
								 TRUE/*ForceAppsClosed*/, TRUE/*RebootAfterShutdown*/,
								 SHTDN_REASON_MAJOR_OPERATINGSYSTEM | SHTDN_REASON_MINOR_RECONFIG | SHTDN_REASON_FLAG_PLANNED);
		//This shutdown flag corresponds to: "Operating System: Reconfiguration (Planned)".
	}
}