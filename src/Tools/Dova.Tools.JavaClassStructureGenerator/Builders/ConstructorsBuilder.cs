using Dova.Common;
using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator.Builders;

internal class ConstructorsBuilder : AbstractBuilder
{
    public override IEnumerable<string> Build(FileInfo outputFile, ClassDefinitionModel model, int tabs = 0)
    {
        if (model.ClassDetailsModel.IsInterface)
        {
            yield break;
        }
        
        yield return AppendLine("");
        yield return AppendLine($"[{nameof(JniSignatureAttribute)}(\"({typeof(IntPtr).FullName})V\", \"public\")]", tabs);
        yield return AppendLine($"public {model.ClassDetailsModel.ClassName}(IntPtr currentRefPtr) : base(currentRefPtr)", tabs);
        yield return AppendLine("{", tabs);
        yield return AppendLine("}", tabs);
        
        for (var index = 0; index < model.ConstructorModels.Count; ++index)
        {
            yield return AppendLine("");
            
            var constructorModel = model.ConstructorModels[index];
            var combinedParameters = GetCombinedParameters(constructorModel.ParameterModels);
            var combinedParameterNames = GetCombinedParameterNames(constructorModel.ParameterModels);
        
            if (!string.IsNullOrWhiteSpace(combinedParameterNames))
            {
                combinedParameterNames = ", " + combinedParameterNames;
            }
        
            yield return AppendLine($"[{nameof(JniSignatureAttribute)}(\"{constructorModel.Signature}\", \"{constructorModel.Modifiers}\")]", tabs);
            yield return AppendLine($"public {model.ClassDetailsModel.ClassName}({combinedParameters}) : base(DovaJvm.Vm.Runtime.NewObjectA({ClassRefPtrStr}, {ConstructorPtrsStr}[{index}]{combinedParameterNames}))", tabs);
            yield return AppendLine("{", tabs);
            yield return AppendLine("}", tabs);
        }
    }
}