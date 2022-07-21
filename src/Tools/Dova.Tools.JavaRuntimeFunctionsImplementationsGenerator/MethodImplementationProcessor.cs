namespace Dova.Tools.JavaRuntimeFunctionsImplementationsGenerator;

public class MethodImplementationProcessor
{
    private readonly string _methodName;
    private readonly string _methodSignature;
    
    private string MethodWithImplementation { get; set; }

    public MethodImplementationProcessor(string methodName, string methodSignature)
    {
        _methodName = methodName;
        _methodSignature = methodSignature;

        MethodWithImplementation = _methodSignature;
    }

    public void Parse()
    {
        MethodWithImplementation = MethodWithImplementation.Replace(";", "");

        var line = $"public {MethodWithImplementation} => _func->{_methodName.Replace("Id", "ID").Replace("Utf", "UTF")}(_env";
        
        MethodWithImplementation = MethodWithImplementation.Replace(" params", "");
        
        var methodParametersWithTypes = MethodWithImplementation.Split("(")[1].Split(")")[0];
        var parameters = ParseParameters(methodParametersWithTypes).ToList();
        var joinParameters = string.Join(", ", parameters);

        joinParameters = joinParameters.Replace(", jvalue);", ", Converters.ToArray(jvalue));");

        if (parameters.Count > 0)
        {
            line += ", ";
        }

        line += joinParameters + ");" + Environment.NewLine;

        MethodWithImplementation = line;
    }

    public void Save()
    {
        File.AppendAllText("OutputImplementations.txt", MethodWithImplementation);
    }
    
    private IEnumerable<string> ParseParameters(string methodParametersWithTypes)
    {
        methodParametersWithTypes = methodParametersWithTypes.Replace(",", "");
        var parts = methodParametersWithTypes.Split(" ");

        for (var i = 0; i < parts.Length; ++i)
        {
            if (i > 0 && 
                i % 2 == 1)
            {
                yield return parts[i];
            }
        }
    }
}