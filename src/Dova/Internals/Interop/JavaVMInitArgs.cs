using System.Runtime.InteropServices;
using Dova.Internals.Types.Primitives;

namespace Dova.Internals.Interop;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct JavaVMInitArgs
{
    public JInt version;
    public JInt nOptions;
    public IntPtr options;
    public JBoolean ignoreUnrecognized;
}