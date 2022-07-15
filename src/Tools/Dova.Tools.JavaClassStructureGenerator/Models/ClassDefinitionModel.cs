namespace Dova.Tools.JavaClassStructureGenerator.Models;

internal class ClassDefinitionModel
{
    public ClassDetailsDefinitionModel ClassDetailsModel { get; set; }
    public ClassElementDefinitionModel BaseClassModel { get; set; }
    public IReadOnlyList<ClassElementDefinitionModel> InterfaceModels { get; set; } = new List<ClassElementDefinitionModel>();
    public IReadOnlyList<ClassElementDefinitionModel> ConstructorModels { get; set; } = new List<ClassElementDefinitionModel>();
    public IReadOnlyList<ClassElementDefinitionModel> FieldModels { get; set; } = new List<ClassElementDefinitionModel>();
    public IReadOnlyList<ClassElementDefinitionModel> MethodModels { get; set; } = new List<ClassElementDefinitionModel>();
    public IReadOnlyList<ClassDefinitionModel> InnerClassModels { get; set; } = new List<ClassDefinitionModel>();
}