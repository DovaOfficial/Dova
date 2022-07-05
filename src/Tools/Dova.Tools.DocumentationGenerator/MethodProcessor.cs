namespace Dova.Tools.DocumentationGenerator;

public class MethodProcessor
{
    private readonly string _methodName;
    private readonly string _methodSignature;
    private readonly string? _htmlDefinition;

    private ICollection<string> Lines { get; } = new List<string>();
    private string ModifiedMethodSignature { get; set; }

    public MethodProcessor(string methodName, string methodSignature, string? htmlDefinition)
    {
        _methodName = methodName;
        _methodSignature = methodSignature;
        _htmlDefinition = htmlDefinition;

        ModifiedMethodSignature = _methodSignature;
    }

    public void Parse()
    {
        var headers = _htmlDefinition.Split("<h4>");

        for (var i = 1; i < headers.Length; ++i)
        {
            var header = headers[i];
            var headerName = header.Split(":")[0];

            try
            {
                switch (headerName)
                {
                    case "PARAMETERS":
                        ProcessParameters(headerName, header);
                        break;
                    default:
                        ProcessStandardHeader(headerName, header);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    public void Save()
    {
        Lines.Add(ModifiedMethodSignature);
        Lines.Add(string.Empty);
        
        File.AppendAllLines("OutputMethods.txt", Lines);
    }

    private void ProcessStandardHeader(string headerName, string header)
    {
        var parts = header.Split("<p>");
        var startIndex = header.Contains("<p>") ? 1 : 0;
        
        for (int i = startIndex; i < parts.Length; ++i)
        {
            var textWithEnd = parts[i];
            
            var text = header.Contains("<p>")
                ? textWithEnd.Split("</p>")[0]
                : textWithEnd.Split("</h4>")[1];
            
            var headerNameLower = headerName.ToLower();
        
            var line = $"/// <{headerNameLower}>{text}</{headerNameLower}>";
            line = line.Replace(Environment.NewLine, " ");
        
            Lines.Add(line);
        }
    }

    private void ProcessParameters(string headerName, string header)
    {
        var parts = header.Split("<p>");
        var variables = new Dictionary<int, string>();
        
        for (int i = 2; i < parts.Length; ++i)
        {
            var textWithEnd = parts[i];
            var text = textWithEnd.Split("</p>")[0];
            var wrappedTextIndex = text.Contains("<code>") || text.Contains("<tt>") ? 1 : 0;
            
            var dividedText = text.Contains("<code>")
                ? text.Split("<code>")[wrappedTextIndex].Split("</code>")
                : text.Split("<tt>")[wrappedTextIndex].Split("</tt>");
            
            var paramName = dividedText[0];

            if (char.IsUpper(paramName[0]) || dividedText.Length < 2)
            {
                continue;
            }
            
            var paramTextArray = dividedText[1]
                .Replace(": ", "")
                .Select((x, index) => index == 0 ? char.ToUpper(x) : x)
                .ToList();
            var paramText = string.Concat(paramTextArray);
        
            var line = $"/// <param name=\"{paramName}\">{paramText}</param>";
            line = line.Replace(Environment.NewLine, " ");

            Lines.Add(line);
            
            variables.Add(i, paramName);
        }
        
        var mmsParts = ModifiedMethodSignature.Split(" ");

        foreach (var kvp in variables)
        {
            var paramIndex = kvp.Key - 2;
            var mmsVariableIndex = 2 + paramIndex * 2;

            if (mmsVariableIndex == mmsParts.Length - 1)
            {
                mmsParts[mmsVariableIndex] = $"{kvp.Value});";
            }
            else
            {
                mmsParts[mmsVariableIndex] = $"{kvp.Value},";
            }
        }

        var newMms = string.Join(" ", mmsParts).Replace("  ", " ");
        ModifiedMethodSignature = newMms;
    }
}