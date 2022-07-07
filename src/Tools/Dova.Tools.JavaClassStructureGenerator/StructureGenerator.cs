using Dova.Tools.Readers;

namespace Dova.Tools.JavaClassStructureGenerator;

internal class StructureGenerator
{
    private GeneratorConfiguration Config { get; }
    private JavaClassReader Reader { get; }

    public StructureGenerator(GeneratorConfiguration config)
    {
        Config = config;
        Reader = new();
    }

    public void Run()
    {
        var jdkSrcPath = Path.Combine(Config.JdkDirectoryPath, "src");

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
            
            ProcessJavaPackage(javaModuleDir.Name, new DirectoryInfo(javaPackageStartPath));
        }
    }

    private void ProcessJavaPackage(string javaModuleName, DirectoryInfo javaPackageDir)
    {
        var javaFiles = javaPackageDir.GetFiles()
            .Where(x => x.Extension.Equals(".java") && !x.Name.Equals("module-info.java"))
            .OrderBy(x => x.Name)
            .ToList();

        javaFiles.ForEach(javaFile =>
        {
            var javaClassDefinitionModel = Reader.Read(javaFile);
            CSharpClassGenerator.Run(Config.OutputDirectoryPath, javaModuleName, javaClassDefinitionModel);
        });

        var javaSubPackagesPaths = Directory.GetDirectories(javaPackageDir.FullName)
            .OrderBy(x => x)
            .ToList();
        
        javaSubPackagesPaths.ForEach(javaSubPackagesPath => ProcessJavaPackage(javaModuleName, new DirectoryInfo(javaSubPackagesPath)));
    }
}