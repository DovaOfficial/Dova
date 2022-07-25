using System.Text.Json;
using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator;

internal static class JavaFileProcessor
{
    public static void Run(GeneratorConfiguration config, FileInfo javaFile)
    {
        var reader = new JavaFileReader(javaFile);
        
        var javaClassFullName = $"{reader.JavaPackage}.{reader.JavaClassName}";
        var javaFilePostfixPath = javaFile.FullName.Replace(config.SourcesDirectoryPath, "");
        var javaFileRelativePath = Path.GetRelativePath(Path.GetPathRoot(javaFilePostfixPath), javaFilePostfixPath);
        var tempOutputPathFull = Path.Combine(config.TempDirPath, javaFileRelativePath + ".json");
        var javaFileRelativePathWithoutFileName = javaFileRelativePath.Replace(javaFile.Name, "");
        
        JavaClassDefinitionGenerator.Generate(config.JavaClassDefinitionGeneratorPath, tempOutputPathFull, javaClassFullName);
        
        if (!File.Exists(tempOutputPathFull))
        {
            Console.WriteLine($"File not found: '{tempOutputPathFull}' based on '{javaFile.FullName}'");
            return;
        }
        
        // Let it wait for file to be saved
        Thread.Sleep(30);

        var javaClassDefinitionModel = Read(tempOutputPathFull, javaFile);
        
        CSharpClassGenerator.Generate(config.OutputDirectoryPath, javaFileRelativePathWithoutFileName, javaClassDefinitionModel);
    }
    
    private static ClassDefinitionModel Read(string jsonPath, FileInfo javaFile)
    {
        var fileContent = File.ReadAllText(jsonPath);

        if (string.IsNullOrWhiteSpace(fileContent))
        {
            throw new ArgumentException($"Empty JSON was generated for file: {javaFile.FullName}");
        }
        
        var model = JsonSerializer.Deserialize<ClassDefinitionModel>(fileContent);
        return model ?? throw new ArgumentException("Cannot read JSON at: " + jsonPath);
    }
}