using System.Text.RegularExpressions;
using Dova.Tools.Readers.Models;

namespace Dova.Tools.Readers;

/// <summary>
/// Used for reading Java classes.
/// </summary>
/// <author>
/// https://github.com/Sejoslaw/Dova
/// </author>
public class JavaClassReader
{
    public virtual JavaClassDefinitionModel Read(FileInfo javaFile) => 
        Read(File.ReadAllLines(javaFile.FullName));

    public virtual JavaClassDefinitionModel Read(string[] javaFileLines)
    {
        var cleanedLines = CleanLines(javaFileLines);
        
        var fileComment = GetComment(cleanedLines);
        var package = GetPackage(fileComment.Lines);
        var imports = GetImports(package.Lines);
        var classComment = GetComment(imports.Lines);

        return new JavaClassDefinitionModel
        {
            Package = package.Package,
            Imports = imports.Imports,
            ClassComment = classComment.Comments
        };
    }

    protected virtual IEnumerable<string> CleanLines(string[] javaFileLines)
    {
        var lines = javaFileLines
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Trim())
            .ToArray();

        return lines;
    }

    protected virtual CommentModel GetComment(IEnumerable<string> javaFileLines)
    {
        if (!javaFileLines.ElementAt(0).StartsWith("/*"))
        {
            return new CommentModel(Array.Empty<string>(), javaFileLines);
        }
        
        var comment = new List<string>();
        
        var lines = javaFileLines
            .SkipWhile(x =>
            {
                comment.Add(x);
                return !Regex.Replace(x, @"\s+", "").Equals("*/");
            })
            .Skip(1)
            .ToList();

        return new CommentModel(comment, lines);
    }
    
    protected virtual PackageModel GetPackage(IEnumerable<string> javaFileLines)
    {
        var packageLine = javaFileLines.ElementAt(0);
        
        if (!packageLine.StartsWith("package "))
        {
            // Sub-class / Inner class
            return new PackageModel(string.Empty, javaFileLines);
        }
        
        var lines = javaFileLines.Skip(1).ToList();

        return new PackageModel(packageLine, lines);
    }
    
    protected virtual ImportsModel GetImports(IEnumerable<string> javaFileLines)
    {
        const string importPrefix = "import ";

        if (!javaFileLines.ElementAt(0).StartsWith(importPrefix))
        {
            // Sub-class / Inner class
            return new ImportsModel(Array.Empty<string>(), javaFileLines);
        }

        var imports = new List<string>();

        var lines = javaFileLines
            .SkipWhile(x =>
            {
                var isImportLine = x.StartsWith(importPrefix);

                if (isImportLine)
                {
                    imports.Add(x);
                }
                
                return isImportLine;
            })
            .ToList();

        return new ImportsModel(imports, lines);
    }
}