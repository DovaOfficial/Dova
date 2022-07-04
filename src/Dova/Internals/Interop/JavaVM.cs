using System.Runtime.InteropServices;

namespace Dova.Internals.Interop;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct JavaVM
{
    public JNIInvokeInterface* functions;
}