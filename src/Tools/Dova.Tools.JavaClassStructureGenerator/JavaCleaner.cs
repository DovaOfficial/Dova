namespace Dova.Tools.JavaClassStructureGenerator;

internal static class JavaCleaner
{
    private static IEnumerable<string> CSharpKeywords { get; } = new List<string>
    {
        "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const",
        "continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event", "explicit", "extern",
        "false", "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit", "in", "int", "interface",
        "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator", "out", "override",
        "params", "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof",
        "stackalloc", "static", "string", "struct", "switch", "this", "throw", "true", "try", "typeof", "uint",
        "ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "void", "volatile", "while"
    };
    
    private static IDictionary<string, string> Replacements { get; } = new Dictionary<string, string>
    {
        // { "java.lang.Byte", "byte" },
        // { "java.lang.Short", "short" },
        // { "java.lang.Integer", "int" },
        // { "java.lang.Long", "long" },
        // { "java.lang.Float", "float" },
        // { "java.lang.Double", "double" },
        // { "java.lang.Boolean", "bool" },
        { "boolean", "java.lang.Boolean" },
        // { "java.lang.Char", "char" },
        { "$", "." },
        { "<?>", "<java.lang.Object>" },
        { "? extends ", "" },
    };

    public static string CleanMethodName(string methodName) => 
        methodName.Replace("$", "_");

    // TODO: Add support for types like 'java.lang.invoke.TypeDescriptor$OfField<java.lang.Class<? extends PrintStream>[]>'
    // TODO: Add support for types like 'java.lang.@ref.FinalReference<java.lang.Object>' (see C# @ref keyword wrapped)
    // TODO: Add support for Type[][] => JavaArray<JavaArray<Type>>
    // TODO: Add support for byte[] => JavaArray<byte>
    public static string CleanJavaClassName(string className)
    {
        var ret = className;

        foreach (var keyword in CSharpKeywords)
        {
            ret = ret.Replace($".{keyword}.", $".@{keyword}.");
        }

        foreach (var pair in Replacements)
        {
            ret = ret.Replace(pair.Key, pair.Value);
        }

        return ret;
    }
}