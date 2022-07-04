using System.Runtime.InteropServices;
using Dova.Internals.Types.Objects;

namespace Dova.Internals.Types.Arrays;

[StructLayout(LayoutKind.Explicit)]
internal struct JCharArray
{
    [FieldOffset(0)] 
    private JArray arr;

    [FieldOffset(0)] 
    private JObject obj;
    
    public static implicit operator JCharArray(JArray value) => new() { arr = value };
    
    public static implicit operator JCharArray(JObject value) => new() { obj = value };

    public static implicit operator JObject(JCharArray value) => value.obj;
    
    public static implicit operator JArray(JCharArray value) => value.arr;
}