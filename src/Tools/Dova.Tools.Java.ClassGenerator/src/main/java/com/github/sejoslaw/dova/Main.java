package com.github.sejoslaw.dova;

import java.lang.reflect.*;
import java.util.Collection;

public class Main {
    public static void main(String[] args) throws ClassNotFoundException {
        var tempOutputPathFull = args[0]; // i.e.: /tmp/<JAVA_MODULE>/share/classes/com/sun/crypto/provider/AESCipher.java.gen
        var javaClassFullName = args[1]; // i.e.: com.sun.crypto.provider.AESCipher

        try {
            ProcessClass(tempOutputPathFull, javaClassFullName);
        } catch(Exception e) {
            System.err.println(e);
            throw e;
        }
    }

    private static void ProcessClass(String tempOutputPathFull, String javaClassFullName) throws ClassNotFoundException {
        var clazz = Class.forName(javaClassFullName);
        var model = new ClassDefinitionModel();

        ProcessClass(clazz, model);

        ModelWriter.Write(tempOutputPathFull, model);
    }

    private static void ProcessClass(Class<?> clazz, ClassDefinitionModel model) {
        GetClassDetails(clazz, model.classDetailsModel);
        GetBaseClass(clazz, model.baseClassModel);
        GetInterfaces(clazz, model.interfaceModels);
        GetConstructors(clazz, model.constructorModels);
        GetFields(clazz, model.fieldModels);
        GetMethods(clazz, model.methodModels);
        GetInnerClasses(clazz, model.innerClassModels);
    }

    private static void GetClassDetails(Class<?> clazz, ClassDetailsDefinitionModel model) {
        model.packageName = clazz.getPackageName();
        model.className = clazz.getSimpleName();
        model.isEnum = clazz.isEnum();
        model.modifiers = GetModifiers(clazz.getModifiers());

        GetTypeParameters(clazz.getTypeParameters(), model.typeParameterModels);
    }

    private static void GetTypeParameters(TypeVariable<? extends Class<?>>[] typeParameters, Collection<TypeParameterModel> models) {
        for (var typeParameter : typeParameters) {
            var model = new TypeParameterModel();

            GetTypeParameter(typeParameter, model);

            models.add(model);
        }
    }

    private static void GetTypeParameters(Type type, Collection<TypeParameterModel> models) {
        if (type instanceof ParameterizedType) {
            var typeParameters = ((ParameterizedType) type).getActualTypeArguments();

            for (var typeParameter : typeParameters) {
                var model = new TypeParameterModel();

                GetTypeParameter(typeParameter, model);

                models.add(model);
            }
        }
    }

    private static void GetTypeParameter(TypeVariable<?> typeParameter, TypeParameterModel model) {
        model.variableName = typeParameter.getName();
        model.typeName = typeParameter.getTypeName();

        GetBounds(typeParameter.getBounds(), model.boundModels);
    }

    private static void GetTypeParameter(Type type, TypeParameterModel model) {
        if (type instanceof TypeVariable<?>) {
            GetTypeParameter((TypeVariable<?>) type, model);
        }
    }

    private static void GetBounds(Type[] bounds, Collection<BoundDefinitionModel> models) {
        for (var bound : bounds) {
            var model = new BoundDefinitionModel();

            model.name = bound.getTypeName();

            models.add(model);
        }
    }

    private static void GetBaseClass(Class<?> clazz, BaseClassDefinitionModel model) {
        var baseClass = clazz.getGenericSuperclass();

        model.typeName = baseClass.getTypeName();

        GetTypeParameters(baseClass, model.typeParameterModels);
    }

    private static void GetMethods(Class<?> clazz, Collection<MethodDefinitionModel> models) {
        for (var method : clazz.getDeclaredMethods()) {
            var model = new MethodDefinitionModel();

            model.modifiers = GetModifiers(method.getModifiers());
            model.methodName = method.getName();

            GetParameters(method.getParameters(), model.parameterModels);

            var type = method.getGenericReturnType();

            model.returnType = type.getTypeName();

            GetTypeParameters(type, model.typeParameterModels);

            models.add(model);
        }
    }

    private static void GetInterfaces(Class<?> clazz, Collection<InterfaceDefinitionModel> models) {
        for (var interfaceType : clazz.getGenericInterfaces()) {
            var model = new InterfaceDefinitionModel();

            model.typeName = interfaceType.getTypeName();

            GetTypeParameters(interfaceType, model.typeParameterModels);

            models.add(model);
        }
    }

    private static void GetFields(Class<?> clazz, Collection<FieldDefinitionModel> fieldModels) {
        for (var field : clazz.getDeclaredFields()) {
            var model = new FieldDefinitionModel();

            model.modifiers = GetModifiers(field.getModifiers());
            model.fieldName = field.getName();

            var type = field.getGenericType();

            model.returnType = type.getTypeName();

            GetTypeParameter(type, model.typeParameterModel);

            fieldModels.add(model);
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

            var type = parameter.getParameterizedType();

            model.type = type.getTypeName();

            GetTypeParameters(type, model.typeParameterModels);

            models.add(model);
        }
    }

    private static void GetInnerClasses(Class<?> clazz, Collection<ClassDefinitionModel> models) {
        for (Class<?> innerClass : clazz.getDeclaredClasses()) {
            var model = new ClassDefinitionModel();

            ProcessClass(innerClass, model);

            models.add(model);
        }
    }
}