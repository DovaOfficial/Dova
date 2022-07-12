package com.github.sejoslaw.dova;

import java.util.ArrayList;
import java.util.Collection;

public class MethodDefinitionModel {
    public String modifiers;
    public String returnType;
    public String methodName;
    public Collection<ParameterDefinitionModel> parameterModels = new ArrayList<>();
}
