using System.Runtime.InteropServices;
using Dova.Internals.Types.Arrays;
using Dova.Internals.Types.Primitives;

namespace Dova.Internals.Types.Objects;

[StructLayout(LayoutKind.Explicit)]
internal struct JValue
{
    [FieldOffset(0)] 
    private JBoolean z;

    [FieldOffset(0)] 
    private JByte b;

    [FieldOffset(0)] 
    private JChar c;

    [FieldOffset(0)] 
    private JShort s;

    [FieldOffset(0)] 
    private JInt i;

    [FieldOffset(0)] 
    private JLong j;

    [FieldOffset(0)] 
    private JFloat f;

    [FieldOffset(0)] 
    private JDouble d;

    [FieldOffset(0)] 
    private JObject l;
    
    // Primitives
    public static implicit operator JValue(JBoolean value) => new() { z = value };
    public static implicit operator JValue(bool value) => new() { z = value };
    public static implicit operator JValue(JByte value) => new() { b = value };
    public static implicit operator JValue(byte value) => new() { b = value };
    public static implicit operator JValue(JChar value) => new() { c = value };
    public static implicit operator JValue(char value) => new() { c = value };
    public static implicit operator JValue(JShort value) => new() { s = value };
    public static implicit operator JValue(short value) => new() { s = value };
    public static implicit operator JValue(JInt value) => new() { i = value };
    public static implicit operator JValue(int value) => new() { i = value };
    public static implicit operator JValue(JLong value) => new() { j = value };
    public static implicit operator JValue(long value) => new() { j = value };
    public static implicit operator JValue(JFloat value) => new() { f = value };
    public static implicit operator JValue(float value) => new() { f = value };
    public static implicit operator JValue(JDouble value) => new() { d = value };
    public static implicit operator JValue(double value) => new() { d = value };
    
    // Objects
    public static implicit operator JValue(JObject value) => new() { l = value };
    public static implicit operator JValue(JClass value) => new() { l = value };
    public static implicit operator JValue(JString value) => new() { l = value };
    public static implicit operator JValue(JThrowable value) => new() { l = value };
    public static implicit operator JValue(JWeak value) => new() { l = value };
    public static implicit operator JValue(IntPtr value) => new() { l = value };
    
    // Arrays
    public static implicit operator JValue(JArray value) => new() { l = value };
    public static implicit operator JValue(JBooleanArray value) => new() { l = value };
    public static implicit operator JValue(JByteArray value) => new() { l = value };
    public static implicit operator JValue(JCharArray value) => new() { l = value };
    public static implicit operator JValue(JDoubleArray value) => new() { l = value };
    public static implicit operator JValue(JFloatArray value) => new() { l = value };
    public static implicit operator JValue(JIntArray value) => new() { l = value };
    public static implicit operator JValue(JLongArray value) => new() { l = value };
    public static implicit operator JValue(JObjectArray value) => new() { l = value };
    public static implicit operator JValue(JShortArray value) => new() { l = value };
}