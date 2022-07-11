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

    public void Run()
    {
        var jdkSrcPath = Path.Combine(JdkDirectoryPath, "src");
        
        var javaModulePaths = Directory.GetDirectories(jdkSrcPath)
            .OrderBy(x => x)
            .ToList();
        
        foreach (var javaModulePath in javaModulePaths)
        {
            var javaModuleDir = new DirectoryInfo(javaModulePath);
        
            if (!javaModuleDir.Name.StartsWith("j")) // java, jdk
            {
                continue;
            }
        
            var javaPackageStartPath = Path.Combine(javaModuleDir.FullName, "share", "classes");
            
            ProcessJavaPackage(javaModuleDir, new DirectoryInfo(javaPackageStartPath));
        }
    }
    
    private void ProcessJavaPackage(DirectoryInfo javaModuleDir, DirectoryInfo javaPackageDir)
    {
        var javaFiles = javaPackageDir.GetFiles()
            .Where(x => x.Extension.Equals(".java") && !x.Name.Equals("module-info.java"))
            .OrderBy(x => x.Name)
            .ToList();

        javaFiles.ForEach(javaFile => Callback?.Invoke(javaModuleDir, javaPackageDir, javaFile));

        var javaSubPackagesPaths = Directory.GetDirectories(javaPackageDir.FullName)
            .OrderBy(x => x)
            .ToList();
        
        javaSubPackagesPaths.ForEach(javaSubPackagesPath => ProcessJavaPackage(javaModuleDir, new DirectoryInfo(javaSubPackagesPath)));
    }
}