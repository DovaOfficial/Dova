using Dova.Tools.DocumentationGenerator;

const string JavaRuntimeInterfacePath = "../../../../../Dova/Core/Runtime/IJavaRuntime.cs";
const string JniFunctionsUrl = "https://docs.oracle.com/javase/8/docs/technotes/guides/jni/spec/functions.html";

if (!File.Exists(JavaRuntimeInterfacePath))
{
    throw new FileLoadException("Java Runtime interface does not exists or was moved");
}

var client = new HttpClient();
var response = await client.GetStringAsync(JniFunctionsUrl);
var dividedResponse = response.Split("<h3><a name=\"");

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
    var responseText = dividedResponse.FirstOrDefault(x => x.Split("\">")[0].ToLower().Equals(kvp.Key.ToLower()));

    if (string.IsNullOrWhiteSpace(responseText))
    {
        File.AppendAllText("OutputMethods.txt", kvp.Value + Environment.NewLine + Environment.NewLine);
        continue;
    }
    
    var methodProcessor = new MethodProcessor(kvp.Key, kvp.Value, responseText);

    methodProcessor.Parse();
    methodProcessor.Save();
}