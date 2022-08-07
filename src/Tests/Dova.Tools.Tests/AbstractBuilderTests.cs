using Dova.Tools.JavaClassStructureGenerator.Builders;
using Dova.Tools.JavaClassStructureGenerator.Common;
using Xunit;

namespace Dova.Tools.Tests;

public class AbstractBuilderTests
{
    [Theory]
    [InlineData("bool", "Boolean")]
    public void Should_prepare_return_type(string type, string retType)
    {
        var cleaned = AbstractBuilder.GetReturnTypePrefix(DefinitionCleaner.CleanJavaClassName(type));
        
        Assert.Equal(cleaned, retType);
    }
}