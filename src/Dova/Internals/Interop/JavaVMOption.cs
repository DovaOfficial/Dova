using System.Runtime.InteropServices;

namespace Dova.Internals.Interop;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct JavaVMOption
{
    public string optionString;
    public void* extraInfo;
}