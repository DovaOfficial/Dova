namespace Dova.Tools.JavaClassStructureGenerator.Models;

internal class ClassDefinitionModel
{
    public ClassDetailsDefinitionModel ClassDetailsModel { get; set; }
    public BaseClassDefinitionModel BaseClassModel { get; set; }
    public IEnumerable<InterfaceDefinitionModel> InterfaceModels { get; set; } = new List<InterfaceDefinitionModel>();
    public IEnumerable<ConstructorDefinitionModel> ConstructorModels { get; set; } = new List<ConstructorDefinitionModel>();
    public IEnumerable<FieldDefinitionModel> FieldModels { get; set; } = new List<FieldDefinitionModel>();
    public IEnumerable<MethodDefinitionModel> MethodModels { get; set; } = new List<MethodDefinitionModel>();
    public IEnumerable<ClassDefinitionModel> InnerClassModels { get; set; } = new List<ClassDefinitionModel>();
}