package com.github.sejoslaw.dova;

import java.util.ArrayList;
import java.util.Collection;

public class ClassDefinitionModel {
    public ClassDetailsDefinitionModel ClassDetailsModel = new ClassDetailsDefinitionModel();
    public BaseClassDefinitionModel BaseClassModel = new BaseClassDefinitionModel();
    public Collection<InterfaceDefinitionModel> InterfaceModels = new ArrayList<>();
    public Collection<ConstructorDefinitionModel> ConstructorModels = new ArrayList<>();
    public Collection<FieldDefinitionModel> FieldModels = new ArrayList<>();
    public Collection<MethodDefinitionModel> MethodModels = new ArrayList<>();
    public Collection<ClassDefinitionModel> InnerClassModels = new ArrayList<>();
}
