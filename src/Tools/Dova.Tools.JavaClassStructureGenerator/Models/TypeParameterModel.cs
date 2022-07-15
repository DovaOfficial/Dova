namespace Dova.Tools.JavaClassStructureGenerator.Models;

internal class TypeParameterModel
{
    public string VariableName { get; set; }
    public string TypeName { get; set; }
    public IReadOnlyList<BoundDefinitionModel> BoundModels { get; set; } = new List<BoundDefinitionModel>();
}