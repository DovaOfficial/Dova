using System.Runtime.InteropServices;
using Dova.Internals.Interop;

namespace Dova.Core;

internal class DovaOptionsBuilder
{
    private readonly DovaConfiguration? _config;

    public DovaOptionsBuilder(DovaConfiguration? config)
    {
        _config = config;
    }

    public IEnumerable<JavaVMOption> BuildJavaVmOptions()
    {
        if (_config?.JavaClassPaths != null)
        {
            var path = string.Join(Path.PathSeparator, _config.JavaClassPaths);
            yield return new JavaVMOption
            {
                optionString = $"-Djava.class.path={path}",
                extraInfo = null
            };
        }
    }

    public JavaVMInitArgs BuildJavaVmInitArgs(IEnumerable<JavaVMOption> options)
    {
        var javaVmOptions = options.ToList();
        var javaVmOptionSize = Marshal.SizeOf<JavaVMOption>();

        var initArgs = new JavaVMInitArgs
        {
            options = Marshal.AllocCoTaskMem(javaVmOptionSize * javaVmOptions.Count),
            nOptions = javaVmOptions.Count,
            version = (int)_config?.Version,
            ignoreUnrecognized = true,
        };

        var pos = initArgs.options;

        foreach (var option in javaVmOptions)
        {
            Marshal.StructureToPtr(option, pos, false);
            pos += javaVmOptionSize;
        }

        return initArgs;
    }

    public IntPtr BuildJavaVmInitArgsPtr(JavaVMInitArgs javaVmInitArgs)
    {
        var javaVmInitArgsPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf<JavaVMInitArgs>());
        Marshal.StructureToPtr(javaVmInitArgs, javaVmInitArgsPtr, false);
        
        return javaVmInitArgsPtr;
    }
}