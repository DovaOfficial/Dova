using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator.Generators;

internal class JavaClassDescriptionGenerator
{
    private readonly JavaClassDetailsModel _detailsModel;
    private readonly JavaClassDescriptionModel _descriptionModel;

    public JavaClassDescriptionGenerator(JavaClassDetailsModel detailsModel, JavaClassDescriptionModel descriptionModel)
    {
        _detailsModel = detailsModel;
        _descriptionModel = descriptionModel;
    }

    public void Run()
    {
        throw new NotImplementedException();
    }
}