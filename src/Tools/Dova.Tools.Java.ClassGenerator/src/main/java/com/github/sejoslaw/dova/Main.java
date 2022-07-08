package com.github.sejoslaw.dova;

public class Main {
    public static void main(String[] args) throws ClassNotFoundException {
        String tempOutputPathFull = args[0]; // i.e.: /tmp/<JAVA_MODULE>/share/classes/com/sun/crypto/provider/AESCipher.java.gen
        String javaClassFullName = args[1]; // i.e.: com.sun.crypto.provider.AESCipher

        ProcessClass(tempOutputPathFull, javaClassFullName);
    }

    private static void ProcessClass(String tempOutputPathFull, String javaClassFullName) {
    }
}