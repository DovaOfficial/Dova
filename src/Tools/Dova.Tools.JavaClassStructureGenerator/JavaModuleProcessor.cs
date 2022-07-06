namespace Dova.Tools.JavaClassStructureGenerator;

internal class JavaModuleProcessor
{
    private readonly JavaClassDetailsModel _model;

    public JavaModuleProcessor(JavaClassDetailsModel model)
    {
        _model = model;
    }

    public void Run()
    {
        var javaModuleUrl = $"{UrlUtils.UrlPrefix}{_model.JavaModuleUrlPostfix}";
        var javaPackageResponse = _model.Client.GetStringAsync(javaModuleUrl).Result;
        
        var javaPackageResponses = javaPackageResponse
            .Split("<a href=\"")
            .Where(x => x.Contains("package-summary.html"))
            .ToList();

        foreach (var javaPackageDetailString in javaPackageResponses)
        {
            var javaPackageDetailStringParts = javaPackageDetailString.Split("\">");
            
            _model.JavaPackageUrlPostfix = javaPackageDetailStringParts[0];
            _model.JavaPackageName = javaPackageDetailStringParts[1].Split("</a>")[0];

            var processor = new JavaPackageProcessor(_model);

            processor.Run();
        }
    }
}