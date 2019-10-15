namespace Epam.HomeWork.Lab3Runner
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public static class FileFinder
    {
        public static IEnumerable<string> FindByPartialNameAndExtension(string fileExtension, string filename, string searchDirectoryName)
        {
            if (string.IsNullOrEmpty(fileExtension))
            {
                throw new ArgumentException("fileExtension cannot be null or empty", "fileExtension");
            }
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentException("filename cannot be null or empty", "filename");
            }
            if (string.IsNullOrEmpty(searchDirectoryName))
            {
                throw new ArgumentException("searchDirectoryName cannot be null or empty", "searchDirectoryName");
            }

            return Directory.GetFiles(searchDirectoryName, $"*{filename}*.{fileExtension}", SearchOption.AllDirectories);
        }
    }
}
