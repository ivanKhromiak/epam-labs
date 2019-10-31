namespace Epam.HomeWork.Lab1.Task1
{
    using System;

    public class Rectangle : ISize, ICoordinates
    {
        private double _width;
        private double _height;

        public double Width
        {
            get => _width;
            set
            {
                _width = value > 0 ? value 
                    : throw new ArgumentException("Width cannot be negative");
            }
        }

        public double Height
        {
            get => _height;
            set
            {
                _height = value > 0 ? value
                    : throw new ArgumentException("Height cannot be negative");
            }
        }

        public double X { get; set; }
        public double Y { get; set; }

        public double Perimetr
            => 2 * (Width + Height);

        public override string ToString()
        {
            return $"Height={Height}, Width={Width}, Perimeter={Perimetr}, X={X}, Y={Y}";
        }
    }
}
