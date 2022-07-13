using System.Diagnostics;

namespace Dova.Tools.JavaClassStructureGenerator;

internal class JavaClassDefinitionGenerator
{
    private string ClassDefinitionGeneratorPath { get; }

    public JavaClassDefinitionGenerator(string classDefinitionGeneratorPath)
    {
        ClassDefinitionGeneratorPath = classDefinitionGeneratorPath;
    }

    public void Generate(string tempOutputPathFull, string javaClassFullName)
    {
        var process = new Process
        {
            StartInfo =
            {
                FileName = "java",
                Arguments = $"-jar {ClassDefinitionGeneratorPath} {tempOutputPathFull} {javaClassFullName}"
            }
        };

        process.ErrorDataReceived += (sender, e) => throw new Exception(sender.ToString());

        process.Start();
        process.WaitForExit();
    }
}