using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.Engine.Logger
{
    public interface ILogger
    {
        void Log(String Message, LogLevel logLevel);
    }

    public enum LogLevel
    {
        FatalError,RecoverableError,Warning
    }

    public static class ActiveLogger
    {
        internal static ILogger logger;

        public static void LogMessage(String message, LogLevel level)
        {

            if (logger != null)
            {
                if (level == LogLevel.Warning)
                {
#if DEBUG
                    logger.Log(message, level);
#endif
                }
                else
                {
                    logger.Log(message, level);
                }
            }

        }
    }
}
