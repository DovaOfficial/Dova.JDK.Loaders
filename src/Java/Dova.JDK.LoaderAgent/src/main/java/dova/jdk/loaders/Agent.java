package dova.jdk.loaders;

import java.io.File;
import java.io.IOException;
import java.lang.instrument.Instrumentation;
import java.util.jar.JarFile;

public class Agent {
    public static void agentmain(String agentArgs, Instrumentation inst) throws IOException {
        var paths = agentArgs.split(",");

        for (var path : paths) {
            try (var jarFile = new JarFile(new File(path))) {
                inst.appendToBootstrapClassLoaderSearch(jarFile);
            }
        }
    }
}