package com.github.sejoslaw.dova;

import com.google.gson.Gson;

import java.io.FileWriter;
import java.io.IOException;
import java.nio.file.FileSystems;
import java.nio.file.Files;

public class ModelWriter {
    public static void Write(String tempOutputPathFull, ClassDefinitionModel model) {
        var filePath = FileSystems.getDefault().getPath(tempOutputPathFull);

        try {
            Files.deleteIfExists(filePath);

            var file = filePath.toFile();

            file.getParentFile().mkdirs();
            file.createNewFile();
        } catch (IOException e) {
            throw new RuntimeException(e);
        }

        var gson = new Gson();

        try {
            gson.toJson(model, new FileWriter(tempOutputPathFull));
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }
}
