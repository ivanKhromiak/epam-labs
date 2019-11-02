namespace Epam.HomeWork.Lab4Runner
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Epam.HomeWork.Common;
    using Epam.HomeWork.Common.IO;
    using Epam.HomeWork.Lab4;
    using Epam.HomeWork.LabRunners.Common;

    public class Lab4Runner : ILabRunner
    {
        public Lab4Runner()
        {
            this.Errors = new List<string>();
            this.Success = false;

            this.Writer = new ConsoleWriter();
            this.Reader = new ConsoleReader();
        }

        public string Description => "Lab 4: Serialization";

        public IList<string> Errors { get; }

        public bool Success { get; set; }

        public IWriter Writer { get; set; }

        public IReader Reader { get; set; }

        public void Run()
        {
            try
            {
                this.RunJsonSerialization();
                this.RunXmlSerialization();
                this.RunBinarySerialization();
            }
            catch (ArgumentException e)
            {
                this.Errors.Add($"{e.TargetSite.Name}: {e.Message}");
                this.Success = false;
            }
            catch (IOException e)
            {
                this.Errors.Add($"{e.TargetSite.Name}: {e.Message}");
                this.Success = false;
            }
            catch(Exception e)
            {
                this.Success = false;
                throw e;
            }
        }

        private void RunJsonSerialization()
        {
            ConsoleWriterHelper
                .WriteHeaderMessage("Task 1: JSON Serialization:\n", this.Writer);

            this.RunSerializationForCarList(new JsonCollectionSerializer(), "carList.json");

            this.Writer.WriteLine("\t\nPress any key to continue...");
            this.Reader.ReadKey();         
        }

        private void RunXmlSerialization()
        {
            ConsoleWriterHelper
                .WriteHeaderMessage("Task 2: XML Serialization:\n", this.Writer);

            this.RunSerializationForCarList(new XmlCollectionSerializer(), "carList.xml");

            this.Writer.WriteLine("\t\nPress any key to continue...");
            this.Reader.ReadKey();
        }

        private void RunBinarySerialization()
        {
            ConsoleWriterHelper
                .WriteHeaderMessage("Task 3: Binary Serialization:\n", this.Writer);

            this.RunSerializationForCarList(new BinaryCollectionSerializer(), "carList.bin");

            this.Writer.WriteLine("\t\nPress any key to continue...");
            this.Reader.ReadKey();
        }

        private void RunSerializationForCarList(ICollectionSerializer serializer, string filename)
        {
            List<Car> carList = GetCarList();

            this.Writer.WriteLine("\tList before serialization:");
            this.PrintCars(carList);

            serializer.Serialize(carList, filename);

            var deserializedCarList = serializer.Deserialize<Car>(filename);

            this.Writer.WriteLine("\tList after deserialization:");
            this.PrintCars(deserializedCarList);
        }

        private void PrintCars(IEnumerable<Car> deserializedCarList)
        {
            foreach (var car in deserializedCarList)
            {
                this.Writer.Write($"\tCar #{car.CarId}; price: {car.Price}; ");
                this.Writer.WriteLine($"quantity: {car.Quantity}; total: {car.Total}");
            }
        }

        private static List<Car> GetCarList()
        {
            return new List<Car>
            {
                new Car
                {
                    CarId = 1,
                    Price = 1000,
                    Quantity = 2,
                    Total = 1400
                },
                new Car
                {
                    CarId = 2,
                    Price = 1000,
                    Quantity = 2,
                    Total = 1400
                }
            };
        }
    }
}
