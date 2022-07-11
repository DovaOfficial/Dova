package com.github.sejoslaw.dova;

import java.lang.reflect.Modifier;
import java.lang.reflect.Parameter;
import java.util.Collection;

public class Main {
    public static void main(String[] args) throws ClassNotFoundException {
        var tempOutputPathFull = args[0]; // i.e.: /tmp/<JAVA_MODULE>/share/classes/com/sun/crypto/provider/AESCipher.java.gen
        var javaClassFullName = args[1]; // i.e.: com.sun.crypto.provider.AESCipher

        ProcessClass(tempOutputPathFull, javaClassFullName);
    }

    private static void ProcessClass(String tempOutputPathFull, String javaClassFullName) throws ClassNotFoundException {
        var clazz = Class.forName(javaClassFullName);

        var model = new ClassDefinitionModel();
        ProcessClass(clazz, model);

        ModelWriter.Write(tempOutputPathFull, model);
    }

    private static void ProcessClass(Class<?> clazz, ClassDefinitionModel model) {
        model.packageName = clazz.getPackageName();
        model.className = clazz.getSimpleName();
        model.baseClass = clazz.getSuperclass().getName();
        model.isEnum = clazz.isEnum();

        model.modifiers = GetModifiers(clazz.getModifiers());

        for (var interfaceClass : clazz.getInterfaces()) {
            var interfaceModel = new ClassDefinitionModel();

            ProcessClass(interfaceClass, interfaceModel);

            model.interfaceModels.add(interfaceModel);
        }

        // TODO: GetGenericInfo(clazz, model);

        GetConstructors(clazz, model.constructorModels);
        // TODO: Fields - GetFields(clazz, model.fieldModels);
        // TODO: Methods - GetMethods(clazz, model.methodModels);

        for (Class<?> innerClass : clazz.getDeclaredClasses()) {
            var innerModel = new ClassDefinitionModel();

            ProcessClass(innerClass, innerModel);

            model.innerClassModels.add(innerModel);
        }
    }

    private static String GetModifiers(int mods) {
        return Modifier.toString(mods);
    }

    private static void GetConstructors(Class<?> clazz, Collection<ConstructorDefinitionModel> models) {
        for (var constructor : clazz.getDeclaredConstructors()) {
            var model = new ConstructorDefinitionModel();

            model.modifiers = GetModifiers(constructor.getModifiers());

            GetParameters(constructor.getParameters(), model.parameterModels);

            models.add(model);
        }
    }

    private static void GetParameters(Parameter[] parameters, Collection<ParameterDefinitionModel> models) {
        for (var parameter : parameters) {
            var model = new ParameterDefinitionModel();

            model.name = parameter.getName();
            model.type = parameter.getType().getName();

            models.add(model);
        }
    }
}