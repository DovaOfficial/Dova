namespace Dova.Tools.JavaClassStructureGenerator;

internal static class StructureGenerator
{
    public static void Run(GeneratorConfiguration config)
    {
        var javaFiles = GetJavaSourceFiles(config.SourcesDirectoryPath);
        
        Parallel.ForEach(javaFiles, javaFile => new JavaFileProcessor().Run(config, javaFile));
    }

    private static IEnumerable<FileInfo> GetJavaSourceFiles(string path)
    {
        var dir = new DirectoryInfo(path);
        
        var javaFiles = dir.GetFiles()
            .Where(x => x.Extension.Equals(".java"))
            .Where(x => !x.Name.Contains("-")) // module-info.java && package-info.java
            .ToList();

        var subDirs = dir.GetDirectories();

        foreach (var subDir in subDirs)
        {
            var subDirJavaFiles = GetJavaSourceFiles(subDir.FullName);
            javaFiles.AddRange(subDirJavaFiles);
        }

        return javaFiles;
    }
}