using System.Runtime.InteropServices;

namespace Dova.Internals.Interop;

[StructLayout(LayoutKind.Sequential)]
internal struct JNINativeMethod
{
    public string name;
    public string signature;
    public IntPtr fnPtr;
}