namespace Epam.HomeWork.Lab5Runner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Epam.HomeWork.Common;
    using Epam.HomeWork.Common.IO;
    using Epam.HomeWork.Lab5;
    using Epam.HomeWork.LabRunners.Common;

    public class Lab5Runner : ILabRunner
    {
        private const string ModuleName = "Lab1.dll";

        public Lab5Runner()
        {
            this.Errors = new List<string>();
            this.Success = false;

            this.Writer = new ConsoleWriter();
            this.Reader = new ConsoleReader();
        }

        public string Description => "Lab5: Assemblies and Reflection";

        public IList<string> Errors { get; }

        public bool Success { get; private set; }

        public IWriter Writer { get; set; }

        public IReader Reader { get; set; }

        public void Run()
        {
            this.Success = true;

            ConsoleWriterHelper
                .WriteHeaderMessage("Task 1: Info about assemblies:\n", this.Writer);

            try
            {
                var labModule = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetModules())
                    .Where(m => m.Name == ModuleName)
                    .FirstOrDefault();

                foreach (var info in ModuleVisualiser.GetInfo(labModule))
                {
                    this.Writer.WriteLine($"{info}");
                }
            }
            catch (ArgumentException e)
            {
                this.Success = false;
                this.Errors.Add($"{e.TargetSite.Name}: {e.Message}");
            }
            catch (Exception e)
            {
                this.Success = false;
                throw e;
            }

            this.Writer.WriteLine("\t\nPress any key to continue...");
            this.Reader.ReadKey();
        }
    }
}
