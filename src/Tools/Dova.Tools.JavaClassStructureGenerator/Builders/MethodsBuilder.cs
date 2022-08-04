using Dova.Common;
using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator.Builders;

internal class MethodsBuilder : AbstractBuilder
{
    public override IEnumerable<string> Build(FileInfo outputFile, ClassDefinitionModel model, int tabs = 0)
    {
        for (var index = 0; index < model.MethodModels.Count; ++index)
        {
            /**
             * TODO: Don't add 2 same method with similar signatures
             *
             * Example from java.lang.String
             *
             * [JniSignatureAttribute("(Ljava/lang/invoke/MethodHandles$Lookup;)Ljava/lang/Object;", "public volatile")]
             * public virtual java.lang.Object resolveConstantDesc(java.lang.@invoke.MethodHandles_Lookup arg0)
             *
             * AND
             *
             * [JniSignatureAttribute("(Ljava/lang/invoke/MethodHandles$Lookup;)Ljava/lang/String;", "public")]
             * public virtual java.lang.String resolveConstantDesc(java.lang.@invoke.MethodHandles_Lookup arg0)
             */
            
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

            var cleanedMethodName = DefinitionCleaner.CleanJavaMethodName(method.Name);
            var genericParams = GetGenericParametersFormatted(method.ParameterModels);
            
            // TODO: Add building generic params from generic parameter args, i.e.: "public virtual R transform(java.util.function.Function<java.lang.String, R> arg0)" - AbstractBuilder.GetGenericParameters

            yield return AppendLine($"[{nameof(JniSignatureAttribute)}(\"{method.Signature}\", \"{method.Modifiers}\")]", tabs);
            yield return AppendLine($"{methodModifier}{modifierPrefix}{returnType} {cleanedMethodName}{genericParams}({combinedParameters})", tabs);

            if (!string.IsNullOrWhiteSpace(genericParams))
            {
                foreach (var genericParam in GetGenericParameters(method.ParameterModels))
                {
                    if (!genericParam.Contains("<") && !genericParam.Contains(" "))
                    {
                        yield return AppendLine($"where {genericParam} : {GetDefaultBounds()}", tabs + 1);
                    }
                }
            }

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