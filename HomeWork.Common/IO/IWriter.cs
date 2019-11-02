namespace Epam.HomeWork.Common.IO
{
    public interface IWriter
    {
        void WriteLine(string message);

        void Write(string message);

        void Write(char ch);
    }
}