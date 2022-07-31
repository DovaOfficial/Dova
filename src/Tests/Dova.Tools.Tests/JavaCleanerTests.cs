using Dova.Tools.JavaClassStructureGenerator;
using Xunit;

namespace Dova.Tools.Tests;

public class JavaCleanerTests
{
    [Theory]
    [InlineData("", "")]
    [InlineData(".event;", ".@event;")]
    [InlineData(".test.event;", ".test.@event;")]
    [InlineData(".test.eventTest;", ".test.@eventTest;")] // Technically this don't need to be changed but for the simplicity it is
    [InlineData("namespace com.test.event;", "namespace com.test.@event;")]
    [InlineData(".event.", ".@event.")]
    [InlineData("TesteventTest", "TesteventTest")]
    [InlineData("void", "void")]
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
    [InlineData("java.util.Class$InnerClass<java.util.Class<java.util.Class$InnerClass<?>>[]>[]", "JavaArray<java.util.InnerClass<JavaArray<java.util.Class<java.util.InnerClass<java.lang.Object>>>>>")]
    [InlineData("java.util.Spliterator$OfInt<java.util.Function<int>, java.util.List$ArrayList<?>, java.util.Class$InnerClass<java.lang.Integer>>", "java.util.OfInt<java.util.Function<int>, java.util.ArrayList<java.lang.Object>, java.util.InnerClass<java.lang.Integer>>")]
    [InlineData("java.util.Function2<java.util.List$ArrayList<int>, java.util.List$ArrayList<double>>", "java.util.Function2<java.util.ArrayList<int>, java.util.ArrayList<double>>")]
    public void Should_clean_Java_class_name(string str, string validStr)
    {
        var cleanedStr = JavaCleaner.CleanJavaClassName(str);
        
        Assert.Equal(cleanedStr, validStr);
    }
}