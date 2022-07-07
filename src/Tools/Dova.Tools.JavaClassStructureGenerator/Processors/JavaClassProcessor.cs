using Dova.Tools.JavaClassStructureGenerator.Generators;
using Dova.Tools.JavaClassStructureGenerator.Models;
using Dova.Tools.JavaClassStructureGenerator.Parsers;

namespace Dova.Tools.JavaClassStructureGenerator.Processors;

internal class JavaClassProcessor
{
    private readonly JavaClassDetailsModel _model;

    public JavaClassProcessor(JavaClassDetailsModel model)
    {
        _model = model;
    }

    public void Run()
    {
        _model.JavaClassDetailsUrl = $"{UrlUtils.UrlPrefix}{_model.JavaModuleName}/{_model.JavaPackageName.Replace(".", "/")}/{_model.JavaClassUrlPostfix}";
        _model.JavaClassDetails =  _model.Client.GetStringAsync(_model.JavaClassDetailsUrl).Result;

        var javaClassDescription = JavaClassDetailsParser.Parse(_model);

        var generator = new JavaClassDescriptionGenerator(_model, javaClassDescription);

        generator.Run();
    }
}