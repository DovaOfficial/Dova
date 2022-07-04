using System.Runtime.InteropServices;
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
    

    public static implicit operator JValue(JBoolean value) => new() { z = value };
    
    public static implicit operator JValue(JByte value) => new() { b = value };
    
    public static implicit operator JValue(JChar value) => new() { c = value };
    
    public static implicit operator JValue(JShort value) => new() { s = value };
    
    public static implicit operator JValue(JInt value) => new() { i = value };
    
    public static implicit operator JValue(JLong value) => new() { j = value };
    
    public static implicit operator JValue(JFloat value) => new() { f = value };
    
    public static implicit operator JValue(JDouble value) => new() { d = value };
    
    public static implicit operator JValue(JObject value) => new() { l = value };

    public static implicit operator JBoolean(JValue value) => value.z;

    public static implicit operator JByte(JValue value) => value.b;
    
    public static implicit operator JChar(JValue value) => value.c;
    
    public static implicit operator JShort(JValue value) => value.s;
    
    public static implicit operator JInt(JValue value) => value.i;
    
    public static implicit operator JLong(JValue value) => value.j;
    
    public static implicit operator JFloat(JValue value) => value.f;
    
    public static implicit operator JDouble(JValue value) => value.d;
    
    public static implicit operator JObject(JValue value) => value.l;
}