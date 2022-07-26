using Dova.Common;
using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator.Builders;

internal class PropertiesBuilder : AbstractBuilder
{
    public override IEnumerable<string> Build(ClassDefinitionModel model, int tabs = 0)
    {
        for (var index = 0; index < model.FieldModels.Count; ++index)
        {
            yield return AppendLine("");
            
            var field = model.FieldModels[index];
            
            var staticPrefix = field.IsStatic
                ? "static "
                : "";
        
            var staticMethodPrefix = field.IsStatic
                ? "Static"
                : "";
        
            var targetObjPtr = field.IsStatic
                ? ClassRefPtrStr
                : nameof(JavaObject.CurrentRefPtr);
        
            var targetObjValue = field.ReturnType.Contains(".")
                ? $"value.{nameof(JavaObject.CurrentRefPtr)}"
                : "value";
        
            yield return AppendLine($"[{nameof(JniSignatureAttribute)}(\"{field.Signature}\", \"{field.Modifiers}\")]", tabs);
            yield return AppendLine($"public {staticPrefix}{CleanJavaClassName(field.ReturnType)} {field.Name}", tabs);
            yield return AppendLine("{", tabs);
            yield return AppendLine("get", tabs + 1);
            yield return AppendLine("{", tabs + 1);
            yield return AppendLine($"var ret = DovaJvm.Vm.Runtime.Get{staticMethodPrefix}{GetReturnTypePrefix(field.ReturnType)}Field{GetGenericType(field.ReturnType)}({targetObjPtr}, {FieldPtrsStr}[{index}]);", tabs + 2);
            yield return AppendLine(field.ReturnType.Contains(".") ? $"return new {CleanJavaClassName(field.ReturnType)}(ret);" : $"return ret;", tabs + 2);
            yield return AppendLine("}", tabs + 1);
            yield return AppendLine($"set => DovaJvm.Vm.Runtime.Set{staticMethodPrefix}{GetReturnTypePrefix(field.ReturnType)}Field({targetObjPtr}, {FieldPtrsStr}[{index}], {targetObjValue});", tabs + 1);
            yield return AppendLine("}", tabs);
        }
    }
}