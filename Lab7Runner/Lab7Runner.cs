namespace Epam.HomeWork.Lab7Runner
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Epam.HomeWork.Common;
    using Epam.HomeWork.Lab7;
    using Microsoft.Extensions.Configuration;

    public class Lab7Runner : IConsoleLabRunner
    {
        private readonly IConfigurationRoot configuration;

        public Lab7Runner()
        {
            Errors = new List<string>();
            Success = false;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            configuration = builder.Build();
        }

        public string Description => "Lab7: Directory Comparer";

        public IList<string> Errors { get; }

        public bool Success { get; private set; }

        public void RunConsoleLab()
        {
            Success = true;
            
            DirectoryInfo firstDir = new DirectoryInfo(configuration["FirstDirectoryName"]);
            DirectoryInfo secondDir = new DirectoryInfo(configuration["SecondDirectoryName"]);

            RunTask1(firstDir, secondDir);
            RunTask2(firstDir, secondDir);
        }

        private void RunTask1(DirectoryInfo firstDir, DirectoryInfo secondDir)
        {
            ConsoleHelper.WriteHeaderMessage(
                "Task 1: Info about duplicate diles in two directories...\n",
                ConsoleColor.Yellow,
                ConsoleColor.Black);

            var duplicateFiles = DirectoryComparer.GetDuplicateFiles(firstDir, secondDir);

            if(configuration["TargetOutput"] == "File")
            {

            }
            else if(configuration["TargetOutput"] == "Console")
            {

            }
            else
            {

            }
        }
        private void RunTask2(DirectoryInfo firstDir, DirectoryInfo secondDir)
        {
            ConsoleHelper.WriteHeaderMessage(
                "Task 2: Info about unique diles in two directories...\n",
                ConsoleColor.Yellow,
                ConsoleColor.Black);

            var uniqueFiles = DirectoryComparer.GetUniqueFiles(firstDir, secondDir);

            if (configuration["TargetOutput"] == "File")
            {

            }
            else if (configuration["TargetOutput"] == "Console")
            {

            }
            else
            {

            }
        }
        
    }
}
