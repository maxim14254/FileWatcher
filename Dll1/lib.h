#include <iostream>

class Lib
{
public:
	bool IsElevated();
	void RunWithAdmin(char* path);
}; 

extern "C" __declspec(dllexport) void* Create()
{
	return (void*) new Lib();
}

extern "C" __declspec(dllexport) bool IsElevated(Lib* lip)
{
	return lip->IsElevated();
}

extern "C" __declspec(dllexport) void RunWithAdmin(Lib * lip, char* path)
{
	return  lip->RunWithAdmin(path);
}
