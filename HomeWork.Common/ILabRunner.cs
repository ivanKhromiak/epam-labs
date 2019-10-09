using System.Collections.Generic;

namespace Epam.HomeWork.Common
{
    public interface ILabRunner
    {
        string Description { get; }

        IList<string> Errors { get; }

        bool Success { get; }
    }
}
