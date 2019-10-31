namespace Epam.HomeWork.Lab3Runner
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;
    using Epam.HomeWork.Common;
    using Epam.HomeWork.Lab3;

    public class Lab3Runner : IConsoleLabRunner
    {
        private const int MaxSubDirectoryDepth = 3;

        public Lab3Runner()
        {
            Success = false;
            Errors = new List<string>();
        }

        public bool Success { get; private set; }

        public IList<string> Errors { get; }

        public string Description
            => "Lab 3: IO";

        public void RunConsoleLab()
        {
            RunDirectoryTask();

            Console.WriteLine("\tPress any key to continue...");
            Console.ReadKey();

            RunFileFindTask();

            Console.WriteLine("\tPress any key to continue...");
            Console.ReadKey();
        }

        private void RunDirectoryTask()
        {
            ConsoleHelper.WriteHeaderMessage(
                "Task 1: Directory Visualizer\n",
                ConsoleColor.Red,
                Console.BackgroundColor);
            try
            {
                Console.Write("\tPlease, enter directory name: ");
                string inputDirectory = Console.ReadLine();
                var files = DirectoryVisualizer.GetFilesFromDirectory(inputDirectory,
                    maxSubDirectoryDepth: MaxSubDirectoryDepth);

                foreach (var file in files)
                {
                    Console.WriteLine(file);
                }
            }
            catch (ArgumentException e)
            {
                Success = false;
                Errors.Add(e.Message);
            }
            catch (IOException e)
            {
                Success = false;
                Errors.Add(e.Message);
            }
        }

        private void RunFileFindTask()
        {
            ConsoleHelper.WriteHeaderMessage(
                "Task 2: File Finder\n",
                ConsoleColor.Red,
                Console.BackgroundColor);

            Console.Write("\tEnter file extension: ");
            string ext = Console.ReadLine();

            Console.Write("\tEnter partial filename: ");
            string filename = Console.ReadLine();

            Console.Write("\tEnter search directory name: ");
            string directoryName = Console.ReadLine();

            Console.WriteLine($"\tSearching for file like *{filename}*.{ext} in {directoryName}");

            try
            {
                var files = FileFinder.FindByPartialNameAndExtension(ext, filename, directoryName);

                if (files.Count() > 0)
                {
                    foreach (var file in files)
                    {
                        Console.WriteLine($"File: {file}");
                    }
                }
                else
                {
                    Console.WriteLine($"\tNo files like *{filename}*.{ext} found!");
                }
            }
            catch (ArgumentException e)
            {
                Success = false;
                Errors.Add(e.Message);
            }
            catch (IOException e)
            {
                Success = false;
                Errors.Add(e.Message);
            }
        }
    }
}
