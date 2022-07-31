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

        var file = new FileInfo(Path);
        var javaClassDefinitionModel = JavaFileParser.Parse(Path, file);
        var lines = new MethodsBuilder().Build(file, javaClassDefinitionModel).ToList();
        
        Assert.True(lines.Any());
    }

    [Theory]
    [InlineData("/tmp/java.base/share/classes/java/lang/String.java.json")]
    [InlineData("/tmp/java.sql/share/classes/java/sql/DriverManager.java.json")]
    public void Should_generate_files(string path)
    {
        var file = new FileInfo(path);
        var model = JavaFileParser.Parse(path, file);
        
        CSharpClassGenerator.Generate("/tmp", "/tmp", model);
    }
}