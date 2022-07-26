using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator.Builders;

internal interface IBuilder
{
    IEnumerable<string> Build(ClassDefinitionModel model, int tabs = 0);
}