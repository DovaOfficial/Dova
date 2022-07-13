package com.github.sejoslaw.dova;

import java.util.ArrayList;
import java.util.Collection;

public class ClassDefinitionModel {
    public ClassDetailsDefinitionModel classDetailsModel = new ClassDetailsDefinitionModel();
    public BaseClassDefinitionModel baseClassModel = new BaseClassDefinitionModel();
    public Collection<InterfaceDefinitionModel> interfaceModels = new ArrayList<>();
    public Collection<ConstructorDefinitionModel> constructorModels = new ArrayList<>();
    public Collection<FieldDefinitionModel> fieldModels = new ArrayList<>();
    public Collection<MethodDefinitionModel> methodModels = new ArrayList<>();
    public Collection<ClassDefinitionModel> innerClassModels = new ArrayList<>();
}
