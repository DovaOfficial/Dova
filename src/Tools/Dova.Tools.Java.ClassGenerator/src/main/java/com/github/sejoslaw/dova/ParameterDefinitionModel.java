package com.github.sejoslaw.dova;

import java.util.ArrayList;
import java.util.Collection;

public class ParameterDefinitionModel {
    public String type;
    public String name;
    public Collection<TypeParameterModel> typeParameterModels = new ArrayList<>();
}
