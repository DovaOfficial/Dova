using Dova.Core;
using Dova.Core.Runtime;

namespace Dova.Common.Arrays;

internal static class JavaArrayStrings
{
    private const string StringClassSignature = "Ljava/lang/String;";

    private static IntPtr StringClassPtr { get; }
    private static IntPtr EmptyStringPtr { get; }

    static JavaArrayStrings()
    {
        StringClassPtr = DovaVM.Runtime.FindClass(StringClassSignature);
        EmptyStringPtr = DovaVM.Runtime.GetString(string.Empty);
    }
    
    public static IntPtr Initialize(int count) => 
        DovaVM.Runtime.NewObjectArray(count, StringClassPtr, EmptyStringPtr);

    public static void ReadBuffer<TElement>(JavaArray<TElement> array)
    {
        for (var index = 0; index < array.Count; ++index)
        {
            var ptr = DovaVM.Runtime.GetObjectArrayElement(array.CurrentRefPtr, index);
            var str = DovaVM.Runtime.GetString(ptr);

            array.Buffer[index] = (TElement)(object)str;
        }
    }

    public static void WriteBuffer<TElement>(JavaArray<TElement> array)
    {
        for (var index = 0; index < array.Count; ++index)
        {
            var element = array.Buffer[index];
            var elementStr = element.ToString();
            var strPtr = DovaVM.Runtime.GetString(elementStr);
            
            DovaVM.Runtime.SetObjectArrayElement(array.CurrentRefPtr, index, strPtr);
        }
    }
}