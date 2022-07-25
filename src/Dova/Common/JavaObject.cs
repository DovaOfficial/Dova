namespace Dova.Common;

/// <summary>
/// Parent class for all Java generated C# classes.
/// This is also a parent class of java.lang.Object.
/// </summary>
public abstract class JavaObject // TODO: : IJavaObject
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
        CurrentRefPtr = currentRefPtr;
    }
    
    #region Methods

    public bool IsAssignableFrom(IntPtr classPtr) => DovaJvm.Vm.Runtime.IsAssignableFrom(GetJavaClassRaw(), classPtr);
    public bool IsSameObject(JavaObject javaObj) => DovaJvm.Vm.Runtime.IsSameObject(CurrentRefPtr, javaObj.CurrentRefPtr);
    public bool IsInstanceOf(IntPtr classPtr) => DovaJvm.Vm.Runtime.IsInstanceOf(CurrentRefPtr, classPtr);

    #endregion

    #region Abstract Methods

    /// <returns>Raw JNI signature. JniSignatureAttribute would return the same.</returns>
    public abstract string GetJavaClassSignature();
    
    /// <returns>Returns a pointer to the current class.</returns>
    public abstract IntPtr GetJavaClassRaw();
    
    /// <returns>Returns a pointer to the reference of the current class.</returns>
    public abstract IntPtr GetJavaClassRefRaw();

    #endregion
}