using Dova.Tools.JavaRuntimeFunctionsImplementationsGenerator;

const string JavaRuntimeInterfacePath = "../../../../../Dova/Core/Runtime/IJavaRuntime.cs";

if (!File.Exists(JavaRuntimeInterfacePath))
{
    throw new FileLoadException("Java Runtime interface does not exists or was moved");
}

var fileLines = File.ReadAllLines(JavaRuntimeInterfacePath);

var methodsSignatures = fileLines
    .SkipWhile(x => !x.Equals("{"))
    .Skip(1)
    .TakeWhile(x => !x.Equals("}"))
    .Where(x => x.Contains(" "))
    .Where(x => !x.Contains("//"))
    .Select(x => x.Trim())
    .ToList();
    
var fileLinesWithMethods = methodsSignatures
    .Select(x => x.Trim())
    .ToDictionary(
        x => x.Split("(")[0].Split(" ")[1],
        x => x);

foreach (var kvp in fileLinesWithMethods)
{
    var processor = new MethodImplementationProcessor(kvp.Key, kvp.Value);

    processor.Parse();
    processor.Save();
}