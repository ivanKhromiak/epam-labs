using System;

namespace CustomLogger
{
    public static class LogManager
    {
        public static LoggingConfiguration Configuration { get; set; }

        public static LogFactory LogFactory { get; }

        public static Logger GetLogger(string name)
        {
            throw new NotImplementedException();
        }

        public static void Shutdown()
        {
            throw new NotImplementedException();
        }

    }
}
