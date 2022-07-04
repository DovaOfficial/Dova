using System.Runtime.InteropServices;

namespace Dova.Internals.Types.Primitives;

[StructLayout(LayoutKind.Explicit)]
internal struct JChar
{
    [FieldOffset(0)]
    public char value;
    
    [FieldOffset(0)]
    public ushort us;
}