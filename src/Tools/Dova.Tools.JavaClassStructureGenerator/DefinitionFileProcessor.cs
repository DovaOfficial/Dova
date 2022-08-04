namespace Dova.Tools.JavaClassStructureGenerator;

internal static class DefinitionFileProcessor
{
    public static void Process(GeneratorConfiguration config, FileInfo definitionFile)
    {
        var javaClassDefinitionModel = DefinitionFileReader.Read(definitionFile.FullName);
        
        var outputPathFull = definitionFile.FullName
            .Replace(config.TempDirPath, config.OutputDirectoryPath)
            .Replace(".class.json", ".cs") // TODO: Temporary, will be removed
            .Replace(".class.dova", ".cs");
        
        CSharpClassGenerator.Generate(outputPathFull, javaClassDefinitionModel);
    }
}