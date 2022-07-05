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
        // Extra
        { typeof(string), typeof(string) },
    };
}