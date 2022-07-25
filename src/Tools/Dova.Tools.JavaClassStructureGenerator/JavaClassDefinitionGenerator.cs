using System.Diagnostics;

namespace Dova.Tools.JavaClassStructureGenerator;

internal static class JavaClassDefinitionGenerator
{
    public static void Generate(string classDefinitionGeneratorPath, string tempOutputPathFull, string javaClassFullName)
    {
        var process = new Process
        {
            StartInfo =
            {
                FileName = "java",
                Arguments = $"-jar {classDefinitionGeneratorPath} {tempOutputPathFull} {javaClassFullName}"
            }
        };

        process.ErrorDataReceived += (sender, e) => Console.WriteLine(sender.ToString());

        process.Start();
        process.WaitForExit();
    }
}