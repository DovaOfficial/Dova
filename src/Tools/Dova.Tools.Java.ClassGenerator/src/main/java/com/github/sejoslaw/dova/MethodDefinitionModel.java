package com.github.sejoslaw.dova;

import java.util.ArrayList;
import java.util.Collection;

public class MethodDefinitionModel {
    public String Modifiers;
    public String ReturnType;
    public String MethodName;
    public Collection<ParameterDefinitionModel> ParameterModels = new ArrayList<>();
    public Collection<TypeParameterModel> TypeParameterModels = new ArrayList<>();
}
