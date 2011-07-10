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
