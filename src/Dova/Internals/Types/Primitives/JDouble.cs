using System.Runtime.InteropServices;

namespace Dova.Internals.Types.Primitives;

[StructLayout(LayoutKind.Explicit)]
internal struct JDouble
{
    [FieldOffset(0)] 
    private readonly double value;

    private JDouble(double d)
    {
        this.value = d;
    }

    public override string ToString() => value.ToString();

    public static implicit operator JDouble(double value) => new (value);

    public static implicit operator double(JDouble value) => value.value;
}