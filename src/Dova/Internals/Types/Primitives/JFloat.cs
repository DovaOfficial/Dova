using System.Runtime.InteropServices;

namespace Dova.Internals.Types.Primitives;

[StructLayout(LayoutKind.Explicit)]
internal struct JFloat
{
    [FieldOffset(0)]
    public float value;
}