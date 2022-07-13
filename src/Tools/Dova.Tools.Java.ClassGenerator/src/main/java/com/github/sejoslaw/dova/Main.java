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
        GetClassDetails(clazz, model.ClassDetailsModel);
        GetBaseClass(clazz, model.BaseClassModel);
        GetInterfaces(clazz, model.InterfaceModels);
        GetConstructors(clazz, model.ConstructorModels);
        GetFields(clazz, model.FieldModels);
        GetMethods(clazz, model.MethodModels);
        GetInnerClasses(clazz, model.InnerClassModels);
    }

    private static void GetClassDetails(Class<?> clazz, ClassDetailsDefinitionModel model) {
        model.PackageName = clazz.getPackageName();
        model.ClassName = clazz.getSimpleName();
        model.IsEnum = clazz.isEnum();
        model.Modifiers = GetModifiers(clazz.getModifiers());

        GetTypeParameters(clazz.getTypeParameters(), model.TypeParameterModels);
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
        model.VariableName = typeParameter.getName();
        model.TypeName = typeParameter.getTypeName();

        GetBounds(typeParameter.getBounds(), model.BoundModels);
    }

    private static void GetTypeParameter(Type type, TypeParameterModel model) {
        if (type instanceof TypeVariable<?>) {
            GetTypeParameter((TypeVariable<?>) type, model);
        }
    }

    private static void GetBounds(Type[] bounds, Collection<BoundDefinitionModel> models) {
        for (var bound : bounds) {
            var model = new BoundDefinitionModel();

            model.Name = bound.getTypeName();

            models.add(model);
        }
    }

    private static void GetBaseClass(Class<?> clazz, BaseClassDefinitionModel model) {
        var baseClass = clazz.getGenericSuperclass();

        model.TypeName = baseClass.getTypeName();

        GetTypeParameters(baseClass, model.TypeParameterModels);
    }

    private static void GetMethods(Class<?> clazz, Collection<MethodDefinitionModel> models) {
        for (var method : clazz.getDeclaredMethods()) {
            var model = new MethodDefinitionModel();

            model.Modifiers = GetModifiers(method.getModifiers());
            model.MethodName = method.getName();

            GetParameters(method.getParameters(), model.ParameterModels);

            var type = method.getGenericReturnType();

            model.ReturnType = type.getTypeName();

            GetTypeParameters(type, model.TypeParameterModels);

            models.add(model);
        }
    }

    private static void GetInterfaces(Class<?> clazz, Collection<InterfaceDefinitionModel> models) {
        for (var interfaceType : clazz.getGenericInterfaces()) {
            var model = new InterfaceDefinitionModel();

            model.TypeName = interfaceType.getTypeName();

            GetTypeParameters(interfaceType, model.TypeParameterModels);

            models.add(model);
        }
    }

    private static void GetFields(Class<?> clazz, Collection<FieldDefinitionModel> fieldModels) {
        for (var field : clazz.getDeclaredFields()) {
            var model = new FieldDefinitionModel();

            model.Modifiers = GetModifiers(field.getModifiers());
            model.FieldName = field.getName();

            var type = field.getGenericType();

            model.ReturnType = type.getTypeName();

            GetTypeParameter(type, model.TypeParameterModel);

            fieldModels.add(model);
        }
    }

    private static String GetModifiers(int mods) {
        return Modifier.toString(mods);
    }

    private static void GetConstructors(Class<?> clazz, Collection<ConstructorDefinitionModel> models) {
        for (var constructor : clazz.getDeclaredConstructors()) {
            var model = new ConstructorDefinitionModel();

            model.Modifiers = GetModifiers(constructor.getModifiers());

            GetParameters(constructor.getParameters(), model.ParameterModels);

            models.add(model);
        }
    }

    private static void GetParameters(Parameter[] parameters, Collection<ParameterDefinitionModel> models) {
        for (var parameter : parameters) {
            var model = new ParameterDefinitionModel();

            model.Name = parameter.getName();

            var type = parameter.getParameterizedType();

            model.Type = type.getTypeName();

            GetTypeParameters(type, model.TypeParameterModels);

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