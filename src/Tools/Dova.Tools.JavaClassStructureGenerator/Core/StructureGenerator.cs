namespace Dova.Tools.JavaClassStructureGenerator.Core;

internal static class StructureGenerator
{
    public static void Run(GeneratorConfiguration config)
    {
        if (config.ForceGenerateJavaDefinitions)
        {
            ToolRunner.Run(config.JavaClassDefinitionGeneratorPath, config.TempDirPath, config.JavaModuleFinderPaths);
        }

        var definitionFiles = GetDefinitionFiles(config.TempDirPath);

        var parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = 20
        };
        
        Parallel.ForEach(definitionFiles, parallelOptions, definitionFile => DefinitionFileProcessor.Process(config, definitionFile));
    }

    private static IEnumerable<FileInfo> GetDefinitionFiles(string path)
    {
        var dir = new DirectoryInfo(path);

        var definitionFilesInDirectory = dir
            .GetFiles()
            .Where(fileInfo => fileInfo.Extension.Equals(".dova"))
            .ToList();

        foreach (var subDir in dir.GetDirectories())
        {
            var definitionFilesInSubDir = GetDefinitionFiles(subDir.FullName);
            definitionFilesInDirectory.AddRange(definitionFilesInSubDir);
        }

        return definitionFilesInDirectory;
    }
}