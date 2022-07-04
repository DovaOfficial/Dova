using System.Runtime.InteropServices;

namespace Dova.Internals.Types.Objects;

[StructLayout(LayoutKind.Explicit)]
internal readonly struct JFieldId
{
    [FieldOffset(0)] 
    private readonly IntPtr ptr;

    private JFieldId(IntPtr value)
    {
        this.ptr = value;
    }

    public override string ToString() => ptr.ToString();

    public static implicit operator JFieldId(IntPtr value) => new (value);

    public static implicit operator IntPtr(JFieldId value) => value.ptr;
}