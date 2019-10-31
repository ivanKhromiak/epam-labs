namespace Epam.HomeWork.Lab4
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    public class BinaryCollectionSerializer : ICollectionSerializer
    {
        public void Serialize<T>(IEnumerable<T> collection, string path) where T : new()
        {
            var type = typeof(T);

            if (!type.IsSerializable)
            {
                throw new ArgumentException($"Item {type.FullName} is not serializable!");
            }

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, collection);
            }
        }

        public IEnumerable<T> Deserialize<T>(string path) where T : new()
        {
            IEnumerable<T> collection = null;

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                collection = (IEnumerable<T>)bf.Deserialize(fs);
            }

            return collection;
        }
    }
}
