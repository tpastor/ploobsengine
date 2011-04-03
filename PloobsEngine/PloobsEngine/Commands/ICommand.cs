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
    public abstract class ICommand
    {
        /// <summary>
        /// Executes the command Call.
        /// </summary>
        protected abstract void execute();
        internal void iexecute()
        {
            execute();
        }
        /// <summary>
        /// Sets the command target.
        /// </summary>
        /// <param name="obj">The obj.</param>
        protected abstract void setTarget(Object obj);
        internal void isetTarget(Object obj)
        {
            setTarget(obj);
        }
        /// <summary>
        /// Gets the name of the command target.
        /// </summary>
        /// <value>
        /// The name of the target.
        /// </value>
        public abstract String TargetName
        {
            get;
        }
        
    }
}
