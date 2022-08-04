using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator.Builders;

internal class GenericBoundsBuilder : AbstractBuilder
{
    public override IEnumerable<string> Build(FileInfo outputFile, ClassDefinitionModel model, int tabs = 0)
    {
        foreach (var typeParam in model.ClassDetailsModel.TypeParameterModels)
        {
            var bounds = typeParam.BoundModels
                .Select(x => CleanJavaClassName(x.Name))
                .Select(x => x.Equals(JavaObjectFullName) ? GetDefaultBounds() : x)
                .ToList();
        
            var totalBounds = string.Join(", ", bounds);
        
            yield return AppendLine($"where {typeParam.VariableName} : {totalBounds}", tabs + 1);
        }
    }
}