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
            Console.WriteLine($"File not found: {tempOutputPathFull}");
            return;
        }

        var javaClassDefinitionModel = Read(tempOutputPathFull);
        
        CSharpClassGenerator.Generate(config.OutputDirectoryPath, javaPackagePath, javaClassDefinitionModel);
    }
    
    private static ClassDefinitionModel Read(string jsonPath)
    {
        var fileContent = File.ReadAllText(jsonPath);
        var model = JsonSerializer.Deserialize<ClassDefinitionModel>(fileContent);
        return model ?? throw new ArgumentException("Cannot read JSON at: " + jsonPath);
    }
}