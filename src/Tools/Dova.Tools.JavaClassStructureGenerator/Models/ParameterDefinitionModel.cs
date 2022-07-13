namespace Dova.Tools.JavaClassStructureGenerator.Models;

internal class ParameterDefinitionModel
{
    public string Type { get; set; }
    public string Name { get; set; }
    public IEnumerable<TypeParameterModel> TypeParameterModels { get; set; } = new List<TypeParameterModel>();
}