namespace Dova.Tools.Readers.Models;

public class PackageModel
{
    /// <summary>
    /// Package line
    /// </summary>
    public string Package { get; }
    
    /// <summary>
    /// Rest of the lines
    /// </summary>
    public IEnumerable<string> Lines { get; }

    public PackageModel(string package, IEnumerable<string> lines)
    {
        Package = package;
        Lines = lines;
    }
}