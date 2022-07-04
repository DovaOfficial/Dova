using System.Runtime.InteropServices;

namespace Dova.Internals.Types.Objects;

[StructLayout(LayoutKind.Explicit)]
internal struct JSize
{
    [FieldOffset(0)]
    public int value;
}