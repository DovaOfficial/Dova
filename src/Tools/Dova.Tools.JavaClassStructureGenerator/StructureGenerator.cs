namespace Dova.Tools.JavaClassStructureGenerator;

internal class StructureGenerator
{
    private readonly JavaClassDetailsModel _model;

    public StructureGenerator(JavaClassDetailsModel model)
    {
        _model = model;
    }

    public void Run()
    {
        var mainWebsiteResponse = _model.Client.GetStringAsync(UrlUtils.MainUrl).Result;
        var mainWebsiteResponseUrls = mainWebsiteResponse.Split("<a href=\"");
        
        var javaUrls = mainWebsiteResponseUrls
            .Where(x => x.Contains("module-summary.html"))
            .ToList();

        foreach (var javaUrl in javaUrls)
        {
            var javaUrlParts = javaUrl.Split("\">");
            
            _model.JavaModuleUrlPostfix = javaUrlParts[0];
            _model.JavaModuleName = javaUrlParts[1].Split("</a>")[0];
            
            var processor = new JavaModuleProcessor(_model);

            processor.Run();
        }
    }
}