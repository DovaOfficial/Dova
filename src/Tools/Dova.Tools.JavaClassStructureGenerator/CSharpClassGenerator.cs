using Dova.Tools.JavaClassStructureGenerator.Builders;
using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator;

internal static class CSharpClassGenerator
{
    private static CoreBuilder Builder { get; } = new();
    
    public static void Generate(string outputDirectoryPath, string javaOutputPathFull, ClassDefinitionModel model)
    {
        var outputDirPath = Path.Combine(outputDirectoryPath, javaOutputPathFull);
        
        if (!Directory.Exists(outputDirPath))
        {
            Directory.CreateDirectory(outputDirPath);
        }
        
        var outputFilePath = Path.Combine(outputDirPath, $"{model.ClassDetailsModel.ClassName}.cs");
        var outputFile = new FileInfo(outputFilePath);
        
        Generate(outputFile, model);

        GenerateInnerClasses(outputFile, model);
    }

    public static void Generate(FileInfo outputFile, ClassDefinitionModel model)
    {
        if (outputFile.Exists)
        {
            outputFile.Delete();
        }
        
        outputFile.Create().Close();
        
        var lines = Builder.Build(outputFile, model).ToList();
        
        using (var writer = new StreamWriter(outputFile.FullName))
        {
            foreach (var line in lines)
            {
                writer.WriteLine(line);
            }
        }
    }
    
    private static void GenerateInnerClasses(FileInfo outputFile, ClassDefinitionModel model)
    {
        Parallel.ForEach(model.InnerClassModels, innerClassModel =>
        {
            var outputFileName = outputFile.Name.Split(".")[0];
            var innerClassFileName = $"{outputFileName}_{innerClassModel.ClassDetailsModel.ClassName}.cs";
            var innerClassFullPath = Path.Combine(outputFile.DirectoryName, innerClassFileName);
            var innerClassFile = new FileInfo(innerClassFullPath);
            
            Generate(innerClassFile, innerClassModel);
            
            GenerateInnerClasses(innerClassFile, innerClassModel);
        });
    }
}