using Dova.JDK.com.sun.tools.attach;
using Dova.JDK.Extensions;
using Dova.JDK.java.lang.management;

namespace Dova.JDK.Loaders;

public static class JarLoader
{
    public static void Load(params FileInfo[] paths)
    {
        var resourceFilesPaths = typeof(JarLoader).Assembly
            .ExtractResourceFiles()
            .ToList();

        var agentJarPath = resourceFilesPaths[0];
        
        Load(agentJarPath, paths);
    }
    
    public static void Load(string javaAgentJarFilePath, params FileInfo[] paths)
    {
        var combinedPaths = string.Join(",", paths.Select(fi => fi.FullName));
        var vmName = ManagementFactory.getRuntimeMXBean().getName();
        var processId = vmName.substring(0, vmName.indexOf('@'));
        var vm = VirtualMachine.attach(processId);
        
        vm.loadAgent(javaAgentJarFilePath.ToJava(), combinedPaths.ToJava());
        vm.detach();
    }
}