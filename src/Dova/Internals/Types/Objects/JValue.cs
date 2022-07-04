using System.Runtime.InteropServices;
using Dova.Internals.Types.Primitives;

namespace Dova.Internals.Types.Objects;

[StructLayout(LayoutKind.Explicit)]
internal struct JValue
{
    [FieldOffset(0)]
    public JBoolean z;

    [FieldOffset(0)]
    public JByte b;

    [FieldOffset(0)]
    public JChar c;

    [FieldOffset(0)]
    public JShort s;

    [FieldOffset(0)]
    public JInt i;

    [FieldOffset(0)]
    public JLong j;

    [FieldOffset(0)]
    public JFloat f;

    [FieldOffset(0)]
    public JDouble d;

    [FieldOffset(0)]
    public JObject l;
}