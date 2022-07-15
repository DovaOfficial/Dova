using System.Text;
using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator;

internal class CSharpClassBuilder
{
    private const string JavaObjectClassFullName = "java.lang.Object";
    
    private ClassDefinitionModel Model { get; }
    private StringBuilder Builder { get; }
    private ICollection<string> Lines { get; }
    
    private string BaseClass { get; set; } = JavaObjectClassFullName;

    public CSharpClassBuilder(ClassDefinitionModel model)
    {
        Model = model;

        Builder = new();
        Lines = new List<string>();
    }

    public IEnumerable<string> Build()
    {
        BuildUsings(0);
        BuildNamespace(0);
        BuildClass(0);

        return Lines;
    }

    private void WithBrackets(int tabs, Action action)
    {
        AppendLine(tabs, "{");
        
        action?.Invoke();
        
        AppendLine(tabs, "}");
    }

    private void AppendLine(int tabs, string line)
    {
        Lines.Add(WithTabs(tabs, line));
    }

    private string WithTabs(int tabs, string line)
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

    private void PrepareNewSection()
    {
        Lines.Add("");
        
        Builder.Clear();
    }
    
    private void BuildClass(int tabs)
    {
        PrepareNewSection();
        
        BuildClassSignature(tabs);
        BuildBaseClass(tabs);
        // TODO: BuildInterfaces(tabs);

        WithBrackets(tabs, () =>
        {
            // TODO: BuildJdkReferences(tabs + 1); // TODO: Use DovaJvm.Vm.Runtime
            // TODO: BuildFields(tabs + 1);
            // TODO: BuildConstructors(tabs + 1);
            // TODO: BuildMethods(tabs + 1);
            // TODO: BuildInnerClasses(tabs + 1); 
        });
    }

    private void BuildUsings(int tabs)
    {
        AppendLine(tabs, "using Dova.JDK;"); // Mainly used for JavaObject
        AppendLine(tabs, "");
        AppendLine(tabs, "using System;");
    }
    
    private void BuildNamespace(int tabs)
    {
        PrepareNewSection();
        
        AppendLine(tabs, $"namespace {Model.ClassDetailsModel.PackageName};");
    }
    
    private void BuildClassSignature(int tabs)
    {
        PrepareNewSection();

        var modifiers = Model.ClassDetailsModel.Modifiers
            .Replace("final", "sealed")
            .Replace("transient", "")
            .Replace("synchronized", "")
            .Replace("volatile", "");
        
        AppendLine(tabs, $"{modifiers} class {Model.ClassDetailsModel.ClassName}");
    }
    
    private void BuildBaseClass(int tabs)
    {
        Builder.Clear();

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

        AppendLine(tabs + 1, $": {BaseClass}");
    }
}