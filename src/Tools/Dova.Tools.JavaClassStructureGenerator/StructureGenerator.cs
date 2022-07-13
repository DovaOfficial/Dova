namespace Dova.Tools.JavaClassStructureGenerator;

internal class StructureGenerator
{
    private GeneratorConfiguration Config { get; }
    private JavaFileFinder Finder { get; }

    private JavaClassDefinitionGenerator DefinitionGenerator { get; }
    // private CSharpClassGenerator ClassGenerator { get; }

    public StructureGenerator(GeneratorConfiguration config)
    {
        Config = config;

        Finder = new(Config.JdkDirectoryPath);
        DefinitionGenerator = new(Config.JavaClassDefinitionGeneratorPath);
        // ClassGenerator = new(Config.OutputDirectoryPath);
    }

    public void Run()
    {
        Finder.OnJavaFileFound((javaModuleDir, javaPackageDir, javaFile) =>
        {
            var tempOutputPath = javaFile.FullName.Replace(javaModuleDir.FullName, "");
            var tempOutputPathFull = $"{Config.TempDirPath}/{javaModuleDir.Name}{tempOutputPath}.json";

            var javaClassFullName = tempOutputPath
                .Replace("/share/classes/", "")
                .Replace("/", ".")
                .Replace(".java", "");

            DefinitionGenerator.Generate(tempOutputPathFull, javaClassFullName);

            var javaClassDefinitionModel = JavaClassDefinitionReader.Read(tempOutputPathFull);

            // ClassGenerator.Generate(javaModuleDir, javaClassDefinitionModel);
        });

        Finder.Run();
    }
}