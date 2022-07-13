namespace Dova.Tools.JavaClassStructureGenerator.Models;

internal class MethodDefinitionModel
{
    public string Modifiers { get; set; }
    public string ReturnType { get; set; }
    public string MethodName { get; set; }
    public IEnumerable<ParameterDefinitionModel> ParameterModels { get; set; } = new List<ParameterDefinitionModel>();
    public IEnumerable<TypeParameterModel> TypeParameterModels { get; set; } = new List<TypeParameterModel>();
}