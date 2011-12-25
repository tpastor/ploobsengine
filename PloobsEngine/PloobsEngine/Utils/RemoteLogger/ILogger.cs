using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.Utils.RemoteLogger
{
    /// <summary>
    /// Logging interface
    /// In this release, the engine is not using the LOGGER interface, its using the
    /// DebugOutput, it will change in the next major release
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Log the message.
        /// </summary>
        /// <param name="Message">The message.</param>
        void LogMessage(String Message);
    }
}
