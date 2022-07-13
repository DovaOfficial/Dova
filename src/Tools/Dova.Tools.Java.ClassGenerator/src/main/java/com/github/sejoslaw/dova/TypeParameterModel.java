package com.github.sejoslaw.dova;

import java.util.ArrayList;
import java.util.Collection;

public class TypeParameterModel {
    public String variableName;
    public String typeName;
    public Collection<BoundDefinitionModel> boundModels = new ArrayList<>();
}
