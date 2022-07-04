using System.Runtime.InteropServices;

namespace Dova.Internals.Types.Objects;

[StructLayout(LayoutKind.Explicit)]
internal struct JThrowable
{
    [FieldOffset(0)]
    public JObject obj;
}