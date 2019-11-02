namespace Epam.HomeWork.Lab2Runner
{
    using System;
    using System.Collections.Generic;
    using Epam.HomeWork.Common;
    using Epam.HomeWork.Common.IO;
    using Epam.HomeWork.Lab2;
    using Epam.HomeWork.LabRunners.Common;

    public class Lab2Runner : ILabRunner
    {
        public Lab2Runner()
        {
            this.Success = false;
            this.Errors = new List<string>();

            this.Writer = new ConsoleWriter();
            this.Reader = new ConsoleReader();
        }

        public bool Success { get; private set; }

        public IList<string> Errors { get; }

        public string Description
            => "Lab 2: Exceptions";

        public IWriter Writer { get; set; }

        public IReader Reader { get; set; }

        public void Run()
        {
            this.Success = true;
            this.RunStackOverFlowTask();
            this.RunIndexOutOfRangeTask();
            this.RunArgumentExceptionTask();

            this.Writer.WriteLine("\t\nPress any key to continue...");
            this.Reader.ReadKey();
        }

        private void RunStackOverFlowTask()
        {
            ConsoleWriterHelper
                 .WriteHeaderMessage("Task 1: StackOverFlow Exception:\n", this.Writer);

            try
            {
                this.Writer.Write("\tDo you want to generate StackOverFlow Exception? (y/n) ");

                var input = this.Reader.ReadLine();

                if (input == "y")
                {
                    Exceptions.GenerateStackOverFlowException();
                }
            }
            catch (StackOverflowException e)
            {
                this.Writer.WriteLine('\t' + e.Message);
            }
        }

        private void RunIndexOutOfRangeTask()
        {
            ConsoleWriterHelper
                 .WriteHeaderMessage("Task 2: IndexOutOfRange Exception:\n", this.Writer);

            try
            {
                Exceptions.GenerateIndexOutOfRangeException();
            }
            catch (IndexOutOfRangeException e)
            {
                this.Writer.WriteLine($"\tLab2.Task2: {e.Message}");
            }
            catch (Exception e)
            {
                this.Success = false;
                this.Errors.Add(e.Message);
                this.Errors.Add("Lab2.Task2: IndexOutOfRange not thrown");
            }
        }

        private void RunArgumentExceptionTask()
        {
            ConsoleWriterHelper
                 .WriteHeaderMessage("Task 3: Argument Exception:\n", this.Writer);

            try
            {
                int a, b;

                this.Writer.Write("\tEnter a: ");
                a = Convert.ToInt32(this.Reader.ReadLine());

                this.Writer.Write("\tEnter b: ");
                b = Convert.ToInt32(this.Reader.ReadLine());

                Exceptions.DoSomeMath(a, b);
            }
            catch (ArgumentException e)
            when (e.ParamName == "a")
            {
                this.Writer.WriteLine('\t' + e.Message);
            }
            catch (ArgumentException e)
            when (e.ParamName == "b")
            {
                this.Writer.WriteLine('\t' + e.Message);
            }
            catch (Exception e)
            {
                this.Success = false;
                this.Errors.Add(e.Message);
                this.Errors.Add("\tLab2.Task3: ArgumentException not thrown");
            }
        }
    }
}
