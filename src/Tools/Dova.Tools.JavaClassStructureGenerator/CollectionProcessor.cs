namespace Dova.Tools.JavaClassStructureGenerator;

internal static class CollectionProcessor
{
    /// <summary>
    /// Processes each element of the specified collection in parallel.
    /// </summary>
    public static void ForEachParallel<TElement>(IEnumerable<TElement> col, Action<TElement> callback)
    {
        var parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = 20
        };
        
        Parallel.ForEach(col, parallelOptions, callback);
    }
}