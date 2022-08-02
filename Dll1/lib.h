class Lib
{
public:
	bool IsElevated();
}; 

extern "C" __declspec(dllexport) void* Create()
{
	return (void*) new Lib();
}

extern "C" __declspec(dllexport) bool IsElevated(Lib* lip)
{
	return lip->IsElevated();
}