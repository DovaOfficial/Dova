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
        };
        
        return model;
    }

    protected virtual string? GetPackage(FileInfo javaFile, string[] lines)
    {
        var line = lines.FirstOrDefault(x => x.StartsWith("package") && x.EndsWith(";"));
        line = line?.Trim().Split(" ")[1].Replace(";", "");

        return line;
    }
}