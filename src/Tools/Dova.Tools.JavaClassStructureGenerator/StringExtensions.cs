namespace Dova.Tools.JavaClassStructureGenerator;

public static class StringExtensions
{
    public static string ToFirstUppercase(this string str) => 
        char.ToUpper(str[0]) + str[1..];
}