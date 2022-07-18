using Dova.Core.Runtime;

namespace Dova.Common;

/// <summary>
/// Parent class for all Java generated C# classes.
/// This is also a parent class of java.lang.Object.
/// </summary>
public partial class JavaObject
{
    // TODO: Store ptr to this object
    // TODO: Add additional required methods - GetField(name, sig), GetMethod(name, sig) (????)
    
    /// <summary>
    /// Currently used runtime.
    /// </summary>
    protected IJavaRuntime Runtime { get; } = DovaJvm.Vm.Runtime;
}