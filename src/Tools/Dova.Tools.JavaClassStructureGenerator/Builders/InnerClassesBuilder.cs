using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator.Builders;

// TODO: Rework inner classes where parent class has generic args
// TODO: Try and exclude inner classes to different files where namespace is build with inner class like: java.util.Separator.OfInt => java.util.separator.OfInt
// TODO: OR add it after the parent class declaration
// TODO: OR add a namespace with capital letter so it won't need to be modified later on
internal class InnerClassesBuilder : AbstractBuilder
{
    private static ClassBuilder Builder { get; } = new();
    
    public override IEnumerable<string> Build(ClassDefinitionModel model, int tabs = 0)
    {
        foreach (var innerClassModel in model.InnerClassModels)
        {
            yield return AppendLine("");
            
            foreach (var line in Builder.Build(innerClassModel, tabs + 1))
                yield return line;
        }
    }
}