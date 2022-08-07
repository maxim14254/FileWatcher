#include "pch.h"
#include "lib.h"
#include <iostream>
#include <windows.h>
#include <ShellApi.h>
#include <comdef.h>


bool Lib::IsElevated() //проверкa уровня привилегий
{
	BOOL fRet = FALSE;
	HANDLE hToken = NULL;

	if (OpenProcessToken(GetCurrentProcess(), TOKEN_QUERY, &hToken)) {
		TOKEN_ELEVATION Elevation;
		DWORD cbSize = sizeof(TOKEN_ELEVATION);
		if (GetTokenInformation(hToken, TokenElevation, &Elevation, sizeof(Elevation), &cbSize)) {
			fRet = Elevation.TokenIsElevated;
		}
	}
	if (hToken) {
		CloseHandle(hToken);
	}
	return fRet;

};

bool  MYSUPERTOKEN = false;

void Lib::RunWithAdmin(wchar_t* path)
{
	SHELLEXECUTEINFO ShExecInfo;
	ShExecInfo.cbSize = sizeof(SHELLEXECUTEINFO);
	ShExecInfo.fMask = NULL;
	ShExecInfo.hwnd = NULL;
	ShExecInfo.lpVerb = L"runas";
	ShExecInfo.lpFile = path;
	ShExecInfo.lpParameters = NULL;
	ShExecInfo.lpDirectory = NULL;
	ShExecInfo.nShow = SW_SHOW;
	ShExecInfo.hInstApp = NULL;
	MYSUPERTOKEN = true;
	if (TRUE == ShellExecuteEx(&ShExecInfo))
	{
		//MYSUPERTOKEN = true;
		WaitForSingleObject(ShExecInfo.hProcess, INFINITE);
		CloseHandle(GetCurrentProcess());
		CloseHandle(GetCurrentThread());
	}

};

BSTR Lib::GetCatalogs(wchar_t* path)
{
	std::wstring catalogs;
	WIN32_FIND_DATA wfd;

	HANDLE const hFind = FindFirstFileW(path, &wfd);

	if (INVALID_HANDLE_VALUE != hFind)
	{
		do
		{
			std::wstring str = wfd.cFileName;
			catalogs += str + L"|";
			//std::wcout < < &wfd.cFileName[0] < < std::endl;
		} while (NULL != FindNextFile(hFind, &wfd));

		FindClose(hFind);
	}

	return SysAllocString(catalogs.c_str());
};

void Lib::Token()
{
	MYSUPERTOKEN = true;
};

bool Lib::IsChangeCatalog(wchar_t* path)
{
	_bstr_t b(path);
	const char* paht2 = b;
	struct stat st;
	stat(paht2, &st);

	time_t t = st.st_mtime;
	time_t t2;
	while (true)
	{
		Sleep(1000);
		stat(paht2, &st);
		t2 = st.st_mtime;

		if (t < t2 || MYSUPERTOKEN)
		{
			MYSUPERTOKEN = false;
			return true;
		}
	}

};

