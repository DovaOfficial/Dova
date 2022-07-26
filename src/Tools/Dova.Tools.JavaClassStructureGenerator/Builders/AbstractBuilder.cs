using System.Text;
using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator.Builders;

// TODO: Add checking if any error occurred -> IJavaRuntime.ExceptionOccurred (Field, Constructor, Method)
internal abstract class AbstractBuilder : IBuilder
{
    protected const string ClassPtrStr = "ClassPtr";
    protected const string ClassRefPtrStr = "ClassRefPtr";
    protected const string FieldPtrsStr = "FieldPtrs";
    protected const string ConstructorPtrsStr = "ConstructorPtrs";
    protected const string MethodPtrsStr = "MethodPtrs";
    
    public abstract IEnumerable<string> Build(ClassDefinitionModel model, int tabs = 0);

    protected string AppendLine(string line, int tabs = 0)
    {
        var sb = new StringBuilder();
        
        for (var i = 0; i < tabs; ++i)
        {
            sb.Append("\t");
        }
        
        sb.Append(line);
        
        var newLine = sb.ToString();
        
        return newLine;
    }
    
    protected static string CombineGenericTypes(IEnumerable<TypeParameterModel> models)
    {
        var genericParams = models
            .Select(x => x.VariableName)
            .ToList();
    
        var genericVariables = string.Join(", ", genericParams);
        var genericArgs = string.Empty;
    
        if (!string.IsNullOrWhiteSpace(genericVariables))
        {
            genericArgs = $"<{genericVariables}>";
        }
    
        return genericArgs;
    }
    
    protected static string CleanJavaClassName(string className) => JavaCleaner.CleanJavaClassName(className);
    
    protected static string GetReturnTypePrefix(string returnType) => 
        returnType switch
        {
            var rt when rt.EndsWith("[]") => "Array",
            var rt when rt.Contains(".") => "Object",
            _ => returnType.ToFirstUppercase()
        };

    protected static string GetReturnType(string type)
    {
        var cleaned = CleanJavaClassName(type);
    
        return cleaned switch
        {
            var cln when cln.Contains("[]") => $"JavaArray{GetGenericType(cleaned)}",
            _ => cleaned
        };
    }

    protected static string GetGenericType(string type) =>
        type switch
        {
            var t when t.Contains("[]") => $"<{CleanJavaClassName(type)}>",
            _ => string.Empty
        };

    protected static string GetCombinedParameters(IEnumerable<ParameterDefinitionModel> models)
    {
        var paramsWithTypes = models
            .Select(x => $"{CleanJavaClassName(x.Type)}{CombineGenericTypes(x.TypeParameterModels)} {x.Name}")
            .ToList();
        
        var combinedParamsWithTypes = string.Join(", ", paramsWithTypes);
        
        return combinedParamsWithTypes;
    }

    protected static string GetCombinedParameterNames(IEnumerable<ParameterDefinitionModel> models) =>
        string.Join(", ", models.Select(x => x.Name));
}