using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using Dova.Core;

namespace Dova.Common;

/// <summary>
/// Represents an array in Java language.
/// </summary>
public unsafe class JavaArray<TElement> : JavaObject, IList<TElement>
{
    public TElement this[int index]
    {
        get => Buffer[index];
        set
        {
            Buffer[index] = value;
            WriteBuffer();
        }
    }

    /// <summary>
    /// Type of elements
    /// </summary>
    private static Type ElementType => typeof(TElement);

    /// <summary>
    /// Stores all read elements.
    /// </summary>
    private TElement[] Buffer { get; }

    public int Count { get; }

    public bool IsReadOnly => false;

    /// <summary>
    /// Initializes new Array with given size as "count" and given classPtr (if array should be for objects, not primitives)
    /// </summary>
    /// <param name="count">Number of elements in array</param>
    /// <param name="classPtr">Pointer to a class (if array should contains objects and not primitives)</param>
    /// <param name="initialValue">Default value for array (try use valid pointer for objects)</param>
    public JavaArray(int count, IntPtr classPtr = default, IntPtr initialValue = default)
        : this(Initialize(count, classPtr, initialValue))
    {
    }

    public JavaArray(IntPtr currentRefPtr) : base(currentRefPtr)
    {
        Count = DovaVM.Runtime.GetArrayLength(currentRefPtr);
        Buffer = new TElement[Count];

        ReadBuffer();
    }

    public override string GetJavaClassSignature() => 
        throw new DovaException("JavaArray does not have a Java class signature.");

    public override IntPtr GetJavaClassRaw() =>
        throw new DovaException("JavaArray does not support a raw Java class pointer.");

    public override IntPtr GetJavaClassRefRaw() =>
        throw new DovaException("JavaArray does not support a raw Java class reference pointer.");

    public IEnumerator<TElement> GetEnumerator() =>
        new JavaArrayEnumerator<TElement>(this);

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();

    public void Add(TElement item) => 
        throw new DovaException($"JavaArray does not support adding new elements into array.");

    public void Clear()
    {
        for (var index = 0; index < Count; ++index)
        {
            Buffer[index] = default;
        }
        
        WriteBuffer();
    }

    public bool Contains(TElement item) => 
        IndexOf(item) >= 0;

    public void CopyTo(TElement[] array, int arrayIndex)
    {
        if (array == null)
        {
            throw new ArgumentNullException($"Null array passed.");
        }

        if (arrayIndex < 0)
        {
            throw new ArgumentOutOfRangeException($"Array Index cannot be less than 0, passed: {arrayIndex}");
        }

        if (arrayIndex >= Count)
        {
            throw new ArgumentOutOfRangeException($"Array Index cannot be more than Count, passed: {arrayIndex}");
        }

        for (var index = 0; index < Count; ++index)
        {
            array[index + arrayIndex] = Buffer[index];
        }
    }

    public bool Remove(TElement item)
    {
        var index = IndexOf(item);

        if (index < 0)
        {
            return false;
        }

        RemoveAt(index);

        return true;
    }

    public int IndexOf(TElement item)
    {
        if (Count == 0 || item == null)
        {
            return -1;
        }

        if (ElementType.IsPrimitive)
        {
            for (var index = 0; index < Count; ++index)
            {
                if (Buffer[index].Equals(item))
                {
                    return index;
                }
            }
        }

        if (this.First() is IJavaObject && item is IJavaObject javaObject)
        {
            for (var index = 0; index < Count; ++index)
            {
                if ((Buffer[index] as IJavaObject).CurrentRefPtr == javaObject.CurrentRefPtr)
                {
                    return index;
                }
            }
        }

        return -1;
    }

    public void Insert(int index, TElement item)
    {
        Buffer[index] = item;
        
        WriteBuffer();
    }

    public void RemoveAt(int index) => 
        Insert(index, default);

    /// <summary>
    /// Reads buffer from JNI
    /// </summary>
    private void ReadBuffer()
    {
        if (ElementType.IsPrimitive)
        {
            var isCopy = false;

            void* buffer = ElementType switch
            {
                var type when type == typeof(bool) => DovaVM.Runtime.GetBooleanArrayElements(CurrentRefPtr, &isCopy),
                var type when type == typeof(byte) => DovaVM.Runtime.GetByteArrayElements(CurrentRefPtr, &isCopy),
                var type when type == typeof(char) => DovaVM.Runtime.GetCharArrayElements(CurrentRefPtr, &isCopy),
                var type when type == typeof(double) => DovaVM.Runtime.GetDoubleArrayElements(CurrentRefPtr, &isCopy),
                var type when type == typeof(float) => DovaVM.Runtime.GetFloatArrayElements(CurrentRefPtr, &isCopy),
                var type when type == typeof(int) => DovaVM.Runtime.GetIntArrayElements(CurrentRefPtr, &isCopy),
                var type when type == typeof(long) => DovaVM.Runtime.GetLongArrayElements(CurrentRefPtr, &isCopy),
                var type when type == typeof(short) => DovaVM.Runtime.GetShortArrayElements(CurrentRefPtr, &isCopy),
                _ => throw new ArgumentOutOfRangeException($"Unknown primitive type: {ElementType}"),
            };

            var bufferBytes = (byte*)ToPointer(Buffer);

            var elementSize = Marshal.SizeOf<TElement>();
            var arraySize = Count * elementSize;

            for (var index = 0; index < arraySize; ++index)
            {
                bufferBytes[index] = ((byte*)buffer)[index];
            }

            return;
        }

        // Default constructor generated for each class in Dova.JDK, which takes only single IntPtr as a parameter
        var constructor = ElementType.GetConstructor(BindingFlags.Instance | BindingFlags.Public, new[] { typeof(IntPtr) });

        for (var index = 0; index < Count; ++index)
        {
            var elementPtr = DovaVM.Runtime.GetObjectArrayElement(CurrentRefPtr, index);
            var obj = constructor.Invoke(new object?[] { elementPtr });

            Buffer[index] = (TElement)obj;
        }
    }

    /// <summary>
    /// Writes buffer to JNI
    /// </summary>
    private void WriteBuffer()
    {
        if (ElementType.IsPrimitive)
        {
            var handlePtr = ToPointer(Buffer);

            switch (ElementType)
            {
                case var typeBool when typeBool == typeof(bool):
                    DovaVM.Runtime.SetBooleanArrayRegion(CurrentRefPtr, 0, Count, (bool*)handlePtr);
                    return;
                case var typeByte when typeByte == typeof(byte):
                    DovaVM.Runtime.SetByteArrayRegion(CurrentRefPtr, 0, Count, (byte*)handlePtr);
                    return;
                case var typeChar when typeChar == typeof(char):
                    DovaVM.Runtime.SetCharArrayRegion(CurrentRefPtr, 0, Count, (char*)handlePtr);
                    return;
                case var typeDouble when typeDouble == typeof(double):
                    DovaVM.Runtime.SetDoubleArrayRegion(CurrentRefPtr, 0, Count, (double*)handlePtr);
                    return;
                case var typeFloat when typeFloat == typeof(float):
                    DovaVM.Runtime.SetFloatArrayRegion(CurrentRefPtr, 0, Count, (float*)handlePtr);
                    return;
                case var typeInt when typeInt == typeof(int):
                    DovaVM.Runtime.SetIntArrayRegion(CurrentRefPtr, 0, Count, (int*)handlePtr);
                    return;
                case var typeLong when typeLong == typeof(long):
                    DovaVM.Runtime.SetLongArrayRegion(CurrentRefPtr, 0, Count, (long*)handlePtr);
                    return;
                case var typeShort when typeShort == typeof(short):
                    DovaVM.Runtime.SetShortArrayRegion(CurrentRefPtr, 0, Count, (short*)handlePtr);
                    return;
            }
        }

        for (var index = 0; index < Count; ++index)
        {
            if (Buffer[index] is IJavaObject javaObject)
            {
                DovaVM.Runtime.SetObjectArrayElement(CurrentRefPtr, index, javaObject.CurrentRefPtr);
            }
            else
            {
                throw new ArgumentException($"JavaArray only supports primitives and objects which inherit from {nameof(IJavaObject)}");
            }
        }
    }
    
    private static IntPtr Initialize(int count, IntPtr classPtr, IntPtr initialValue) =>
        ElementType switch
        {
            var type when type == typeof(bool) => DovaVM.Runtime.NewBooleanArray(count),
            var type when type == typeof(byte) => DovaVM.Runtime.NewByteArray(count),
            var type when type == typeof(char) => DovaVM.Runtime.NewCharArray(count),
            var type when type == typeof(double) => DovaVM.Runtime.NewDoubleArray(count),
            var type when type == typeof(float) => DovaVM.Runtime.NewFloatArray(count),
            var type when type == typeof(int) => DovaVM.Runtime.NewIntArray(count),
            var type when type == typeof(long) => DovaVM.Runtime.NewLongArray(count),
            var type when type == typeof(short) => DovaVM.Runtime.NewShortArray(count),
            _ => DovaVM.Runtime.NewObjectArray(count, classPtr, initialValue)
        };

    private static IntPtr ToPointer(object obj)
    {
        var handle = GCHandle.Alloc(obj, GCHandleType.Pinned);
        var handlePtr = handle.AddrOfPinnedObject();

        return handlePtr;
    }
}

public class JavaArrayEnumerator<TElement> : IEnumerator<TElement>
{
    private JavaArray<TElement> Array { get; }

    private int Index { get; set; }

    public TElement Current => Array[Index];

    object IEnumerator.Current => Current;

    public JavaArrayEnumerator(JavaArray<TElement> array)
    {
        Array = array;

        Reset();
    }

    public bool MoveNext()
    {
        Index++;

        return Index < Array.Count;
    }

    public void Reset() =>
        Index = 0;

    public void Dispose() =>
        Reset();
}