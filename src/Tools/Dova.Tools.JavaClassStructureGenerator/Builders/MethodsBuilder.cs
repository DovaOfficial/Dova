using Dova.Common;
using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator.Builders;

// TODO: Generate only signatures for interfaces
// TODO: Add support for methods with names like 'lambda$indent$1'
// TODO: Needs to firstly call IJavaRuntime method and then return wrapped type.
internal class MethodsBuilder : AbstractBuilder
{
    public override IEnumerable<string> Build(ClassDefinitionModel model, int tabs = 0)
    {
        if (model.MethodModels.Count > 0)
        {
            yield return AppendLine("");
        }
        
        for (var index = 0; index < model.MethodModels.Count; ++index)
        {
            var method = model.MethodModels[index];
            
            var modifierPrefix = method.IsStatic
                ? "static "
                : method.HasParent
                    ? "override "
                    : "virtual ";
            
            var combinedParameters = GetCombinedParameters(method.ParameterModels);
            
            var methodSignature = $"public {modifierPrefix}{GetReturnType(method.ReturnType)} {method.Name}({combinedParameters})";
            
            var staticMethodPrefix = method.IsStatic
                ? "Static"
                : "";
            
            var targetObjPtr = method.IsStatic
                ? ClassRefPtrStr
                : nameof(JavaObject.CurrentRefPtr);
            
            var combinedParameterNames = GetCombinedParameterNames(method.ParameterModels);
            
            if (!string.IsNullOrWhiteSpace(combinedParameterNames))
            {
                combinedParameterNames = ", " + combinedParameterNames;
            }
            
            var methodCallback = $"DovaJvm.Vm.Runtime.Call{staticMethodPrefix}{GetReturnTypePrefix(method.ReturnType)}MethodA{GetGenericType(method.ReturnType)}({targetObjPtr}, {MethodPtrsStr}[{index}]{combinedParameterNames});";
            
            yield return AppendLine($"[{nameof(JniSignatureAttribute)}(\"{method.Signature}\", \"{method.Modifiers}\")]", tabs);
            yield return AppendLine($"{methodSignature} => {methodCallback}", tabs);
            
            if (index != model.MethodModels.Count - 1)
            {
                AppendLine("");
            }
        }
    }
}