namespace Epam.HomeWork.Lab3
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public static class DirectoryVisualizer
    {
        public static IEnumerable<string> GetFilesFromDirectory(string inputDirectory, int maxSubDirectoryDepth)
        {
            return GetFilesFromDirectory(inputDirectory, 0, maxSubDirectoryDepth);
        }

        private static IEnumerable<string> GetFilesFromDirectory(string inputDirectory, int padLeft, int maxSubDirectoryDepth)
        {
            if (string.IsNullOrEmpty(inputDirectory))
            {
                throw new ArgumentException("inputDirectory cannot be null or empty", "inputDirectory");
            }
            if (maxSubDirectoryDepth < 0)
            {
                throw new ArgumentException("Max subdirectory depth cannot be a negative value", "maxDepth");
            }

            var fileList = new List<string>();

            if (maxSubDirectoryDepth != 0)
            {
                var dirInfo = new DirectoryInfo(inputDirectory);

                fileList.Add($"{GetLeftPad(padLeft)}{inputDirectory}");

                foreach (var file in dirInfo.GetFiles().OrderBy(f => f.Name))
                {
                    fileList.Add($"{GetLeftPad(padLeft + 1)}{file.Name}");
                }

                foreach (var subDir in dirInfo.GetDirectories().OrderBy(d => d.Name))
                {
                    fileList.AddRange(GetFilesFromDirectory(subDir.FullName, padLeft + 2, maxSubDirectoryDepth - 1));
                }
            }

            return fileList;
        }

        private static string GetLeftPad(int length)
        {
            return length >= 1 ? '|' + new string('-', length - 1) : "";
        }
    }
}
