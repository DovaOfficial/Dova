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

    [Fact]
    public void Should_generate_files()
    {
        const string Path = "/tmp/java.base/share/classes/java/lang/String.java.json";

        var file = new FileInfo(Path);
        var model = JavaFileParser.Parse(Path, file);
        
        CSharpClassGenerator.Generate("/tmp", "/tmp", model);
    }
}