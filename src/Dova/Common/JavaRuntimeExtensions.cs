using Dova.Core.Runtime;

namespace Dova.Common;

/// <summary>
/// Extension methods which are specific for JavaObject's.
/// Thy are similar to basic JNI methods to simplify class generation.
/// </summary>
public static unsafe class JavaRuntimeExtensions
{
    public static string GetStringField(this IJavaRuntime runtime, IntPtr javaObj, IntPtr fieldId)
    {
        var strPtr = runtime.GetObjectField(javaObj, fieldId);
        var str = runtime.GetString(strPtr);
        return str;
    }

    public static void SetStringField(this IJavaRuntime runtime, IntPtr javaObj, IntPtr fieldId, string value)
    {
        var strPtr = runtime.GetString(value);
        runtime.SetObjectField(javaObj, fieldId, strPtr);
    }
    
    public static string GetStaticStringField(this IJavaRuntime runtime, IntPtr javaClass, IntPtr fieldId)
    {
        var strPtr = runtime.GetStaticObjectField(javaClass, fieldId);
        var str = runtime.GetString(strPtr);
        return str;
    }
    
    public static void SetStaticStringField(this IJavaRuntime runtime, IntPtr javaClass, IntPtr fieldId, string value)
    {
        var strPtr = runtime.GetString(value);
        runtime.SetStaticObjectField(javaClass, fieldId, strPtr);
    }

    public static string CallStringMethodA(this IJavaRuntime runtime, IntPtr javaObj, IntPtr methodId, params object[] jvalue)
    {
        var strPtr = runtime.CallObjectMethodA(javaObj, methodId, jvalue);
        var str = runtime.GetString(strPtr);
        return str;
    }
    
    public static string CallStaticStringMethodA(this IJavaRuntime runtime, IntPtr javaClass, IntPtr methodId, params object[] jvalue)
    {
        var strPtr = runtime.CallStaticObjectMethodA(javaClass, methodId, jvalue);
        var str = runtime.GetString(strPtr);
        return str;
    }

    public static JavaArray<TElement> GetArrayField<TElement>(this IJavaRuntime runtime, IntPtr javaObj, IntPtr fieldId)
    {
        var arrPtr = runtime.GetObjectField(javaObj, fieldId);
        return new JavaArray<TElement>(arrPtr);
    }
    
    public static void SetArrayField<TElement>(this IJavaRuntime runtime, IntPtr javaObj, IntPtr fieldId, JavaArray<TElement> arr)
    {
        runtime.SetObjectField(javaObj, fieldId, arr.CurrentRefPtr);
    }
    
    public static JavaArray<TElement> GetStaticArrayField<TElement>(this IJavaRuntime runtime, IntPtr javaClass, IntPtr fieldId)
    {
        var arrPtr = runtime.GetStaticObjectField(javaClass, fieldId);
        return new JavaArray<TElement>(arrPtr);
    }
    
    public static void SetStaticArrayField<TElement>(this IJavaRuntime runtime, IntPtr javaClass, IntPtr fieldId, JavaArray<TElement> arr)
    {
        runtime.SetStaticObjectField(javaClass, fieldId, arr.CurrentRefPtr);
    }
    
    public static JavaArray<TElement> CallArrayMethodA<TElement>(this IJavaRuntime runtime, IntPtr javaObj, IntPtr methodId, params object[] jvalue)
    {
        var arrPtr = runtime.CallObjectMethodA(javaObj, methodId, jvalue);
        return new JavaArray<TElement>(arrPtr);
    }
    
    public static JavaArray<TElement> CallStaticArrayMethodA<TElement>(this IJavaRuntime runtime, IntPtr javaClass, IntPtr methodId, params object[] jvalue)
    {
        var arrPtr = runtime.CallStaticObjectMethodA(javaClass, methodId, jvalue);
        return new JavaArray<TElement>(arrPtr);
    }
}