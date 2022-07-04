using Dova.Core.Jni;

namespace Dova.Core;

public class DovaConfiguration
{
    public string? JavaRuntimePath { get; set; }
    public IEnumerable<string>? JavaClassPaths { get; set; }
    public int Version { get; set; } = (int)JniVersion.JNI_VERSION_10;
    // TODO: Add option to add custom JavaVMOptions
}