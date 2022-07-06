namespace Dova.Tools.JavaClassStructureGenerator;

internal static class JavaClassDetailsParser
{
    public static JavaClassDescriptionModel Parse(JavaClassDetailsModel detailsModel)
    {
        var descriptionModel = new JavaClassDescriptionModel();

        descriptionModel.Type = GetDefinitionType(detailsModel.JavaClassDetails);
        descriptionModel.ParentClass = GetParentClass(detailsModel.JavaClassDetails);

        return descriptionModel;
    }

    private static string GetDefinitionType(string html) => 
        html.Split("<h1 title=\"")[1].Split(" ")[0];

    private static string GetParentClass(string html)
    {
        var inheritanceTree = html.Split("<div class=\"inheritanc");
        var parentIndex = inheritanceTree.Length - 2;
        var parentClass = inheritanceTree[parentIndex].Split("\">")[2].Split("</a>")[0];

        return parentClass;
    }
}