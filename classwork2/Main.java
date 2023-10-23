package org.example;



import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;
import java.lang.annotation.*;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.lang.reflect.Modifier;
import org.example.InitMethod;


public class Main {
    public static void main(String[] args) throws Exception {
        Properties properties = new Properties();
        try (InputStream inputStream = Main.class.getResourceAsStream("resources/myapp.properties")) {
            properties.load(inputStream);
        }
        String className = properties.getProperty("className");
        Class<?> clazz = Class.forName(className);
        Object object = clazz.getDeclaredConstructor().newInstance();
        if (clazz.isAnnotationPresent(InitMethod.class)) {
            Method[] methods = clazz.getDeclaredMethods();
            for (Method method : methods) {
                if (method.isAnnotationPresent(InitMethod.class)) {
                    method.invoke(object);
                }
            }
        }
    }
}

