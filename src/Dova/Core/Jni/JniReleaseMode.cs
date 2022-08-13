namespace Dova.Core.Jni;

/// <summary>
/// When you call ReleaseTypeArrayElements, the last parameter is a mode flag. <br />
/// The mode flag is used to avoid unnecessary copying to the Java heap when working with a copied array. <br />
/// The mode flag is ignored if you are working with an array that has been pinned. <br />
/// <br />
/// You must call ReleaseType once for every GetType call, regardless of the value of the isCopy parameter. <br />
/// This step is necessary because calling ReleaseType deletes JNI local references that might otherwise prevent garbage collection. <br />
/// </summary>
public enum JniReleaseMode : int
{
    /// <summary>
    /// Update the data on the Java heap. Free the space used by the copy.
    /// </summary>
    JNI_DEFAULT = 0,
    
    /// <summary>
    /// Update the data on the Java heap. DO NOT free the space used by the copy.
    /// </summary>
    JNI_COMMIT = 1,
    
    /// <summary>
    /// DO NOT update the data on the Java heap. Free the space used by the copy.
    /// </summary>
    JNI_ABORT = 2,
}