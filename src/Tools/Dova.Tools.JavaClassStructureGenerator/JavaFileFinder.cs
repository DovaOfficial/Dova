namespace Dova.Tools.JavaClassStructureGenerator;

internal class JavaFileFinder
{
    private string JdkDirectoryPath { get; }
    private Action<DirectoryInfo, DirectoryInfo, FileInfo> Callback { get; set; }

    public JavaFileFinder(string jdkDirectoryPath)
    {
        JdkDirectoryPath = jdkDirectoryPath;
    }

    public void OnJavaFileFound(Action<DirectoryInfo, DirectoryInfo, FileInfo> callback) => 
        this.Callback = callback;

    public async Task RunAsync()
    {
        var jdkSrcPath = Path.Combine(JdkDirectoryPath, "src");
        
        var javaModulePaths = Directory.GetDirectories(jdkSrcPath)
            .OrderBy(x => x)
            .ToList();

        var tasks = javaModulePaths
            .Select(javaModulePath => 
                Task.Run(() => 
                {
                    var javaModuleDir = new DirectoryInfo(javaModulePath);

                    if (javaModuleDir.Name.StartsWith("j")) // java, jdk
                    {
                        var javaPackageStartPath = Path.Combine(javaModuleDir.FullName, "share", "classes");
                        
                        ProcessJavaPackage(javaModuleDir, new DirectoryInfo(javaPackageStartPath));
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