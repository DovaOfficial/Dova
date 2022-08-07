using System.Text;
using Dova.Tools.JavaClassStructureGenerator.Common;
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
        DefinitionCleaner.CleanJavaClassName(className);

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

    public static IEnumerable<string> GetGenericParameters(IEnumerable<ParameterDefinitionModel> models)
    {
        if (!models.Any())
        {
            return new List<string>();
        }

        var parts = models
            .Select(x => x.TypeParameterModels)
            .SelectMany(x => x)
            .Where(x => !x.TypeName.Contains(".") && !x.TypeName.Contains(" "))
            .Select(x => CleanJavaClassName(x.TypeName))
            .Distinct()
            .ToList();

        var clearedParts = DefinitionCleaner.CleanUnknownGenerics(parts);

        return clearedParts;
    }

    public static string GetGenericParametersFormatted(IEnumerable<ParameterDefinitionModel> models)
    {
        var clearedParts = GetGenericParameters(models);

        if (!clearedParts.Any())
        {
            return string.Empty;
        }

        var combined = string.Join(", ", clearedParts);

        return $"<{combined}>";
    }

    public static IEnumerable<ClassElementDefinitionModel> FilterMethodsToGenerate(IEnumerable<ClassElementDefinitionModel> models)
    {
        var groupedByName = models.GroupBy(m => m.Name);
        
        foreach (var nameGroup in groupedByName) // i.e.: "resolveConstantDesc"
        {
            var groupedByParameters = nameGroup.GroupBy(x => x.ParameterModels.Count);

            foreach (var parametersGroup in groupedByParameters) // i.e.: 1 param (or N params)
            {
                var combinedParametersGroups = parametersGroup.GroupBy(x =>
                    string.Join(", ", x.ParameterModels.Select(m => m.Type)));

                foreach (var combinedParametersGroup in combinedParametersGroups) // i.e.: "java.lang.@invoke.MethodHandles_Lookup" (or N combined params)
                {
                    var method = combinedParametersGroup.Count() == 1
                        ? combinedParametersGroup.First()
                        : FilterMethods(combinedParametersGroup); // Methods return different types

                    yield return method;
                }
            }
        }
    }

    public static string GetDefaultBounds() => JavaObjectFullName;

    // TODO: Implement better way of handling new objects of interface types.
    public static string BuildReturnString(ClassDefinitionModel classDefModel, ClassElementDefinitionModel classElDefModel, string returnType) =>
        IsObjectType(returnType) ? $"return DovaInterfaceFactory.Get<{returnType}>(ret);" : $"return ret;";
    
    private static ClassElementDefinitionModel FilterMethods(IEnumerable<ClassElementDefinitionModel> models)
    {
        var notDefaultReturn = models.FirstOrDefault(x => !x.ReturnType.Equals(JavaObjectFullName));

        if (notDefaultReturn != null)
        {
            return notDefaultReturn;
        }

        // TODO: Find most valid type beside java.lang.Object
        return models.First();
    }
}