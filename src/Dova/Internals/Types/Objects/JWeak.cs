using System.Runtime.InteropServices;

namespace Dova.Internals.Types.Objects;

[StructLayout(LayoutKind.Explicit)]
internal struct JWeak
{
    [FieldOffset(0)]
    public JObject obj;
}