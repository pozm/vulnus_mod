using System.Diagnostics;
using System.Reflection;
using SharpMonoInjector;

string assemblyPath = Directory.GetCurrentDirectory()+"/vulnus_mod.dll";
string @namespace = "vulnus_mod";
string className = "Loader";
string methodName = ( args.Length > 0 && args[0].ToLower() == "true") ? "Init_D" : "Init";
bool eject = args.Length > 0 && args[0].ToLower() == "ej";
byte[] assembly;

try {
    assembly = File.ReadAllBytes(assemblyPath);
} catch {
    Console.WriteLine("Could not read the file " + assemblyPath);
    return;
}
            
SharpMonoInjector.Injector injector = new SharpMonoInjector.Injector("Vulnus");
using (injector) {
    IntPtr remoteAssembly = IntPtr.Zero;
    if (!File.Exists("./last_inject.addr"))
        File.Create("./last_inject.addr");
    if (eject)
    {

        try
        {


            var pid = Process.GetProcessesByName("Vulnus").First().Id;
            var last = File.ReadAllLines("./last_inject.addr");
            if (last.ElementAtOrDefault(1) != pid.ToString())
            {
                Console.WriteLine("unable to eject, different pid.");
                return;
            }
            var ptrVarient = new IntPtr(Convert.ToInt64(last[0]));
            Console.WriteLine($"{last[0]} > {ptrVarient.ToString()}");
            injector.Eject(ptrVarient, @namespace, className, "Unload");

        }
        catch (Exception e)
        {
            Console.WriteLine($"Unable to eject.\n{e.ToString()}");
        }
        return;
    }
    
    try
    {
        var asm = Assembly.Load(assembly);
        Console.WriteLine($"got assembly; {asm.ImageRuntimeVersion} | {asm.GetName().Version}");
        remoteAssembly = injector.Inject(assembly, @namespace, className, methodName);
    } catch (InjectorException ie) {
        Console.WriteLine("Failed to inject assembly: " + ie);
    } catch (Exception exc) {
        Console.WriteLine("Failed to inject assembly (unknown error): " + exc);
    }
        
    if (remoteAssembly == IntPtr.Zero)
        return;
    var addr = injector.Is64Bit ? remoteAssembly.ToInt64() : remoteAssembly.ToInt32();
    var pid2 = Process.GetProcessesByName("Vulnus").First().Id;
    File.WriteAllText("./last_inject.addr",$"{addr.ToString()}\n{pid2}");
    Console.WriteLine($"Injected @ {addr}");
}