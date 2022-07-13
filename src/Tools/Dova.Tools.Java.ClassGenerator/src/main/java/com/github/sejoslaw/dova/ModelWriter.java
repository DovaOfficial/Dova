package com.github.sejoslaw.dova;

import org.codehaus.jackson.map.ObjectMapper;

import java.io.IOException;
import java.nio.file.FileSystems;
import java.nio.file.Files;

public class ModelWriter {
    public static void Write(String tempOutputPathFull, ClassDefinitionModel model) {
        var filePath = FileSystems.getDefault().getPath(tempOutputPathFull);
        var file = filePath.toFile();

        try {
            Files.deleteIfExists(filePath);

            file.getParentFile().mkdirs();
            file.createNewFile();
        } catch (IOException e) {
            throw new RuntimeException(e);
        }

        var mapper = new ObjectMapper();

        try {
            mapper.writeValue(file, model);
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }
}
