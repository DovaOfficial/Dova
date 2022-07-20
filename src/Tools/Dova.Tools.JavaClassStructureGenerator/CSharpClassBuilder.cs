using System.Text;
using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator;

internal class CSharpClassBuilder
{
    private const string JavaObjectClassFullName = "java.lang.Object";
    
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
            AsNewSection(BuildMethods);
            AsNewSection(BuildInnerClasses);
        });
    }

    private void BuildUsings()
    {
        AppendLine("using Dova.Common;"); // Mainly used for JavaObject
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
                BaseClass = "JavaObject"; // Do not use ref to Dova.JDK project (nameof)
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
        AppendLine("private static IntPtr ClassPtr { get; }", 1);
        AppendLine("private static IntPtr ClassRefPtr { get; }", 1);

        if (Model.FieldModels.Count > 0)
        {
            AppendLine("private static IList<IntPtr> FieldPtrs { get; } = new List<IntPtr>();", 1);
        }

        if (Model.ConstructorModels.Count > 0)
        {
            AppendLine("private static IList<IntPtr> ConstructorPtrs { get; } = new List<IntPtr>();", 1);
        }

        if (Model.MethodModels.Count > 0)
        {
            AppendLine("private static IList<IntPtr> MethodPtrs { get; } = new List<IntPtr>();", 1);
        }

        AsNewSection(() =>
        {
            AppendLine($"static {Model.ClassDetailsModel.ClassName}()", 1);
        
            WithBrackets(() =>
            {
                AppendLine($"ClassPtr = DovaJvm.Vm.Runtime.FindClass(\"" + Model.ClassDetailsModel.Signature + "\");", 2);
                AppendLine($"ClassRefPtr = DovaJvm.Vm.Runtime.NewGlobalRef(ClassPtr);", 2);

                foreach (var fieldModel in Model.FieldModels)
                {
                    var runtimeMethod = fieldModel.IsStatic
                        ? "GetStaticFieldId"
                        : "GetFieldId";

                    AppendLine($"FieldPtrs.Add(DovaJvm.Vm.Runtime.{runtimeMethod}(ClassPtr, \"{fieldModel.Name}\", \"{fieldModel.Signature}\"));", 2);
                }
                
                foreach (var fieldModel in Model.ConstructorModels)
                {
                    var runtimeMethod = fieldModel.IsStatic
                        ? "GetStaticMethodId"
                        : "GetMethodId";

                    AppendLine($"ConstructorPtrs.Add(DovaJvm.Vm.Runtime.{runtimeMethod}(ClassPtr, \"{Model.ClassDetailsModel.ClassName}\", \"{fieldModel.Signature}\"));", 2);
                }
                
                foreach (var fieldModel in Model.MethodModels)
                {
                    var runtimeMethod = fieldModel.IsStatic
                        ? "GetStaticMethodId"
                        : "GetMethodId";

                    AppendLine($"MethodPtrs.Add(DovaJvm.Vm.Runtime.{runtimeMethod}(ClassPtr, \"{fieldModel.Name}\", \"{fieldModel.Signature}\"));", 2);
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
                ? "ClassRefPtr"
                : "CurrentRefPtr";

            var targetObjValue = field.ReturnType.Contains(".")
                ? "value.CurrentRefPtr"
                : "value";

            AppendLine($"[JniSignature(\"{field.Signature}\", \"{field.Modifiers}\")]", 1);
            AppendLine($"public {staticPrefix}{field.ReturnType} {field.Name}", 1);
            WithBrackets(() =>
            {
                AppendLine($"get", 2);
                WithBrackets(() =>
                {
                    if (field.ReturnType.Contains("."))
                    {
                        AppendLine($"var objPtr = DovaJvm.Vm.Runtime.Get{staticMethodPrefix}ObjectField({targetObjPtr}, FieldPtrs[{index}]);", 3);
                        AppendLine($"return new {field.ReturnType}(objPtr);", 3);
                    }
                    else
                    {
                        AppendLine($"return DovaJvm.Vm.Runtime.Get{staticMethodPrefix}{field.ReturnType.ToFirstUppercase()}Field({targetObjPtr}, FieldPtrs[{index}]);", 3);
                    }
                }, 2);
                
                AppendLine("");

                AppendLine($"set => DovaJvm.Vm.Runtime.Set{staticMethodPrefix}{GetReturnType(field.ReturnType)}Field({targetObjPtr}, FieldPtrs[{index}], {targetObjValue});", 2);
            }, 1);
        }
    }

    private void BuildConstructors()
    {
        // TODO: Add constructors
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