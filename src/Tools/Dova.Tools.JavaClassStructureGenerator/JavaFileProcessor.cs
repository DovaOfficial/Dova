using System.Text.Json;
using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator;

internal class JavaFileProcessor
{
    public void Run(GeneratorConfiguration config, FileInfo javaFile)
    {
        var reader = new JavaFileReader(javaFile);
        
        var javaClassFullName = $"{reader.JavaPackage}.{reader.JavaClassName}";
        var javaPackagePath = Path.Combine(reader.JavaPackage.Split("."));
        var tempOutputPathFull = Path.Combine(config.TempDirPath, javaPackagePath, $"{reader.JavaClassName}.json");
        
        JavaClassDefinitionGenerator.Generate(config.JavaClassDefinitionGeneratorPath, tempOutputPathFull, javaClassFullName);
        
        if (!File.Exists(tempOutputPathFull))
        {
            Console.WriteLine($"File not found: '{tempOutputPathFull}' based on '{javaFile.FullName}'");
            return;
        }
        
        // Let it wait for file to be saved
        Thread.Sleep(30);

        var javaClassDefinitionModel = Read(tempOutputPathFull, javaFile);
        
        CSharpClassGenerator.Generate(config.OutputDirectoryPath, javaPackagePath, javaClassDefinitionModel);
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