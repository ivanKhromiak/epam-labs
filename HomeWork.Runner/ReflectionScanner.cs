namespace Epam.HomeWork.Runner
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public class ReflectionScanner
    {

        public static string DirPath = AppDomain.CurrentDomain.BaseDirectory;

        public static string[] ScanLibs()
        {
            return Directory.GetFiles(DirPath, "*.dll");
        }

        public static List<T> Scan<T>()
        {
            var result = new List<T>();
            var filePaths = ScanLibs();

            foreach (var filePath in filePaths)
            {
                var a = Assembly.LoadFrom(filePath);
                var allTypes = a.GetTypes();

                var typeslist = allTypes.Where(p =>
                {
                    return typeof(T).IsAssignableFrom(p)
                    && p.Name != typeof(T).Name;
                });

                foreach (var item in typeslist)
                {
                    var type = typeslist.First();
                    var newInstance = (T)a.CreateInstance(type.ToString());

                    if (newInstance == null)
                        throw new Exception($"Could not initialize instance in dll: {filePath}");

                    result.Add(newInstance);
                }
            }
            return result;
        }
    }
}
