using System.Runtime.InteropServices;

namespace Dova.Internals.Types.Objects;

[StructLayout(LayoutKind.Explicit)]
internal struct JMethodID
{
    [FieldOffset(0)]
    public IntPtr ptr;
}