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
