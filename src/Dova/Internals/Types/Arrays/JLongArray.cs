using System.Runtime.InteropServices;
using Dova.Internals.Types.Objects;

namespace Dova.Internals.Types.Arrays;

[StructLayout(LayoutKind.Explicit)]
internal struct JLongArray
{
    [FieldOffset(0)] 
    private JArray arr;

    [FieldOffset(0)] 
    private JObject obj;
    
    public static implicit operator JLongArray(JArray value) => new() { arr = value };
    
    public static implicit operator JLongArray(JObject value) => new() { obj = value };

    public static implicit operator JObject(JLongArray value) => value.obj;
    
    public static implicit operator JArray(JLongArray value) => value.arr;
}