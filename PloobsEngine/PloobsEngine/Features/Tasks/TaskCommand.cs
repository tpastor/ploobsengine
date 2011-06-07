using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Commands;

namespace PloobsEngine.Features
{
    /// <summary>
    /// Command that encapsulates a task
    /// </summary>
    public class TaskCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskCommand"/> class.
        /// </summary>
        /// <param name="task">The task.</param>
        public TaskCommand(ITask task)
        {
            this.task = task;
        }

        private TaskProcessor tp;
        private ITask task;

        #region IComponentCommand Members

        /// <summary>
        /// Executes the command Call.
        /// </summary>
        protected override void execute()
        {
            tp.StartTask(task);
        }

        /// <summary>
        /// Sets the command target.
        /// </summary>
        /// <param name="obj">The obj.</param>
        protected override void setTarget(object obj)
        {
            this.tp = obj as TaskProcessor;
        }

        /// <summary>
        /// Gets the name of the command target.
        /// </summary>
        /// <value>
        /// The name of the target.
        /// </value>
        public override string TargetName
        {
            get { return TaskProcessor.MyName; }
        }

        #endregion
    }
}
