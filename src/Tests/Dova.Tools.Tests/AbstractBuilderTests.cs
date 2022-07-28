using Dova.Tools.JavaClassStructureGenerator;
using Dova.Tools.JavaClassStructureGenerator.Builders;
using Xunit;

namespace Dova.Tools.Tests;

public class AbstractBuilderTests
{
    [Theory]
    [InlineData("bool", "Boolean")]
    public void Should_prepare_return_type(string type, string retType)
    {
        var cleaned = AbstractBuilder.GetReturnTypePrefix(JavaCleaner.CleanJavaClassName(type));
        
        Assert.Equal(cleaned, retType);
    }
}