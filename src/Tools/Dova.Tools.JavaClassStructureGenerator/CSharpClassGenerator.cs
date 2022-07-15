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
        var outputDirPath = Path.Combine(OutputDirectoryPath, javaOutputPathFull);
        
        if (!Directory.Exists(outputDirPath))
        {
            Directory.CreateDirectory(outputDirPath);
        }
        
        var outputFilePath = Path.Combine(outputDirPath, $"{model.ClassDetailsModel.ClassName}.cs");
        var outputFile = new FileInfo(outputFilePath);

        if (outputFile.Exists)
        {
            outputFile.Delete();
        }
        
        outputFile.Create().Close();
        
        var builder = new CSharpClassBuilder(model);
        var lines = builder.Build();

        using (var writer = new StreamWriter(outputFile.FullName))
        {
            foreach (var line in lines)
            {
                writer.WriteLine(line);
            }
        }
    }
}