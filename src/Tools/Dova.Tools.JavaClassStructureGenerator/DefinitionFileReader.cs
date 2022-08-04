using System.Text.Json;
using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator;

internal static class DefinitionFileReader
{
    public static ClassDefinitionModel Read(string path)
    {
        var fileContent = File.ReadAllText(path);
        var model = JsonSerializer.Deserialize<ClassDefinitionModel>(fileContent);
        
        return model ?? throw new ArgumentException("Cannot read definition file at: " + path);
    }
}