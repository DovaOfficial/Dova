using Dova.Common;
using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator.Builders;

// TODO: Add support for methods with names like 'lambda$indent$1'
internal class MethodsBuilder : AbstractBuilder
{
    public override IEnumerable<string> Build(ClassDefinitionModel model, int tabs = 0)
    {
        for (var index = 0; index < model.MethodModels.Count; ++index)
        {
            yield return AppendLine("");
            
            var method = model.MethodModels[index];
            
            var modifierPrefix = method.IsStatic 
                ? "static " 
                : method.HasParent 
                    ? "override " 
                    : "virtual ";
            
            var combinedParameters = GetCombinedParameters(method.ParameterModels);

            var methodModifier = model.ClassDetailsModel.IsInterface
                ? string.Empty
                : "public ";

            var methodPostfix = model.ClassDetailsModel.IsInterface
                ? ";"
                : string.Empty;

            yield return AppendLine($"[{nameof(JniSignatureAttribute)}(\"{method.Signature}\", \"{method.Modifiers}\")]", tabs);
            yield return AppendLine($"{methodModifier}{modifierPrefix}{GetReturnType(method.ReturnType)} {method.Name}({combinedParameters}){methodPostfix}", tabs);

            if (model.ClassDetailsModel.IsInterface)
            {
                continue;
            }
            
            yield return AppendLine("{", tabs);

            var returnTypePrefix = GetReturnTypePrefix(method.ReturnType);
            
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

            if (returnTypePrefix.ToLower().Equals("void"))
            {
                yield return AppendLine(methodCallback, tabs + 1);
            }
            else
            {
                yield return AppendLine($"var ret = {methodCallback}", tabs + 1);
                yield return AppendLine(method.ReturnType.Contains(".") ? $"return new {CleanJavaClassName(method.ReturnType)}(ret);" : $"return ret;", tabs + 1);
            }
            
            yield return AppendLine("}", tabs);
        }
    }
}