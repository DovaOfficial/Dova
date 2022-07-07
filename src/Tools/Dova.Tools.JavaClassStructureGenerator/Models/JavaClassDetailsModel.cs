namespace Dova.Tools.JavaClassStructureGenerator.Models;

internal class JavaClassDetailsModel
{
    public HttpClient Client { get; set; } = new();
    public string JavaModuleUrlPostfix { get; set; }
    public string JavaModuleName { get; set; }
    public string JavaPackageUrlPostfix { get; set; }
    public string JavaPackageName { get; set; }
    public string JavaClassUrlPostfix { get; set; }
    public string JavaClassName { get; set; }
    public string JavaClassDetailsUrl { get; set; }
    public string JavaClassDetails { get; set; }
}