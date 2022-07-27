namespace Dova.Tools.JavaClassStructureGenerator;

internal static class JavaCleaner
{
    private static IDictionary<string, string> Replacements { get; } = new Dictionary<string, string>
    {
        { "java.lang.Byte", "byte" },
        { "java.lang.Short", "short" },
        { "java.lang.Integer", "int" },
        { "java.lang.Long", "long" },
        { "java.lang.Float", "float" },
        { "java.lang.Double", "double" },
        { "java.lang.Boolean", "bool" },
        { "boolean", "bool" },
        { "java.lang.Char", "char" },
        { ".ref.", ".@ref." },
        { ".in.", ".@in." },
        { ".out.", ".@out." },
        { ".as.", ".@as." },
        { "$", "." },
        { "<?>", "<java.lang.Object>" },
        { "? extends ", "" },
    };

    public static string CleanMethodName(string methodName) => 
        methodName.Replace("$", "_");

    // TODO: Add support for types like 'java.lang.invoke.TypeDescriptor$OfField<java.lang.Class<? extends PrintStream>[]>'
    // TODO: Add support for types like 'java.lang.@ref.FinalReference<java.lang.Object>' (see C# @ref keyword wrapped)
    public static string CleanJavaClassName(string className)
    {
        var ret = className;

        foreach (var pair in Replacements)
        {
            ret = ret.Replace(pair.Key, pair.Value);
        }

        return ret;
    }
}