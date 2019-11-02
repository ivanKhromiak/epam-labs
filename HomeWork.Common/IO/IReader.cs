namespace Epam.HomeWork.Common.IO
{
    using System;

    public interface IReader
    {
        bool TryRead(out int input);

        bool TryRead(out long input);

        bool TryRead(out double input);

        bool TryRead(out float input);

        string ReadLine();

        ConsoleKeyInfo ReadKey();
    }
}