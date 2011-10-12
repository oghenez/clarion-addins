#include "EnumResources.h"

#include <windows.h>
#include <vcclr.h>

#include <vector>
using namespace std;

#pragma comment(lib, "user32.lib")

using namespace System::Collections::Generic;
using namespace System::Diagnostics;
using namespace System::Runtime::InteropServices;

namespace UacHelpers {

	struct ResourceCollectionWrapper
	{
		gcroot<List<FileResource>^> Resources;
	};

	BOOL CALLBACK ResourceNameEnum(HMODULE hModule, LPCWSTR lpType, LPWSTR lpName, LONG_PTR lParam)
	{
		ResourceCollectionWrapper* pResources = (ResourceCollectionWrapper*)lParam;

		HRSRC hRes = FindResource(hModule, lpName, lpType);
		HGLOBAL hGlob = LoadResource(hModule, hRes);
		LPVOID data = LockResource(hGlob);
		DWORD dwSize = SizeofResource(hModule, hRes);

		array<System::Byte>^ resourceValue = gcnew array<System::Byte>(dwSize);
		Marshal::Copy((System::IntPtr)data, resourceValue, 0, dwSize);

		UnlockResource(hGlob);
		FreeResource(hGlob);

		System::String^ resourceType = ((LONG_PTR)lpType & 0xFFFF0000) ? gcnew System::String(lpType) : "";
		System::String^ resourceName = ((LONG_PTR)lpName & 0xFFFF0000) ? gcnew System::String(lpName) : "";

		pResources->Resources->Add(FileResource(resourceType, resourceName, resourceValue));

		return TRUE;
	}

	BOOL CALLBACK ResourceTypeEnum(HMODULE hModule, LPWSTR lpType, LONG_PTR lParam)
	{
		return EnumResourceNames(hModule, lpType, ResourceNameEnum, lParam);
	}

	array<FileResource>^ FileResources::GetResources()
	{
		pin_ptr<const wchar_t> pModule = PtrToStringChars(_fileName);
		HMODULE hModule = ::LoadLibrary(pModule);

		ResourceCollectionWrapper wrapper;
		wrapper.Resources = gcnew List<FileResource>();
		EnumResourceTypes(hModule, ResourceTypeEnum, (LONG_PTR)&wrapper);

		FreeLibrary(hModule);

		return wrapper.Resources->ToArray();
	}

}