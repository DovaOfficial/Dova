using System.Runtime.InteropServices;
using Dova.Internals.Types.Objects;

namespace Dova.Internals.Types.Arrays;

[StructLayout(LayoutKind.Explicit)]
internal struct JDoubleArray
{
    [FieldOffset(0)] 
    private JArray arr;

    [FieldOffset(0)] 
    private JObject obj;
    
    public static implicit operator JDoubleArray(JArray value) => new() { arr = value };
    
    public static implicit operator JDoubleArray(JObject value) => new() { obj = value };
    
    public static implicit operator JDoubleArray(IntPtr value) => new() { obj = value, arr = value };

    public static implicit operator JObject(JDoubleArray value) => value.obj;
    
    public static implicit operator JArray(JDoubleArray value) => value.arr;
    
    public static implicit operator IntPtr(JDoubleArray value) => value.arr != IntPtr.Zero ? value.arr : value.obj;
}