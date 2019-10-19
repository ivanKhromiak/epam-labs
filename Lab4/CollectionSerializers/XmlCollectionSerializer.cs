namespace Epam.HomeWork.Lab4
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    public class XmlCollectionSerializer : ICollectionSerializer
    {
        public void Serialize<T>(IEnumerable<T> collection, string path) where T : new()
        {
            var type = typeof(T);

            if (!type.IsSerializable)
            {
                throw new ArgumentException($"Item {type.FullName} is not serializable!");
            }

            var serializer = new XmlSerializer(typeof(List<T>));

            using (var sw = new StringWriter())
            {
                serializer.Serialize(sw, collection.ToList());
                File.WriteAllText(path, sw.ToString());
            }
        }


        public IEnumerable<T> Deserialize<T>(string path) where T : new()
        {
            var serializer = new XmlSerializer(typeof(List<T>));
            List<T> collection = null;

            using (StreamReader reader = new StreamReader(path))
            {
                collection = (List<T>)serializer.Deserialize(reader);
            }

            return collection;
        }
    }
}
