namespace Dova.Tools.JavaClassStructureGenerator;

internal static class JavaConstants
{
    public static IEnumerable<string> Modifiers { get; } = new[] { "public", "private", "protected", "final", "abstract", "transient", "synchronized", "volatile", "static" };
    public static IEnumerable<string> Types { get; } = new[] { "class", "interface", "enum" };
}