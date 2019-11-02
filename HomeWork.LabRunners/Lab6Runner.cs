namespace Epam.HomeWork.Lab1Runner
{
    using System;
    using System.Collections.Generic;
    using Epam.HomeWork.Common;
    using Epam.HomeWork.Common.IO;
    using Epam.HomeWork.Lab6.Common;
    using Epam.HomeWork.Lab6.Task1;
    using Epam.HomeWork.LabRunners.Common;

    public class Lab6Runner : ILabRunner
    {
        public Lab6Runner()
        {
            this.Success = false;
            this.Errors = new List<string>();

            this.Writer = new ConsoleWriter();
            this.Reader = new ConsoleReader();
        }

        public bool Success { get; private set; }

        public IList<string> Errors { get; }

        public string Description
            => "Lab 6: Style cop";

        public IWriter Writer { get; set; }

        public IReader Reader { get; set; }

        public void Run()
        {
            this.Success = true;

            ConsoleWriterHelper
                .WriteHeaderMessage("Task 1: Rectangles intersection...\n", this.Writer);

            this.RunRectangleTask();

            this.Writer.WriteLine("\t\nPress any key to continue...");
            this.Reader.ReadKey();
        }

        private void RunRectangleTask()
        {
            const int FirstLength = 10;

            var firstRect = new Rectangle(FirstLength, FirstLength, PointF.Origin);
            var secondRect = new Rectangle(FirstLength / 2, FirstLength / 2, PointF.Origin);

            this.PrintRect(firstRect, "First Rectangle");
            this.PrintRect(secondRect, "Second Rectangle");

            this.PrintRect(firstRect.Intersect(secondRect), "Intersection");
        }

        private void PrintRect(Rectangle rect, string name)
        {
            if (rect == null)
            {
                throw new ArgumentNullException(nameof(rect));
            }

            this.Writer.WriteLine($"\t{name}: Bottom Left: {rect.BottomLeft}," +
                $" width: {rect.Width}, height: {rect.Height}");
        }
    }
}
