using Dova.Tools.JavaClassStructureGenerator;

// Step 1. Clone repository locally -> https://github.com/openjdk/jdk.git
// Step 2. Set SourcesDirectoryPath as full path to "jdk" directory as 1st parameter (or other directory with sources)
// Step 3. Set OutputDirectoryPath as full output path where to generate C# classes as 2nd parameter
// Step 4. Set TempDirPath as path to temporary directory
// Step 5. Set JavaToolPath as path to java class generating tool (.jar)

var generatorConfig = new GeneratorConfiguration
{
    SourcesDirectoryPath = args[0], // i.e.: /home/<user>/Dev/Java/jdk
    OutputDirectoryPath = args[1], // i.e.: /home/<user>/Dev/Dotnet/Dova/src/Dova.JDK
    TempDirPath = args[2], // i.e.: /tmp
    JavaClassDefinitionGeneratorPath = args[3], // i.e.: /home/<user>/Dev/Dotnet/Dova/src/Tools/Dova.Tools.Java.ClassGenerator/build/libs/Dova.Tools.Java.ClassGenerator-0.1.jar
};

var generator = new StructureGenerator(generatorConfig);

await generator.RunAsync();