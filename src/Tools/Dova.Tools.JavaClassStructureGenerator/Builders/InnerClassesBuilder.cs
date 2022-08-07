using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator.Builders;

internal class InnerClassesBuilder : AbstractBuilder
{
    private static ClassBuilder Builder { get; } = new();

    public override IEnumerable<string> Build(FileInfo outputFile, ClassDefinitionModel model, int tabs = 0)
    {
        foreach (var innerClassModel in model.InnerClassModels)
        {
            yield return AppendLine("");
            
            foreach (var line in Builder.Build(outputFile, innerClassModel, tabs + 1))
                yield return line;
        }
    }
}