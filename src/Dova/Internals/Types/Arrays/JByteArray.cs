using System.Runtime.InteropServices;
using Dova.Internals.Types.Objects;

namespace Dova.Internals.Types.Arrays;

[StructLayout(LayoutKind.Explicit)]
internal struct JByteArray
{
    [FieldOffset(0)] 
    private JArray arr;

    [FieldOffset(0)] 
    private JObject obj;
    
    public static implicit operator JByteArray(JArray value) => new() { arr = value };
    
    public static implicit operator JByteArray(JObject value) => new() { obj = value };
    
    public static implicit operator JByteArray(IntPtr value) => new() { obj = value, arr = value };

    public static implicit operator JObject(JByteArray value) => value.obj;
    
    public static implicit operator JArray(JByteArray value) => value.arr;
    
    public static implicit operator IntPtr(JByteArray value) => value.arr != IntPtr.Zero ? value.arr : value.obj;
}