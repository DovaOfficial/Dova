using System.Runtime.InteropServices;

namespace Dova.Internals.Types.Primitives;

[StructLayout(LayoutKind.Explicit)]
internal readonly struct JFloat
{
    [FieldOffset(0)] 
    private readonly float value;

    private JFloat(float f)
    {
        this.value = f;
    }

    public override string ToString() => value.ToString();

    public static implicit operator JFloat(float value) => new (value);

    public static implicit operator float(JFloat value) => value.value;
}