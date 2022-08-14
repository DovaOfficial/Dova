using System.Collections;

namespace Dova.Common.Arrays;

internal class JavaArrayEnumerator<TElement> : IEnumerator<TElement>
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