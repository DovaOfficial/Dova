using Dova.Core.Jni;

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
}