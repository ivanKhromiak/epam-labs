namespace Epam.HomeWork.Lab3Runner
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;
    using Epam.HomeWork.Common;
    using Epam.HomeWork.Lab3;
    using Epam.HomeWork.Common.IO;
    using Epam.HomeWork.LabRunners.Common;

    public class Lab3Runner : ILabRunner
    {
        private const int MaxSubDirectoryDepth = 3;

        public Lab3Runner()
        {
            this.Success = false;
            this.Errors = new List<string>();

            this.Writer = new ConsoleWriter();
            this.Reader = new ConsoleReader();
        }

        public bool Success { get; private set; }

        public IList<string> Errors { get; }

        public string Description
            => "Lab 3: IO";

        public IWriter Writer { get; set; }

        public IReader Reader { get; set; }

        public void Run()
        {
            this.RunDirectoryTask();

            this.Writer.WriteLine("\t\nPress any key to continue...");
            this.Reader.ReadKey();

            this.RunFileFindTask();

            this.Writer.WriteLine("\t\nPress any key to continue...");
            this.Reader.ReadKey();
        }

        private void RunDirectoryTask()
        {
            ConsoleWriterHelper
                 .WriteHeaderMessage("Task 1: Directory Visualizer\n", this.Writer);

            try
            {
                this.Writer.Write("\tPlease, enter directory name: ");
                string inputDirectory = Console.ReadLine();

                var files = DirectoryVisualizer.GetFilesFromDirectory(inputDirectory,
                    maxSubDirectoryDepth: MaxSubDirectoryDepth);

                foreach (var file in files)
                {
                    this.Writer.WriteLine($"\t{file}");
                }
            }
            catch (ArgumentException e)
            {
                this.Success = false;
                this.Errors.Add($"{e.TargetSite.Name}: {e.Message}");
            }
            catch (IOException e)
            {
                this.Success = false;
                this.Errors.Add($"{e.TargetSite.Name}: {e.Message}");
            }
            catch (Exception e)
            {
                this.Success = false;
                throw e;
            }
        }

        private void RunFileFindTask()
        {
            ConsoleWriterHelper
                 .WriteHeaderMessage("Task 2: File Finder\n", this.Writer);

            this.Writer.Write("\tEnter file extension: ");
            string ext = this.Reader.ReadLine();

            this.Writer.Write("\tEnter partial filename: ");
            string filename = this.Reader.ReadLine();

            this.Writer.Write("\tEnter search directory name: ");
            string directoryName = this.Reader.ReadLine();

            this.Writer.WriteLine($"\tSearching for file like *{filename}*.{ext} in {directoryName}");

            try
            {
                var files = FileFinder.FindByPartialNameAndExtension(ext, filename, directoryName);

                if (files.Count() > 0)
                {
                    foreach (var file in files)
                    {
                        this.Writer.WriteLine($"\tFile: {file}");
                    }
                }
                else
                {
                    this.Writer.WriteLine($"\tNo files like *{filename}*.{ext} found!");
                }
            }
            catch (ArgumentException e)
            {
                this.Success = false;
                this.Errors.Add($"{e.TargetSite.Name}: {e.Message}");
            }
            catch (IOException e)
            {
                this.Success = false;
                this.Errors.Add($"{e.TargetSite.Name}: {e.Message}");
            }
            catch (Exception e)
            {
                this.Success = false;
                throw e;
            }
        }
    }
}
