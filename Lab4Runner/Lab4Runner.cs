namespace Epam.HomeWork.Lab4Runner
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Epam.HomeWork.Common;
    using Epam.HomeWork.Lab4;

    public class Lab4Runner : IConsoleLabRunner
    {
        public Lab4Runner()
        {
            Errors = new List<string>();
            Success = false;
        }

        public string Description => "Serialization";

        public IList<string> Errors { get; }

        public bool Success { get; set; }

        public void RunConsoleLab()
        {
            try
            {
                RunJsonSerialization();
                RunXmlSerialization();
                RunBinarySerialization();
            }
            catch (ArgumentException e)
            {
                Errors.Add(e.Message);
                Success = false;
            }
            catch (IOException e)
            {
                Errors.Add(e.Message);
                Success = false;
            }
            catch(Exception e)
            {
                Success = false;
                throw e;
            }
        }

        private void RunJsonSerialization()
        {
            ConsoleHelper.WriteHeaderMessage("Task 1: Json Serialization...\n", ConsoleColor.Yellow, ConsoleColor.Black);
            RunSerializationForCarList(new JsonCollectionSerializer(), "carList.json");

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();         
        }

        private void RunXmlSerialization()
        {
            ConsoleHelper.WriteHeaderMessage("Task 2: Xml Serialization...\n", ConsoleColor.Yellow, ConsoleColor.Black);
            RunSerializationForCarList(new XmlCollectionSerializer(), "carList.xml");

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void RunBinarySerialization()
        {
            ConsoleHelper.WriteHeaderMessage("Task 3: Binary Serialization...\n", ConsoleColor.Yellow, ConsoleColor.Black);
            RunSerializationForCarList(new BinaryCollectionSerializer(), "carList.bin");

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void RunSerializationForCarList(ICollectionSerializer serializer, string filename)
        {
            var carList = GetCarList();

            Console.WriteLine("\tList before serialization:");
            PrintCars(carList);

            serializer.Serialize(carList, filename);

            var deserializedCarList = serializer.Deserialize<Car>(filename);

            Console.WriteLine("\tList after deserialization:");
            PrintCars(deserializedCarList);
        }

        private static void PrintCars(IEnumerable<Car> deserializedCarList)
        {
            foreach (var car in deserializedCarList)
            {
                Console.Write($"\tCar #{car.CarId}; price: {car.Price}; ");
                Console.WriteLine($"quantity: {car.Quantity}; total: {car.Total}");
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
