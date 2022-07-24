namespace Dova.Tools.JavaClassStructureGenerator;

internal class JavaFileFinder
{
    private string SourcesDirectoryPath { get; }
    private Action<DirectoryInfo, DirectoryInfo, FileInfo> Callback { get; set; }

    public JavaFileFinder(string sourcesDirectoryPath)
    {
        SourcesDirectoryPath = sourcesDirectoryPath;
    }

    public void OnJavaFileFound(Action<DirectoryInfo, DirectoryInfo, FileInfo> callback) => 
        this.Callback = callback;

    public async Task RunAsync()
    {
        var javaModulePaths = Directory.GetDirectories(SourcesDirectoryPath)
            .OrderBy(x => x)
            .ToList();

        var tasks = javaModulePaths
            .Select(javaModulePath => 
                Task.Run(() => 
                {
                    var javaModuleDir = new DirectoryInfo(javaModulePath);

                    // TODO: Maybe remove this if (???) - allow for generating sources from any directory
                    if (javaModuleDir.Name.StartsWith("j")) // java, jdk
                    {
                        ProcessJavaPackage(javaModuleDir, new DirectoryInfo(javaModuleDir.FullName));
                    }
                }))
            .ToArray();

        Task.WaitAll(tasks);
    }
    
    private void ProcessJavaPackage(DirectoryInfo javaModuleDir, DirectoryInfo javaPackageDir)
    {
        var javaFiles = javaPackageDir.GetFiles()
            .Where(x => x.Extension.Equals(".java")
                        && !x.Name.Equals("module-info.java")
                        && !x.Name.Equals("package-info.java"))
            .OrderBy(x => x.Name)
            .ToList();

        var tasks = javaFiles
            .Select(javaFile => 
                Task.Run(() => Callback?.Invoke(javaModuleDir, javaPackageDir, javaFile)))
            .ToArray();

        Task.WaitAll(tasks);

        var javaSubPackagesPaths = Directory.GetDirectories(javaPackageDir.FullName)
            .OrderBy(x => x)
            .ToList();

        tasks = javaSubPackagesPaths
            .Select(javaSubPackagesPath =>
                Task.Run(() => ProcessJavaPackage(javaModuleDir, new DirectoryInfo(javaSubPackagesPath))))
            .ToArray();

        Task.WaitAll(tasks);
    }
}