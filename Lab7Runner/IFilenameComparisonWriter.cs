namespace Epam.HomeWork.Lab7Runner
{
    using System.Collections.Generic;

    public interface IFilenameComparisonWriter
    {
        void WriteFilenameData(string headerMessage, IEnumerable<string> filenames, string filepath);
    }
}
