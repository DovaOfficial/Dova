using Dova.Common;
using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator.Builders;

internal class MethodsBuilder : AbstractBuilder
{
    public override IEnumerable<string> Build(FileInfo outputFile, ClassDefinitionModel model, int tabs = 0)
    {
        for (var index = 0; index < model.MethodModels.Count; ++index)
        {
            yield return AppendLine("");
            
            var method = model.MethodModels[index];
            
            var modifierPrefix = model.ClassDetailsModel.IsInterface 
                ? string.Empty 
                : method.IsStatic 
                    ? "static " 
                    : method.HasParent 
                        ? "override " 
                        : "virtual ";
            
            var combinedParameters = GetCombinedParameters(method.ParameterModels);

            var methodModifier = model.ClassDetailsModel.IsInterface
                ? string.Empty
                : "public ";

            var returnType = CleanJavaClassName(method.ReturnType);

            var cleanedMethodName = JavaCleaner.CleanJavaMethodName(method.Name);
            var genericParams = GetGenericParameters(method.ParameterModels);

            yield return AppendLine($"[{nameof(JniSignatureAttribute)}(\"{method.Signature}\", \"{method.Modifiers}\")]", tabs);
            yield return AppendLine($"{methodModifier}{modifierPrefix}{returnType} {cleanedMethodName}{genericParams}({combinedParameters})", tabs);

            yield return AppendLine("{", tabs);

            var returnTypePrefix = GetReturnTypePrefix(returnType);
            
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
            
            var methodCallback = $"DovaJvm.Vm.Runtime.Call{staticMethodPrefix}{returnTypePrefix}MethodA({targetObjPtr}, {MethodPtrsStr}[{index}]{combinedParameterNames});";

            if (returnTypePrefix.ToLower().Equals("void"))
            {
                yield return AppendLine(methodCallback, tabs + 1);
            }
            else
            {
                yield return AppendLine($"var ret = {methodCallback}", tabs + 1);

                var returnString = BuildReturnString(model, method, returnType);
                
                yield return AppendLine(returnString, tabs + 1);
            }
            
            yield return AppendLine("}", tabs);
        }
    }
}