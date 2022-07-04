using System.Runtime.InteropServices;

namespace Dova.Internals.Interop;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct JNIEnv
{
    public JNINativeInterface* functions;
}