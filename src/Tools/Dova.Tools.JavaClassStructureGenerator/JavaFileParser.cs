using System.Text.Json;
using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator;

internal static class JavaFileParser
{
    public static ClassDefinitionModel Parse(string jsonPath, FileSystemInfo javaFile)
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