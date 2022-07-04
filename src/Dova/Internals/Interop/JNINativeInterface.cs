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

    public delegate* unmanaged<JNIEnv*, JInt> GetVersion;
    public delegate* unmanaged<JNIEnv*, string, JObject, JByte*, JSize, JClass> DefineClass;
    public delegate* unmanaged<JNIEnv*, string, JClass> FindClass;
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID> FromReflectedMethod;
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID> FromReflectedField;
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JBoolean, JObject> ToReflectedMethod;
    public delegate* unmanaged<JNIEnv*, JClass, JClass> GetSuperclass;
    public delegate* unmanaged<JNIEnv*, JClass, JClass, JBoolean> IsAssignableFrom;
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JBoolean, JObject> ToReflectedField;
    public delegate* unmanaged<JNIEnv*, JThrowable, JInt> Throw;
    public delegate* unmanaged<JNIEnv*, JClass, string, JInt> ThrowNew;
    public delegate* unmanaged<JNIEnv*, JThrowable> ExceptionOccurred;
    public delegate* unmanaged<JNIEnv*, void> ExceptionDescribe;
    public delegate* unmanaged<JNIEnv*, void> ExceptionClear;
    public delegate* unmanaged<JNIEnv*, string, void> FatalError;
    public delegate* unmanaged<JNIEnv*, JInt, JInt> PushLocalFrame;
    public delegate* unmanaged<JNIEnv*, JObject, JObject> PopLocalFrame;
    public delegate* unmanaged<JNIEnv*, JObject, JObject> NewGlobalRef;
    public delegate* unmanaged<JNIEnv*, JObject, void> DeleteGlobalRef;
    public delegate* unmanaged<JNIEnv*, JObject, void> DeleteLocalRef;
    public delegate* unmanaged<JNIEnv*, JObject, JObject, JBoolean> IsSameObject;
    public delegate* unmanaged<JNIEnv*, JObject, JObject> NewLocalRef;
    public delegate* unmanaged<JNIEnv*, JInt, JInt> EnsureLocalCapacity;
    public delegate* unmanaged<JNIEnv*, JClass, JObject> AllocObject;

    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JObject> NewObject;
    public IntPtr NewObjectV;
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JValue[], JObject> NewObjectA;

    public delegate* unmanaged<JNIEnv*, JObject, JClass> GetObjectClass;
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JBoolean> IsInstanceOf;
    public delegate* unmanaged<JNIEnv*, JClass, string, string, JMethodID> GetMethodID;

    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JObject> CallObjectMethod;
    public IntPtr CallObjectMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JValue[], JObject> CallObjectMethodA;

    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JBoolean> CallBooleanMethod;
    public IntPtr CallBooleanMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JValue[], JBoolean> CallBooleanMethodA;

    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JByte> CallByteMethod;
    public IntPtr CallByteMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JValue[], JByte> CallByteMethodA;

    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JChar> CallCharMethod;
    public IntPtr CallCharMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JValue[], JChar> CallCharMethodA;

    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JShort> CallShortMethod;
    public IntPtr CallShortMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JValue[], JShort> CallShortMethodA;

    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JInt> CallIntMethod;
    public IntPtr CallIntMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JValue[], JInt> CallIntMethodA;

    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JLong> CallLongMethod;
    public IntPtr CallLongMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JValue[], JLong> CallLongMethodA;

    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JFloat> CallFloatMethod;
    public IntPtr CallFloatMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JValue[], JFloat> CallFloatMethodA;

    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JDouble> CallDoubleMethod;
    public IntPtr CallDoubleMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JValue[], JDouble> CallDoubleMethodA;

    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, void> CallVoidMethod;
    public IntPtr CallVoidMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JMethodID, JValue[], void> CallVoidMethodA;

    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JObject> CallNonvirtualObjectMethod;
    public IntPtr CallNonvirtualObjectMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JValue[], JObject> CallNonvirtualObjectMethodA;

    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JBoolean> CallNonvirtualBooleanMethod;
    public IntPtr CallNonvirtualBooleanMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JValue[], JBoolean> CallNonvirtualBooleanMethodA;

    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JByte> CallNonvirtualByteMethod;
    public IntPtr CallNonvirtualByteMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JValue[], JByte> CallNonvirtualByteMethodA;

    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JChar> CallNonvirtualCharMethod;
    public IntPtr CallNonvirtualCharMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JValue[], JChar> CallNonvirtualCharMethodA;

    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JShort> CallNonvirtualShortMethod;
    public IntPtr CallNonvirtualShortMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JValue[], JShort> CallNonvirtualShortMethodA;

    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JInt> CallNonvirtualIntMethod;
    public IntPtr CallNonvirtualIntMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JValue[], JInt> CallNonvirtualIntMethodA;

    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JLong> CallNonvirtualLongMethod;
    public IntPtr CallNonvirtualLongMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JValue[], JLong> CallNonvirtualLongMethodA;

    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JFloat> CallNonvirtualFloatMethod;
    public IntPtr CallNonvirtualFloatMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JValue[], JFloat> CallNonvirtualFloatMethodA;

    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JDouble> CallNonvirtualDoubleMethod;
    public IntPtr CallNonvirtualDoubleMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JValue[], JDouble> CallNonvirtualDoubleMethodA;

    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, void> CallNonvirtualVoidMethod;
    public IntPtr CallNonvirtualVoidMethodV;
    public delegate* unmanaged<JNIEnv*, JObject, JClass, JMethodID, JValue[], void> CallNonvirtualVoidMethodA;

    public delegate* unmanaged<JNIEnv*, JClass, string, string, JFieldID> GetFieldID;
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JObject> GetObjectField;
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JBoolean> GetBooleanField;
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JByte> GetByteField;
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JChar> GetCharField;
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JShort> GetShortField;
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JInt> GetIntField;
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JLong> GetLongField;
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JFloat> GetFloatField;
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JDouble> GetDoubleField;
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JObject, void> SetObjectField;
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JBoolean, void> SetBooleanField;
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JByte, void> SetByteField;
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JChar, void> SetCharField;
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JShort, void> SetShortField;
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JInt, void> SetIntField;
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JLong, void> SetLongField;
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JFloat, void> SetFloatField;
    public delegate* unmanaged<JNIEnv*, JObject, JFieldID, JDouble, void> SetDoubleField;
    public delegate* unmanaged<JNIEnv*, JClass, string, string, JMethodID> GetStaticMethodID;

    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JObject> CallStaticObjectMethod;
    public IntPtr CallStaticObjectMethodV;
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JValue[], JObject> CallStaticObjectMethodA;

    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JBoolean> CallStaticBooleanMethod;
    public IntPtr CallStaticBooleanMethodV;
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JValue[], JBoolean> CallStaticBooleanMethodA;

    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JByte> CallStaticByteMethod;
    public IntPtr CallStaticByteMethodV;
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JValue[], JByte> CallStaticByteMethodA;

    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JChar> CallStaticCharMethod;
    public IntPtr CallStaticCharMethodV;
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JValue[], JChar> CallStaticCharMethodA;

    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JShort> CallStaticShortMethod;
    public IntPtr CallStaticShortMethodV;
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JValue[], JShort> CallStaticShortMethodA;

    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JInt> CallStaticIntMethod;
    public IntPtr CallStaticIntMethodV;
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JValue[], JInt> CallStaticIntMethodA;

    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JLong> CallStaticLongMethod;
    public IntPtr CallStaticLongMethodV;
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JValue[], JLong> CallStaticLongMethodA;

    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JFloat> CallStaticFloatMethod;
    public IntPtr CallStaticFloatMethodV;
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JValue[], JFloat> CallStaticFloatMethodA;

    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JDouble> CallStaticDoubleMethod;
    public IntPtr CallStaticDoubleMethodV;
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JValue[], JDouble> CallStaticDoubleMethodA;

    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, void> CallStaticVoidMethod;
    public IntPtr CallStaticVoidMethodV;
    public delegate* unmanaged<JNIEnv*, JClass, JMethodID, JValue[], void> CallStaticVoidMethodA;

    public delegate* unmanaged<JNIEnv*, JClass, string, string, JFieldID> GetStaticFieldID;
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JObject> GetStaticObjectField;
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JBoolean> GetStaticBooleanField;
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JByte> GetStaticByteField;
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JChar> GetStaticCharField;
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JShort> GetStaticShortField;
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JInt> GetStaticIntField;
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JLong> GetStaticLongField;
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JFloat> GetStaticFloatField;
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JDouble> GetStaticDoubleField;
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JObject, void> SetStaticObjectField;
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JBoolean, void> SetStaticBooleanField;
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JByte, void> SetStaticByteField;
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JChar, void> SetStaticCharField;
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JShort, void> SetStaticShortField;
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JInt, void> SetStaticIntField;
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JLong, void> SetStaticLongField;
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JFloat, void> SetStaticFloatField;
    public delegate* unmanaged<JNIEnv*, JClass, JFieldID, JDouble, void> SetStaticDoubleField;
    public delegate* unmanaged<JNIEnv*, JChar*, JSize, JString> NewString;
    public delegate* unmanaged<JNIEnv*, JString, JSize> GetStringLength;
    public delegate* unmanaged<JNIEnv*, JString, JBoolean*, JChar*> GetStringChars;
    public delegate* unmanaged<JNIEnv*, JString, JChar*, void> ReleaseStringChars;
    public delegate* unmanaged<JNIEnv*, byte*, JString> NewStringUTF;
    public delegate* unmanaged<JNIEnv*, JString, JSize> GetStringUTFLength;
    public delegate* unmanaged<JNIEnv*, JString, JBoolean*, byte*> GetStringUTFChars;
    public delegate* unmanaged<JNIEnv*, JString, byte*, void> ReleaseStringUTFChars;
    public delegate* unmanaged<JNIEnv*, JArray, JSize> GetArrayLength;
    public delegate* unmanaged<JNIEnv*, JSize, JClass, JObject, JObjectArray> NewObjectArray;
    public delegate* unmanaged<JNIEnv*, JObjectArray, JSize, JObject> GetObjectArrayElement;
    public delegate* unmanaged<JNIEnv*, JObjectArray, JSize, JObject, void> SetObjectArrayElement;
    public delegate* unmanaged<JNIEnv*, JSize, JBooleanArray> NewBooleanArray;
    public delegate* unmanaged<JNIEnv*, JSize, JByteArray> NewByteArray;
    public delegate* unmanaged<JNIEnv*, JSize, JCharArray> NewCharArray;
    public delegate* unmanaged<JNIEnv*, JSize, JShortArray> NewShortArray;
    public delegate* unmanaged<JNIEnv*, JSize, JIntArray> NewIntArray;
    public delegate* unmanaged<JNIEnv*, JSize, JLongArray> NewLongArray;
    public delegate* unmanaged<JNIEnv*, JSize, JFloatArray> NewFloatArray;
    public delegate* unmanaged<JNIEnv*, JSize, JDoubleArray> NewDoubleArray;
    public delegate* unmanaged<JNIEnv*, JBooleanArray, JBoolean*, JBoolean*> GetBooleanArrayElements;
    public delegate* unmanaged<JNIEnv*, JByteArray, JBoolean*, JByte*> GetByteArrayElements;
    public delegate* unmanaged<JNIEnv*, JCharArray, JBoolean*, JChar*> GetCharArrayElements;
    public delegate* unmanaged<JNIEnv*, JShortArray, JBoolean*, JShort*> GetShortArrayElements;
    public delegate* unmanaged<JNIEnv*, JIntArray, JBoolean*, JInt*> GetIntArrayElements;
    public delegate* unmanaged<JNIEnv*, JLongArray, JBoolean*, JLong*> GetLongArrayElements;
    public delegate* unmanaged<JNIEnv*, JFloatArray, JBoolean*, JFloat*> GetFloatArrayElements;
    public delegate* unmanaged<JNIEnv*, JDoubleArray, JBoolean*, JDouble*> GetDoubleArrayElements;
    public delegate* unmanaged<JNIEnv*, JBooleanArray, JBoolean*, JInt, void> ReleaseBooleanArrayElements;
    public delegate* unmanaged<JNIEnv*, JByteArray, JByte*, JInt, void> ReleaseByteArrayElements;
    public delegate* unmanaged<JNIEnv*, JCharArray, JChar*, JInt, void> ReleaseCharArrayElements;
    public delegate* unmanaged<JNIEnv*, JShortArray, JShort*, JInt, void> ReleaseShortArrayElements;
    public delegate* unmanaged<JNIEnv*, JIntArray, JInt*, JInt, void> ReleaseIntArrayElements;
    public delegate* unmanaged<JNIEnv*, JLongArray, JLong*, JInt, void> ReleaseLongArrayElements;
    public delegate* unmanaged<JNIEnv*, JFloatArray, JFloat*, JInt, void> ReleaseFloatArrayElements;
    public delegate* unmanaged<JNIEnv*, JDoubleArray, JDouble*, JInt, void> ReleaseDoubleArrayElements;
    public delegate* unmanaged<JNIEnv*, JBooleanArray, JSize, JSize, JBoolean*, void> GetBooleanArrayRegion;
    public delegate* unmanaged<JNIEnv*, JByteArray, JSize, JSize, JByte*, void> GetByteArrayRegion;
    public delegate* unmanaged<JNIEnv*, JCharArray, JSize, JSize, JChar*, void> GetCharArrayRegion;
    public delegate* unmanaged<JNIEnv*, JShortArray, JSize, JSize, JShort*, void> GetShortArrayRegion;
    public delegate* unmanaged<JNIEnv*, JIntArray, JSize, JSize, JInt*, void> GetIntArrayRegion;
    public delegate* unmanaged<JNIEnv*, JLongArray, JSize, JSize, JLong*, void> GetLongArrayRegion;
    public delegate* unmanaged<JNIEnv*, JFloatArray, JSize, JSize, JFloat*, void> GetFloatArrayRegion;
    public delegate* unmanaged<JNIEnv*, JDoubleArray, JSize, JSize, JDouble*, void> GetDoubleArrayRegion;
    public delegate* unmanaged<JNIEnv*, JBooleanArray, JSize, JSize, JBoolean*, void> SetBooleanArrayRegion;
    public delegate* unmanaged<JNIEnv*, JByteArray, JSize, JSize, JByte*, void> SetByteArrayRegion;
    public delegate* unmanaged<JNIEnv*, JCharArray, JSize, JSize, JChar*, void> SetCharArrayRegion;
    public delegate* unmanaged<JNIEnv*, JShortArray, JSize, JSize, JShort*, void> SetShortArrayRegion;
    public delegate* unmanaged<JNIEnv*, JIntArray, JSize, JSize, JInt*, void> SetIntArrayRegion;
    public delegate* unmanaged<JNIEnv*, JLongArray, JSize, JSize, JLong*, void> SetLongArrayRegion;
    public delegate* unmanaged<JNIEnv*, JFloatArray, JSize, JSize, JFloat*, void> SetFloatArrayRegion;
    public delegate* unmanaged<JNIEnv*, JDoubleArray, JSize, JSize, JDouble*, void> SetDoubleArrayRegion;
    public delegate* unmanaged<JNIEnv*, JClass, void*, JInt, JInt> RegisterNatives;
    public delegate* unmanaged<JNIEnv*, JClass, JInt> UnregisterNatives;
    public delegate* unmanaged<JNIEnv*, JObject, JInt> MonitorEnter;
    public delegate* unmanaged<JNIEnv*, JObject, JInt> MonitorExit;
    public delegate* unmanaged<JNIEnv*, JavaVM**, JInt> GetJavaVM;
    public delegate* unmanaged<JNIEnv*, JString, JSize, JSize, JChar*, void> GetStringRegion;
    public delegate* unmanaged<JNIEnv*, JString, JSize, JSize, byte*, void> GetStringUTFRegion;
    public delegate* unmanaged<JNIEnv*, JArray, JBoolean*, void*> GetPrimitiveArrayCritical;
    public delegate* unmanaged<JNIEnv*, JArray, void*, JInt, void> ReleasePrimitiveArrayCritical;
    public delegate* unmanaged<JNIEnv*, JString, JBoolean*, JChar*> GetStringCritical;
    public delegate* unmanaged<JNIEnv*, JString, JChar*, void> ReleaseStringCritical;
    public delegate* unmanaged<JNIEnv*, JObject, JWeak> NewWeakGlobalRef;
    public delegate* unmanaged<JNIEnv*, JWeak, void> DeleteWeakGlobalRef;
    public delegate* unmanaged<JNIEnv*, JBoolean> ExceptionCheck;
    public delegate* unmanaged<JNIEnv*, void*, JLong, JObject> NewDirectByteBuffer;
    public delegate* unmanaged<JNIEnv*, JObject, void*> GetDirectBufferAddress;
    public delegate* unmanaged<JNIEnv*, JObject, JLong> GetDirectBufferCapacity;

    /* New JNI 1.6 Features */
    public delegate* unmanaged<JNIEnv*, JObject, JObjectRefType> GetObjectRefType;

    /* Module Features */
    public delegate* unmanaged<JNIEnv*, JClass, JObject> GetModule;
}