using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.Utils
{
    /// <summary>
    /// Helper For Delaying a variable setting action
    /// Values setted to this variable are Staged until ApplyValue() is called
    /// Usefull in multThread environment (when we need to let the user set a variable, but make its values current after some time)
    /// Use StaggedValue to set a value and Value to get
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StageVariable<T>
    {
        private T currentval;
        private T newval;

        /// <summary>
        /// Make the stagged value to be the actual value of the variable
        /// </summary>
        public void ApplyValue()
        {
            this.currentval = newval;
        }

        /// <summary>
        /// Gets the actual Value (not the stagged)
        /// </summary>
        /// <value>
        /// The value of the variable
        /// </value>
        public T Value
        {
            get
            {
                return currentval;
            }
            set
            {
                this.newval = value;
            }
        }

        /// <summary>
        /// Gets or sets the stagged value. (That will become the Variables's value when ApplyValue is called)
        /// </summary>
        /// <value>
        /// The stagged value.
        /// </value>
        public T StaggedValue
        {
            get
            {
                return newval;
            }
            set
            {
                this.newval = value;
            }
        }
    }
}
