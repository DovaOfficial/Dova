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
        var outputDirPath = $"{OutputDirectoryPath}/{javaOutputPathFull}";
        
        if (!Directory.Exists(outputDirPath))
        {
            Directory.CreateDirectory(outputDirPath);
        }
        
        var outputFilePath = $"{outputDirPath}/{model.ClassDetailsModel.ClassName}.cs";
        var outputFile = new FileInfo(outputFilePath);

        if (outputFile.Exists)
        {
            outputFile.Delete();
        }
        
        outputFile.Create().Close();
        
        using (var writer = new StreamWriter(outputFile.FullName))
        {
            var lines = new List<string>();
            
            CSharpClassBuilder.Build(lines, model);
            
            lines.ForEach(writer.WriteLine);
        }
    }
}