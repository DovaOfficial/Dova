using System.Runtime.InteropServices;
using Dova.Internals.Interop;
using Dova.Internals.Types.Objects;
using Dova.Internals.Types.Primitives;

namespace Dova.Core.Jni;

internal static unsafe class JniFunctions
{
    public const string JavaJvmLibName = "jvm";
    
    [DllImport(JavaJvmLibName, CallingConvention = CallingConvention.Winapi)]
    public static extern JInt JNI_GetDefaultJavaVMInitArgs(void* vm_args);
    
    [DllImport(JavaJvmLibName, CallingConvention = CallingConvention.Winapi)]
    public static extern JInt JNI_CreateJavaVM(JavaVM** p_vm, void** p_env, void* vm_args);
    
    [DllImport(JavaJvmLibName, CallingConvention = CallingConvention.Winapi)]
    public static extern JInt JNI_GetCreatedJavaVMs(JavaVM** vmBuf, JSize bufLen, JSize* nVMs);
}