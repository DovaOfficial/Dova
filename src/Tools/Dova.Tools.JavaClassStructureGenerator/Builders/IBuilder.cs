using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator.Builders;

internal interface IBuilder
{
    IEnumerable<string> Build(FileInfo outputFile, ClassDefinitionModel model, int tabs = 0);
}