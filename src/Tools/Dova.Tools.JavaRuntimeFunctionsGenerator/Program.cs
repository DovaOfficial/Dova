using Dova.Tools.JavaRuntimeFunctionsGenerator;

const string JniFunctionsContainerPath = "../../../../../Dova/Internals/Interop/JNINativeInterface.cs";

if (!File.Exists(JniFunctionsContainerPath))
{
    throw new ArgumentException("Unknown Path");
}

var lines = File.ReadAllLines(JniFunctionsContainerPath);

var functionLines = lines
    .Where(x => x.Contains("public delegate*"))
    .Select(x => x.Trim())
    .ToList();
    
var lineProcessors = functionLines
    .Select(line => new LineProcessor(line))
    .ToList();

lineProcessors.ForEach(x => x.ParseLine());
lineProcessors.ForEach(x => x.SaveLine());