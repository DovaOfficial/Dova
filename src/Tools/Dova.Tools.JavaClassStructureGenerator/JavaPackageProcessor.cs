namespace Dova.Tools.JavaClassStructureGenerator;

internal class JavaPackageProcessor
{
    private readonly JavaClassDetailsModel _model;

    public JavaPackageProcessor(JavaClassDetailsModel model)
    {
        _model = model;
    }

    public void Run()
    {
        var javaClassUrl = $"{UrlUtils.UrlPrefix}{_model.JavaModuleName}/{_model.JavaPackageUrlPostfix}";
        var javaClassResponse = _model.Client.GetStringAsync(javaClassUrl).Result;

        var javaClassResponses = javaClassResponse
            .Split("<a href=\"")
            .Where(x => x.Contains("title=\"class in") && !x.StartsWith(".."))
            .ToList();

        foreach (var javaClassDetailsString in javaClassResponses)
        {
            _model.JavaClassUrlPostfix = javaClassDetailsString.Split("\"")[0];
            _model.JavaClassName = javaClassDetailsString.Split("\">")[1].Split("</a>")[0];

            var processor = new JavaClassProcessor(_model);

            processor.Run();
        }
    }
}