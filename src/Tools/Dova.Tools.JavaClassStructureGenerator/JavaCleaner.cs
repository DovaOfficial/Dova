using Dova.Tools.JavaClassStructureGenerator.Builders;

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
        { "boolean", "bool" },
        { "<?>", $"<{AbstractBuilder.JavaObjectFullName}>" },
        { "? extends ", "" },
        { "? super ", "" },
        { "$", "." },
    };
    
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

        ret = PerformInnerClean(ret);

        return ret;
    }

    private static string PerformInnerClean(string str)
    {
        return str switch
        {
            var s when !s.Contains(".") && !s.Contains("[]") && !s.Contains("<") => s, // i.e.: byte or MyClass
            var s when s.Contains(".") && !s.Contains("[]") && !s.Contains("<") => s, // i.e.: java.lang.Byte or com.package.MyClass
            var s when s.EndsWith("[]") => $"JavaArray<{CleanJavaClassName(s[..^2])}>", // i.e.: byte[] or java.lang.Byte[]
            var s when s.EndsWith(">") => PerformInnerCleanForGeneric(s),// i.e.: com.package.MyClass<...>
            _ => str
        };
    }

    private static string PerformInnerCleanForGeneric(string str)
    {
        var startIndex = str.IndexOf("<", StringComparison.Ordinal);
        var genericPrefix = str[..startIndex];
        var genericBody = str[(startIndex + 1)..^1];
        var cleaned = CleanJavaClassName(genericBody);

        return $"{genericPrefix}<{cleaned}>";
    }

    public static string CleanJavaFieldName(string fieldName)
    {
        var ret = fieldName.Replace("$", ""); // Specific case for fields - we cannot create a '.' in front of a field name

        foreach (var keyword in CSharpKeywords)
        {
            if (ret.Equals(keyword))
            {
                ret = ret.Replace(keyword, $"@{keyword}");
            }
        }

        return ret;
    }
}