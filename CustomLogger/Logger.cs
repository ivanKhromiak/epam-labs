namespace CustomLogger
{
    using System;
    using System.Text;

    /// <summary>
    /// Logs messages with targets logging config
    /// </summary>
    public class Logger : ILogger
    {
        internal Logger(string name, LogFactory logFactory, LoggingConfiguration configuration)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            LogFactory = logFactory ?? throw new ArgumentNullException(nameof(logFactory));
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string Name { get; }

        public LogFactory LogFactory { get; }

        internal LoggingConfiguration Configuration { get; set; }

        public void Debug(string message, Exception exception)
        {
            Log($"{GetTimeStamp()}: {message}\n" +
                $"Exception: {GetInfoFromException(exception, LogLevel.Debug)}");
        }

        public void Error(string message, Exception exception)
        {
            Log($"{GetTimeStamp()}: {message}\n" +
                $"Exception: {GetInfoFromException(exception, LogLevel.Error)}");
        }

        public void Fatal(string message, Exception exception)
        {
            Log($"{GetTimeStamp()}: {message}\n" +
                $"Exception: {GetInfoFromException(exception, LogLevel.Fatal)}");
        }

        public void Info(string message, Exception exception)
        {
            Log($"{GetTimeStamp()}: {message}\n" +
                $"Exception: {GetInfoFromException(exception, LogLevel.Info)}");
        }

        public void LogMessage(string message, LogLevel level)
        {
            Log($"{GetTimeStamp()}: {message}");
        }

        public void LogMessage(string message, LogLevel level, Exception exception)
        {
            Log($"{GetTimeStamp()}: {message}\n" +
                $"Exception: {GetInfoFromException(exception, level)}");
        }

        public void Trace(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message, Exception exception)
        {
            Log($"{GetTimeStamp()}: {message}\n" +
                $"Exception: {GetInfoFromException(exception, LogLevel.Warn)}");
        }

        private void Log(string message)
        {
            WriteToAllTargets($"{message}\n");
        }

        private void LogAsync(string message)
        {
            throw new NotImplementedException();
        }

        private void WriteToAllTargets(string message)
        {
            var targetWriters = Configuration.GetTargetWriters();

            foreach(var writer in targetWriters)
            {
                writer.WriteMessage(message);
            }
        }

        private void WriteToAllTargetsAsync(string message)
        {
            throw new NotImplementedException();
        }

        private string GetInfoFromException(Exception e, LogLevel level)
        {
            var strBuilder = new StringBuilder();

            if(level <= LogLevel.Trace)
            {
                AppendTraceInfo(e, strBuilder);
            }
            if(level <= LogLevel.Debug)
            {
                AppendDebugInfo(e, strBuilder);
            }
            if (level <= LogLevel.Warn)
            {
                strBuilder.Append($"Exception message: {e?.Message};");
            }
            
            return strBuilder.ToString();
        }

        private static void AppendDebugInfo(Exception e, StringBuilder strBuilder)
        {
            if (e is null)
            {
                return;
            }

            if (strBuilder is null)
            {
                throw new ArgumentNullException(nameof(strBuilder));
            }

            strBuilder.Append($"Module Name: {e.TargetSite.Module.FullyQualifiedName}");
            strBuilder.Append($"TargetSite Name: {e.TargetSite.Name}");
            strBuilder.Append($"Source: {e.Source}");
            strBuilder.Append($"Help link: {e.HelpLink}");
        }

        private void AppendTraceInfo(Exception e, StringBuilder strBuilder)
        {
            if(e == null)
            {
                return;
            }

            if (strBuilder is null)
            {
                throw new ArgumentNullException(nameof(strBuilder));
            }

            strBuilder.Append($"Stack trace: {e.StackTrace}; ");
            strBuilder.Append("Inner exception info: ");
            strBuilder.Append(GetInfoFromException(e?.InnerException, LogLevel.Trace));
        }

        private static string GetTimeStamp()
        {
            return DateTime.Now.ToString();
        }
    }
}
