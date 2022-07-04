using System.Runtime.InteropServices;

namespace Dova.Internals.Types.Primitives;

[StructLayout(LayoutKind.Explicit)]
internal struct JShort
{
    [FieldOffset(0)]
    public short value;
}