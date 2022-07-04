using System.Runtime.InteropServices;
using Dova.Internals.Types.Objects;
using Dova.Internals.Types.Primitives;

namespace Dova.Internals.Interop;

[StructLayout(LayoutKind.Sequential)]
internal struct JavaVMAttachArgs
{
    public JInt version;
    public string name;
    public JObject group;
}