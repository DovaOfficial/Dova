using System.Runtime.InteropServices;

namespace Dova.Internals.Types.Primitives;

[StructLayout(LayoutKind.Explicit)]
internal readonly struct JBoolean
{
    [FieldOffset(0)]
    [MarshalAs(UnmanagedType.I1)]
    private readonly bool value;

    private JBoolean(bool b)
    {
        this.value = b;
    }

    public override string ToString() => value.ToString();

    public static implicit operator JBoolean(bool value) => new (value);

    public static implicit operator bool(JBoolean value) => value.value;
}