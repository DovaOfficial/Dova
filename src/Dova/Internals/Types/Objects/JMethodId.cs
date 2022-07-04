using System.Runtime.InteropServices;

namespace Dova.Internals.Types.Objects;

[StructLayout(LayoutKind.Explicit)]
internal readonly struct JMethodId
{
    [FieldOffset(0)] 
    private readonly IntPtr ptr;

    private JMethodId(IntPtr value)
    {
        this.ptr = value;
    }

    public override string ToString() => ptr.ToString();

    public static implicit operator JMethodId(IntPtr value) => new (value);

    public static implicit operator IntPtr(JMethodId value) => value.ptr;
}