using System.Reflection;
using System.Runtime.InteropServices;
using Dova.Core.Jni;

namespace Dova.Core;

internal static class DovaLoader
{
    private static IDictionary<OSPlatform, string> JvmOsPlatformPaths = new Dictionary<OSPlatform, string>
    {
        { OSPlatform.Linux, @"lib/server/libjvm.so" },
        { OSPlatform.Windows, @"bin\server\jvm.dll" },
        { OSPlatform.OSX, @"lib/server/libjvm.dylib" }
    };
    
    private static IDictionary<string, DllImportResolver> Resolvers { get; } = new Dictionary<string, DllImportResolver>();
    
    private static string JvmFullPath { get; set; }

    static DovaLoader()
    {
        NativeLibrary.SetDllImportResolver(typeof(DovaLoader).Assembly, DllImportResolver);
    }

    public static void SetupJvm(DovaConfiguration? config)
    {
        var javaPath = config?.JavaRuntimePath
                       ?? Environment.GetEnvironmentVariable("JAVA_HOME")
                       ?? throw new InvalidOperationException("Unknown Java Runtime Path");

        var platformPath = JvmOsPlatformPaths
            .FirstOrDefault(x => RuntimeInformation.IsOSPlatform(x.Key))
            .Value;

        JvmFullPath = Path.Combine(javaPath, platformPath);

        if (!File.Exists(JvmFullPath))
        {
            throw new InvalidCastException($"Unknown Java Runtime Path: {JvmFullPath}");
        }

        if (!Resolvers.ContainsKey(JniFunctions.JavaJvmLibName))
        {
            Resolvers.Add(JniFunctions.JavaJvmLibName, (_, _, _) => NativeLibrary.Load(JvmFullPath));
        }
    }

    private static IntPtr DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath) => 
        Resolvers
            .FirstOrDefault(x => x.Key.Equals(libraryName))
            .Value(libraryName, assembly, searchPath);
}