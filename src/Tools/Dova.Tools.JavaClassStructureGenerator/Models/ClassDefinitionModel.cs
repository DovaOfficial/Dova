namespace Dova.Tools.JavaClassStructureGenerator.Models;

internal class ClassDefinitionModel
{
    public ClassDetailsDefinitionModel ClassDetailsModel { get; set; }
    public ClassElementDefinitionModel BaseClassModel { get; set; }
    public IEnumerable<ClassElementDefinitionModel> InterfaceModels { get; set; } = new List<ClassElementDefinitionModel>();
    public IEnumerable<ClassElementDefinitionModel> ConstructorModels { get; set; } = new List<ClassElementDefinitionModel>();
    public IEnumerable<ClassElementDefinitionModel> FieldModels { get; set; } = new List<ClassElementDefinitionModel>();
    public IEnumerable<ClassElementDefinitionModel> MethodModels { get; set; } = new List<ClassElementDefinitionModel>();
    public IEnumerable<ClassDefinitionModel> InnerClassModels { get; set; } = new List<ClassDefinitionModel>();
}