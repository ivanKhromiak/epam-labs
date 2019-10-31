namespace Epam.HomeWork.Lab4
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;

    public class JsonCollectionSerializer : ICollectionSerializer
    {
        public void Serialize<T>(IEnumerable<T> collection, string path)
            where T : new()
        {
            var type = typeof(T);

            if(!type.IsSerializable)
            {
                throw new ArgumentException($"Item {type.FullName} is not serializable!");
            }

            string jsonCollection = JsonConvert.SerializeObject(collection, Formatting.Indented);

            File.WriteAllText(path, jsonCollection);
                
        }

        public IEnumerable<T> Deserialize<T>(string path)
            where T : new()
        {
            string jsonCollection = File.ReadAllText(path);

            var collection = JsonConvert.DeserializeObject<IEnumerable<T>>(jsonCollection);

            return collection;
        }
    }
}
