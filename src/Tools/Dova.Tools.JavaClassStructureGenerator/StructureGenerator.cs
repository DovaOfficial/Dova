namespace Dova.Tools.JavaClassStructureGenerator;

internal class StructureGenerator
{
    private GeneratorConfiguration Config { get; }
    private JavaFileFinder Finder { get; }
    // private JavaClassDefinitionGenerator ClassDefinitionGenerator { get; }
    // private CSharpClassGenerator ClassGenerator { get; }

    public StructureGenerator(GeneratorConfiguration config)
    {
        Config = config;

        Finder = new(Config.JdkDirectoryPath);
        // ClassDefinitionGenerator = new(Config.JavaClassDefinitionGeneratorPath);
        // ClassGenerator = new(Config.OutputDirectoryPath);
    }

    public void Run()
    {
        Finder.OnJavaFileFind((javaModuleDir, javaPackageDir, javaFile) =>
        {
            var tempOutputPath = javaFile.FullName.Replace(javaModuleDir.FullName, "");
            var tempOutputPathFull = $"{Config.TempDirPath}{tempOutputPath}.gen";

            var javaClassFullName = tempOutputPath
                .Replace("/share/classes/", "")
                .Replace("/", ".")
                .Replace(".java", "");

            // ClassDefinitionGenerator.Run(tempOutputPathFull, javaClassFullName);

            // var javaClassDefinitionModel = JavaClassDefinitionReader.Read(tempOutputPathFull);

            // ClassGenerator.Generate(javaModuleDir, javaClassDefinitionModel);
        });
        
        Finder.Run();
    }
}