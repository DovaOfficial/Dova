namespace Dova.Common;

/// <summary>
/// Root interface for all generated JDK interfaces.
/// </summary>
public interface IJavaObject
{
    /// <summary>
    /// Reference to the current object.
    /// </summary>
    IntPtr CurrentRefPtr { get; }
    
    /// <returns>True if the object can be assigned from the given class; otherwise false.</returns>
    bool IsAssignableFrom(IntPtr classPtr);
    
    /// <returns>True if the objects (pointers) are the same; otherwise false.</returns>
    bool IsSameObject(JavaObject javaObj);
    
    /// <returns>True if the object is an instance of a given class; otherwise false.</returns>
    bool IsInstanceOf(IntPtr classPtr);

    /// <returns>String read from CurrentRefPtr.</returns>
    string ToCSharpString();
    
    /// <returns>Raw JNI signature. JniSignatureAttribute would return the same.</returns>
    string GetJavaClassSignature();
    
    /// <returns>Returns a pointer to the current class.</returns>
    IntPtr GetJavaClassRaw();
    
    /// <returns>Returns a pointer to the reference of the current class.</returns>
    IntPtr GetJavaClassRefRaw();
}