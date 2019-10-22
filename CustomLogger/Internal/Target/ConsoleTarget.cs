namespace CustomLogger.Internal
{
    /// <summary>
    /// Console Target
    /// </summary>
    internal class ConsoleTarget : Target
    {
        /// <summary>
        /// Gets a ITargetWriter for Console
        /// </summary>
        /// <returns>ConsoleWriter</returns>
        public override ITargetWriter GetTargetWriter()
        {
            return new ConsoleWriter();
        }
    }
}
