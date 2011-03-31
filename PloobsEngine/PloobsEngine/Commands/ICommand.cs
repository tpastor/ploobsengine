using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.Commands
{
    /// <summary>
    /// Command Pattern
    /// Its a Class that encapsulates a function call
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Executes the command Call.
        /// </summary>
        void execute();
        /// <summary>
        /// Sets the command target.
        /// </summary>
        /// <param name="obj">The obj.</param>
        void setTarget(Object obj);
        /// <summary>
        /// Gets the name of the command target.
        /// </summary>
        /// <value>
        /// The name of the target.
        /// </value>
        String TargetName
        {
            get;
        }
        
    }
}
