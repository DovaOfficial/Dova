using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator.Builders;

internal class InterfacesBuilder : AbstractBuilder
{
    public override IEnumerable<string> Build(ClassDefinitionModel model, int tabs = 0)
    {
        if (model.InterfaceModels.Count == 0)
        {
            yield break;
        }
        
        foreach (var interfaceModel in model.InterfaceModels)
        {
            var cleanedName = CleanJavaClassName(interfaceModel.Name);
            yield return AppendLine($", {cleanedName}", tabs + 1);
        }
    }
}