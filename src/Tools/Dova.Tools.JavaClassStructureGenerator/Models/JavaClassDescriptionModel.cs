namespace Dova.Tools.JavaClassStructureGenerator.Models;

internal class JavaClassDescriptionModel
{
    public string Type { get; set; }
    public string ParentClass { get; set; }
    public IEnumerable<string> Interfaces { get; set; }
    public IEnumerable<string> ClassComments { get; set; }
    public IEnumerable<JavaConstructorModel> Constructors { get; set; }
}