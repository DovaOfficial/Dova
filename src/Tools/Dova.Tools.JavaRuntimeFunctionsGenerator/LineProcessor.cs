namespace Dova.Tools.JavaRuntimeFunctionsGenerator;

internal class LineProcessor
{
    private readonly string _line; // i.e.: "public delegate* unmanaged<JNIEnv*, JObject, JMethodId> FromReflectedMethod;"
    
    private string MethodName { get; set; }
    private string ReturnType { get; set; }
    private IEnumerable<string> Args { get; set; }

    public LineProcessor(string line)
    {
        _line = line;
    }
    
    public void ParseLine()
    {
        var cleanLine = _line.Replace("public delegate* unmanaged<", ""); // "JNIEnv*, JObject, JMethodId> FromReflectedMethod;"
        cleanLine = cleanLine.Replace(";", ""); // "JNIEnv*, JObject, JMethodId> FromReflectedMethod"

        var parts = cleanLine.Split(">"); // "JNIEnv*, JObject, JMethodId" and " FromReflectedMethod"
        MethodName = parts[1].Trim(); // "FromReflectedMethod"

        var argsWithReturn = parts[0]
            .Replace(" ", "") // "JNIEnv*,JObject,JMethodId"
            .Split(","); // "JNIEnv*" and "JObject" and "JMethodId"

        ReturnType = argsWithReturn[argsWithReturn.Length - 1]; // "JMethodId"

        Args = argsWithReturn[1..^1];
    }

    public void SaveLine()
    {
        var methodArgsWithVariables = Args.Select(x => $"{x} {x.ToLower()}").ToList();
        var methodArgs = string.Join(',', methodArgsWithVariables)
            .Replace(",", ", ")
            .Replace("*,", ",");
        var mappedMethodArgs = JniTypesMapper.Map(methodArgs);
        var mappedReturnType = JniTypesMapper.Map(ReturnType);
        
        var classMethod = $"public {ReturnType} {MethodName}({methodArgs}) => _func->{MethodName}(_enc, {methodArgs});{Environment.NewLine}";
        File.AppendAllText("ClassMethods.txt", classMethod);

        var interfaceMethod = $"public {ReturnType} {MethodName}({methodArgs});{Environment.NewLine}";
        File.AppendAllText("InterfaceMethods.txt", interfaceMethod);
    }
}