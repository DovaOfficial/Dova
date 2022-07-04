using System.Runtime.InteropServices;

namespace Dova.Internals.Types.Primitives;

[StructLayout(LayoutKind.Explicit)]
internal readonly struct JInt
{
    [FieldOffset(0)] 
    private readonly int value;

    private JInt(int i)
    {
        this.value = i;
    }

    public override string ToString() => value.ToString();

    public static implicit operator JInt(int value) => new (value);

    public static implicit operator int(JInt value) => value.value;
}