using System.Runtime.InteropServices;

namespace Dova.Internals.Types.Primitives;

[StructLayout(LayoutKind.Explicit)]
internal readonly struct JByte
{
    [FieldOffset(0)]
    private readonly byte value;

    private JByte(byte b)
    {
        this.value = b;
    }

    public override string ToString() => value.ToString();

    public static implicit operator JByte(byte value) => new (value);

    public static implicit operator byte(JByte value) => value.value;
}