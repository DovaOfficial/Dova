using System.Runtime.InteropServices;
using Dova.Internals.Types.Objects;

namespace Dova.Internals.Types.Arrays;

[StructLayout(LayoutKind.Explicit)]
internal struct JIntArray
{
    [FieldOffset(0)]
    public JArray arr;

    [FieldOffset(0)]
    public JObject obj;
}