using System.Text;

namespace Dova.Tools.JavaClassStructureGenerator;

internal static class GenericBodyReader
{
    public static IEnumerable<string> Read(string genericBody)
    {
        var body = genericBody.Replace(" ", "");
        var depth = 0;
        var sb = new StringBuilder();

        foreach (var bodyChar in body)
        {
            switch (bodyChar)
            {
                case '<':
                    sb.Append(bodyChar);
                    depth++;
                    break;
                case '>':
                    sb.Append(bodyChar);
                    depth--;
                    break;
                case var ch when ch.Equals(',') && depth == 0:
                    yield return sb.ToString();
                    sb.Clear();
                    break;
                default:
                    sb.Append(bodyChar);
                    break;
            }
        }
        
        yield return sb.ToString();
        
        sb.Clear();
    }
}