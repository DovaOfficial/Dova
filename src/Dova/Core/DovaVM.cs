using Dova.Core.Jni;
using Dova.Core.Runtime;
using Dova.Internals.Interop;

namespace Dova.Core;

/// <summary>
/// </summary>
/// <author>
/// https://github.com/Sejoslaw/Dova
/// </author>
public unsafe class DovaVM : IDisposable
{
    private DovaConfiguration Config { get; }

    public IJavaRuntime Runtime { get; }

    public DovaVM(DovaConfiguration config)
    {
        Config = config;
        Runtime = Initialize();
    }

    public void Dispose()
    {
    }

    private IJavaRuntime Initialize()
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