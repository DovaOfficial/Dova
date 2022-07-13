namespace Dova.Tools.JavaClassStructureGenerator.Models;

internal class FieldDefinitionModel
{
    public string Modifiers { get; set; }
    public string ReturnType { get; set; }
    public string FieldName { get; set; }
    public TypeParameterModel TypeParameterModel { get; set; } = new();
}