package com.github.sejoslaw.dova;

import java.util.ArrayList;
import java.util.Collection;

public class ClassDefinitionModel {
    public String packageName;
    public String className;
    public String baseClass;
    public boolean isEnum;
    public String modifiers;
    public Collection<ConstructorDefinitionModel> constructorModels = new ArrayList<>();
    public Collection<ClassDefinitionModel> innerClassModels = new ArrayList<>();
}
