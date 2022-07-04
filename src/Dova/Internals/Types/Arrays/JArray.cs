using System.Runtime.InteropServices;
using Dova.Internals.Types.Objects;

namespace Dova.Internals.Types.Arrays;

[StructLayout(LayoutKind.Explicit)]
internal readonly struct JArray
{
    [FieldOffset(0)] 
    private readonly JObject obj;

    private JArray(JObject value)
    {
        this.obj = value;
    }

    public override string ToString() => obj.ToString();

    public static implicit operator JArray(JObject value) => new (value);

    public static implicit operator JObject(JArray value) => value.obj;
}