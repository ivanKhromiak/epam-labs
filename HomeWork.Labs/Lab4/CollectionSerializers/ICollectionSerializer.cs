using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Epam.HomeWork.Lab4
{
    public interface ICollectionSerializer
    {
        void Serialize<T>(IEnumerable<T> collection, string path) where T : new();

        IEnumerable<T> Deserialize<T>(string path) where T : new();
    }
}
