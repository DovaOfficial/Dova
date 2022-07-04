using Dova.Internals.Interop;

namespace Dova.Core.Runtime;

internal unsafe class JavaRuntime : IJavaRuntime
{
    private readonly JavaVM* _vm;
    private readonly JNIEnv* _env;
    private readonly JNINativeInterface* _func;

    public JavaRuntime(JavaVM* vm, JNIEnv* env)
    {
        _vm = vm;
        _env = env;
        _func = _env->functions;
    }

    public int GetVersion() => _func->GetVersion(_env);
    // TODO: DefineClass method
    public IntPtr FindClass(string className) => _func->FindClass(_env, className);
    public IntPtr FromReflectedMethod(IntPtr obj) => _func->FromReflectedMethod(_env, obj);
}