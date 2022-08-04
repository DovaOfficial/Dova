using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator.Builders;

internal class NamespaceBuilder : AbstractBuilder
{
    public override IEnumerable<string> Build(FileInfo outputFile, ClassDefinitionModel model, int tabs = 0)
    {
        yield return AppendLine("");

        var cleanedNamespace = DefinitionCleaner.CleanJavaClassName(model.ClassDetailsModel.PackageName);
        var fullNamespace = $"Dova.JDK.{cleanedNamespace}";

        yield return AppendLine($"namespace {fullNamespace};");
    }
}