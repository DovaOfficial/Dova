using Dova.Tools.Readers.Models;

namespace Dova.Tools.Readers;

/// <summary>
/// Used for reading Java classes.
/// </summary>
/// <author>
/// https://github.com/Sejoslaw/Dova
/// </author>
public partial class JavaClassReader
{
    public virtual JavaClassDefinitionModel Read(FileInfo javaFile)
    {
        var lines = File.ReadAllLines(javaFile.FullName);

        var model = new JavaClassDefinitionModel
        {
            Package = GetPackage(javaFile, lines),
            Imports = GetImports(javaFile, lines).ToArray(),
        };
        
        return model;
    }

    protected virtual string? GetPackage(FileInfo javaFile, string[] lines)
    {
        var line = lines.FirstOrDefault(x => x.StartsWith("package") && x.EndsWith(";"));
        line = line?.Trim().Split(" ")[1].Replace(";", "");

        return line;
    }
    
    protected virtual IEnumerable<string> GetImports(FileInfo javaFile, string[] lines)
    {
        var importLines = lines
            .Where(x => x.Contains("import ") && x.Contains(";"))
            .Select(x => x.Trim())
            .Where(x => x.StartsWith("import ") && x.EndsWith(";"))
            .ToList();

        foreach (var importLine in importLines)
        {
            var import = importLine.Split(" ")[1].Replace(";", "");
            
            yield return import;
        }
    }
}