using System.Runtime.InteropServices;

namespace Dova.Internals.Types.Objects;

[StructLayout(LayoutKind.Explicit)]
internal readonly struct JClass
{
    [FieldOffset(0)] 
    private readonly JObject obj;

    private JClass(JObject value)
    {
        this.obj = value;
    }

    public override string ToString() => obj.ToString();

    public static implicit operator JClass(JObject value) => new (value);
    
    public static implicit operator JClass(IntPtr value) => new (value);

    public static implicit operator JObject(JClass value) => value.obj;
    
    public static implicit operator IntPtr(JClass value) => value.obj;
}