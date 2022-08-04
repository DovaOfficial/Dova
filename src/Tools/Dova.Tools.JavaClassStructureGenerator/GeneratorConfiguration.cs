namespace Dova.Tools.JavaClassStructureGenerator;

internal class GeneratorConfiguration
{
    public string OutputDirectoryPath { get; set; }
    public string TempDirPath { get; set; }
    public string JavaClassDefinitionGeneratorPath { get; set; }
    public string JavaModuleFinderPaths { get; set; }
    public bool ForceGenerateJavaDefinitions { get; set; }
}