using Dova.Tools.JavaClassStructureGenerator;

// Step 1. Clone repository locally -> https://github.com/openjdk/jdk.git
// Step 2. Set full path to "jdk" directory as 1st parameter
// Step 3. Set full output path where to generate C# classes as 2nd parameter

var generatorConfig = new GeneratorConfiguration
{
    JdkDirectoryPath = args[0],
    OutputDirectoryPath = args[1]
};

var generator = new StructureGenerator(generatorConfig);

generator.Run();