using System.Runtime.InteropServices;

namespace Dova.Internals.Types.Primitives;

[StructLayout(LayoutKind.Explicit)]
internal struct JByte
{
    [FieldOffset(0)]
    public byte value;
}