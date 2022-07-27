namespace Dova.Tools.JavaClassStructureGenerator;

internal class JavaFileReader
{
    public string JavaPackage { get; }
    public string JavaClassName { get; }
    
    public JavaFileReader(FileSystemInfo javaFile)
    {
        JavaClassName = Path.GetFileNameWithoutExtension(javaFile.FullName);
        
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

            if (isInComment // Skip comment line
                || string.IsNullOrWhiteSpace(line)) // Skip empty line
            {
                continue;
            }

            if (line.StartsWith("package ")
                && line.Contains(";"))
            {
                JavaPackage = line
                    .Replace("package ", "")
                    .Split(";")[0];
                
                continue;
            }

            if (!string.IsNullOrWhiteSpace(JavaPackage) 
                && !string.IsNullOrWhiteSpace(JavaClassName))
            {
                return;
            }
        }
        
        Console.WriteLine($"No package signature found in file: {javaFile.FullName}");
        
        JavaPackage = "UnknownPackage";
    }
}