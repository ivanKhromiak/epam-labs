namespace Epam.HomeWork.Common
{
    using System.Collections.Generic;
    using Epam.HomeWork.Common.IO;

    public interface ILabRunner
    {
        IWriter Writer { get; set; }

        IReader Reader { get; set; }

        string Description { get; }

        IList<string> Errors { get; }

        bool Success { get; }

        void Run();
    }
}
