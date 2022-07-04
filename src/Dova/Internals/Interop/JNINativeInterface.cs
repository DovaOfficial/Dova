using System.Runtime.InteropServices;
using Dova.Internals.Types.Arrays;
using Dova.Internals.Types.Objects;
using Dova.Internals.Types.Primitives;

namespace Dova.Internals.Interop;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct JNINativeInterface
{
    public IntPtr reserved0;
    public IntPtr reserved1;
    public IntPtr reserved2;
    public IntPtr reserved3;

    // GetVersion
    public delegate* unmanaged<JNIEnv*, JInt> GetVersion;

    // DefineClass
    public delegate* unmanaged<JNIEnv*, string, JObject, JByte*, JSize, JClass> DefineClass;

    // FindClass
    public delegate* unmanaged<JNIEnv*, string, JClass> FindClass;

    // FromReflectedMethod
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID> FromReflectedMethod;

    // FromReflectedField
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID> FromReflectedField;

    // ToReflectedMethod
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JBoolean, JObject> ToReflectedMethod;

    // GetSuperclass
    public delegate* unmanaged<JNIEnv*, JClass, JClass> GetSuperclass;

    // IsAssignableFrom
    public delegate* unmanaged<JNIEnv*, JClass, JClass, JBoolean> IsAssignableFrom;

    // ToReflectedField
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JBoolean, JObject> ToReflectedField;

    // Throw
    public delegate* unmanaged<JNIEnv*, JThrowable, JInt> Throw;

    // ThrowNew
    public delegate* unmanaged<JNIEnv*, JClass, string, JInt> ThrowNew;

    // ExceptionOccurred
    public delegate* unmanaged<JNIEnv*, JThrowable> ExceptionOccurred;

    // ExceptionDescribe
    public delegate* unmanaged<JNIEnv*, void> ExceptionDescribe;

    // ExceptionClear
    public delegate* unmanaged<JNIEnv*, void> ExceptionClear;

    // FatalError
    public delegate* unmanaged<JNIEnv*, string, void> FatalError;

    // PushLocalFrame
    public delegate* unmanaged<JNIEnv*, JInt, JInt> PushLocalFrame;

    // PopLocalFrame
    public delegate* unmanaged<JNIEnv*, JObject, JObject> PopLocalFrame;

    // NewGlobalRef
    public delegate* unmanaged<JNIEnv*, JObject, JObject> NewGlobalRef;

    // DeleteGlobalRef
    public delegate* unmanaged<JNIEnv*, JObject, void> DeleteGlobalRef;

    // DeleteLocalRef
    public delegate* unmanaged<JNIEnv*, JObject, void> DeleteLocalRef;

    // IsSameObject
    public delegate* unmanaged<JNIEnv*, JObject, JObject, JBoolean> IsSameObject;

    // NewLocalRef
    public delegate* unmanaged<JNIEnv*, JObject, JObject> NewLocalRef;

    // EnsureLocalCapacity
    public delegate* unmanaged<JNIEnv*, JInt, JInt> EnsureLocalCapacity;

    // AllocObject
    public delegate* unmanaged<JNIEnv*, JClass, JObject> AllocObject;

    // NewObject
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JObject> NewObject;
    public IntPtr NewObjectV;
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JValue[], JObject> NewObjectA;

    // GetObjectClass
    public delegate* unmanaged<JNIEnv*, JObject, JClass> GetObjectClass;

    // IsInstanceOf
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JBoolean> IsInstanceOf;

    // GetMethodID
    public delegate* unmanaged<JNIEnv*, JClass, string, string, JMethodID> GetMethodID;

    // CallObjectMethod
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JObject> CallObjectMethod;
    public IntPtr CallObjectMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JValue[], JObject> CallObjectMethodA;

    // CallBooleanMethod
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JBoolean> CallBooleanMethod;
    public IntPtr CallBooleanMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JValue[], JBoolean> CallBooleanMethodA;

    // CallByteMethod
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JByte> CallByteMethod;
    public IntPtr CallByteMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JValue[], JByte> CallByteMethodA;

    // CallCharMethod
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JChar> CallCharMethod;
    public IntPtr CallCharMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JValue[], JChar> CallCharMethodA;

    // CallShortMethod
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JShort> CallShortMethod;
    public IntPtr CallShortMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JValue[], JShort> CallShortMethodA;

    // CallIntMethod
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JInt> CallIntMethod;
    public IntPtr CallIntMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JValue[], JInt> CallIntMethodA;

    // CallLongMethod
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JLong> CallLongMethod;
    public IntPtr CallLongMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JValue[], JLong> CallLongMethodA;

    // CallFloatMethod
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JFloat> CallFloatMethod;
    public IntPtr CallFloatMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JValue[], JFloat> CallFloatMethodA;

    // CallDoubleMethod
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JDouble> CallDoubleMethod;
    public IntPtr CallDoubleMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JValue[], JDouble> CallDoubleMethodA;

    // CallVoidMethod
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, void> CallVoidMethod;
    public IntPtr CallVoidMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JValue[], void> CallVoidMethodA;

    // CallNonvirtualObjectMethod
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JObject> CallNonvirtualObjectMethod;
    public IntPtr CallNonvirtualObjectMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JValue[], JObject> CallNonvirtualObjectMethodA;

    // CallNonvirtualBooleanMethod
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JBoolean> CallNonvirtualBooleanMethod;
    public IntPtr CallNonvirtualBooleanMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JValue[], JBoolean> CallNonvirtualBooleanMethodA;

    // CallNonvirtualByteMethod
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JByte> CallNonvirtualByteMethod;
    public IntPtr CallNonvirtualByteMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JValue[], JByte> CallNonvirtualByteMethodA;

    // CallNonvirtualCharMethod
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JChar> CallNonvirtualCharMethod;
    public IntPtr CallNonvirtualCharMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JValue[], JChar> CallNonvirtualCharMethodA;

    // CallNonvirtualShortMethod
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JShort> CallNonvirtualShortMethod;
    public IntPtr CallNonvirtualShortMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JValue[], JShort> CallNonvirtualShortMethodA;

    // CallNonvirtualIntMethod
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JInt> CallNonvirtualIntMethod;
    public IntPtr CallNonvirtualIntMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JValue[], JInt> CallNonvirtualIntMethodA;

    // CallNonvirtualLongMethod
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JLong> CallNonvirtualLongMethod;
    public IntPtr CallNonvirtualLongMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JValue[], JLong> CallNonvirtualLongMethodA;

    // CallNonvirtualFloatMethod
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JFloat> CallNonvirtualFloatMethod;
    public IntPtr CallNonvirtualFloatMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JValue[], JFloat> CallNonvirtualFloatMethodA;

    // CallNonvirtualDoubleMethod
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JDouble> CallNonvirtualDoubleMethod;
    public IntPtr CallNonvirtualDoubleMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JValue[], JDouble> CallNonvirtualDoubleMethodA;

    // CallNonvirtualVoidMethod
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, void> CallNonvirtualVoidMethod;
    public IntPtr CallNonvirtualVoidMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JValue[], void> CallNonvirtualVoidMethodA;

    // GetFieldID
    public delegate* unmanaged<JNIEnv*, JClass, string, string, JFieldID> GetFieldID;

    // GetObjectField
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JObject> GetObjectField;

    // GetBooleanField
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JBoolean> GetBooleanField;

    // GetByteField
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JByte> GetByteField;

    // GetCharField
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JChar> GetCharField;

    // GetShortField
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JShort> GetShortField;

    // GetIntField
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JInt> GetIntField;

    // GetLongField
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JLong> GetLongField;

    // GetFloatField
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JFloat> GetFloatField;

    // GetDoubleField
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JDouble> GetDoubleField;

    // SetObjectField
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JObject, void> SetObjectField;

    // SetBooleanField
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JBoolean, void> SetBooleanField;

    // SetByteField
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JByte, void> SetByteField;

    // SetCharField
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JChar, void> SetCharField;

    // SetShortField
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JShort, void> SetShortField;

    // SetIntField
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JInt, void> SetIntField;

    // SetLongField
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JLong, void> SetLongField;

    // SetFloatField
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JFloat, void> SetFloatField;

    // SetDoubleField
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JDouble, void> SetDoubleField;

    // GetStaticMethodID
    public delegate* unmanaged<JNIEnv*, JClass, string, string, JMethodID> GetStaticMethodID;

    // CallStaticObjectMethod
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JObject> CallStaticObjectMethod;
    public IntPtr CallStaticObjectMethodV;
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JValue[], JObject> CallStaticObjectMethodA;

    // CallStaticBooleanMethod
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JBoolean> CallStaticBooleanMethod;
    public IntPtr CallStaticBooleanMethodV;
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JValue[], JBoolean> CallStaticBooleanMethodA;

    // CallStaticByteMethod
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JByte> CallStaticByteMethod;
    public IntPtr CallStaticByteMethodV;
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JValue[], JByte> CallStaticByteMethodA;

    // CallStaticCharMethod
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JChar> CallStaticCharMethod;
    public IntPtr CallStaticCharMethodV;
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JValue[], JChar> CallStaticCharMethodA;

    // CallStaticShortMethod
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JShort> CallStaticShortMethod;
    public IntPtr CallStaticShortMethodV;
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JValue[], JShort> CallStaticShortMethodA;

    // CallStaticIntMethod
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JInt> CallStaticIntMethod;
    public IntPtr CallStaticIntMethodV;
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JValue[], JInt> CallStaticIntMethodA;

    // CallStaticLongMethod
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JLong> CallStaticLongMethod;
    public IntPtr CallStaticLongMethodV;
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JValue[], JLong> CallStaticLongMethodA;

    // CallStaticFloatMethod
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JFloat> CallStaticFloatMethod;
    public IntPtr CallStaticFloatMethodV;
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JValue[], JFloat> CallStaticFloatMethodA;

    // CallStaticDoubleMethod
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JDouble> CallStaticDoubleMethod;
    public IntPtr CallStaticDoubleMethodV;
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JValue[], JDouble> CallStaticDoubleMethodA;

    // CallStaticVoidMethod
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, void> CallStaticVoidMethod;
    public IntPtr CallStaticVoidMethodV;
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JValue[], void> CallStaticVoidMethodA;

    // GetStaticFieldID
    public delegate* unmanaged<JNIEnv*, JClass, string, string, JFieldID> GetStaticFieldID;

    // GetStaticObjectField
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JObject> GetStaticObjectField;

    // GetStaticBooleanField
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JBoolean> GetStaticBooleanField;

    // GetStaticByteField
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JByte> GetStaticByteField;

    // GetStaticCharField
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JChar> GetStaticCharField;

    // GetStaticShortField
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JShort> GetStaticShortField;

    // GetStaticIntField
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JInt> GetStaticIntField;

    // GetStaticLongField
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JLong> GetStaticLongField;

    // GetStaticFloatField
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JFloat> GetStaticFloatField;

    // GetStaticDoubleField
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JDouble> GetStaticDoubleField;

    // SetStaticObjectField
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JObject, void> SetStaticObjectField;

    // SetStaticBooleanField
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JBoolean, void> SetStaticBooleanField;

    // SetStaticByteField
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JByte, void> SetStaticByteField;

    // SetStaticCharField
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JChar, void> SetStaticCharField;

    // SetStaticShortField
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JShort, void> SetStaticShortField;

    // SetStaticIntField
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JInt, void> SetStaticIntField;

    // SetStaticLongField
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JLong, void> SetStaticLongField;

    // SetStaticFloatField
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JFloat, void> SetStaticFloatField;

    // SetStaticDoubleField
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JDouble, void> SetStaticDoubleField;

    // NewString
    public delegate* unmanaged<JNIEnv*, JChar*, JSize, JString> NewString;

    // GetStringLength
    public delegate* unmanaged<JNIEnv*, JString, JSize> GetStringLength;

    // GetStringChars
    public delegate* unmanaged<JNIEnv*, JString, JBoolean*, JChar*> GetStringChars;

    // ReleaseStringChars
    public delegate* unmanaged<JNIEnv*, JString, JChar*, void> ReleaseStringChars;

    // NewStringUTF
    public delegate* unmanaged<JNIEnv*, byte*, JString> NewStringUTF;

    // GetStringUTFLength
    public delegate* unmanaged<JNIEnv*, JString, JSize> GetStringUTFLength;

    // GetStringUTFChars
    public delegate* unmanaged<JNIEnv*, JString, JBoolean*, byte*> GetStringUTFChars;

    // ReleaseStringUTFChars
    public delegate* unmanaged<JNIEnv*, JString, byte*, void> ReleaseStringUTFChars;

    // GetArrayLength
    public delegate* unmanaged<JNIEnv*, JArray, JSize> GetArrayLength;

    // NewObjectArray
    public delegate* unmanaged<JNIEnv*, JSize, JClass, JObject, JObjectArray> NewObjectArray;

    // GetObjectArrayElement
    public delegate* unmanaged<JNIEnv*, JObjectArray, JSize, JObject> GetObjectArrayElement;

    // SetObjectArrayElement
    public delegate* unmanaged<JNIEnv*, JObjectArray, JSize, JObject, void> SetObjectArrayElement;

    // NewBooleanArray
    public delegate* unmanaged<JNIEnv*, JSize, JBooleanArray> NewBooleanArray;

    // NewByteArray
    public delegate* unmanaged<JNIEnv*, JSize, JByteArray> NewByteArray;

    // NewCharArray
    public delegate* unmanaged<JNIEnv*, JSize, JCharArray> NewCharArray;

    // NewShortArray
    public delegate* unmanaged<JNIEnv*, JSize, JShortArray> NewShortArray;

    // NewIntArray
    public delegate* unmanaged<JNIEnv*, JSize, JIntArray> NewIntArray;

    // NewLongArray
    public delegate* unmanaged<JNIEnv*, JSize, JLongArray> NewLongArray;

    // NewFloatArray
    public delegate* unmanaged<JNIEnv*, JSize, JFloatArray> NewFloatArray;

    // NewDoubleArray
    public delegate* unmanaged<JNIEnv*, JSize, JDoubleArray> NewDoubleArray;

    // GetBooleanArrayElements
    public delegate* unmanaged<JNIEnv*, JBooleanArray, JBoolean*, JBoolean*> GetBooleanArrayElements;

    // GetByteArrayElements
    public delegate* unmanaged<JNIEnv*, JByteArray, JBoolean*, JByte*> GetByteArrayElements;

    // GetCharArrayElements
    public delegate* unmanaged<JNIEnv*, JCharArray, JBoolean*, JChar*> GetCharArrayElements;

    // GetShortArrayElements
    public delegate* unmanaged<JNIEnv*, JShortArray, JBoolean*, JShort*> GetShortArrayElements;

    // GetIntArrayElements
    public delegate* unmanaged<JNIEnv*, JIntArray, JBoolean*, JInt*> GetIntArrayElements;

    // GetLongArrayElements
    public delegate* unmanaged<JNIEnv*, JLongArray, JBoolean*, JLong*> GetLongArrayElements;

    // GetFloatArrayElements
    public delegate* unmanaged<JNIEnv*, JFloatArray, JBoolean*, JFloat*> GetFloatArrayElements;

    // GetDoubleArrayElements
    public delegate* unmanaged<JNIEnv*, JDoubleArray, JBoolean*, JDouble*> GetDoubleArrayElements;

    // ReleaseBooleanArrayElements
    public delegate* unmanaged<JNIEnv*, JBooleanArray, JBoolean*, JInt, void> ReleaseBooleanArrayElements;

    // ReleaseByteArrayElements
    public delegate* unmanaged<JNIEnv*, JByteArray, JByte*, JInt, void> ReleaseByteArrayElements;

    // ReleaseCharArrayElements
    public delegate* unmanaged<JNIEnv*, JCharArray, JChar*, JInt, void> ReleaseCharArrayElements;

    // ReleaseShortArrayElements
    public delegate* unmanaged<JNIEnv*, JShortArray, JShort*, JInt, void> ReleaseShortArrayElements;

    // ReleaseIntArrayElements
    public delegate* unmanaged<JNIEnv*, JIntArray, JInt*, JInt, void> ReleaseIntArrayElements;

    // ReleaseLongArrayElements
    public delegate* unmanaged<JNIEnv*, JLongArray, JLong*, JInt, void> ReleaseLongArrayElements;

    // ReleaseFloatArrayElements
    public delegate* unmanaged<JNIEnv*, JFloatArray, JFloat*, JInt, void> ReleaseFloatArrayElements;

    // ReleaseDoubleArrayElements
    public delegate* unmanaged<JNIEnv*, JDoubleArray, JDouble*, JInt, void> ReleaseDoubleArrayElements;

    // GetBooleanArrayRegion
    public delegate* unmanaged<JNIEnv*, JBooleanArray, JSize, JSize, JBoolean*, void> GetBooleanArrayRegion;

    // GetByteArrayRegion
    public delegate* unmanaged<JNIEnv*, JByteArray, JSize, JSize, JByte*, void> GetByteArrayRegion;

    // GetCharArrayRegion
    public delegate* unmanaged<JNIEnv*, JCharArray, JSize, JSize, JChar*, void> GetCharArrayRegion;

    // GetShortArrayRegion
    public delegate* unmanaged<JNIEnv*, JShortArray, JSize, JSize, JShort*, void> GetShortArrayRegion;

    // GetIntArrayRegion
    public delegate* unmanaged<JNIEnv*, JIntArray, JSize, JSize, JInt*, void> GetIntArrayRegion;

    // GetLongArrayRegion
    public delegate* unmanaged<JNIEnv*, JLongArray, JSize, JSize, JLong*, void> GetLongArrayRegion;

    // GetFloatArrayRegion
    public delegate* unmanaged<JNIEnv*, JFloatArray, JSize, JSize, JFloat*, void> GetFloatArrayRegion;

    // GetDoubleArrayRegion
    public delegate* unmanaged<JNIEnv*, JDoubleArray, JSize, JSize, JDouble*, void> GetDoubleArrayRegion;

    // SetBooleanArrayRegion
    public delegate* unmanaged<JNIEnv*, JBooleanArray, JSize, JSize, JBoolean*, void> SetBooleanArrayRegion;

    // SetByteArrayRegion
    public delegate* unmanaged<JNIEnv*, JByteArray, JSize, JSize, JByte*, void> SetByteArrayRegion;

    // SetCharArrayRegion
    public delegate* unmanaged<JNIEnv*, JCharArray, JSize, JSize, JChar*, void> SetCharArrayRegion;

    // SetShortArrayRegion
    public delegate* unmanaged<JNIEnv*, JShortArray, JSize, JSize, JShort*, void> SetShortArrayRegion;

    // SetIntArrayRegion
    public delegate* unmanaged<JNIEnv*, JIntArray, JSize, JSize, JInt*, void> SetIntArrayRegion;

    // SetLongArrayRegion
    public delegate* unmanaged<JNIEnv*, JLongArray, JSize, JSize, JLong*, void> SetLongArrayRegion;

    // SetFloatArrayRegion
    public delegate* unmanaged<JNIEnv*, JFloatArray, JSize, JSize, JFloat*, void> SetFloatArrayRegion;

    // SetDoubleArrayRegion
    public delegate* unmanaged<JNIEnv*, JDoubleArray, JSize, JSize, JDouble*, void> SetDoubleArrayRegion;

    // RegisterNatives
    public delegate* unmanaged<JNIEnv*, JClass, void*, JInt, JInt> RegisterNatives;

    // UnregisterNatives
    public delegate* unmanaged<JNIEnv*, JClass, JInt> UnregisterNatives;

    // MonitorEnter
    public delegate* unmanaged<JNIEnv*, JObject, JInt> MonitorEnter;

    // MonitorExit
    public delegate* unmanaged<JNIEnv*, JObject, JInt> MonitorExit;

    // GetJavaVM
    public delegate* unmanaged<JNIEnv*, JavaVM**, JInt> GetJavaVM;

    // GetStringRegion
    public delegate* unmanaged<JNIEnv*, JString, JSize, JSize, JChar*, void> GetStringRegion;

    // GetStringUTFRegion
    public delegate* unmanaged<JNIEnv*, JString, JSize, JSize, byte*, void> GetStringUTFRegion;

    // GetPrimitiveArrayCritical
    public delegate* unmanaged<JNIEnv*, JArray, JBoolean*, void*> GetPrimitiveArrayCritical;

    // ReleasePrimitiveArrayCritical
    public delegate* unmanaged<JNIEnv*, JArray, void*, JInt, void> ReleasePrimitiveArrayCritical;

    // GetStringCritical
    public delegate* unmanaged<JNIEnv*, JString, JBoolean*, JChar*> GetStringCritical;

    // ReleaseStringCritical
    public delegate* unmanaged<JNIEnv*, JString, JChar*, void> ReleaseStringCritical;

    // NewWeakGlobalRef
    public delegate* unmanaged<JNIEnv*, JObject, JWeak> NewWeakGlobalRef;

    // DeleteWeakGlobalRef
    public delegate* unmanaged<JNIEnv*, JWeak, void> DeleteWeakGlobalRef;

    // ExceptionCheck
    public delegate* unmanaged<JNIEnv*, JBoolean> ExceptionCheck;

    // NewDirectByteBuffer
    public delegate* unmanaged<JNIEnv*, void*, JLong, JObject> NewDirectByteBuffer;

    // GetDirectBufferAddress
    public delegate* unmanaged<JNIEnv*, JObject, void*> GetDirectBufferAddress;

    // GetDirectBufferCapacity
    public delegate* unmanaged<JNIEnv*, JObject, JLong> GetDirectBufferCapacity;

    /* New JNI 1.6 Features */

    // GetObjectRefType
    public delegate* unmanaged<JNIEnv*, JObject, JObjectRefType> GetObjectRefType;

    /* Module Features */

    // GetModule
    public delegate* unmanaged<JNIEnv*, JClass, JObject> GetModule;
}