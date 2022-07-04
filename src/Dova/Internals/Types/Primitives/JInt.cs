using System.Runtime.InteropServices;

namespace Dova.Internals.Types.Primitives;

[StructLayout(LayoutKind.Explicit)]
internal struct JInt
{
    [FieldOffset(0)]
    public int value;
}