using System.Runtime.InteropServices;
using Dova.Internals.Types.Objects;

namespace Dova.Internals.Types.Arrays;

[StructLayout(LayoutKind.Explicit)]
internal struct JObjectArray
{
    [FieldOffset(0)] 
    private JArray arr;

    [FieldOffset(0)] 
    private JObject obj;
    
    public static implicit operator JObjectArray(JArray value) => new() { arr = value };
    
    public static implicit operator JObjectArray(JObject value) => new() { obj = value };
    
    public static implicit operator JObjectArray(IntPtr value) => new() { obj = value, arr = value };

    public static implicit operator JObject(JObjectArray value) => value.obj;
    
    public static implicit operator JArray(JObjectArray value) => value.arr;
    
    public static implicit operator IntPtr(JObjectArray value) => value.arr != IntPtr.Zero ? value.arr : value.obj;
}