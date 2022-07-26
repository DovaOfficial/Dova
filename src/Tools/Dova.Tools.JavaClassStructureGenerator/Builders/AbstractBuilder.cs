using System.Text;
using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator.Builders;

// TODO: Add checking if any error occurred -> IJavaRuntime.ExceptionOccurred
// TODO: Convert Java base types like java.lang.Long to C# 'long"
// TODO: Before generating type-specific method, clean Java class name
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

    // TODO: Add support for types like 'java.lang.invoke.TypeDescriptor$OfField<java.lang.Class<? extends PrintStream>[]>'
    // TODO: Add support for types like 'java.lang.@ref.FinalReference<java.lang.Object>' (see C# @ref keyword wrapped)
    protected static string CleanJavaClassName(string className) => className;
    
    // TODO: Add better type analyzing (same above)
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
            var t when t.Contains("[]") => $"<{CleanJavaArrayAndType(type)}>",
            _ => string.Empty
        };
    
    protected static string CleanJavaArrayAndType(string type) =>
        type.Replace("[]", "")
            .Replace("boolean", "bool");
    
    protected static string GetCombinedParameters(IEnumerable<ParameterDefinitionModel> models)
    {
        var paramsWithTypes = models
            .Select(x =>
            {
                var genericArgs = CombineGenericTypes(x.TypeParameterModels);
    
                return $"{x.Type}{genericArgs} {x.Name}";
            })
            .ToList();
        
        var combinedParamsWithTypes = string.Join(", ", paramsWithTypes);
        
        return combinedParamsWithTypes;
    }

    protected static string GetCombinedParameterNames(IEnumerable<ParameterDefinitionModel> models) =>
        string.Join(", ", models.Select(x => x.Name));
}