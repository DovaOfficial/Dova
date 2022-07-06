using System.Runtime.InteropServices;
using Dova.Internals.Types.Objects;

namespace Dova.Internals.Types.Arrays;

[StructLayout(LayoutKind.Explicit)]
internal struct JBooleanArray
{
    [FieldOffset(0)] 
    private JArray arr;

    [FieldOffset(0)] 
    private JObject obj;
    
    public static implicit operator JBooleanArray(JArray value) => new() { arr = value };
    
    public static implicit operator JBooleanArray(JObject value) => new() { obj = value };
    
    public static implicit operator JBooleanArray(IntPtr value) => new() { obj = value, arr = value };

    public static implicit operator JObject(JBooleanArray value) => value.obj;
    
    public static implicit operator JArray(JBooleanArray value) => value.arr;
    
    public static implicit operator IntPtr(JBooleanArray value) => value.arr != IntPtr.Zero ? value.arr : value.obj;
}