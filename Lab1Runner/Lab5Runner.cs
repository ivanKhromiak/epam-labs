namespace Epam.HomeWork.Lab5Runner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Epam.HomeWork.Common;
    using Epam.HomeWork.Lab5;

    public class Lab5Runner : IConsoleLabRunner
    {
        private const string ModuleName = "Lab1.dll";

        public Lab5Runner()
        {
            Errors = new List<string>();
            Success = false;
        }

        public string Description => "Lab5: Assemblies and Reflection";

        public IList<string> Errors { get; }

        public bool Success { get; private set; }

        public void RunConsoleLab()
        {
            Success = true;
            ConsoleHelper.WriteHeaderMessage("Task 1: Info about an assemblies...\n",
                ConsoleColor.Yellow, ConsoleColor.Black);

            try
            {
                var labModule = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetModules())
                    .Where(m => m.Name == ModuleName)
                    .FirstOrDefault();

                foreach (var info in ModuleVisualiser.GetInfo(labModule))
                {
                    Console.WriteLine(info);
                }
            }
            catch (ArgumentException e)
            {
                Success = false;
                Errors.Add(e.Message);
            }
        }
    }
}
