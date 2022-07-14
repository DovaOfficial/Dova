using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator;

internal class CSharpClassGenerator
{
    private string OutputDirectoryPath { get; }

    public CSharpClassGenerator(string outputDirectoryPath)
    {
        OutputDirectoryPath = outputDirectoryPath;
    }

    public void Generate(string javaOutputPathFull, ClassDefinitionModel model)
    {
        throw new NotImplementedException();
    }
}