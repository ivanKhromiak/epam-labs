namespace Epam.HomeWork.Lab1Runner
{
    using System;
    using System.Collections.Generic;
    using Epam.HomeWork.Common;
    using Epam.HomeWork.Lab6.Common;
    using Epam.HomeWork.Lab6.Task1;

    public class Lab6Runner : IConsoleLabRunner
    {
        public Lab6Runner()
        {
            Success = false;
            Errors = new List<string>();
        }

        public bool Success { get; private set; }

        public IList<string> Errors { get; }

        public string Description
            => "Lab 6: Style cop";

        public void RunConsoleLab()
        {
            Success = true;

            ConsoleHelper.WriteHeaderMessage("Task 1: Rectangles intersection...\n", ConsoleColor.Yellow, ConsoleColor.Black);

            RunRectangleTask();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void RunRectangleTask()
        {
            const int FirstLength = 10;

            var firstRect = new Rectangle(FirstLength, FirstLength, PointF.Origin);
            var secondRect = new Rectangle(FirstLength / 2, FirstLength / 2, PointF.Origin);

            PrintRect(firstRect, "First Rectangle");
            PrintRect(secondRect, "Second Rectangle");

            PrintRect(firstRect.Intersect(secondRect), "Intersection");
        }

        private static void PrintRect(Rectangle rect, string name)
        {
            if (rect == null)
            {
                throw new ArgumentNullException(nameof(rect));
            }

            Console.WriteLine($"\t{name}: Bottom Left: {rect.BottomLeft}," +
                $" width: {rect.Width}, height: {rect.Height}");
        }
    }
}
