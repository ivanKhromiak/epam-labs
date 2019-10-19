namespace Epam.HomeWork.Lab4
{
    using System;

    [Serializable]
    public class Car
    {
        public int CarId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal Total { get; set; }

        public Car() { }
    }
}
