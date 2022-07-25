namespace Dova.Tools.JavaClassStructureGenerator;

internal static class StructureGenerator
{
    public static void Run(GeneratorConfiguration config)
    {
        var directoriesToSkip = config.SkipDirectories.Split(",");
        var javaFiles = GetJavaSourceFiles(directoriesToSkip, config.SourcesDirectoryPath);

        Parallel.ForEach(javaFiles, javaFile => new JavaFileProcessor().Run(config, javaFile));
    }

    private static IEnumerable<FileInfo> GetJavaSourceFiles(string[] directoriesToSkip, string path)
    {
        var dir = new DirectoryInfo(path);
        
        var javaFiles = dir.GetFiles()
            .Where(fileInfo => fileInfo.Extension.Equals(".java"))
            .Where(fileInfo => !fileInfo.Name.Contains("-")) // module-info.java && package-info.java
            .ToList();

        var subDirs = dir.GetDirectories()
            .Where(dirInfo => directoriesToSkip.All(dirToSkip => !dirInfo.Name.Equals(dirToSkip)))
            .ToArray();

        foreach (var subDir in subDirs)
        {
            var subDirJavaFiles = GetJavaSourceFiles(directoriesToSkip, subDir.FullName);
            javaFiles.AddRange(subDirJavaFiles);
        }

        return javaFiles;
    }
}