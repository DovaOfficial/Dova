using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator.Builders;

internal class CoreBuilder : AbstractBuilder
{
    private static IEnumerable<IBuilder> Builders = new List<IBuilder>
    {
        new UsingsBuilder(),
        new NamespaceBuilder(),
        new ClassBuilder(),
    };
    
    public override IEnumerable<string> Build(FileInfo outputFile, ClassDefinitionModel model, int tabs = 0) => 
        Builders.SelectMany(builder => builder.Build(outputFile, model, tabs));
}