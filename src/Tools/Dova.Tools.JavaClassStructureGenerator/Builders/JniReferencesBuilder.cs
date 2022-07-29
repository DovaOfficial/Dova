using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator.Builders;

internal class JniReferencesBuilder : AbstractBuilder
{
    public override IEnumerable<string> Build(ClassDefinitionModel model, int tabs = 0)
    {
        yield return AppendLine($"public static {typeof(IntPtr).FullName} {ClassPtrStr} {{ get; }}", tabs);
        yield return AppendLine($"public static {typeof(IntPtr).FullName} {ClassRefPtrStr} {{ get; }}", tabs);
        
        yield return AppendLine("");
        
        if (model.FieldModels.Count > 0)
        {
            yield return AppendLine($"private static {typeof(IList<>).Namespace}.{nameof(IList<IntPtr>)} {FieldPtrsStr} {{ get; }} = new {typeof(List<>).Namespace}.{nameof(List<IntPtr>)}();", tabs);
        }
        
        if (model.ConstructorModels.Count > 0)
        {
            yield return AppendLine($"private static {typeof(IList<>).Namespace}.{nameof(IList<IntPtr>)} {ConstructorPtrsStr} {{ get; }} = new {typeof(List<>).Namespace}.{nameof(List<IntPtr>)}();", tabs);
        }
        
        if (model.MethodModels.Count > 0)
        {
            yield return AppendLine($"private static {typeof(IList<>).Namespace}.{nameof(IList<IntPtr>)} {MethodPtrsStr} {{ get; }} = new {typeof(List<>).Namespace}.{nameof(List<IntPtr>)}();", tabs);
        }

        yield return AppendLine("");
        
        yield return AppendLine($"static {model.ClassDetailsModel.ClassName}()", tabs);
        yield return AppendLine("{", tabs);
        
        yield return AppendLine($"{ClassPtrStr} = DovaJvm.Vm.Runtime.FindClass(\"{model.ClassDetailsModel.Signature}\");", tabs + 1);
        yield return AppendLine($"{ClassRefPtrStr} = DovaJvm.Vm.Runtime.NewGlobalRef({ClassPtrStr});", tabs + 1);
        
        foreach (var fieldModel in model.FieldModels)
        {
            var runtimeMethod = fieldModel.IsStatic
                ? "GetStaticFieldId"
                : "GetFieldId";
        
            yield return AppendLine($"{FieldPtrsStr}.Add(DovaJvm.Vm.Runtime.{runtimeMethod}({ClassRefPtrStr}, \"{fieldModel.Name}\", \"{fieldModel.Signature}\"));", tabs + 1);
        }
        
        foreach (var fieldModel in model.ConstructorModels)
        {
            var runtimeMethod = fieldModel.IsStatic
                ? "GetStaticMethodId"
                : "GetMethodId";
        
            yield return AppendLine($"{ConstructorPtrsStr}.Add(DovaJvm.Vm.Runtime.{runtimeMethod}({ClassRefPtrStr}, \"{model.ClassDetailsModel.ClassName}\", \"{fieldModel.Signature}\"));", tabs + 1);
        }
        
        foreach (var fieldModel in model.MethodModels)
        {
            var runtimeMethod = fieldModel.IsStatic
                ? "GetStaticMethodId"
                : "GetMethodId";
        
            yield return AppendLine($"{MethodPtrsStr}.Add(DovaJvm.Vm.Runtime.{runtimeMethod}({ClassRefPtrStr}, \"{fieldModel.Name}\", \"{fieldModel.Signature}\"));", tabs + 1);
        }
        
        yield return AppendLine("}", tabs);
    }
}