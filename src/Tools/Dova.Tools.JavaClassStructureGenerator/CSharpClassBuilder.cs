using System.Text;
using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator;

internal static class CSharpClassBuilder
{
    private static StringBuilder Builder { get; } = new();
    
    public static void Build(ICollection<string> lines, ClassDefinitionModel model, int tabs = 0)
    {
        BuildUsings(lines, model, tabs);
        BuildNamespace(lines, model, tabs);
        BuildClass(lines, model, tabs);
    }

    private static void WithBrackets(ICollection<string> lines, int tabs, Action action)
    {
        AppendLine(lines, tabs, "{");
        
        action?.Invoke();
        
        AppendLine(lines, tabs, "}");
    }

    private static void AppendLine(ICollection<string> lines, int tabs, string line)
    {
        lines.Add(WithTabs(line, tabs));
    }

    private static string WithTabs(string line, int tabs)
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

    private static void PrepareNewSection(ICollection<string> lines)
    {
        lines.Add("");
        
        Builder.Clear();
    }
    
    private static void BuildClass(ICollection<string> lines, ClassDefinitionModel model, int tabs)
    {
        PrepareNewSection(lines);
        
        BuildClassSignature(lines, model, tabs);
        BuildBaseClass(lines, model, tabs);
        // TODO: BuildInterfaces(lines, model, tabs);

        WithBrackets(lines, tabs, () =>
        {
            // TODO: BuildJdkReferences(lines, model, tabs + 1); // TODO: Use DovaJvm.Vm.Runtime
            // TODO: BuildFields(lines, model, tabs + 1);
            // TODO: BuildConstructors(lines, model, tabs + 1);
            // TODO: BuildMethods(lines, model, tabs + 1);
            // TODO: BuildInnerClasses(lines, model, tabs + 1); 
        });
    }

    private static void BuildUsings(ICollection<string> lines, ClassDefinitionModel model, int tabs)
    {
        AppendLine(lines, tabs, "using Dova.JDK;"); // Mainly used for JavaObject
        AppendLine(lines, tabs, "");
        AppendLine(lines, tabs, "using System;");
    }
    
    private static void BuildNamespace(ICollection<string> lines, ClassDefinitionModel model, int tabs)
    {
        PrepareNewSection(lines);
        
        AppendLine(lines, tabs, $"namespace {model.ClassDetailsModel.PackageName};");
    }
    
    private static void BuildClassSignature(ICollection<string> lines, ClassDefinitionModel model, int tabs)
    {
        PrepareNewSection(lines);

        var modifiers = model.ClassDetailsModel.Modifiers
            .Replace("final", "sealed")
            .Replace("transient", "")
            .Replace("synchronized", "")
            .Replace("volatile", "");
        
        AppendLine(lines, tabs, $"{modifiers} class {model.ClassDetailsModel.ClassName}");
    }
    
    private static void BuildBaseClass(ICollection<string> lines, ClassDefinitionModel model, int tabs)
    {
        Builder.Clear();

        const string javaObject = "java.lang.Object";

        var baseClass = javaObject; // Explicitly declare Java base class

        if (!string.IsNullOrWhiteSpace(model.BaseClassModel.Name))
        {
            baseClass = model.BaseClassModel.Name;
        }
        else
        {
            var fullName = $"{model.ClassDetailsModel.PackageName}.{model.ClassDetailsModel.ClassName}";

            if (fullName.Equals(javaObject))
            {
                baseClass = "JavaObject"; // Do not use ref to Dova.JDK project (nameof)
            }
        }

        AppendLine(lines, tabs + 1, $": {baseClass}");
    }
}