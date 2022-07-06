using Dova.Internals;

namespace Dova.Tools.JavaRuntimeFunctionsGenerator;

internal static class JniTypesMapper
{
    public static string? Map(string? s)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            return s;
        }

        foreach (var kvp in Converters.JavaToDotnetTypes)
        {
            s = s.Replace(kvp.Key.Name, kvp.Value.Name);
        }

        return s;
    }
}