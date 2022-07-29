using System.IO;
using System.Linq;
using Dova.Tools.JavaClassStructureGenerator;
using Dova.Tools.JavaClassStructureGenerator.Builders;
using Xunit;

namespace Dova.Tools.Tests;

public class BuilderTests
{
    [Fact]
    public void Should_parse_methods()
    {
        const string Path = "/tmp/java.base/share/classes/java/lang/String.java.json";
        
        var javaClassDefinitionModel = JavaFileParser.Parse(Path, new FileInfo(Path));
        var lines = new MethodsBuilder().Build(javaClassDefinitionModel).ToList();
        
        Assert.True(lines.Any());
    }
}