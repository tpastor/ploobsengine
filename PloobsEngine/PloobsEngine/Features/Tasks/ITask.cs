using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.Features
{
    /// <summary>
    /// Specification of a task
    /// Task is a class that operates in ANOTHER THREAD    
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// Called when the task ends
        /// </summary>
        /// <param name="result">The result.</param>
        void Result(IAsyncResult result);

        /// <summary>
        /// Processes the task.
        /// </summary>
        void Process();

        TaskEndType TaskEndType
        {
            get;           
        }

    }

    /// <summary>
    /// Quando a funcao result sera chamada, qd o process terminar
    /// </summary>
    public enum TaskEndType
    {
        INSTANT, ON_NEXT_UPDATE
    }

    
}
