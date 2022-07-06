using Dova.Internals.Types.Arrays;
using Dova.Internals.Types.Objects;
using Dova.Internals.Types.Primitives;

namespace Dova.Internals;

public static class Converters
{
    public static IReadOnlyDictionary<Type, Type> JavaToDotnetTypes { get; } = new Dictionary<Type, Type>
    {
        // Arrays
        { typeof(JArray), typeof(IntPtr) },
        { typeof(JBooleanArray), typeof(IntPtr) },
        { typeof(JByteArray), typeof(IntPtr) },
        { typeof(JCharArray), typeof(IntPtr) },
        { typeof(JDoubleArray), typeof(IntPtr) },
        { typeof(JFloatArray), typeof(IntPtr) },
        { typeof(JIntArray), typeof(IntPtr) },
        { typeof(JLongArray), typeof(IntPtr) },
        { typeof(JObjectArray), typeof(IntPtr) },
        { typeof(JShortArray), typeof(IntPtr) },
        // Objects
        { typeof(JClass), typeof(IntPtr) },
        { typeof(JFieldId), typeof(IntPtr) },
        { typeof(JMethodId), typeof(IntPtr) },
        { typeof(JObject), typeof(IntPtr) },
        { typeof(JSize), typeof(int) },
        { typeof(JString), typeof(IntPtr) },
        { typeof(JThrowable), typeof(IntPtr) },
        { typeof(JWeak), typeof(IntPtr) },
        // Primitives
        { typeof(JBoolean), typeof(bool) },
        { typeof(JByte), typeof(byte) },
        { typeof(JChar), typeof(char) },
        { typeof(JDouble), typeof(double) },
        { typeof(JFloat), typeof(float) },
        { typeof(JInt), typeof(int) },
        { typeof(JLong), typeof(long) },
        { typeof(JShort), typeof(short) },
    };

    internal static JValue[] ToArray(params object[] values) => 
        values.Select(ToJValue).ToArray();

    internal static JValue ToJValue(object obj) => obj switch
    {
        // Primitives
        bool b => b,
        byte b => b,
        char c => c,
        double d => d,
        float f => f,
        int i => i,
        long l => l,
        short s => s,
        
        // Objects
        JObject jo => jo,
        JClass jc => jc,
        JString js => js,
        JThrowable jt => jt,
        JWeak jw => jw,
        IntPtr ptr => ptr,
        
        // Arrays
        JArray ja => ja,
        JBooleanArray jba => jba,
        JByteArray jba => jba,
        JCharArray jca => jca,
        JDoubleArray jda => jda,
        JFloatArray jfa => jfa,
        JIntArray jia => jia,
        JLongArray jla => jla,
        JObjectArray joa => joa,
        JShortArray jsa => jsa,
    };
}