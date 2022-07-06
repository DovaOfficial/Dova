namespace Dova.Tools.JavaClassStructureGenerator;

internal static class JavaClassDetailsParser
{
    public static JavaClassDescriptionModel Parse(JavaClassDetailsModel detailsModel)
    {
        var descriptionModel = new JavaClassDescriptionModel();

        descriptionModel.Type = GetDefinitionType(detailsModel.JavaClassDetails);
        descriptionModel.ParentClass = GetParentClass(detailsModel.JavaClassDetails);
        descriptionModel.Interfaces = GetInterfaces(detailsModel.JavaClassDetails);
        // descriptionModel.ClassComment = GetClassComment(detailsModel.JavaClassDetails);
        // descriptionModel.Constructors = GetConstructors(detailsModel.JavaClassDetails);
        // descriptionModel.Fields = GetFields(detailsModel.JavaClassDetails);
        // descriptionModel.Methods = GetMethods(detailsModel.JavaClassDetails);

        return descriptionModel;
    }

    private static IEnumerable<string> GetInterfaces(string html)
    {
        const string allImplementedInterfaces = "All Implemented Interfaces:";
        
        if (!html.Contains(allImplementedInterfaces))
        {
            return Array.Empty<string>();
        }

        var interfaceNodes = html
            .Split("All Implemented Interfaces:")[1]
            .Split("<dd>")[1]
            .Split("</dd>")[0]
            .Split("<a href=\"");

        var interfaceNames = new HashSet<string>();

        foreach (var node in interfaceNodes)
        {
            const string namePrefix = "\">";
            
            if (!node.Contains(namePrefix))
            {
                continue;
            }

            var nodeParts = node.Split(namePrefix);
            var interfacePackage = nodeParts[0].Split("interface in ")[1];
            var interfaceName = nodeParts[1].Split("</a>")[0];
            var interfaceNameWithPackage = $"{interfacePackage}.{interfaceName}";

            interfaceNames.Add(interfaceNameWithPackage);
        }

        return interfaceNames;
    }

    private static string GetDefinitionType(string html) => 
        html.Split("<span class=\"modifiers\">")[1].Split("</span>")[0].Trim();

    private static string GetParentClass(string html)
    {
        var inheritanceTree = html.Split("<div class=\"inheritanc");
        var parentIndex = inheritanceTree.Length - 2;
        var parentClass = inheritanceTree[parentIndex].Split("\">")[2].Split("</a>")[0];

        return parentClass;
    }
}