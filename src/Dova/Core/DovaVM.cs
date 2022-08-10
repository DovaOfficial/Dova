using Dova.Core.Jni;
using Dova.Core.Runtime;
using Dova.Internals.Interop;

namespace Dova.Core;

/// <summary>
/// </summary>
/// <author>
/// https://github.com/Sejoslaw/Dova
/// </author>
public unsafe class DovaVM
{
    private static DovaConfiguration Config { get; set; }

    public static IJavaRuntime Runtime { get; private set; }

    public static void Initialize(DovaConfiguration config)
    {
        Config = config;
        Runtime = Initialize();
    }

    private static IJavaRuntime Initialize()
    {
        var builder = new DovaOptionsBuilder(Config);
        
        var javaVmOptions = builder.BuildJavaVmOptions();

        DovaLoader.SetupJvm(Config);

        var javaVmInitArgs = builder.BuildJavaVmInitArgs(javaVmOptions);
        var javaVmInitArgsPtr = builder.BuildJavaVmInitArgsPtr(javaVmInitArgs);
        
        JavaVM* vm;
        JNIEnv* env;

        var returnCode = JniFunctions.JNI_CreateJavaVM(&vm, (void**)&env, (void*)javaVmInitArgsPtr);

        DovaGuard.CheckReturnCode(returnCode);

        return new JavaRuntime(vm, env);
    }
}