using System.Text;
using Dova.Tools.JavaClassStructureGenerator.Builders;

namespace Dova.Tools.JavaClassStructureGenerator.Common;

internal static class DefinitionCleaner
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
    
    public static IReadOnlyList<string> CleanUnknownGenerics(IEnumerable<string> strings) => 
        strings.Select((str, index) => str.Equals("?") ? Letters[index] : str).ToList();

    private static string CleanInnerNamespace(string str)
    {
        var ret = str;
        
        var parts = ret.Split(".");

        if (parts.Length > 1)
        {
            ret = parts.Aggregate((previous, next) =>
            {
                var currentLastPart = previous.Split(".").Last();
                var isClassNamePart = char.IsUpper(currentLastPart[0]);
                return isClassNamePart ? $"{previous}_{next}" : $"{previous}.{next}";
            });
        }

        if (parts.Length > 1
            || ret.EndsWith(";")) // For namespaces
        {
            var containedKeyword = CSharpKeywords
                .Where(keyword => ret.Contains(keyword));

            parts = ret.Split(".");
            
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
            var s when !s.Contains(".") && !s.Contains("[]") && !s.Contains("<") => CleanInnerNamespace(s), // i.e.: byte or MyClass
            var s when s.Contains(".") && !s.Contains("[]") && !s.Contains("<") => CleanInnerNamespace(s), // i.e.: java.lang.Byte or com.package.MyClass
            var s when s.EndsWith("[]") => $"JavaArray<{CleanJavaClassName(s[..^2])}>", // i.e.: byte[] or java.lang.Byte[]
            var s when s.EndsWith(">") && !s.Contains(">.") => PerformInnerCleanForGeneric(s),// i.e.: com.package.MyClass<...>
            var s when s.Contains(">.") => CleanInnerGeneric(str), // i.e.: java.lang.invoke.ClassSpecializer<T, K, S>.SpeciesData
            _ => str
        };

    private static string CleanInnerGeneric(string str)
    {
        var ret = str;
        
        while (ret.Contains("<") && ret.Contains(">"))
        {
            var openBracketIndex = ret.IndexOf("<");
            var closeBracketIndex = ret.IndexOf(">");

            if (closeBracketIndex + 1 == ret.Length) // + 1 for ">" to check if it is a closing bracket at the end of the class name / definition
            {
                break;
            }
            
            var subStr = ret[openBracketIndex..(closeBracketIndex + 2)]; // +2 in order to take additional ">."

            ret = ret.Replace(subStr, "_");
        }

        ret = PerformInnerClean(ret);

        return ret;
    }

    private static string PerformInnerCleanForGeneric(string str)
    {
        var startIndex = str.IndexOf("<", StringComparison.Ordinal);
        
        var genericPrefix = str[..startIndex];
        var genericPrefixCleaned = CleanInnerNamespace(genericPrefix);
        var genericBody = str[(startIndex + 1)..^1]; // skip '<' and '>'

        var genericArgs = ReadGenericBody(genericBody)
            .Select(CleanJavaClassName)
            .ToArray();

        var cleaned = string.Join(", ", genericArgs);
        
        return $"{genericPrefixCleaned}<{cleaned}>";
    }

    private static IEnumerable<string> ReadGenericBody(string genericBody)
    {
        var body = genericBody.Replace(" ", "");
        var depth = 0;
        var sb = new StringBuilder();

        foreach (var bodyChar in body)
        {
            switch (bodyChar)
            {
                case '<':
                    sb.Append(bodyChar);
                    depth++;
                    break;
                case '>':
                    sb.Append(bodyChar);
                    depth--;
                    break;
                case var ch when ch.Equals(',') && depth == 0:
                    yield return sb.ToString();
                    sb.Clear();
                    break;
                default:
                    sb.Append(bodyChar);
                    break;
            }
        }
        
        yield return sb.ToString();
        
        sb.Clear();
    }
}