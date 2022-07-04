using System.Runtime.InteropServices;

namespace Dova.Internals.Types.Primitives;

[StructLayout(LayoutKind.Explicit)]
internal struct JBoolean
{
    [FieldOffset(0)]
    [MarshalAs(UnmanagedType.I1)]
    public bool value;
}