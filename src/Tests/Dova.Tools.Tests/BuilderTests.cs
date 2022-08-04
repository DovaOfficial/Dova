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
        const string Path = "/tmp/dova_generated/java.base/share/classes/java/lang/String.class.dova";
        
        var javaClassDefinitionModel = DefinitionFileReader.Read(Path);
        var file = new FileInfo(Path);
        var lines = new MethodsBuilder().Build(file, javaClassDefinitionModel).ToList();
        
        Assert.True(lines.Any());
    }

    [Theory]
    [InlineData("/tmp/dova_generated/java.base/share/classes/java/lang/String.class.dova")]
    [InlineData("/tmp/dova_generated/java.sql/share/classes/java/sql/DriverManager.class.dova")]
    public void Should_generate_files(string path)
    {
        var model = DefinitionFileReader.Read(path);
        
        CSharpClassGenerator.Generate(path, model);
    }
}