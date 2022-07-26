using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator.Builders;

internal class NamespaceBuilder : AbstractBuilder
{
    public override IEnumerable<string> Build(ClassDefinitionModel model, int tabs = 0)
    {
        yield return AppendLine("");
        yield return AppendLine($"namespace Dova.JDK.{model.ClassDetailsModel.PackageName};");
    }
}