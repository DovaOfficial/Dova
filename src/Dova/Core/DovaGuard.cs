using Dova.Core.Jni;
using Dova.Core.Runtime;

namespace Dova.Core;

public static class DovaGuard
{
    public static void CheckReturnCode(int returnCode)
    {
        var jniReturnValue = (JniReturnValue)returnCode;
        
        if (jniReturnValue == JniReturnValue.JNI_OK)
        {
            return;
        }

        throw new DovaException($"JNI Exception Caught: {jniReturnValue}");
    }

    public static void CheckForException()
    {
        if (!DovaVM.Runtime.ExceptionCheck())
        {
            return;
        }
        
        var exceptionPtr = DovaVM.Runtime.ExceptionOccurred();
        var throwableClass = DovaVM.Runtime.GetObjectClass(exceptionPtr);
        var getMessageMethod = DovaVM.Runtime.GetMethodId(throwableClass, "getMessage", "()Ljava/lang/String;");
        var messagePtr = DovaVM.Runtime.CallObjectMethod(exceptionPtr, getMessageMethod);
        var message = DovaVM.Runtime.GetString(messagePtr);

        throw new DovaException($"JNI exception occurred: '{message}'");
    }
}