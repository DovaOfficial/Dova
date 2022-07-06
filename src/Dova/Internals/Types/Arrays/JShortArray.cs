using System.Runtime.InteropServices;
using Dova.Internals.Types.Objects;

namespace Dova.Internals.Types.Arrays;

[StructLayout(LayoutKind.Explicit)]
internal struct JShortArray
{
    [FieldOffset(0)] 
    private JArray arr;

    [FieldOffset(0)] 
    private JObject obj;
    
    public static implicit operator JShortArray(JArray value) => new() { arr = value };
    
    public static implicit operator JShortArray(JObject value) => new() { obj = value };
    
    public static implicit operator JShortArray(IntPtr value) => new() { obj = value, arr = value };

    public static implicit operator JObject(JShortArray value) => value.obj;
    
    public static implicit operator JArray(JShortArray value) => value.arr;
    
    public static implicit operator IntPtr(JShortArray value) => value.arr != IntPtr.Zero ? value.arr : value.obj;
}