namespace Dova.Tools.Readers.Models;

public class JavaClassDefinitionModel
{
    public string? Package { get; set; }
    public IEnumerable<string>? Imports { get; set; }
    public IEnumerable<string>? ClassComment { get; set; }
}