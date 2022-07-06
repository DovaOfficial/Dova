using Dova.Tools.JavaClassStructureGenerator;

var model = new JavaClassDetailsModel();
var generator = new StructureGenerator(model);

generator.Run();