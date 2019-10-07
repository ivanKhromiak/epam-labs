namespace Epam.HomeWork.Lab1Runner
{
    using System;
    using System.Collections.Generic;
    using Epam.HomeWork.Common;
    using Epam.HomeWork.Lab1.Task1;
    using Epam.HomeWork.Lab1.Task2;

    public class Lab1Runner : IConsoleLabRunner, ILabRunner
    {
        public Lab1Runner()
        {
            Succes = false;
            Errors = new List<string>();
        }

        public bool Succes { get; private set; }

        public IList<string> Errors { get; }

        public string Description
            => "Lab 1: Strcuts and Enums";

        public void RunConsoleLab()
        {
            Succes = true;
            RunTask1();
            RunTask2();
        }

        private void RunTask1()
        {
            ConsoleHelper.WriteHeaderMessage("Task 1: Structs...\n", ConsoleColor.Yellow, ConsoleColor.Black);
            RunPersonTask();
            RunRectangleTask();
        }

        private void RunTask2()
        {
            ConsoleHelper.WriteHeaderMessage("Task 2: Enums...\n", ConsoleColor.Yellow, ConsoleColor.Black);
            RunMonthsTask();
            RunColorsTask();
            RunLongRangesTask();
        }

        #region Task 1 helper methods

        private void RunPersonTask()
        {
            ConsoleHelper.WriteHeaderMessage("\tPerson task...\n", ConsoleColor.Red, Console.BackgroundColor);

            try
            {
                var person = ReadPersonFromConsole();

                Console.Write("\tEnter age value: ");
                int ageValue = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine($"\t{person.OlderThan(ageValue)}\n");
            }
            catch (Exception e)
            {
                Errors.Add($"RunPersonTask() error: {e.Message}");
                Succes = false;
            }
        }

        private void RunRectangleTask()
        {
            ConsoleHelper.WriteHeaderMessage("\tRectangle task...\n", ConsoleColor.Red, Console.BackgroundColor);

            try
            {
                double width, height, x, y;

                Console.Write("\tEnter rectangle width: ");
                width = Convert.ToDouble(Console.ReadLine());

                Console.Write("\tEnter rectangle height: ");
                height = Convert.ToDouble(Console.ReadLine());

                Console.Write("\tEnter rectangle x coord: ");
                x = Convert.ToDouble(Console.ReadLine());

                Console.Write("\tEnter rectangle y coord: ");
                y = Convert.ToDouble(Console.ReadLine());

                var rect = new Rectangle
                {
                    Height = height,
                    Width = width,
                    X = x,
                    Y = y
                };

                Console.WriteLine($"\tRectangle: {rect}\n");
            }
            catch (Exception e)
            {
                Errors.Add($"RunRectangleTask() error: {e.Message}");
                Succes = false;
            }
        }

        private static Person ReadPersonFromConsole()
        {
            Console.Write("\tEnter name: ");
            string name = Console.ReadLine();

            Console.Write("\tEnter surname: ");
            string surname = Console.ReadLine();

            Console.Write("\tEnter age: ");
            int age = Convert.ToInt32(Console.ReadLine());

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
            ConsoleHelper.WriteHeaderMessage("\tMonth enum task...\n", ConsoleColor.Red, Console.BackgroundColor);
            try
            {
                Console.Write("\tEnter month number: ");
                int n = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine($"\tMonth: {Month.GetMonth(n)}\n");
            }
            catch (Exception e)
            {
                Errors.Add($"RunMonthsTask() error: {e.Message}");
                Succes = false;
            }
        }

        private void RunColorsTask()
        {
            ConsoleHelper.WriteHeaderMessage("\tColors enum task...\n", ConsoleColor.Red, Console.BackgroundColor);
            Colors red = Colors.Red;

            var allColorsStrings = red.GetAllColors();

            Console.WriteLine("\tAll colors in Colors enum:");
            foreach (var colorString in allColorsStrings)
            {
                Console.WriteLine($"\t - {colorString}");
            }

            Console.WriteLine();
        }

        private void RunLongRangesTask()
        {
            ConsoleHelper.WriteHeaderMessage("\tLonge range task...\n", ConsoleColor.Red, Console.BackgroundColor);
            Console.WriteLine($"\tLong ranges: min={(long)LongRange.Min}, max={(long)LongRange.Max}");
        }

        #endregion
    }
}
