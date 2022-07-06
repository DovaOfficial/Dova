using System.Runtime.InteropServices;
using Dova.Internals.Types.Primitives;

namespace Dova.Core.Runtime;

public static unsafe class JavaRuntimeExtensions
{
    public static IntPtr GetString(this IJavaRuntime runtime, string str)
    {
        var jChars = (JChar*)Marshal.StringToCoTaskMemUni(str);
        var jString = runtime.NewString((char*)jChars, str.Length);
        
        return jString;
    }

    public static string GetString(this IJavaRuntime runtime, IntPtr ptr)
    {
        var jChars = runtime.GetStringChars(ptr, null);
        var str = new string(jChars);
        
        return str;
    }
}