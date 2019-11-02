namespace Epam.HomeWork.Lab1.Task1
{
    using System;

    public class Rectangle : ISize, ICoordinates
    {
        private double _width;
        private double _height;

        public double Width
        {
            get => this._width;
            set
            {
                this._width = value > 0 ? value 
                    : throw new ArgumentException("Width cannot be negative");
            }
        }

        public double Height
        {
            get => this._height;
            set
            {
                this._height = value > 0 ? value
                    : throw new ArgumentException("Height cannot be negative");
            }
        }

        public double X { get; set; }
        public double Y { get; set; }

        public double Perimetr
            => 2 * (this.Width + this.Height);

        public override string ToString()
        {
            return $"Height={this.Height}, Width={this.Width}, Perimeter={this.Perimetr}, X={this.X}, Y={this.Y}";
        }
    }
}
