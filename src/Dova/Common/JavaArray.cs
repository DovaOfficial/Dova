namespace Dova.Common;

/// <summary>
/// Represents an array in Java language.
/// </summary>
public class JavaArray<TElement> : JavaObject // TODO: Implement IList<TElement>, IEnumerator<TElement>, IDisposable (this should call Release method)
{
    // TODO: Add array-operations
    
    // TODO: In base() add creating new empty array
    // public JavaArray() : base()
    // {
    // }

    public JavaArray(IntPtr currentRefPtr) : base(currentRefPtr)
    {
    }

    public override string GetJavaClassSignature() => string.Empty;
    public override IntPtr GetJavaClassRaw() => default;
    public override IntPtr GetJavaClassRefRaw() => default;
}