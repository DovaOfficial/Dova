using System.Runtime.InteropServices;

namespace Dova.Internals.Types.Objects;

[StructLayout(LayoutKind.Explicit)]
internal readonly struct JThrowable
{
    [FieldOffset(0)] 
    private readonly JObject obj;

    private JThrowable(JObject value)
    {
        this.obj = value;
    }

    public override string ToString() => obj.ToString();

    public static implicit operator JThrowable(JObject value) => new (value);
    
    public static implicit operator JThrowable(IntPtr value) => new (value);

    public static implicit operator JObject(JThrowable value) => value.obj;
    
    public static implicit operator IntPtr(JThrowable value) => value.obj;
}