using Dova.Tools.JavaClassStructureGenerator;

// Step 1. Clone repository locally -> https://github.com/openjdk/jdk.git
// Step 2. Set JdkDirectoryPath as full path to "jdk" directory as 1st parameter
// Step 3. Set OutputDirectoryPath as full output path where to generate C# classes as 2nd parameter
// Step 4. Set TempDirPath as path to temporary directory
// Step 5. Set JavaToolPath as path to java class generating tool (.jar)

var generatorConfig = new GeneratorConfiguration
{
    JdkDirectoryPath = args[0],
    OutputDirectoryPath = args[1],
    TempDirPath = args[2],
    JavaClassDefinitionGeneratorPath = args[3],
};

var generator = new StructureGenerator(generatorConfig);

generator.Run();