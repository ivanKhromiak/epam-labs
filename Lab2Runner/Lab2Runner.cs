namespace Epam.HomeWork.Lab2Runner
{
    using System;
    using System.Collections.Generic;
    using Epam.HomeWork.Common;
    using Epam.HomeWork.Lab2;

    public class Lab2Runner : IConsoleLabRunner
    {
        public Lab2Runner()
        {
            Succes = false;
            Errors = new List<string>();
        }

        public bool Succes { get; private set; }

        public IList<string> Errors { get; }

        public string Description
            => "Lab 2: Exceptions";

        public void RunConsoleLab()
        {
            Succes = true;
            RunTask1();
            RunTask2();
            RunTask3();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void RunTask1()
        {
            ConsoleHelper.WriteHeaderMessage(
                "Task 1: StackOverFlow Exception...\n",
                ConsoleColor.Yellow,
                ConsoleColor.Black);
            try
            {
                Console.WriteLine("Do you want to generate StackOverFlow Exception? (y/n)");

                var input = Console.ReadLine();
                Console.In.Close();

                if (input == "y")
                {
                    Exceptions.GenerateStackOverFlowException();
                }
            }
            catch (StackOverflowException e)
            {
                Console.WriteLine('\t' + e.Message);
            }
        }

        private void RunTask2()
        {
            ConsoleHelper.WriteHeaderMessage(
                "Task 2: IndexOutOfRange Exception...\n",
                ConsoleColor.Yellow,
                ConsoleColor.Black);
            try
            {
                Exceptions.GenerateIndexOutOfRangeException();
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine('\t' + e.Message);
            }
            catch (Exception e)
            {
                Succes = false;
                Errors.Add("Lab2.Task2: IndexOutOfRange not thrown");
                Console.WriteLine('\t' + e.Message);
            }
        }

        private void RunTask3()
        {
            ConsoleHelper.WriteHeaderMessage(
                "Task 3: Argument Exception...\n",
                ConsoleColor.Yellow,
                ConsoleColor.Black);

            try
            {
                int a, b;

                Console.Write("\tEnter a: ");
                a = Convert.ToInt32(Console.ReadLine());

                Console.Write("\tEnter b: ");
                b = Convert.ToInt32(Console.ReadLine());

                Exceptions.DoSomeMath(a, b);
            }
            catch (ArgumentException e)
            {
                if (e.ParamName == "a")
                {
                    Console.WriteLine('\t' + e.Message);
                }
                else if (e.ParamName == "b")
                {
                    Console.WriteLine('\t' + e.Message);
                }
            }
            catch (Exception e)
            {
                Succes = false;
                Errors.Add("\tLab2.Task3: ArgumentException not thrown");
                Console.WriteLine('\t' + e.Message);
            }
        }
    }
}
