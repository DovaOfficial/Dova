package com.github.sejoslaw.dova;

import java.util.ArrayList;
import java.util.Collection;

public class ClassDetailsDefinitionModel {
    public String PackageName;
    public String ClassName;
    public boolean IsEnum;
    public String Modifiers;
    public Collection<TypeParameterModel> TypeParameterModels = new ArrayList<>();
}