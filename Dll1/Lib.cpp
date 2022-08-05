#include "pch.h"
#include "lib.h"
#include <iostream>
#include <windows.h>
#include <ShellApi.h>


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

void Lib::RunWithAdmin(char* path)
{
	ShellExecuteA(NULL, "runas", path, NULL, NULL, SW_SHOW);	
};
