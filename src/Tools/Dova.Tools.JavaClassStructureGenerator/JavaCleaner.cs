using Dova.Tools.JavaClassStructureGenerator.Builders;

namespace Dova.Tools.JavaClassStructureGenerator;

internal static class JavaCleaner
{
    private static IReadOnlyList<string> Letters { get; } = new List<string>
    {
        "A", "B", "C", "D", "E", 
        "F", "G", "H", "I", "J",
        "K", "L", "M", "N", "O", 
        "P", "Q", "R", "S", "T",
        "U", "W", "X", "Y", "Z"
    };

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

        foreach (var pair in Replacements)
        {
            ret = ret.Replace(pair.Key, pair.Value);
        }

        ret = PerformInnerClean(ret);

        return ret;
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

    public static string CleanJavaMethodName(string methodName)
    {
        var ret = methodName.Replace("$", "_"); // Specific case for methods - we cannot create a '.' in front of a method name

        foreach (var keyword in CSharpKeywords)
        {
            if (ret.Equals(keyword))
            {
                ret = ret.Replace(keyword, $"@{keyword}");
            }
        }

        return ret;
    }

    private static string CleanInnerNamespace(string str)
    {
        var ret = str;
        
        var parts = ret.Split(".");

        if (parts.Length > 1)
        {
            const string ToBeRemoved = "TO_BE_REMOVED";
            
            for (var i = 0; i < parts.Length - 1; ++i) // -1 because we don't want to include class name which is at last position
            {
                if (parts[i].Length > 0 
                    && char.IsUpper(parts[i][0])
                    && !parts[i].Contains("<")
                    && !parts[i].Contains(">"))
                {
                    parts[i] = ToBeRemoved;
                }
            }
            
            parts = parts
                .Where(x => !x.Equals(ToBeRemoved))
                .ToArray();
            
            var containedKeyword = CSharpKeywords
                .Where(keyword => ret.Contains(keyword));

            ret = string.Join(".", parts
                .Select(part => 
                    containedKeyword.Any(part.StartsWith) 
                    && !part.Contains(" ")
                        ? $"@{part}" 
                        : part));
        }

        return ret;
    }

    private static string PerformInnerClean(string str) =>
        str switch
        {
            var s when !s.Contains(".") && !s.Contains("[]") && !s.Contains("<") => s, // i.e.: byte or MyClass
            var s when s.Contains(".") && !s.Contains("[]") && !s.Contains("<") => CleanInnerNamespace(s), // i.e.: java.lang.Byte or com.package.MyClass
            var s when s.EndsWith("[]") => $"JavaArray<{CleanJavaClassName(s[..^2])}>", // i.e.: byte[] or java.lang.Byte[]
            var s when s.EndsWith(">") => PerformInnerCleanForGeneric(s),// i.e.: com.package.MyClass<...>
            _ => str
        };

    private static string PerformInnerCleanForGeneric(string str)
    {
        var startIndex = str.IndexOf("<", StringComparison.Ordinal);

        if (startIndex > 0)
        {
            var genericPrefix = str[..startIndex];
            var genericPrefixCleaned = CleanInnerNamespace(genericPrefix);
            var genericBody = str[(startIndex + 1)..^1];
        
            var genericArgs = genericBody.Split(",")
                .Select(x => x.Trim())
                .Select(CleanJavaClassName)
                .ToArray();

            var cleaned = string.Join(", ", genericArgs);
        
            return $"{genericPrefixCleaned}<{cleaned}>";
        }
        else
        {
            // TODO: Temporary fix - check why the param was 'L[]>' ????
            return str;
        }
    }

    public static IReadOnlyList<string> CleanUnknownGenerics(IEnumerable<string> strings) => 
        strings.Select((str, index) => str.Equals("?") ? Letters[index] : str).ToList();
}