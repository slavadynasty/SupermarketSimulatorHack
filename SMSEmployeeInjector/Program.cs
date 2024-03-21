using System;
using System.Diagnostics;
using System.IO;
using SharpMonoInjector;
using static System.String;

namespace SMSEmployeeInjector
{
    internal static class Program
    {
        private const string GameName = "Supermarket Simulator";
        private static Process _gameProcess;

        private static Injector _injector;
        private const string LibName = "SMSEmployeeHack.dll";
        private const string LibNamespace = "SMSEmployeeHack";
        private const string LibClass = "EntryPoint";
        private const string LibLoadMethod = "Load";

        private static void Main()
        {
            Console.WriteLine("Preparing injection into " + GameName);
            
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                if (process.ProcessName == GameName) _gameProcess = process;
            }

            if (_gameProcess == null)
            {
                throw new Exception("Injection error! Game process not found!");
            }
            
            Console.WriteLine($"Injecting to Supermarket Simulator ({_gameProcess.Id})");

            _injector = new Injector(_gameProcess.Id);
            Inject();
            
            Console.WriteLine("Injection succeeds");
            Console.WriteLine(Empty);
        }

        private static void Inject()
        {
            byte[] assembly;

            try
            {
                assembly = File.ReadAllBytes(LibName);
            } catch {
                throw new Exception("Could not found the file " + LibName);
            }
            
            using (_injector) {
                try {
                    _injector.Inject(assembly, LibNamespace, LibClass, LibLoadMethod);
                } catch (InjectorException ie) {
                    Console.WriteLine("Failed to inject assembly: " + ie);
                } catch (Exception exc) {
                    Console.WriteLine("Failed to inject assembly (unknown error): " + exc);
                }
            }
        }
    }
}