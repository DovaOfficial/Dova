using Dova.Common;
using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator.Builders;

internal class ExtraMethodsBuilder : AbstractBuilder
{
    public override IEnumerable<string> Build(ClassDefinitionModel model, int tabs = 0)
    {
        if (model.ClassDetailsModel.IsInterface)
        {
            yield break;
        }
        
        yield return AppendLine("");
        yield return AppendLine($"public override string {nameof(JavaObject.GetJavaClassSignature)}() => \"{model.ClassDetailsModel.Signature}\";", tabs);
        yield return AppendLine($"public override IntPtr {nameof(JavaObject.GetJavaClassRaw)}() => {ClassPtrStr};", tabs);
        yield return AppendLine($"public override IntPtr {nameof(JavaObject.GetJavaClassRefRaw)}() => {ClassRefPtrStr};", tabs);
    }
}