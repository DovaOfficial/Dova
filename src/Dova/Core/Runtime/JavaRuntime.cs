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
    public IntPtr FromReflectedField(IntPtr obj) => _func->FromReflectedField(_env, obj);
    public IntPtr ToReflectedMethod(IntPtr jClass, IntPtr jMethodId, bool isStatic) => _func->ToReflectedMethod(_env, jClass, jMethodId, isStatic);
    public IntPtr GetSuperclass(IntPtr jClass) => _func->GetSuperclass(_env, jClass);
    public bool IsAssignableFrom(IntPtr jClass1, IntPtr jClass2) => _func->IsAssignableFrom(_env, jClass1, jClass2);
}