#region License
/*
    PloobsEngine Game Engine Version 0.3 Beta
    Copyright (C) 2011  Ploobs

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.Engine.Logger
{
    /// <summary>
    /// Interface to define a Ploobs Logger
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="logLevel">The log level.</param>
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
        Warning,

        /// <summary>
        /// Information only
        /// </summary>
        Info
    }

    /// <summary>
    /// Static Class responsible for Ploobs logging 
    /// </summary>
    public static class ActiveLogger
    {
        internal static ILogger logger;

        /// <summary>
        /// Logs a message.
        /// In Release, warning messages are not logged
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="level">The level.</param>
        public static void LogMessage(String message, LogLevel level)
        {

            if (logger != null)
            {
                if (level == LogLevel.Warning || level == LogLevel.Info)
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
