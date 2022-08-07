using System.Text.Json;
using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator.Core;

internal static class DefinitionFileReader
{
    public static ClassDefinitionModel Read(string path)
    {
        var fileContent = File.ReadAllText(path);

        if (string.IsNullOrEmpty(fileContent))
        {
            Console.WriteLine($"No file content found in: {path}");
            return null;
        }
        
        var model = JsonSerializer.Deserialize<ClassDefinitionModel>(fileContent);
        
        return model ?? throw new ArgumentException("Cannot read definition file at: " + path);
    }
}