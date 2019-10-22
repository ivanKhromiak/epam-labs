namespace CustomLogger.Internal
{
    internal abstract class Target
    {
        public abstract ITargetWriter GetTargetWriter();
    }
}