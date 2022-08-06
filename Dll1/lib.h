#include <iostream>
#include <vector>
#include <comdef.h>

class Lib
{
public:
	bool IsElevated();
	void RunWithAdmin(wchar_t* path);
	BSTR GetCatalogs(wchar_t* path);
	bool IsChangeCatalog(wchar_t* path);
	void Token();
}; 

extern "C" __declspec(dllexport) void* Create()
{
	return (void*) new Lib();
}

extern "C" __declspec(dllexport) bool IsElevated(Lib* lip)
{
	return lip->IsElevated();
}

extern "C" __declspec(dllexport) void RunWithAdmin(Lib * lip, wchar_t* path)
{
	return  lip->RunWithAdmin(path);
}

extern "C" __declspec(dllexport) BSTR GetCatalogs(Lib * lip, wchar_t* path)
{
	return  lip->GetCatalogs(path);
}

extern "C" __declspec(dllexport) bool IsChangeCatalog(Lib * lip, wchar_t* path)
{
	return  lip->IsChangeCatalog(path);
}

extern "C" __declspec(dllexport) void Token(Lib * lip)
{
	return  lip->Token();
}