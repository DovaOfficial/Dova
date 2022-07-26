using Dova.Common;
using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator.Builders;

internal class ClassSignatureBuilder : AbstractBuilder
{
    public override IEnumerable<string> Build(ClassDefinitionModel model, int tabs = 0)
    {
        yield return AppendLine("");
        
        var type = model.ClassDetailsModel.IsInterface
            ? "interface "
            : "class ";
        
        var genericTypes = CombineGenericTypes(model.ClassDetailsModel.TypeParameterModels);
        
        yield return AppendLine($"[{nameof(JniSignatureAttribute)}(\"{model.ClassDetailsModel.Signature}\", \"{model.ClassDetailsModel.Modifiers}\")]", tabs);
        yield return AppendLine($"public partial {type}{model.ClassDetailsModel.ClassName}{genericTypes}", tabs);
    }
}