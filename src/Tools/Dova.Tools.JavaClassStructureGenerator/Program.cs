using Dova.Tools.JavaClassStructureGenerator.Generators;
using Dova.Tools.JavaClassStructureGenerator.Models;

var model = new JavaClassDetailsModel();
var generator = new StructureGenerator(model);

generator.Run();