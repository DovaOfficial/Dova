using System.Runtime.InteropServices;

namespace Dova.Internals.Types.Objects;

[StructLayout(LayoutKind.Explicit)]
internal readonly struct JSize
{
    [FieldOffset(0)] 
    private readonly int value;

    private JSize(int i)
    {
        this.value = i;
    }

    public override string ToString() => value.ToString();

    public static implicit operator JSize(int value) => new (value);

    public static implicit operator int(JSize value) => value.value;
}