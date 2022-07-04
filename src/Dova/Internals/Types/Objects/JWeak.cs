using System.Runtime.InteropServices;

namespace Dova.Internals.Types.Objects;

[StructLayout(LayoutKind.Explicit)]
internal readonly struct JWeak
{
    [FieldOffset(0)] 
    private readonly JObject obj;

    private JWeak(JObject value)
    {
        this.obj = value;
    }

    public override string ToString() => obj.ToString();

    public static implicit operator JWeak(JObject value) => new (value);

    public static implicit operator JObject(JWeak value) => value.obj;
}