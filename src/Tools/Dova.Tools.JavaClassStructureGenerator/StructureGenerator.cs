namespace Dova.Tools.JavaClassStructureGenerator;

internal class StructureGenerator
{
    private GeneratorConfiguration Config { get; }
    private JavaFileFinder Finder { get; }
    private JavaClassDefinitionGenerator DefinitionGenerator { get; }
    private CSharpClassGenerator ClassGenerator { get; }

    public StructureGenerator(GeneratorConfiguration config)
    {
        Config = config;

        Finder = new(Config.SourcesDirectoryPath);
        DefinitionGenerator = new(Config.JavaClassDefinitionGeneratorPath);
        ClassGenerator = new(Config.OutputDirectoryPath);
    }

    public async Task RunAsync()
    {
        Finder.OnJavaFileFound((javaModuleDir, javaPackageDir, javaFile) =>
        {
            var tempOutputPath = javaFile.FullName.Replace(javaModuleDir.FullName, "");
            var tempOutputPathFull = Path.Combine(Config.TempDirPath, $"{javaModuleDir.Name}{tempOutputPath}.json");

            var javaClassFullName = tempOutputPath
                .Split("/classes/")[^1]
                .Replace("/", ".")
                .Replace(".java", "");

            DefinitionGenerator.Generate(tempOutputPathFull, javaClassFullName);

            if (!File.Exists(tempOutputPathFull))
            {
                Console.WriteLine($"File not found: {tempOutputPathFull}");
                return;
            }

            var javaClassDefinitionModel = JavaClassDefinitionReader.Read(tempOutputPathFull);

            var javaPackageOutputPath = javaPackageDir.FullName
                .Replace(javaModuleDir.FullName, "")
                .Split("/classes/")[^1];
            
            var javaOutputPathFull = Path.Combine(javaModuleDir.Name, javaPackageOutputPath);

            ClassGenerator.Generate(javaOutputPathFull, javaClassDefinitionModel);
        });

        await Finder.RunAsync();
    }
}