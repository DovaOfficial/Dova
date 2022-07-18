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
            // TODO: BuildJdkReferences(); // TODO: Use DovaJvm.Vm.Runtime // tabs + 1
            // TODO: BuildFields(); // tabs + 1
            // TODO: BuildConstructors(); // tabs + 1
            // TODO: BuildMethods(); // tabs + 1
            // TODO: BuildInnerClasses(); // tabs + 1
        });
    }

    private void BuildUsings()
    {
        AppendLine("using Dova.JDK;"); // Mainly used for JavaObject
        AppendLine("");
        AppendLine("using System;");
    }
    
    private void BuildNamespace()
    {
        AppendLine($"namespace {Model.ClassDetailsModel.PackageName};");
    }
    
    private void BuildClassSignature()
    {
        const string interfaceType = "interface";
        
        var type = "class ";
        
        var modifiers = Model.ClassDetailsModel.Modifiers
            .Replace("final", "sealed")
            .Replace("transient", "")
            .Replace("synchronized", "")
            .Replace("volatile", "");

        if (modifiers.Contains(interfaceType))
        {
            type = string.Empty;
        }

        var genericParams = Model.ClassDetailsModel.TypeParameterModels
            .Select(x => x.VariableName)
            .ToList();

        var genericVariables = string.Join(',', genericParams);
        var genericArgs = string.Empty;

        if (!string.IsNullOrWhiteSpace(genericVariables))
        {
            genericArgs = $"<{genericVariables}>";
        }

        AppendLine($"{modifiers} {type}{Model.ClassDetailsModel.ClassName}{genericArgs}");

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
            BaseClass = Model.BaseClassModel.Name;
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
}