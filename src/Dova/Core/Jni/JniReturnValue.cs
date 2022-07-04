namespace Dova.Core.Jni;

public enum JniReturnValue : int
{
    /// <summary>
    /// Success
    /// </summary>
    JNI_OK = 0,
    
    /// <summary>
    /// Unknown error
    /// </summary>
    JNI_ERR = -1,
    
    /// <summary>
    /// Thread detached from the VM
    /// </summary>
    JNI_EDETACHED = -2,
    
    /// <summary>
    /// JNI version error
    /// </summary>
    JNI_EVERSION = -3,
    
    /// <summary>
    /// Not enough memory
    /// </summary>
    JNI_ENOMEM = -4,
    
    /// <summary>
    /// VM already created
    /// </summary>
    JNI_EEXIST = -5,
    
    /// <summary>
    /// Invalid arguments
    /// </summary>
    JNI_EINVAL = -6,
}