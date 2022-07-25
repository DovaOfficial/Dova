namespace Dova.Tools.JavaClassStructureGenerator;

internal class JavaFileReader
{
    public string JavaPackage { get; private set; }
    public string JavaClassName { get; private set; }
    
    public JavaFileReader(FileSystemInfo javaFile)
    {
        var lines = File.ReadAllLines(javaFile.FullName);

        var trimmedLines = lines
            .Select(x => x.Trim())
            .ToList();

        var isInComment = false;

        foreach (var line in trimmedLines)
        {
            if (line.Contains("/*")) // Comment starts
            {
                isInComment = true;
                continue;
            }

            if (line.Contains("*/")) // Comment ends
            {
                isInComment = false;
                continue;
            }

            if (isInComment) // Skip comment line
            {
                continue;
            }

            if (line.StartsWith("package ") 
                && line.EndsWith(";"))
            {
                ReadJavaPackage(line);
                continue;
            }

            if (JavaConstants.Types.Any(x => line.Contains($"{x} ")))
            {
                ReadJavaClassName(line);
                continue;
            }

            if (!string.IsNullOrWhiteSpace(JavaPackage) 
                && !string.IsNullOrWhiteSpace(JavaClassName))
            {
                return;
            }
        }

        throw new ArgumentException("Error when reading Java class: " + javaFile.FullName);
    }

    private void ReadJavaPackage(string line)
    {
        JavaPackage = line
            .Replace("package ", "")
            .Replace(";", "");
    }

    private void ReadJavaClassName(string line)
    {
        var parsedLine = JavaConstants.Modifiers.Aggregate(line, (current, modifier) => current.Replace(modifier, ""));
        parsedLine = JavaConstants.Types.Aggregate(parsedLine, (current, type) => current.Replace(type, ""));
        parsedLine = parsedLine.Trim();

        JavaClassName = parsedLine.Contains("<") 
            ? parsedLine.Split("<")[0] 
            : parsedLine.Split(" ")[0];
    }
}