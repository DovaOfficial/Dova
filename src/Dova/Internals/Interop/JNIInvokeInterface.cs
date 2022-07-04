using System.Runtime.InteropServices;
using Dova.Internals.Types.Primitives;

namespace Dova.Internals.Interop;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct JNIInvokeInterface
{
    public IntPtr reserved0;
    public IntPtr reserved1;
    public IntPtr reserved2;

    public delegate* unmanaged<JavaVM*, JInt> DestroyJavaVM;
    public delegate* unmanaged<JavaVM*, void**, void*, JInt> AttachCurrentThread;
    public delegate* unmanaged<JavaVM*, JInt> DetachCurrentThread;
    public delegate* unmanaged<JavaVM*, void**, JInt, JInt> GetEnv;
    public delegate* unmanaged<JavaVM*, void**, void*, JInt> AttachCurrentThreadAsDaemon;
}