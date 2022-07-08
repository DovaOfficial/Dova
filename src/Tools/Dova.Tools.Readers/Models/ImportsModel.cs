namespace Dova.Tools.Readers.Models;

public class ImportsModel
{
    /// <summary>
    /// All found imports
    /// </summary>
    public IEnumerable<string> Imports { get; }
    
    /// <summary>
    /// Rest of the lines
    /// </summary>
    public IEnumerable<string> Lines { get; }

    public ImportsModel(IEnumerable<string> imports, IEnumerable<string> lines)
    {
        Imports = imports;
        Lines = lines;
    }
}