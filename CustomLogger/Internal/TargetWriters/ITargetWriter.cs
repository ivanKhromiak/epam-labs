namespace CustomLogger
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for log message target writers
    /// </summary>
    internal interface ITargetWriter
    {
        void WriteMessage(string message);
        Task WriteMessageAsync(string message);
        void Flush();
    }
}
