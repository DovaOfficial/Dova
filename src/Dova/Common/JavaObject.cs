namespace Dova.Common;

/// <summary>
/// Parent class for all Java generated C# classes.
/// This is also a parent class of java.lang.Object.
/// </summary>
public partial class JavaObject
{
    /// <summary>
    /// Reference to the current object.
    /// </summary>
    public IntPtr CurrentRefPtr { get; }

    /// <summary>
    /// This constructor should not be used beside the generated classes.
    /// </summary>
    /// <param name="currentRefPtr">Ptr to the object which is a field of a class.</param>
    public JavaObject(IntPtr currentRefPtr)
    {
        this.CurrentRefPtr = currentRefPtr;
    }
}