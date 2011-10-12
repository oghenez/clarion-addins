// UacHelpers.CppLibrary.h

#pragma once

using namespace System::Diagnostics;
using namespace System::Security::Principal;

namespace UacHelpers {

	///<summary>
	///Provides facilities for enabling and disabling User Account Control (UAC),
	///determining elevation and virtualization status, and launching a process
	///under elevated credentials.
	///</summary>
	///<remarks>
	///Note that there's a delicate scenario where the registry key has already been
	///changed, but the user has not logged off yet so the token hasn't been filtered.
	///In that case, we will think that UAC is on but the user is not an admin (because
	///the token is not a split token).
	///</remarks>
	public ref class UserAccountControl abstract sealed
	{
	public:
		///<summary>
		///Returns <b>true</b> if the current user has administrator privileges.
		///</summary>
		///<remarks>
		///If UAC is on, then this property will return <b>true</b> even if the
		///current process is not running elevated.  If UAC is off, then this
		///property will return <b>true</b> if the user is part of the built-in
		///<i>Administrators</i> group.
		///</remarks>
		static property bool IsUserAdmin
        {
            bool get();
        }

		///<summary>
		///Returns <b>true</b> if User Account Control (UAC) is enabled on
		///this machine.
		///</summary>
		///<remarks>
		///This value is obtained by checking the LUA registry key.  It is possible
		///that the user has not restarted the machine after enabling/disabling UAC.
		///In that case, the value of the registry key does not reflect the true state
		///of affairs.  It is possible to devise a custom solution that would provide
		///a mechanism for tracking whether a restart occurred since UAC settings were
		///changed (using the RunOnce mechanism, temporary files, or volatile registry keys).
		///</remarks>
		static property bool IsUacEnabled
        {
            bool get();
        }

		///<summary>
		///Returns <b>true</b> if the current process is using UAC virtualization.
		///</summary>
		///<remarks>
		///Under UAC virtualization, file system and registry accesses to specific
		///locations performed by an application are redirected to provide backwards-
		///compatibility.  64-bit applications or applications that have an associated
		///manifest do not enjoy UAC virtualization because they are assumed to be
		///compatible with Vista and UAC.
		///</remarks>
        static property bool IsCurrentProcessVirtualized
        {
            bool get();
        }

		///<summary>
		///Returns <b>true</b> if the current process is elevated, i.e. if the process
		///went through an elevation consent phase.
		///</summary>
		///<remarks>
		///This property will return <b>false</b> if UAC is disabled and the process
		///is running as admin.  It only determines whether the process went through
		///the elevation procedure.
		///</remarks>
		static property bool IsCurrentProcessElevated
        {
            bool get();
        }

		///<summary>
		///Disables User Account Control by changing the LUA registry key.
		///The changes do not have effect until the system is restarted.
		///</summary>
		static void DisableUac();
		
		///<summary>
		///Disables User Account Control and restarts the system.
		///</summary>
		static void DisableUacAndRestartWindows();

		///<summary>
		///Enables User Account Control by changing the LUA registry key.
		///The changes do not have effect until the system is restarted.
		///</summary>
		static void EnableUac();

		///<summary>
		///Enables User Account Control and restarts the system.
		///</summary>
		static void EnableUacAndRestartWindows();

		///<summary>
		///Creates a process under the elevated token, regardless of UAC settings
		///or the manifest associated with that process.
		///</summary>
		///<param name="exePath">The path to the executable file.</param>
		///<param name="arguments">The command-line arguments to pass to the process.</param>
		///<returns>A <see cref="Process"/> object representing the newly created process.</returns>
		static Process^ CreateProcessAsAdmin(System::String^ exePath, System::String^ arguments);

		///<summary>
		///Creates a process under the standard user if the current process is elevated.  The identity
		///of the standard user is determined by retrieving the user token of the currently running Explorer
		//(shell) process.  If the current process is not elevated, the standard user is used.
		///</summary>
		///<param name="exePath">The path to the executable file.</param>
		///<param name="arguments">The command-line arguments to pass to the process.</param>
		///<returns>A <see cref="Process"/> object representing the newly created process.</returns>
		static Process^ CreateProcessAsStandardUser(System::String^ exePath, System::String^ arguments);

	private:
		static int GetProcessTokenElevationType();
		static void SetUacRegistryValue(bool enable);
		static void RestartWindows();

		static System::String^ UacRegistryKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";
		static System::String^ UacRegistryValue = "EnableLUA";
	};
}	// end namespace UacHelpers