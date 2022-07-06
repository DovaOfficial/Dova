namespace Dova.Core.Runtime;

/// <summary>
/// For more information please check:
/// https://docs.oracle.com/javase/8/docs/technotes/guides/jni/spec/functions.html
/// </summary>
/// <author>
/// https://github.com/Sejoslaw/Dova
/// </author>
public unsafe interface IJavaRuntime
{
    /// <linkage>Index 4 in the JNIEnv interface function table.</linkage>
    /// <returns>Returns the major version number in the higher 16 bits and the minor version number in the lower 16 bits.</returns>
    /// <returns>In JDK/JRE 1.1, <code class= "cCode">GetVersion()</code> returns <code>0x00010001</code>.</returns>
    /// <returns>In JDK/JRE 1.2, <code>GetVersion()</code> returns <code>0x00010002</code>.</returns>
    /// <returns>In JDK/JRE 1.4, <code>GetVersion()</code> returns <code>0x00010004</code>.</returns>
    /// <returns>In JDK/JRE 1.6, <code>GetVersion()</code> returns <code>0x00010006</code>.</returns>
    int GetVersion();

    /// <linkage>Index 5 in the JNIEnv interface function table.</linkage>
    /// <param name="name">The name of the class or interface to be defined. The string is encoded in modified UTF-8.</param>
    /// <param name="loader">A class loader assigned to the defined class.</param>
    /// <param name="buf">Buffer containing the </param>
    /// <param name="bufLen">Buffer length.</param>
    /// <returns>Returns a Java class object or <code class= "cCode">NULL</code> if an error occurs.</returns>
    /// <throws><code>ClassFormatError</code>: if the class data does not specify a valid class.</throws>
    /// <throws><code>ClassCircularityError</code>: if a class or interface would be its own superclass or superinterface.</throws>
    /// <throws><code>OutOfMemoryError</code>: if the system runs out of memory.</throws>
    /// <throws><code>SecurityException</code>: if the caller attempts to define a class in the "java" package tree.</throws>
    IntPtr DefineClass(string name, IntPtr loader, byte* buf, int bufLen);

    /// <linkage>Index 6 in the JNIEnv interface function table.</linkage>
    /// <param name="name">A fully-qualified class name (that is, a package name, delimited by &#8220;</param>
    /// <returns>Returns a class object from a fully-qualified name, or <code>NULL</code> if the class cannot be found.</returns>
    /// <throws><code>ClassFormatError</code>: if the class data does not specify a valid class.</throws>
    /// <throws><code>ClassCircularityError</code>: if a class or interface would be its own superclass or superinterface.</throws>
    /// <throws><code>NoClassDefFoundError</code>: if no definition for a requested class or interface can be found.</throws>
    /// <throws><code>OutOfMemoryError</code>: if the system runs out of memory.</throws>
    IntPtr FindClass(string name);

    /// <linkage>Index 7 in the JNIEnv interface function table.</linkage>
    /// <since>JDK/JRE 1.2</since>
    IntPtr FromReflectedMethod(IntPtr obj);

    /// <linkage>Index 8 in the JNIEnv interface function table.</linkage>
    /// <since>JDK/JRE 1.2</since>
    IntPtr FromReflectedField(IntPtr obj);

    /// <linkage>Index 9 in the JNIEnv interface function table.</linkage>
    /// <since>JDK/JRE 1.2</since>
    IntPtr ToReflectedMethod(IntPtr jClass, IntPtr jMethodId, bool isStatic);

    /// <linkage>Index 10 in the JNIEnv interface function table.</linkage>
    /// <param name="clazz">A Java class object.</param>
    /// <returns>Returns the superclass of the class represented by <code>clazz</code>, or <code class= "cCode">NULL</code>.</returns>
    IntPtr GetSuperclass(IntPtr clazz);

    /// <linkage>Index 11 in the JNIEnv interface function table.</linkage>
    /// <param name="clazz1">The first class argument.</param>
    /// <param name="clazz2">The second class argument.</param>
    /// <returns>Returns <code>JNI_TRUE</code> if either of the following is true:</returns>
    bool IsAssignableFrom(IntPtr clazz1, IntPtr clazz2);

    /// <linkage>Index 12 in the JNIEnv interface function table.</linkage>
    /// <since>JDK/JRE 1.2</since>
    IntPtr ToReflectedField(IntPtr jclass, IntPtr jfieldid, bool jboolean);

    /// <linkage>Index 13 in the JNIEnv interface function table.</linkage>
    /// <param name="obj">A <code class= "cCode">java.lang.Throwable</param>
    /// <returns>Returns 0 on success; a negative value on failure.</returns>
    /// <throws>the <code>java.lang.Throwable</code> <code>object</code> <code class= "cCode">obj</code><code>.</code></throws>
    int Throw(IntPtr obj);

    /// <linkage>Index 14 in the JNIEnv interface function table.</linkage>
    /// <param name="clazz">A subclass of </param>
    /// <param name="message">The message used to construct the <code class= "cCode">java.lang.Throwable</param>
    /// <returns>Returns 0 on success; a negative value on failure.</returns>
    /// <throws>the newly constructed <code class= "cCode">java.lang.Throwable</code> object.</throws>
    int ThrowNew(IntPtr clazz, string message);

    /// <linkage>Index 15 in the JNIEnv interface function table.</linkage>
    /// <returns>Returns the exception object that is currently in the process of being thrown, or <code>NULL</code> if no exception is currently being thrown.</returns>
    IntPtr ExceptionOccurred();

    /// <linkage>Index 16 in the JNIEnv interface function table.</linkage>
    void ExceptionDescribe();

    /// <linkage>Index 17 in the JNIEnv interface function table.</linkage>
    void ExceptionClear();

    /// <linkage>Index 18 in the JNIEnv interface function table.</linkage>
    /// <param name="msg">An error message. The string is encoded in modified UTF-8.</param>
    void FatalError(string msg);

    /// <linkage> Index 19 in the JNIEnv interface function table. </linkage>
    /// <since>JDK/JRE 1.2</since>
    int PushLocalFrame(int capacity);

    /// <linkage> Index 20 in the JNIEnv interface function table. </linkage>
    /// <since>JDK/JRE 1.2</since>
    IntPtr PopLocalFrame(IntPtr jobject);

    /// <linkage>Index 21 in the JNIEnv interface function table.</linkage>
    /// <param name="obj">A global or local reference.</param>
    /// <returns>Returns a global reference, or <code class= "cCode">NULL</code> if the system runs out of memory.</returns>
    IntPtr NewGlobalRef(IntPtr obj);

    /// <linkage>Index 22 in the JNIEnv interface function table.</linkage>
    /// <param name="globalRef">A global reference.</param>
    void DeleteGlobalRef(IntPtr globalRef);

    /// <linkage>Index 23 in the JNIEnv interface function table.</linkage>
    /// <param name="localRef">A local reference.</param>
    void DeleteLocalRef(IntPtr localRef);

    /// <linkage>Index 24 in the JNIEnv interface function table.</linkage>
    /// <param name="ref1">A Java object.</param>
    /// <param name="ref2">A Java object.</param>
    /// <returns>Returns <code>JNI_TRUE</code> if <code>ref1</code> and <code>ref2</code> refer to the same Java object, or are both <code class= "cCode">NULL</code>; otherwise, returns <code class= "cCode">JNI_FALSE</code>.</returns>
    bool IsSameObject(IntPtr ref1, IntPtr ref2);

    /// <linkage> Index 25 in the JNIEnv interface function table. </linkage>
    /// <since>JDK/JRE 1.2</since>
    /// <since>Weak global references in JNI are a simplified version of the Java Weak References, available as part of the Java 2 Platform API ( <code>java.lang.ref</code> package and its classes). <a name= "weakrefs__clarification"></a></since>
    /// <since><strong>Clarification</strong> &nbsp;&nbsp;&nbsp;<i>(added June 2001)</i></since>
    /// <since><i>Since garbage collection may occur while native methods are running, objects referred to by weak global references can be freed at any time. While weak global references can be used where global references are used, it is generally inappropriate to do so, as they may become functionally equivalent to</i> <code>NULL</code> <i>without notice.</i></since>
    /// <since><i>While</i> <code>IsSameObject</code> <i>can be used to determine whether a weak global reference refers to a freed object, it does not prevent the object from being freed immediately thereafter. Consequently, programmers may not rely on this check to determine whether a weak global reference may used (as a non-</i><code>NULL</code> <i>reference) in any future JNI function call.</i></since>
    /// <since><i>To overcome this inherent limitation, it is recommended that a standard (strong) local or global reference to the same object be acquired using the JNI functions</i> <code>NewLocalRef</code> <i>or</i> <code>NewGlobalRef</code><i>, and that this strong reference be used to access the intended object. These functions will return</i> <code>NULL</code> <i>if the object has been freed, and otherwise will return a strong reference (which will prevent the object from being freed). The new reference should be explicitly deleted when immediate access to the object is no longer required, allowing the object to be freed.</i></since>
    /// <since><i>The weak global reference is weaker than other types of weak references (Java objects of the SoftReference or WeakReference classes). A weak global reference to a specific object will not become functionally equivalent to</i> <code>NULL</code> <i>until after SoftReference or WeakReference objects referring to that same specific object have had their references cleared.</i></since>
    /// <since><i>The weak global reference is weaker than Java's internal references to objects requiring finalization. A weak global reference will not become functionally equivalent to</i> <code>NULL</code> <i>until after the completion of the finalizer for the referenced object, if present.</i></since>
    /// <since><i>Interactions between weak global references and PhantomReferences are undefined. In particular, implementations of a Java VM may (or may not) process weak global references after PhantomReferences, and it may (or may not) be possible to use weak global references to hold on to objects which are also referred to by PhantomReference objects. This undefined use of weak global references should be avoided.</i></since>
    IntPtr NewLocalRef(IntPtr jobject);

    /// <linkage> Index 26 in the JNIEnv interface function table. </linkage>
    /// <since>JDK/JRE 1.2</since>
    int EnsureLocalCapacity(int capacity);

    /// <linkage>Index 27 in the JNIEnv interface function table.</linkage>
    /// <param name="clazz">A Java class object.</param>
    /// <returns>Returns a Java object, or <code class= "cCode">NULL</code> if the object cannot be constructed.</returns>
    /// <throws><code>InstantiationException</code>: if the class is an interface or an abstract class.</throws>
    /// <throws><code>OutOfMemoryError</code>: if the system runs out of memory.</throws>
    IntPtr AllocObject(IntPtr clazz);

    /// <newobject</h4>  <p>programmers place all arguments that are to be passed to the constructor immediately following the <code class= "ccode">methodid</code> argument. <code class= "ccode">newobject()</code> accepts these arguments and passes them to the java method that the programmer wishes to invoke.</p>  >Programmers place all arguments that are to be passed to the constructor immediately following the <code class= "cCode">methodID</code> argument. <code class= "cCode">NewObject()</code> accepts these arguments and passes them to the Java method that the programmer wishes to invoke.</newobject</h4>  <p>programmers place all arguments that are to be passed to the constructor immediately following the <code class= "ccode">methodid</code> argument. <code class= "ccode">newobject()</code> accepts these arguments and passes them to the java method that the programmer wishes to invoke.</p>  >
    /// <linkage>Index 28 in the JNIEnv interface function table.</linkage>
    /// <newobjecta</h4>  <p>programmers place all arguments that are to be passed to the constructor in an <code>args</code> array of <code>jvalues</code> that immediately follows the <code>methodid</code> argument. <code>newobjecta()</code> accepts the arguments in this array, and, in turn, passes them to the java method that the programmer wishes to invoke.</p>  >Programmers place all arguments that are to be passed to the constructor in an <code>args</code> array of <code>jvalues</code> that immediately follows the <code>methodID</code> argument. <code>NewObjectA()</code> accepts the arguments in this array, and, in turn, passes them to the Java method that the programmer wishes to invoke.</newobjecta</h4>  <p>programmers place all arguments that are to be passed to the constructor in an <code>args</code> array of <code>jvalues</code> that immediately follows the <code>methodid</code> argument. <code>newobjecta()</code> accepts the arguments in this array, and, in turn, passes them to the java method that the programmer wishes to invoke.</p>  >
    /// <linkage>Index 30 in the JNIEnv interface function table.</linkage>
    /// <newobjectv</h4>  <p>programmers place all arguments that are to be passed to the constructor in an <code>args</code> argument of type <code>va_list</code> that immediately follows the <code>methodid</code> argument. <code>newobjectv()</code> accepts these arguments, and, in turn, passes them to the java method that the programmer wishes to invoke.</p>  >Programmers place all arguments that are to be passed to the constructor in an <code>args</code> argument of type <code>va_list</code> that immediately follows the <code>methodID</code> argument. <code>NewObjectV()</code> accepts these arguments, and, in turn, passes them to the Java method that the programmer wishes to invoke.</newobjectv</h4>  <p>programmers place all arguments that are to be passed to the constructor in an <code>args</code> argument of type <code>va_list</code> that immediately follows the <code>methodid</code> argument. <code>newobjectv()</code> accepts these arguments, and, in turn, passes them to the java method that the programmer wishes to invoke.</p>  >
    /// <linkage>Index 29 in the JNIEnv interface function table.</linkage>
    /// <param name="clazz">A Java class object.</param>
    /// <param name="methodID">The method ID of the constructor.</param>
    /// <additional parameter for newobject>arguments to the constructor.</additional parameter for newobject>
    /// <additional parameter for newobjecta><code>args</code>: an array of arguments to the constructor.</additional parameter for newobjecta>
    /// <additional parameter for newobjectv><code>args</code>: a va_list of arguments to the constructor.</additional parameter for newobjectv>
    /// <returns>Returns a Java object, or <code class= "cCode">NULL</code> if the object cannot be constructed.</returns>
    /// <throws><code>InstantiationException</code>: if the class is an interface or an abstract class.</throws>
    /// <throws><code>OutOfMemoryError</code>: if the system runs out of memory.</throws>
    /// <throws>Any exceptions thrown by the constructor.</throws>
    IntPtr NewObject(IntPtr clazz, IntPtr methodID);

    IntPtr NewObjectA(IntPtr clazz, IntPtr methodId, params object[] jvalue);

    /// <linkage>Index 31 in the JNIEnv interface function table.</linkage>
    /// <param name="obj">A Java object (must not be </param>
    /// <returns>Returns a Java class object.</returns>
    IntPtr GetObjectClass(IntPtr obj);

    /// <linkage>Index 32 in the JNIEnv interface function table.</linkage>
    /// <param name="obj">A Java object.</param>
    /// <param name="clazz">A Java class object.</param>
    /// <returns>Returns <code>JNI_TRUE</code> if <code>obj</code> can be cast to <code class= "cCode">clazz</code>; otherwise, returns <code class= "cCode">JNI_FALSE</code>. A <code>NULL</code> object can be cast to any class.</returns>
    bool IsInstanceOf(IntPtr obj, IntPtr clazz);

    /// <linkage>Index 33 in the JNIEnv interface function table.</linkage>
    /// <param name="clazz">A Java class object.</param>
    /// <param name="name">The method name in a 0-terminated modified UTF-8 string.</param>
    /// <param name="sig">The method signature in 0-terminated modified UTF-8 string.</param>
    /// <returns>Returns a method ID, or <code class= "cCode">NULL</code> if the specified method cannot be found.</returns>
    /// <throws><code>NoSuchMethodError</code>: if the specified method cannot be found.</throws>
    /// <throws><code class= "cCode">ExceptionInInitializerError</code>: if the class initializer fails due to an exception.</throws>
    /// <throws><code>OutOfMemoryError</code>: if the system runs out of memory.</throws>
    IntPtr GetMethodId(IntPtr clazz, string name, string sig);

    IntPtr CallObjectMethod(IntPtr jobject, IntPtr jmethodid);

    IntPtr CallObjectMethodA(IntPtr jobject, IntPtr jmethodid, params object[] jvalue);

    bool CallBooleanMethod(IntPtr jobject, IntPtr jmethodid);

    bool CallBooleanMethodA(IntPtr jobject, IntPtr jmethodid, params object[] jvalue);

    byte CallByteMethod(IntPtr jobject, IntPtr jmethodid);

    byte CallByteMethodA(IntPtr jobject, IntPtr jmethodid, params object[] jvalue);

    char CallCharMethod(IntPtr jobject, IntPtr jmethodid);

    char CallCharMethodA(IntPtr jobject, IntPtr jmethodid, params object[] jvalue);

    short CallShortMethod(IntPtr jobject, IntPtr jmethodid);

    short CallShortMethodA(IntPtr jobject, IntPtr jmethodid, params object[] jvalue);

    int CallIntMethod(IntPtr jobject, IntPtr jmethodid);

    int CallIntMethodA(IntPtr jobject, IntPtr jmethodid, params object[] jvalue);

    long CallLongMethod(IntPtr jobject, IntPtr jmethodid);

    long CallLongMethodA(IntPtr jobject, IntPtr jmethodid, params object[] jvalue);

    float CallFloatMethod(IntPtr jobject, IntPtr jmethodid);

    float CallFloatMethodA(IntPtr jobject, IntPtr jmethodid, params object[] jvalue);

    double CallDoubleMethod(IntPtr jobject, IntPtr jmethodid);

    double CallDoubleMethodA(IntPtr jobject, IntPtr jmethodid, params object[] jvalue);

    void CallVoidMethod(IntPtr jobject, IntPtr jmethodid);

    void CallVoidMethodA(IntPtr jobject, IntPtr jmethodid, params object[] jvalue);

    IntPtr CallNonvirtualObjectMethod(IntPtr jobject, IntPtr jclass, IntPtr jmethodid);

    IntPtr CallNonvirtualObjectMethodA(IntPtr jobject, IntPtr jclass, IntPtr jmethodid, params object[] jvalue);

    bool CallNonvirtualBooleanMethod(IntPtr jobject, IntPtr jclass, IntPtr jmethodid);

    bool CallNonvirtualBooleanMethodA(IntPtr jobject, IntPtr jclass, IntPtr jmethodid, params object[] jvalue);

    byte CallNonvirtualByteMethod(IntPtr jobject, IntPtr jclass, IntPtr jmethodid);

    byte CallNonvirtualByteMethodA(IntPtr jobject, IntPtr jclass, IntPtr jmethodid, params object[] jvalue);

    char CallNonvirtualCharMethod(IntPtr jobject, IntPtr jclass, IntPtr jmethodid);

    char CallNonvirtualCharMethodA(IntPtr jobject, IntPtr jclass, IntPtr jmethodid, params object[] jvalue);

    short CallNonvirtualShortMethod(IntPtr jobject, IntPtr jclass, IntPtr jmethodid);

    short CallNonvirtualShortMethodA(IntPtr jobject, IntPtr jclass, IntPtr jmethodid, params object[] jvalue);

    int CallNonvirtualIntMethod(IntPtr jobject, IntPtr jclass, IntPtr jmethodid);

    int CallNonvirtualIntMethodA(IntPtr jobject, IntPtr jclass, IntPtr jmethodid, params object[] jvalue);

    long CallNonvirtualLongMethod(IntPtr jobject, IntPtr jclass, IntPtr jmethodid);

    long CallNonvirtualLongMethodA(IntPtr jobject, IntPtr jclass, IntPtr jmethodid, params object[] jvalue);

    float CallNonvirtualFloatMethod(IntPtr jobject, IntPtr jclass, IntPtr jmethodid);

    float CallNonvirtualFloatMethodA(IntPtr jobject, IntPtr jclass, IntPtr jmethodid, params object[] jvalue);

    double CallNonvirtualDoubleMethod(IntPtr jobject, IntPtr jclass, IntPtr jmethodid);

    double CallNonvirtualDoubleMethodA(IntPtr jobject, IntPtr jclass, IntPtr jmethodid, params object[] jvalue);

    void CallNonvirtualVoidMethod(IntPtr jobject, IntPtr jclass, IntPtr jmethodid);

    void CallNonvirtualVoidMethodA(IntPtr jobject, IntPtr jclass, IntPtr jmethodid, params object[] jvalue);

    /// <linkage>Index 94 in the JNIEnv interface function table.</linkage>
    /// <param name="clazz">A Java class object.</param>
    /// <param name="name">The field name in a 0-terminated modified UTF-8 string.</param>
    /// <param name="sig">The field signature in a 0-terminated modified UTF-8 string.</param>
    /// <returns>Returns a field ID, or <code class= "cCode">NULL</code> if the operation fails.</returns>
    /// <throws><code>NoSuchFieldError</code>: if the specified field cannot be found.</throws>
    /// <throws><code class= "cCode">ExceptionInInitializerError</code>: if the class initializer fails due to an exception.</throws>
    /// <throws><code>OutOfMemoryError</code>: if the system runs out of memory.</throws>
    IntPtr GetFieldId(IntPtr clazz, string name, string sig);

    IntPtr GetObjectField(IntPtr jobject, IntPtr jfieldid);

    bool GetBooleanField(IntPtr jobject, IntPtr jfieldid);

    byte GetByteField(IntPtr jobject, IntPtr jfieldid);

    char GetCharField(IntPtr jobject, IntPtr jfieldid);

    short GetShortField(IntPtr jobject, IntPtr jfieldid);

    int GetIntField(IntPtr jobject, IntPtr jfieldid);

    long GetLongField(IntPtr jobject, IntPtr jfieldid);

    float GetFloatField(IntPtr jobject, IntPtr jfieldid);

    double GetDoubleField(IntPtr jobject, IntPtr jfieldid);

    void SetObjectField(IntPtr jobject, IntPtr jfieldid, IntPtr jobjectValue);

    void SetBooleanField(IntPtr jobject, IntPtr jfieldid, bool jboolean);

    void SetByteField(IntPtr jobject, IntPtr jfieldid, byte jbyte);

    void SetCharField(IntPtr jobject, IntPtr jfieldid, char jchar);

    void SetShortField(IntPtr jobject, IntPtr jfieldid, short jshort);

    void SetIntField(IntPtr jobject, IntPtr jfieldid, int value);

    void SetLongField(IntPtr jobject, IntPtr jfieldid, long jlong);

    void SetFloatField(IntPtr jobject, IntPtr jfieldid, float jfloat);

    void SetDoubleField(IntPtr jobject, IntPtr jfieldid, double jdouble);

    /// <linkage> Index 113 in the JNIEnv interface function table. </linkage>
    /// <param name="clazz">A Java class object.</param>
    /// <param name="name">The static method name in a 0-terminated modified UTF-8 string.</param>
    /// <param name="sig">The method signature in a 0-terminated modified UTF-8 string.</param>
    /// <returns>Returns a method ID, or <code class= "cCode">NULL</code> if the operation fails.</returns>
    /// <throws><code>NoSuchMethodError</code>: if the specified static method cannot be found.</throws>
    /// <throws><code class= "cCode">ExceptionInInitializerError</code>: if the class initializer fails due to an exception.</throws>
    /// <throws><code>OutOfMemoryError</code>: if the system runs out of memory.</throws>
    IntPtr GetStaticMethodId(IntPtr clazz, string name, string sig);

    IntPtr CallStaticObjectMethod(IntPtr jclass, IntPtr jmethodid);

    IntPtr CallStaticObjectMethodA(IntPtr jclass, IntPtr jmethodid, params object[] jvalue);

    bool CallStaticBooleanMethod(IntPtr jclass, IntPtr jmethodid);

    bool CallStaticBooleanMethodA(IntPtr jclass, IntPtr jmethodid, params object[] jvalue);

    byte CallStaticByteMethod(IntPtr jclass, IntPtr jmethodid);

    byte CallStaticByteMethodA(IntPtr jclass, IntPtr jmethodid, params object[] jvalue);

    char CallStaticCharMethod(IntPtr jclass, IntPtr jmethodid);

    char CallStaticCharMethodA(IntPtr jclass, IntPtr jmethodid, params object[] jvalue);

    short CallStaticShortMethod(IntPtr jclass, IntPtr jmethodid);

    short CallStaticShortMethodA(IntPtr jclass, IntPtr jmethodid, params object[] jvalue);

    int CallStaticIntMethod(IntPtr jclass, IntPtr jmethodid);

    int CallStaticIntMethodA(IntPtr jclass, IntPtr jmethodid, params object[] jvalue);

    long CallStaticLongMethod(IntPtr jclass, IntPtr jmethodid);

    long CallStaticLongMethodA(IntPtr jclass, IntPtr jmethodid, params object[] jvalue);

    float CallStaticFloatMethod(IntPtr jclass, IntPtr jmethodid);

    float CallStaticFloatMethodA(IntPtr jclass, IntPtr jmethodid, params object[] jvalue);

    double CallStaticDoubleMethod(IntPtr jclass, IntPtr jmethodid);

    double CallStaticDoubleMethodA(IntPtr jclass, IntPtr jmethodid, params object[] jvalue);

    void CallStaticVoidMethod(IntPtr jclass, IntPtr jmethodid);

    void CallStaticVoidMethodA(IntPtr jclass, IntPtr jmethodid, params object[] jvalue);

    /// <linkage>Index 144 in the JNIEnv interface function table.</linkage>
    /// <param name="clazz">A Java class object.</param>
    /// <param name="name">The static field name in a 0-terminated modified UTF-8 string.</param>
    /// <param name="sig">The field signature in a 0-terminated modified UTF-8 string.</param>
    /// <returns>Returns a field ID, or <code class= "cCode">NULL</code> if the specified static field cannot be found.</returns>
    /// <throws><code>NoSuchFieldError</code>: if the specified static field cannot be found.</throws>
    /// <throws><code class= "cCode">ExceptionInInitializerError</code>: if the class initializer fails due to an exception.</throws>
    /// <throws><code>OutOfMemoryError</code>: if the system runs out of memory.</throws>
    IntPtr GetStaticFieldId(IntPtr clazz, string name, string sig);

    IntPtr GetStaticObjectField(IntPtr jclass, IntPtr jfieldid);

    bool GetStaticBooleanField(IntPtr jclass, IntPtr jfieldid);

    byte GetStaticByteField(IntPtr jclass, IntPtr jfieldid);

    char GetStaticCharField(IntPtr jclass, IntPtr jfieldid);

    short GetStaticShortField(IntPtr jclass, IntPtr jfieldid);

    int GetStaticIntField(IntPtr jclass, IntPtr jfieldid);

    long GetStaticLongField(IntPtr jclass, IntPtr jfieldid);

    float GetStaticFloatField(IntPtr jclass, IntPtr jfieldid);

    double GetStaticDoubleField(IntPtr jclass, IntPtr jfieldid);

    void SetStaticObjectField(IntPtr jclass, IntPtr jfieldid, IntPtr jobject);

    void SetStaticBooleanField(IntPtr jclass, IntPtr jfieldid, bool jboolean);

    void SetStaticByteField(IntPtr jclass, IntPtr jfieldid, byte jbyte);

    void SetStaticCharField(IntPtr jclass, IntPtr jfieldid, char jchar);

    void SetStaticShortField(IntPtr jclass, IntPtr jfieldid, short jshort);

    void SetStaticIntField(IntPtr jclass, IntPtr jfieldid, int value);

    void SetStaticLongField(IntPtr jclass, IntPtr jfieldid, long jlong);

    void SetStaticFloatField(IntPtr jclass, IntPtr jfieldid, float jfloat);

    void SetStaticDoubleField(IntPtr jclass, IntPtr jfieldid, double jdouble);

    /// <linkage> Index 163 in the JNIEnv interface function table. </linkage>
    /// <param name="unicodeChars">Pointer to a Unicode string.</param>
    /// <param name="len">Length of the Unicode string.</param>
    /// <returns>Returns a Java string object, or <code class= "cCode">NULL</code> if the string cannot be constructed.</returns>
    /// <throws><code>OutOfMemoryError</code>: if the system runs out of memory.</throws>
    IntPtr NewString(char* unicodeChars, int len);

    /// <linkage> Index 164 in the JNIEnv interface function table. </linkage>
    /// <param name="str">A Java string object.</param>
    /// <returns>Returns the length of the Java string.</returns>
    int GetStringLength(IntPtr str);

    /// <linkage> Index 165 in the JNIEnv interface function table. </linkage>
    /// <param name="str">A Java string object.</param>
    /// <param name="isCopy">A pointer to a boolean.</param>
    /// <returns>Returns a pointer to a Unicode string, or <code>NULL</code> if the operation fails.</returns>
    char* GetStringChars(IntPtr str, bool* isCopy);

    /// <linkage> Index 166 in the JNIEnv interface function table. </linkage>
    /// <param name="str">A Java string object.</param>
    /// <param name="chars">A pointer to a Unicode string.</param>
    void ReleaseStringChars(IntPtr str, char* chars);

    /// <linkage> Index 167 in the JNIEnv interface function table. </linkage>
    /// <param name="bytes">The pointer to a modified UTF-8 string.</param>
    /// <returns>Returns a Java string object, or <code class= "cCode">NULL</code> if the string cannot be constructed.</returns>
    /// <throws><code>OutOfMemoryError</code>: if the system runs out of memory.</throws>
    IntPtr NewStringUtf(byte* bytes);

    /// <linkage> Index 168 in the JNIEnv interface function table. </linkage>
    /// <param name="str">A Java string object.</param>
    /// <returns>Returns the UTF-8 length of the string.</returns>
    int GetStringUtfLength(IntPtr str);

    /// <linkage> Index 169 in the JNIEnv interface function table. </linkage>
    /// <param name="str">A Java string object.</param>
    /// <param name="isCopy">A pointer to a boolean.</param>
    /// <returns>Returns a pointer to a modified UTF-8 string, or <code>NULL</code> if the operation fails.</returns>
    byte* GetStringUtfChars(IntPtr str, bool* isCopy);

    /// <linkage> Index 170 in the JNIEnv interface function table. </linkage>
    /// <param name="str">A Java string object.</param>
    /// <param name="utf">A pointer to a modified UTF-8 string.</param>
    void ReleaseStringUtfChars(IntPtr str, byte* utf);

    /// <linkage> Index 171 in the JNIEnv interface function table. </linkage>
    /// <param name="array">A Java array object.</param>
    /// <returns>Returns the length of the array.</returns>
    int GetArrayLength(IntPtr array);

    /// <linkage> Index 172 in the JNIEnv interface function table. </linkage>
    /// <param name="length">Array size.</param>
    /// <param name="elementClass">Array element class.</param>
    /// <param name="initialElement">: initialization value.</param>
    /// <returns>Returns a Java array object, or <code class= "cCode">NULL</code> if the array cannot be constructed.</returns>
    /// <throws><code>OutOfMemoryError</code>: if the system runs out of memory.</throws>
    IntPtr NewObjectArray(int length, IntPtr elementClass, IntPtr initialElement);

    /// <linkage> Index 173 in the JNIEnv interface function table. </linkage>
    /// <param name="array">A Java array.</param>
    /// <param name="index">Array index.</param>
    /// <returns>Returns a Java object.</returns>
    /// <throws><code class= "cCode">ArrayIndexOutOfBoundsException</code>: if <code class= "cCode">index</code> does not specify a valid index in the array.</throws>
    IntPtr GetObjectArrayElement(IntPtr array, int index);

    /// <linkage> Index 174 in the JNIEnv interface function table. </linkage>
    /// <param name="array">A Java array.</param>
    /// <param name="index">Array index.</param>
    /// <param name="value">The new value.</param>
    /// <throws><code class= "cCode">ArrayIndexOutOfBoundsException</code>: if <code class= "cCode">index</code> does not specify a valid index in the array.</throws>
    /// <throws><code>ArrayStoreException</code>: if the class of <code>value</code> is not a subclass of the element class of the array.</throws>
    void SetObjectArrayElement(IntPtr array, int index, IntPtr value);

    IntPtr NewBooleanArray(int jsize);

    IntPtr NewByteArray(int jsize);

    IntPtr NewCharArray(int jsize);

    IntPtr NewShortArray(int jsize);

    IntPtr NewIntArray(int jsize);

    IntPtr NewLongArray(int jsize);

    IntPtr NewFloatArray(int jsize);

    IntPtr NewDoubleArray(int jsize);

    bool* GetBooleanArrayElements(IntPtr jbooleanarray, bool* isCopy);

    byte* GetByteArrayElements(IntPtr jbytearray, bool* isCopy);

    char* GetCharArrayElements(IntPtr jchararray, bool* isCopy);

    short* GetShortArrayElements(IntPtr jshortarray, bool* isCopy);

    int* GetIntArrayElements(IntPtr intarray, bool* isCopy);

    long* GetLongArrayElements(IntPtr jlongarray, bool* isCopy);

    float* GetFloatArrayElements(IntPtr jfloatarray, bool* isCopy);

    double* GetDoubleArrayElements(IntPtr jdoublearray, bool* isCopy);

    void ReleaseBooleanArrayElements(IntPtr jbooleanarray, bool* elements, int mode);

    void ReleaseByteArrayElements(IntPtr jbytearray, byte* elements, int mode);

    void ReleaseCharArrayElements(IntPtr jchararray, char* elements, int mode);

    void ReleaseShortArrayElements(IntPtr jshortarray, short* elements, int mode);

    void ReleaseIntArrayElements(IntPtr intarray, int* elements, int mode);

    void ReleaseLongArrayElements(IntPtr jlongarray, long* elements, int mode);

    void ReleaseFloatArrayElements(IntPtr jfloatarray, float* elements, int mode);

    void ReleaseDoubleArrayElements(IntPtr jdoublearray, double* elements, int mode);

    void GetBooleanArrayRegion(IntPtr jbooleanarray, int startIndex, int length, bool* buffer);

    void GetByteArrayRegion(IntPtr jbytearray, int startIndex, int length, byte* buffer);

    void GetCharArrayRegion(IntPtr jchararray, int startIndex, int length, char* buffer);

    void GetShortArrayRegion(IntPtr jshortarray, int startIndex, int length, short* buffer);

    void GetIntArrayRegion(IntPtr intarray, int startIndex, int length, int* buffer);

    void GetLongArrayRegion(IntPtr jlongarray, int startIndex, int length, long* buffer);

    void GetFloatArrayRegion(IntPtr jfloatarray, int startIndex, int length, float* buffer);

    void GetDoubleArrayRegion(IntPtr jdoublearray, int startIndex, int length, double* buffer);

    void SetBooleanArrayRegion(IntPtr jbooleanarray, int startIndex, int length, bool* buffer);

    void SetByteArrayRegion(IntPtr jbytearray, int startIndex, int length, byte* buffer);

    void SetCharArrayRegion(IntPtr jchararray, int startIndex, int length, char* buffer);

    void SetShortArrayRegion(IntPtr jshortarray, int startIndex, int length, short* buffer);

    void SetIntArrayRegion(IntPtr intarray, int startIndex, int length, int* buffer);

    void SetLongArrayRegion(IntPtr jlongarray, int startIndex, int length, long* buffer);

    void SetFloatArrayRegion(IntPtr jfloatarray, int startIndex, int length, float* buffer);

    void SetDoubleArrayRegion(IntPtr jdoublearray, int startIndex, int length, double* buffer);

    /// <linkage>Index 215 in the JNIEnv interface function table.</linkage>
    /// <param name="clazz">A Java class object.</param>
    /// <param name="methods">The native methods in the class.</param>
    /// <param name="nMethods">The number of native methods in the class.</param>
    /// <returns>Returns &#8220;0&#8221; on success; returns a negative value on failure.</returns>
    /// <throws><code>NoSuchMethodError</code>: if a specified method cannot be found or if the method is not native.</throws>
    int RegisterNatives(IntPtr clazz, void* methods, int nMethods);

    /// <linkage> Index 216 in the JNIEnv interface function table. </linkage>
    /// <param name="clazz">A Java class object.</param>
    /// <returns>Returns &#8220;0&#8221; on success; returns a negative value on failure.</returns>
    int UnregisterNatives(IntPtr clazz);

    /// <linkage> Index 217 in the JNIEnv interface function table. </linkage>
    /// <param name="obj">A normal Java object or class object.</param>
    /// <returns>Returns &#8220;0&#8221; on success; returns a negative value on failure.</returns>
    int MonitorEnter(IntPtr obj);

    /// <linkage> Index 218 in the JNIEnv interface function table. </linkage>
    /// <param name="obj">A normal Java object or class object.</param>
    /// <returns>Returns &#8220;0&#8221; on success; returns a negative value on failure.</returns>
    /// <exceptions><code>IllegalMonitorStateException</code>: if the current thread does not own the monitor.</exceptions>
    /// <exceptions>The NIO-related entry points allow native code to access <code>java.nio</code> <em>direct buffers</em>. The contents of a direct buffer can, potentially, reside in native memory outside of the ordinary garbage-collected heap. For information about direct buffers, please see <a href="../../io/index.html">New I/O APIs</a> and the specification of the <a href= "../../../../api/java/nio/ByteBuffer.html"><tt>java.nio.ByteBuffer</tt></a> class.</exceptions>
    /// <exceptions>Every implementation of the Java virtual machine must support these functions, but not every implementation is required to support JNI access to direct buffers. If a JVM does not support such access then the <tt>NewDirectByteBuffer</tt> and <tt>GetDirectBufferAddress</tt> functions must always return <tt>NULL</tt>, and the <tt>GetDirectBufferCapacity</tt> function must always return <tt>-1</tt>. If a JVM <em>does</em> support such access then these three functions must be implemented to return the appropriate values.</exceptions>
    int MonitorExit(IntPtr obj);

    /// <linkage>Index 219 in the JNIEnv interface function table.</linkage>
    /// <param name="vm">A pointer to where the result should be placed.</param>
    /// <returns>Returns &#8220;0&#8221; on success; returns a negative value on failure.</returns>
    int GetJavaVm(IntPtr vm);

    /// <linkage> Index 220 in the JNIEnv interface function table. </linkage>
    /// <since>JDK/JRE 1.2</since>
    void GetStringRegion(IntPtr jstring, int startIndex, int length, char* buffer);

    /// <linkage> Index 221 in the JNIEnv interface function table. </linkage>
    /// <since>JDK/JRE 1.2</since>
    void GetStringUtfRegion(IntPtr jstring, int startIndex, int length, byte* buffer);

    void* GetPrimitiveArrayCritical(IntPtr jarray, bool* isCopy);

    void ReleasePrimitiveArrayCritical(IntPtr jarray, void* carray, int mode);

    char* GetStringCritical(IntPtr jstring, bool* isCopy);

    void ReleaseStringCritical(IntPtr jstring, char* carray);

    /// <linkage> Index 226 in the JNIEnv interface function table. </linkage>
    /// <since>JDK/JRE 1.2</since>
    IntPtr NewWeakGlobalRef(IntPtr jobject);

    /// <linkage> Index 227 in the JNIEnv interface function table. </linkage>
    /// <since>JDK/JRE 1.2<a name="arrayops"></a></since>
    void DeleteWeakGlobalRef(IntPtr jweak);

    /// <linkage> Index 228 in the JNIEnv interface function table. </linkage>
    /// <since>JDK/JRE 1.2</since>
    bool ExceptionCheck();

    /// <linkage>Index 229 in the JNIEnv interface function table.</linkage>
    /// <param name="address">The starting address of the memory region (must not be </param>
    /// <param name="capacity">The size in bytes of the memory region (must be positive)</param>
    /// <returns>Returns a local reference to the newly-instantiated <tt>java.nio.ByteBuffer</tt> object. Returns <tt>NULL</tt> if an exception occurs, or if JNI access to direct buffers is not supported by this virtual machine.</returns>
    /// <exceptions><tt>OutOfMemoryError</tt>: if allocation of the <tt>ByteBuffer</tt> object fails</exceptions>
    /// <since>JDK/JRE 1.4</since>
    IntPtr NewDirectByteBuffer(void* address, long capacity);

    /// <linkage>Index 230 in the JNIEnv interface function table.</linkage>
    /// <param name="buf">A direct </param>
    /// <returns>Returns the starting address of the memory region referenced by the buffer. Returns <tt>NULL</tt> if the memory region is undefined, if the given object is not a direct <tt>java.nio.Buffer</tt>, or if JNI access to direct buffers is not supported by this virtual machine.</returns>
    /// <since>JDK/JRE 1.4</since>
    void* GetDirectBufferAddress(IntPtr buf);

    /// <linkage>Index 231 in the JNIEnv interface function table.</linkage>
    /// <param name="buf">A direct </param>
    /// <returns>Returns the capacity of the memory region associated with the buffer. Returns <tt>-1</tt> if the given object is not a direct <tt>java.nio.Buffer</tt>, if the object is an unaligned view buffer and the processor architecture does not support unaligned access, or if JNI access to direct buffers is not supported by this virtual machine.</returns>
    /// <since>JDK/JRE 1.4</since>
    /// <since>Programmers can use the JNI to call Java methods or access Java fields if they know the name and type of the methods or fields. The Java Core Reflection API allows programmers to introspect Java classes at runtime. JNI provides a set of conversion functions between field and method IDs used in the JNI to field and method objects used in the Java Core Reflection API.</since>
    long GetDirectBufferCapacity(IntPtr buf);

    /// <linkage>Index 232 in the JNIEnv interface function table.</linkage>
    /// <param name="obj">A local, global or weak global reference.<br /> <br /> </param>
    /// <returns>The function <code>GetObjectRefType</code> returns one of the following enumerated values defined as a <code class= "cCode">jobjectRefType</code>:</returns>
    /// <returns>If the argument <code>obj</code> is a weak global reference type, the return will be <code class= "cCode">JNIWeakGlobalRefType</code>.</returns>
    /// <returns>If the argument <code>obj</code> is a global reference type, the return value will be <code class= "cCode">JNIGlobalRefType</code>.</returns>
    /// <returns>If the argument <code>obj</code> is a local reference type, the return will be <code>JNILocalRefType</code>.</returns>
    /// <returns>If the <code>obj</code> argument is not a valid reference, the return value for this function will be <code class= "cCode">JNIInvalidRefType</code>.</returns>
    /// <returns>An invalid reference is a reference which is not a valid handle. That is, the <code>obj</code> pointer address does not point to a location in memory which has been allocated from one of the Ref creation functions or returned from a JNI function.</returns>
    /// <returns>As such, <code>NULL</code> would be an invalid reference and <code>GetObjectRefType(env,NULL)</code> would return <code>JNIInvalidRefType</code>.</returns>
    /// <returns>On the other hand, a null reference, which is a reference that points to a null, would return the type of reference that the null reference was originally created as.</returns>
    /// <returns><code>GetObjectRefType</code> cannot be used on deleted references.</returns>
    /// <returns>Since references are typically implemented as pointers to memory data structures that can potentially be reused by any of the reference allocation services in the VM, once deleted, it is not specified what value the <code class= "cCode">GetObjectRefType</code> will return.</returns>
    /// <since>JDK/JRE 1.6</since>
    int GetObjectRefType(IntPtr obj);

    IntPtr GetModule(IntPtr jclass);
}