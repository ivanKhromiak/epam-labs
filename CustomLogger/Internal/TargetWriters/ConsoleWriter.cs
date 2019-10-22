namespace CustomLogger.Internal
{
    using System;
    using System.Threading.Tasks;

    internal class ConsoleWriter : ITargetWriter
    {
        public void Flush()
        {
            throw new NotImplementedException();
        }

        public void WriteMessage(string message)
        {
            Console.Out.Flush();
        }

        public async Task WriteMessageAsync(string message)
        {
            await Console.Out.WriteLineAsync(message);
        }
    }
}
