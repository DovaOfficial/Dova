using Dova.Core;

namespace Dova.Common;

public static class DovaJvm
{
    /// <summary>
    /// Static JVM used by all of the generated classes.
    /// It can be user-defined VM.
    /// </summary>
    public static DovaVM Vm { get; set; } = new();
}