namespace Epam.HomeWork.Lab1Runner
{
    using System;
    using System.Collections.Generic;
    using Epam.HomeWork.Common;
    using Epam.HomeWork.Common.IO;
    using Epam.HomeWork.Lab1.Task1;
    using Epam.HomeWork.Lab1.Task2;
    using Epam.HomeWork.LabRunners.Common;

    public class Lab1Runner : ILabRunner
    {
        public Lab1Runner()
        {
            this.Success = false;
            this.Errors = new List<string>();

            this.Writer = new ConsoleWriter();
            this.Reader = new ConsoleReader();
        }

        public bool Success { get; private set; }

        public IList<string> Errors { get; }

        public string Description
            => "Lab 1: Strcuts and Enums";

        public IWriter Writer { get; set; }

        public IReader Reader { get; set; }

        public void Run()
        {
            this.Success = true;
            this.RunStructTask();
            this.RunEnumTask();

            this.Writer.WriteLine("\t\nPress any key to continue...");
            this.Reader.ReadKey();
        }

        private void RunStructTask()
        {
            ConsoleWriterHelper
                 .WriteHeaderMessage("Task 1: Structs:\n", this.Writer);

            try
            {
                this.RunPersonTask();
                this.RunRectangleTask();
            }
            catch (FormatException e)
            {
                this.Errors.Add($"{e.TargetSite.Name}: {e.Message}");
                this.Success = false;
            }
            catch (OverflowException e)
            {
                this.Errors.Add($"{e.TargetSite.Name}: {e.Message}");
                this.Success = false;
            }
            catch (Exception e)
            {
                this.Success = false;
                throw e;
            }
        }

        private void RunEnumTask()
        {
            this.Success = true;

            ConsoleWriterHelper
                    .WriteHeaderMessage("Task 2: Enums:\n", this.Writer);

            try
            {
                this.RunMonthsTask();
                this.RunColorsTask();
                this.RunLongRangesTask();
            }
            catch (FormatException e)
            {
                this.Errors.Add($"{e.TargetSite.Name}: {e.Message}");
                this.Success = false;
            }
            catch (OverflowException e)
            {
                this.Errors.Add($"{e.TargetSite.Name}: {e.Message}");
                this.Success = false;
            }
            catch (Exception e)
            {
                this.Success = false;
                throw e;
            }
        }

        #region Task 1 helper methods

        private void RunPersonTask()
        {
            ConsoleWriterHelper
                 .WriteHeaderMessage("\tPerson task:\n", this.Writer);

            Person person = this.ReadPersonFromConsole();

            this.Writer.Write("\tEnter age value: ");

            int ageValue = Convert.ToInt32(this.Reader.ReadLine());

            this.Writer.WriteLine($"\t{person.OlderThan(ageValue)}\n");

        }

        private void RunRectangleTask()
        {
            ConsoleWriterHelper
                    .WriteHeaderMessage("\tRectangle task:\n", this.Writer);

            double width, height, x, y;

            this.Writer.Write("\tEnter rectangle width: ");
            width = Convert.ToDouble(this.Reader.ReadLine());

            this.Writer.Write("\tEnter rectangle height: ");
            height = Convert.ToDouble(this.Reader.ReadLine());

            this.Writer.Write("\tEnter rectangle x coord: ");
            x = Convert.ToDouble(this.Reader.ReadLine());

            this.Writer.Write("\tEnter rectangle y coord: ");
            y = Convert.ToDouble(this.Reader.ReadLine());

            var rect = new Rectangle
            {
                Height = height,
                Width = width,
                X = x,
                Y = y
            };

            this.Writer.WriteLine($"\tRectangle: {rect}\n");

        }

        private Person ReadPersonFromConsole()
        {
            this.Writer.Write("\tEnter name: ");
            string name = this.Reader.ReadLine();

            this.Writer.Write("\tEnter surname: ");
            string surname = this.Reader.ReadLine();

            this.Writer.Write("\tEnter age: ");
            int age = Convert.ToInt32(this.Reader.ReadLine());

            return new Person
            {
                Name = name,
                Surname = surname,
                Age = age
            };
        }

        #endregion

        #region Task 2 helper methdos

        private void RunMonthsTask()
        {
            ConsoleWriterHelper
                       .WriteHeaderMessage("\tMonth enum task:\n", this.Writer);

            this.Writer.Write("\tEnter month number: ");
            int n = Convert.ToInt32(this.Reader.ReadLine());

            this.Writer.WriteLine($"\tMonth: {Month.GetMonth(n)}\n");

        }

        private void RunColorsTask()
        {
            ConsoleWriterHelper
                       .WriteHeaderMessage("\tColors enum task:\n", this.Writer);

            Colors red = Colors.Red;

            var allColorsStrings = red.GetAllColors();

            this.Writer.WriteLine("\tAll colors in Colors enum:");
            foreach (var colorString in allColorsStrings)
            {
                this.Writer.WriteLine($"\t - {colorString}");
            }

            this.Writer.WriteLine(string.Empty);
        }

        private void RunLongRangesTask()
        {
            ConsoleWriterHelper
                       .WriteHeaderMessage("\tLong range task:\n", this.Writer);

            this.Writer.WriteLine($"\tLong ranges: min={(long)LongRange.Min}, max={(long)LongRange.Max}");
        }

        #endregion
    }
}
