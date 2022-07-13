using System.Text.Json;
using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator;

internal static class JavaClassDefinitionReader
{
    public static ClassDefinitionModel Read(string tempOutputPathFull)
    {
        var fileContent = File.ReadAllText(tempOutputPathFull);
        var model = JsonSerializer.Deserialize<ClassDefinitionModel>(fileContent);
        return model;
    }
}