namespace Dova.Internals.Types.Objects;

internal enum JObjectRefType : int
{
    JNIInvalidRefType = 0,
    JNILocalRefType = 1,
    JNIGlobalRefType = 2,
    JNIWeakGlobalRefType = 3,
}