using System.Diagnostics;
using System.Text.Json;
using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator.Core;

internal static class ClassGenerator
{
    public static void Generate(ClassGeneratorConfig config)
    {
        if (config.ForceGenerateJavaDefinitions)
        {
            GenerateJavaClassDefinitions(config.JavaClassDefinitionGeneratorPath, config.TempDirPath, config.JavaModuleFinderPaths);
        }

        var definitionFiles = GetDefinitionFiles(config.TempDirPath);

        var parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = 20
        };
        
        Parallel.ForEach(definitionFiles, parallelOptions, definitionFile => Process(config, definitionFile));
    }
    
    public static void GenerateJavaClassDefinitions(string classDefinitionGeneratorPath, string tempOutputPathFull, string javaModuleFinderPaths = "")
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

    public static IEnumerable<FileInfo> GetDefinitionFiles(string path)
    {
        var dir = new DirectoryInfo(path);

        var definitionFilesInDirectory = dir
            .GetFiles()
            .Where(fileInfo => fileInfo.Extension.Equals(".dova") && fileInfo.Length > 0)
            .ToList();

        foreach (var subDir in dir.GetDirectories())
        {
            var definitionFilesInSubDir = GetDefinitionFiles(subDir.FullName);
            definitionFilesInDirectory.AddRange(definitionFilesInSubDir);
        }

        return definitionFilesInDirectory;
    }
    
    public static void Process(ClassGeneratorConfig config, FileInfo definitionFile)
    {
        var javaClassDefinitionModel = ReadClassDefinition(definitionFile.FullName);

        var outputPathFull = definitionFile.FullName
            .Replace(config.TempDirPath, config.OutputDirectoryPath)
            .Replace(".class.dova", ".cs");
        
        CSharpClassGenerator.Generate(outputPathFull, javaClassDefinitionModel);
    }
    
    public static ClassDefinitionModel ReadClassDefinition(string path)
    {
        var fileContent = File.ReadAllText(path);
        var model = JsonSerializer.Deserialize<ClassDefinitionModel>(fileContent);
        
        return model ?? throw new ArgumentException("Cannot read definition file at: " + path);
    }
}