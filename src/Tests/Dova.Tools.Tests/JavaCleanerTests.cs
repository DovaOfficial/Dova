using Dova.Tools.JavaClassStructureGenerator;
using Xunit;

namespace Dova.Tools.Tests;

public class JavaCleanerTests
{
    [Theory]
    [InlineData("", "")]
    [InlineData("byte", "byte")]
    [InlineData("byte[]", "JavaArray<byte>")]
    [InlineData("byte[][]", "JavaArray<JavaArray<byte>>")]
    [InlineData("byte[][][]", "JavaArray<JavaArray<JavaArray<byte>>>")]
    [InlineData("java.lang.Byte", "java.lang.Byte")]
    [InlineData("java.lang.Byte[]", "JavaArray<java.lang.Byte>")]
    [InlineData("java.lang.Byte[][]", "JavaArray<JavaArray<java.lang.Byte>>")]
    [InlineData("java.lang.Byte[][][]", "JavaArray<JavaArray<JavaArray<java.lang.Byte>>>")]
    [InlineData("boolean", "bool")]
    [InlineData("java.lang.Class<?>", "java.lang.Class<java.lang.Object>")]
    [InlineData("java.lang.Class<? extends java.util.PrintStream>", "java.lang.Class<java.util.PrintStream>")]
    [InlineData("java.util.Vector<java.util.Vector<java.text.EntryPair>>", "java.util.Vector<java.util.Vector<java.text.EntryPair>>")]
    [InlineData("java.util.Vector<java.util.Vector<java.text.EntryPair>>[]", "JavaArray<java.util.Vector<java.util.Vector<java.text.EntryPair>>>")]
    [InlineData("java.util.Class<java.util.Class<java.lang.Object>[]>", "java.util.Class<JavaArray<java.util.Class<java.lang.Object>>>")]
    [InlineData("java.util.Class$InnerClass<java.util.Class<java.util.Class$InnerClass<?>>[]>[]", "JavaArray<java.util.Class.InnerClass<JavaArray<java.util.Class<java.util.Class.InnerClass<java.lang.Object>>>>>")]
    public void Should_clean_Java_class_name(string className, string validClassName)
    {
        var cleanedClassName = JavaCleaner.CleanJavaClassName(className);
        
        Assert.Equal(cleanedClassName, validClassName);
    }
}