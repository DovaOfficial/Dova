namespace Dova.Tools.JavaClassStructureGenerator.Models;

internal class ClassDetailsDefinitionModel
{
    public string PackageName { get; set; }
    public string ClassName { get; set; }
    public bool IsEnum { get; set; }
    public string Modifiers { get; set; }
    public IEnumerable<TypeParameterModel> TypeParameterModels { get; set; } = new List<TypeParameterModel>();
    public string Signature { get; set; }
}