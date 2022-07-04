using System.Runtime.InteropServices;
using Dova.Internals.Types.Objects;

namespace Dova.Internals.Types.Arrays;

[StructLayout(LayoutKind.Explicit)]
internal struct JFloatArray
{
    [FieldOffset(0)] 
    private JArray arr;

    [FieldOffset(0)] 
    private JObject obj;
    
    public static implicit operator JFloatArray(JArray value) => new() { arr = value };
    
    public static implicit operator JFloatArray(JObject value) => new() { obj = value };

    public static implicit operator JObject(JFloatArray value) => value.obj;
    
    public static implicit operator JArray(JFloatArray value) => value.arr;
}