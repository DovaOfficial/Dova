using System.Runtime.InteropServices;

namespace Dova.Internals.Types.Objects;

[StructLayout(LayoutKind.Explicit)]
internal readonly struct JString
{
    [FieldOffset(0)] 
    private readonly JObject obj;

    private JString(JObject value)
    {
        this.obj = value;
    }

    public override string ToString() => obj.ToString();

    public static implicit operator JString(JObject value) => new (value);
    
    public static implicit operator JString(IntPtr value) => new (value);

    public static implicit operator JObject(JString value) => value.obj;
    
    public static implicit operator IntPtr(JString value) => value.obj;
}