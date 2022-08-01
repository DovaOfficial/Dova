using System.Text;
using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator.Builders;

// TODO: Add checking if any error occurred -> IJavaRuntime.ExceptionOccurred (Field, Constructor, Method)
internal abstract class AbstractBuilder : IBuilder
{
    public const string JavaObjectFullName = "java.lang.Object";
    
    protected const string ClassPtrStr = "ClassPtr";
    protected const string ClassRefPtrStr = "ClassRefPtr";
    protected const string FieldPtrsStr = "FieldPtrs";
    protected const string ConstructorPtrsStr = "ConstructorPtrs";
    protected const string MethodPtrsStr = "MethodPtrs";

    public abstract IEnumerable<string> Build(FileInfo outputFile, ClassDefinitionModel model, int tabs = 0);

    public string AppendLine(string line, int tabs = 0)
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

    public static string CleanJavaClassName(string className) => 
        JavaCleaner.CleanJavaClassName(className);

    public static string GetReturnTypePrefix(string returnType) => 
        returnType switch
        {
            var rt when IsObjectType(rt) => "Object",
            _ => returnType
                .ToFirstUppercase()
                .Replace("Bool", "Boolean")
        };

    public static bool IsObjectType(string type) =>
        type.Contains(".") // Most cases will have full package name + class name, divided by '.'
        || type.Contains("JavaArray") // Dova's custom type for an array (array wrapper)
        || char.IsUpper(type[0]); // Most generics might have (1;N) chars but first is almost always an upper char

    public static string GetCombinedParameters(IEnumerable<ParameterDefinitionModel> models)
    {
        var paramsWithTypes = models
            .Select(x => $"{CleanJavaClassName(x.Type)} {x.Name}")
            .ToList();
        
        var combinedParamsWithTypes = string.Join(", ", paramsWithTypes);
        
        return combinedParamsWithTypes;
    }
    
    public static string CombineGenericTypes(IEnumerable<TypeParameterModel> models)
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

    public static string GetCombinedParameterNames(IEnumerable<ParameterDefinitionModel> models) =>
        string.Join(", ", models.Select(x => x.Name));

    public static string GetGenericParameters(IEnumerable<ParameterDefinitionModel> models)
    {
        if (!models.Any())
        {
            return string.Empty;
        }

        var parts = models
            .Select(x => x.TypeParameterModels)
            .SelectMany(x => x)
            .Where(x => !x.TypeName.Contains("."))
            .Select(x => CleanJavaClassName(x.TypeName))
            .ToList();

        var clearedParts = JavaCleaner.CleanUnknownGenerics(parts);

        if (clearedParts.Count == 0)
        {
            return string.Empty;
        }

        var combined = string.Join(", ", clearedParts);

        return $"<{combined}>";
    }

    // TODO: Implement better way of handling new objects of interface types.
    public static string BuildReturnString(ClassDefinitionModel classDefModel, ClassElementDefinitionModel classElDefModel, string returnType) =>
        IsObjectType(returnType) ? $"return DovaInterfaceFactory.Get<{returnType}>(ret);" : $"return ret;";
}