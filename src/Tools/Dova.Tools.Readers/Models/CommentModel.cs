namespace Dova.Tools.Readers.Models;

public class CommentModel
{
    /// <summary>
    /// All comment lines
    /// </summary>
    public IEnumerable<string> Comments { get; }
    
    /// <summary>
    /// Rest of the lines
    /// </summary>
    public IEnumerable<string> Lines { get; }

    public CommentModel(IEnumerable<string> comments, IEnumerable<string> lines)
    {
        Comments = comments;
        Lines = lines;
    }
}