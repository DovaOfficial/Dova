using Dova.Tools.JavaClassStructureGenerator.Builders;
using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator.Core;

internal static class CSharpClassGenerator
{
    private static CoreBuilder Builder { get; } = new();
    private static object key = new();

    public static void Generate(string outputPathFull, ClassDefinitionModel model)
    {
        var outputClassFile = new FileInfo(outputPathFull);

        if (!Directory.Exists(outputClassFile.DirectoryName))
        {
            Directory.CreateDirectory(outputClassFile.DirectoryName);
        }

        var fileName = outputClassFile.Name.Split(".")[0];

        // Make sure the file and class names are equal
        if (!model.ClassDetailsModel.ClassName.Equals(fileName))
        {
            model.ClassDetailsModel.ClassName = fileName;
        }

        Generate(outputClassFile, model);
    }

    public static void Generate(FileInfo outputFile, ClassDefinitionModel model)
    {
        if (outputFile.Exists)
        {
            outputFile.Delete();
        }
        
        var lines = Builder.Build(outputFile, model).ToList();

        lock (key)
        {
            using (var writer = new StreamWriter(outputFile.FullName))
            {
                foreach (var line in lines)
                {
                    writer.WriteLine(line);
                }
            }
        }
    }
}