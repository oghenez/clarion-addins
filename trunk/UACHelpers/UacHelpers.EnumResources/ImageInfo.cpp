#include "ImageInfo.h"

#include <windows.h>
#include <dbghelp.h>

#pragma comment (lib, "dbghelp.lib")

#include <vcclr.h>

namespace UacHelpers {

	ImageInfo::ImageInfo(System::String^ imageName)
		: _architecture(System::Reflection::ProcessorArchitecture::None)
	{
		pin_ptr<const wchar_t> FileName = PtrToStringChars(imageName);
		HANDLE FileHandle = CreateFile(FileName, GENERIC_READ, FILE_SHARE_READ, NULL, OPEN_EXISTING, 0, NULL);
		if (FileHandle == INVALID_HANDLE_VALUE)
			throw gcnew System::ArgumentException("Error opening file: " + imageName, "imageName");

		HANDLE FileMapping = NULL;
		LPVOID BaseAddress = NULL;
		try
		{
			FileMapping = CreateFileMapping(FileHandle, NULL, PAGE_READONLY, 0, 0, NULL);
			if (FileMapping == NULL)
				throw gcnew System::ComponentModel::Win32Exception(GetLastError());

			BaseAddress = MapViewOfFile(FileMapping, FILE_MAP_READ, 0, 0, 0);
			if (BaseAddress == NULL)
				throw gcnew System::ComponentModel::Win32Exception(GetLastError());

			PIMAGE_NT_HEADERS ImageHeader = ImageNtHeader(BaseAddress);
			switch (ImageHeader->FileHeader.Machine)
			{
			case IMAGE_FILE_MACHINE_AMD64:
				_architecture = System::Reflection::ProcessorArchitecture::Amd64;
				break;
			case IMAGE_FILE_MACHINE_IA64:
				_architecture = System::Reflection::ProcessorArchitecture::IA64;
				break;
			case IMAGE_FILE_MACHINE_I386:
				_architecture = System::Reflection::ProcessorArchitecture::X86;
				break;
			default:
				_architecture = System::Reflection::ProcessorArchitecture::None;
			}
		}
		finally
		{
			if (BaseAddress != NULL && UnmapViewOfFile(BaseAddress) == FALSE)
				throw gcnew System::ComponentModel::Win32Exception(GetLastError());
			if (FileMapping != NULL)
				CloseHandle(FileMapping);
			CloseHandle(FileHandle);
		}
	}

	System::Reflection::ProcessorArchitecture ImageInfo::Architecture::get()
	{
		return _architecture;
	}

}