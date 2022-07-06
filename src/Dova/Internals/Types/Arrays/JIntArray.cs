using System.Runtime.InteropServices;
using Dova.Internals.Types.Objects;

namespace Dova.Internals.Types.Arrays;

[StructLayout(LayoutKind.Explicit)]
internal struct JIntArray
{
    [FieldOffset(0)] 
    private JArray arr;

    [FieldOffset(0)] 
    private JObject obj;
    
    public static implicit operator JIntArray(JArray value) => new() { arr = value };
    
    public static implicit operator JIntArray(JObject value) => new() { obj = value };
    
    public static implicit operator JIntArray(IntPtr value) => new() { obj = value, arr = value };

    public static implicit operator JObject(JIntArray value) => value.obj;
    
    public static implicit operator JArray(JIntArray value) => value.arr;
    
    public static implicit operator IntPtr(JIntArray value) => value.arr != IntPtr.Zero ? value.arr : value.obj;
}