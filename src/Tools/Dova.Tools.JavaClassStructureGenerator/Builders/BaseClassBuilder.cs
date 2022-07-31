using Dova.Common;
using Dova.Tools.JavaClassStructureGenerator.Models;

namespace Dova.Tools.JavaClassStructureGenerator.Builders;

internal class BaseClassBuilder : AbstractBuilder
{
    public override IEnumerable<string> Build(FileInfo outputFile, ClassDefinitionModel model, int tabs = 0)
    {
        var baseClass = JavaObjectFullName;
        
        if (model.ClassDetailsModel.IsInterface 
            && (string.IsNullOrWhiteSpace(model.BaseClassModel.Name)  
                || model.BaseClassModel.Name.Equals(baseClass)))
        {
            baseClass = nameof(IJavaObject);
        }
        else if (string.IsNullOrWhiteSpace(model.BaseClassModel.Name))
        {
            baseClass = nameof(JavaObject);
        }
        else
        {
            baseClass = model.BaseClassModel.Name;
        }
        
        baseClass = CleanJavaClassName(baseClass);
        
        yield return AppendLine($": {baseClass}", tabs + 1);
    }
}