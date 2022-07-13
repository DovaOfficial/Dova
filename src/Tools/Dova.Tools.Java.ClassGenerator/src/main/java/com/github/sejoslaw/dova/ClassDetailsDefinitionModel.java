package com.github.sejoslaw.dova;

import java.util.ArrayList;
import java.util.Collection;

public class ClassDetailsDefinitionModel {
    public String packageName;
    public String className;
    public boolean isEnum;
    public String modifiers;
    public Collection<TypeParameterModel> typeParameterModels = new ArrayList<>();
}