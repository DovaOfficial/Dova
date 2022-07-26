using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator.Builders;

internal class ClassBuilder : AbstractBuilder
{
    private static IEnumerable<IBuilder> Builders = new List<IBuilder>
    {
        new ClassSignatureBuilder(),
        new BaseClassBuilder(),
        new InterfacesBuilder(),
        new GenericBoundsBuilder(),
        new ClassBodyBuilder(),
    };
    
    public override IEnumerable<string> Build(ClassDefinitionModel model, int tabs = 0) => 
        Builders.SelectMany(builder => builder.Build(model, tabs));
}