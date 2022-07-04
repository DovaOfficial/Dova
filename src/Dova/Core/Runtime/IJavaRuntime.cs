namespace Dova.Core.Runtime;

public interface IJavaRuntime
{
    int GetVersion();
    IntPtr FindClass(string className);
}