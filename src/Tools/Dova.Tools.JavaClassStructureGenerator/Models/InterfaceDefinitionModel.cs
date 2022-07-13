namespace Dova.Tools.JavaClassStructureGenerator.Models;

internal class InterfaceDefinitionModel
{
    public string TypeName { get; set; }
    public IEnumerable<TypeParameterModel> TypeParameterModels { get; set; } = new List<TypeParameterModel>();
}