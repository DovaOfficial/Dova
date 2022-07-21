using System.Text;
using Dova.Common;
using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator;

// TODO: Add checking if any error occurred -> IJavaRuntime.ExceptionOccurred
internal class CSharpClassBuilder
{
    private const string JavaObjectClassFullName = "java.lang.Object";
    private const string ClassPtrStr = "ClassPtr";
    private const string ClassRefPtrStr = "ClassRefPtr";
    private const string FieldPtrsStr = "FieldPtrs";
    private const string ConstructorPtrsStr = "ConstructorPtrs";
    private const string MethodPtrsStr = "MethodPtrs";
    
    private ClassDefinitionModel Model { get; }
    private int Tabs { get; }
    private StringBuilder Builder { get; }
    private ICollection<string> Lines { get; }
    
    private string BaseClass { get; set; } = JavaObjectClassFullName;

    public CSharpClassBuilder(ClassDefinitionModel model, int tabs = 0)
    {
        Model = model;
        Tabs = tabs;

        Builder = new();
        Lines = new List<string>();
    }

    public IEnumerable<string> Build()
    {
        BuildUsings();
        AsNewSection(BuildNamespace);
        BuildClass();

        return Lines;
    }

    private void WithBrackets(Action action, int tabs = 0)
    {
        AppendLine("{", tabs);
        
        action?.Invoke();
        
        AppendLine("}", tabs);
    }

    private void AppendLine(string line, int tabs = 0)
    {
        Lines.Add(WithTabs(line, Tabs + tabs));
    }

    private string WithTabs(string line, int tabs)
    {
        Builder.Clear();
        
        for (var i = 0; i < tabs; ++i)
        {
            Builder.Append("\t");
        }

        Builder.Append(line);

        var newLine = Builder.ToString();

        return newLine;
    }
    
    private void AsNewSection(Action action)
    {
        Lines.Add("");
        
        AsNewLine(action);
    }
    
    private void AsNewLine(Action action)
    {
        Builder.Clear();
        
        action?.Invoke();
    }

    private void BuildClass()
    {
        AsNewSection(BuildClassSignature);
        AsNewLine(BuildBaseClass);
        AsNewLine(BuildInterfaces);

        WithBrackets(() =>
        {
            AsNewLine(BuildJniReferences);
            AsNewSection(BuildProperties);
            AsNewSection(BuildConstructors);
            AsNewSection(BuildExtraMethods);
            AsNewSection(BuildMethods);
            AsNewSection(BuildInnerClasses);
        });
    }

    private void BuildUsings()
    {
        AppendLine($"using { typeof(JavaObject).Namespace };");
        AppendLine("");
        AppendLine("using System;");
    }
    
    private void BuildNamespace()
    {
        AppendLine($"namespace {Model.ClassDetailsModel.PackageName};");
    }
    
    private void BuildClassSignature()
    {
        var type = Model.ClassDetailsModel.IsInterface
            ? string.Empty
            : "class ";

        var genericParams = Model.ClassDetailsModel.TypeParameterModels
            .Select(x => x.VariableName)
            .ToList();

        var genericVariables = string.Join(',', genericParams);
        var genericArgs = string.Empty;

        if (!string.IsNullOrWhiteSpace(genericVariables))
        {
            genericArgs = $"<{genericVariables}>";
        }

        AppendLine($"[JniSignature(\"{Model.ClassDetailsModel.Signature}\", \"{Model.ClassDetailsModel.Modifiers}\")]");
        AppendLine($"public {type}{Model.ClassDetailsModel.ClassName}{genericArgs}");

        foreach (var typeParam in Model.ClassDetailsModel.TypeParameterModels)
        {
            var bounds = typeParam.BoundModels
                .Select(x => x.Name)
                .ToList();

            var totalBounds = string.Join(',', bounds);

            AppendLine($"where {typeParam.VariableName} : {totalBounds}", 1);
        }
    }
    
    private void BuildBaseClass()
    {
        if (!string.IsNullOrWhiteSpace(Model.BaseClassModel.Name))
        {
            BaseClass = Model.BaseClassModel.Name
                .Replace("$", ".");
        }
        else
        {
            var fullName = $"{Model.ClassDetailsModel.PackageName}.{Model.ClassDetailsModel.ClassName}";

            if (fullName.Equals(JavaObjectClassFullName))
            {
                BaseClass = nameof(JavaObject);
            }
        }

        AppendLine($": {BaseClass}", 1);
    }
    
    private void BuildInterfaces()
    {
        if (Model.InterfaceModels.Count == 0)
        {
            return;
        }

        foreach (var interfaceModel in Model.InterfaceModels)
        {
            AppendLine($", {interfaceModel.Name}", 1);
        }
    }
    
    private void BuildJniReferences()
    {
        AppendLine($"public static IntPtr {ClassPtrStr} {{ get; }}", 1);
        AppendLine($"public static IntPtr {ClassRefPtrStr} {{ get; }}", 1);

        if (Model.FieldModels.Count > 0)
        {
            AppendLine($"private static IList<IntPtr> {FieldPtrsStr} {{ get; }} = new List<IntPtr>();", 1);
        }

        if (Model.ConstructorModels.Count > 0)
        {
            AppendLine($"private static IList<IntPtr> {ConstructorPtrsStr} {{ get; }} = new List<IntPtr>();", 1);
        }

        if (Model.MethodModels.Count > 0)
        {
            AppendLine($"private static IList<IntPtr> {MethodPtrsStr} {{ get; }} = new List<IntPtr>();", 1);
        }

        AsNewSection(() =>
        {
            AppendLine($"static {Model.ClassDetailsModel.ClassName}()", 1);
        
            WithBrackets(() =>
            {
                AppendLine($"{ClassPtrStr} = DovaJvm.Vm.Runtime.FindClass(\"" + Model.ClassDetailsModel.Signature + "\");", 2);
                AppendLine($"{ClassRefPtrStr} = DovaJvm.Vm.Runtime.NewGlobalRef({ClassPtrStr});", 2);

                foreach (var fieldModel in Model.FieldModels)
                {
                    var runtimeMethod = fieldModel.IsStatic
                        ? "GetStaticFieldId"
                        : "GetFieldId";

                    AppendLine($"{FieldPtrsStr}.Add(DovaJvm.Vm.Runtime.{runtimeMethod}({ClassPtrStr}, \"{fieldModel.Name}\", \"{fieldModel.Signature}\"));", 2);
                }
                
                foreach (var fieldModel in Model.ConstructorModels)
                {
                    var runtimeMethod = fieldModel.IsStatic
                        ? "GetStaticMethodId"
                        : "GetMethodId";

                    AppendLine($"{ConstructorPtrsStr}.Add(DovaJvm.Vm.Runtime.{runtimeMethod}({ClassPtrStr}, \"{Model.ClassDetailsModel.ClassName}\", \"{fieldModel.Signature}\"));", 2);
                }
                
                foreach (var fieldModel in Model.MethodModels)
                {
                    var runtimeMethod = fieldModel.IsStatic
                        ? "GetStaticMethodId"
                        : "GetMethodId";

                    AppendLine($"{MethodPtrsStr}.Add(DovaJvm.Vm.Runtime.{runtimeMethod}({ClassPtrStr}, \"{fieldModel.Name}\", \"{fieldModel.Signature}\"));", 2);
                }
            }, 1);
        });
    }
    
    private void BuildProperties()
    {
        for (var index = 0; index < Model.FieldModels.Count; ++index)
        {
            var field = Model.FieldModels[index];
            
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

            AppendLine($"[JniSignature(\"{field.Signature}\", \"{field.Modifiers}\")]", 1);
            AppendLine($"public {staticPrefix}{field.ReturnType} {field.Name}", 1);
            WithBrackets(() =>
            {
                AppendLine($"get", 2);
                WithBrackets(() =>
                {
                    AppendLine($"var ret = DovaJvm.Vm.Runtime.Get{staticMethodPrefix}{GetReturnType(field.ReturnType)}Field({targetObjPtr}, {FieldPtrsStr}[{index}]);", 3);
                    
                    if (field.ReturnType.Contains("."))
                    {
                        AppendLine($"return new {field.ReturnType}(ret);", 3);
                    }
                    else
                    {
                        AppendLine($"return ret;", 3);
                    }
                }, 2);
                
                AppendLine("");

                AppendLine($"set => DovaJvm.Vm.Runtime.Set{staticMethodPrefix}{GetReturnType(field.ReturnType)}Field({targetObjPtr}, {FieldPtrsStr}[{index}], {targetObjValue});", 2);
            }, 1);
            
            AppendLine("");
        }
    }

    private void BuildConstructors()
    {
        // TODO: Add constructors
    }
    
    private void BuildExtraMethods()
    {
        AppendLine($"public override string {nameof(JavaObject.GetJavaClassSignature)}() => \"{Model.ClassDetailsModel.Signature}\";", 1);
        AppendLine($"public override IntPtr {nameof(JavaObject.GetJavaClassRaw)}() => {ClassPtrStr};", 1);
        AppendLine($"public override IntPtr {nameof(JavaObject.GetJavaClassRefRaw)}() => {ClassRefPtrStr};", 1);
    }
    
    private void BuildMethods()
    {
        // TODO: Add methods
    }
    
    private void BuildInnerClasses()
    {
        foreach (var innerClassModel in Model.InnerClassModels)
        {
            var builder = new CSharpClassBuilder(innerClassModel, Tabs + 1);
            
            builder.BuildClass();

            foreach (var line in builder.Lines)
            {
                Lines.Add(line);
            }
        }
    }
    
    private static string GetReturnType(string returnType) => 
        returnType.Contains(".") 
            ? "Object" 
            : returnType.ToFirstUppercase();
}