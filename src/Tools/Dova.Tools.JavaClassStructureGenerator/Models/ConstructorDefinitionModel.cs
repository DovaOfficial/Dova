namespace Dova.Tools.JavaClassStructureGenerator.Models;

internal class ConstructorDefinitionModel
{
    public string Modifiers { get; set; }
    public IEnumerable<TypeParameterModel> TypeParameterModels { get; set; } = new List<TypeParameterModel>();
}