namespace Dova.Tools.JavaClassStructureGenerator;

internal static class JavaCleaner
{
    public static string CleanMethodName(string methodName) => 
        methodName.Replace("$", "_");

    // TODO: Add support for types like 'java.lang.invoke.TypeDescriptor$OfField<java.lang.Class<? extends PrintStream>[]>'
    // TODO: Add support for types like 'java.lang.@ref.FinalReference<java.lang.Object>' (see C# @ref keyword wrapped)
    // TODO: Convert Java base types like java.lang.Long to C# 'long"
    // TODO: Before generating type-specific method, clean Java class name
    public static string CleanJavaClassName(string className) => className;
}