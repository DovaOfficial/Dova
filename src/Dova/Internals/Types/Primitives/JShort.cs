using System.Runtime.InteropServices;

namespace Dova.Internals.Types.Primitives;

[StructLayout(LayoutKind.Explicit)]
internal readonly struct JShort
{
    [FieldOffset(0)] 
    private readonly short value;

    private JShort(short s)
    {
        this.value = s;
    }

    public override string ToString() => value.ToString();

    public static implicit operator JShort(short value) => new (value);

    public static implicit operator short(JShort value) => value.value;
}