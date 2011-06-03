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

    /// <summary>
    /// Log Levels
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// WHen a Fatal error occurrs, Normally the application also throw an exception when this happen
        /// </summary>
        FatalError,
        /// <summary>
        /// Errors that the engine can handle, but the user should correct
        /// </summary>
        RecoverableError,
        /// <summary>
        /// When something happens that can be an error
        /// </summary>
        Warning
    }

    public static class ActiveLogger
    {
        internal static ILogger logger;

        public static void LogMessage(String message, LogLevel level)
        {

            if (logger != null)
            {
                if (level == LogLevel.Warning )
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
