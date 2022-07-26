using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator.Builders;

internal class ClassBodyBuilder : AbstractBuilder
{
    private static IEnumerable<IBuilder> Builders = new List<IBuilder>
    {
        new JniReferencesBuilder(),
        new PropertiesBuilder(),
        new ConstructorsBuilder(),
        new ExtraMethodsBuilder(),
        new MethodsBuilder(),
        new InnerClassesBuilder(),
    };
    
    public override IEnumerable<string> Build(ClassDefinitionModel model, int tabs = 0)
    {
        yield return AppendLine("{", tabs);

        foreach (var line in Builders.SelectMany(builder => builder.Build(model, tabs + 1)))
        {
            yield return line;
        }
        
        yield return AppendLine("}", tabs);
    }
}