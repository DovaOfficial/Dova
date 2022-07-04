using System.Runtime.InteropServices;

namespace Dova.Internals.Types.Primitives;

[StructLayout(LayoutKind.Explicit)]
internal struct JLong
{
    [FieldOffset(0)]
    public long value;
}