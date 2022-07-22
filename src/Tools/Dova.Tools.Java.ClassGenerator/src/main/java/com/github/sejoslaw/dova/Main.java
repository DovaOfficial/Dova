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
        model.IsInterface = Modifier.isInterface(clazz.getModifiers());
        model.IsAbstract = Modifier.isAbstract(clazz.getModifiers());

        GetTypeParameters(clazz.getTypeParameters(), model.TypeParameterModels);

        model.Signature = SignatureBuilder.Build(clazz);
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
            model.Signature = SignatureBuilder.Build(bound);

            models.add(model);
        }
    }

    private static void GetBaseClass(Class<?> clazz, ClassElementDefinitionModel model) {
        var baseClass = clazz.getGenericSuperclass();

        if (baseClass == null) {
            return;
        }

        model.Name = baseClass.getTypeName();

        GetTypeParameters(baseClass, model.TypeParameterModels);

        model.Signature = SignatureBuilder.Build(baseClass);
    }

    private static void GetMethods(Class<?> clazz, Collection<ClassElementDefinitionModel> models) {
        for (var method : clazz.getDeclaredMethods()) {
            var model = new ClassElementDefinitionModel();

            model.Modifiers = GetModifiers(method.getModifiers());
            model.Name = method.getName();
            model.IsStatic = Modifier.isStatic(method.getModifiers());

            GetParameters(method.getParameters(), model.ParameterModels);

            var type = method.getGenericReturnType();

            model.ReturnType = type.getTypeName();

            GetTypeParameters(type, model.TypeParameterModels);

            model.Signature = SignatureBuilder.Build(method);

            model.HasParent = HasParentMethod(clazz, method);

            models.add(model);
        }
    }

    private static boolean HasParentMethod(Class<?> clazz, Method method) {
        try {
            var parentMethod = clazz.getSuperclass().getDeclaredMethod(method.getName(), method.getParameterTypes());
            return true;
        } catch (NoSuchMethodException | NullPointerException ex) {
            return false;
        }
    }

    private static void GetInterfaces(Class<?> clazz, Collection<ClassElementDefinitionModel> models) {
        for (var interfaceType : clazz.getGenericInterfaces()) {
            var model = new ClassElementDefinitionModel();

            model.Name = interfaceType.getTypeName();

            GetTypeParameters(interfaceType, model.TypeParameterModels);

            model.Signature = SignatureBuilder.Build(interfaceType);

            models.add(model);
        }
    }

    private static void GetFields(Class<?> clazz, Collection<ClassElementDefinitionModel> fieldModels) {
        for (var field : clazz.getDeclaredFields()) {
            var model = new ClassElementDefinitionModel();

            model.Modifiers = GetModifiers(field.getModifiers());
            model.Name = field.getName();
            model.IsStatic = Modifier.isStatic(field.getModifiers());

            var type = field.getGenericType();

            model.ReturnType = type.getTypeName();

            GetTypeParameters(type, model.TypeParameterModels);

            model.Signature = SignatureBuilder.Build(field);

            fieldModels.add(model);
        }
    }

    private static String GetModifiers(int mods) {
        return Modifier.toString(mods);
    }

    private static void GetConstructors(Class<?> clazz, Collection<ClassElementDefinitionModel> models) {
        for (var constructor : clazz.getDeclaredConstructors()) {
            var model = new ClassElementDefinitionModel();

            model.Modifiers = GetModifiers(constructor.getModifiers());

            GetParameters(constructor.getParameters(), model.ParameterModels);

            model.Signature = SignatureBuilder.Build(constructor);

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

            model.Signature = SignatureBuilder.Build(type);

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