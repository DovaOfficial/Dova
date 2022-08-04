using System.Diagnostics;

namespace Dova.Tools.JavaClassStructureGenerator;

internal static class ToolRunner
{
    public static void Run(string classDefinitionGeneratorPath, string tempOutputPathFull, string javaModuleFinderPaths = "")
    {
        var process = new Process
        {
            StartInfo =
            {
                FileName = "java",
                Arguments = $"-jar {classDefinitionGeneratorPath} {tempOutputPathFull} {javaModuleFinderPaths}"
            }
        };

        process.ErrorDataReceived += (sender, e) => Console.WriteLine(sender.ToString());

        process.Start();
        process.WaitForExit();
    }
}