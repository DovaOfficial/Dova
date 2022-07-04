using System.Runtime.InteropServices;

namespace Dova.Internals.Types.Primitives;

[StructLayout(LayoutKind.Explicit)]
internal readonly struct JLong
{
    [FieldOffset(0)] 
    private readonly long value;

    private JLong(long l)
    {
        this.value = l;
    }

    public override string ToString() => value.ToString();

    public static implicit operator JLong(long value) => new (value);

    public static implicit operator long(JLong value) => value.value;
}