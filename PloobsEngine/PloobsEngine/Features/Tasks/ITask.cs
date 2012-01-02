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

namespace PloobsEngine.Features
{
    /// <summary>
    /// Specification of a task
    /// Task is a class that operates in ANOTHER THREAD    
    /// </summary>
    public abstract class ITask
    {
#if !WINDOWS_PHONE
        public event Action<ITask, IAsyncResult> Ended;
#else
        public event Action<ITask> Ended;
#endif

#if !WINDOWS_PHONE
        /// <summary>
        /// Called when the task ends
        /// </summary>
        /// <param name="result">The result.</param>
        public virtual void Result(IAsyncResult result) 
        { 
            
            if (Ended != null) 
                Ended(this,result); 
        }
#else
        /// <summary>
        /// Called when the task ends
        /// </summary>
        public virtual void Result()
        {
            if (Ended != null)
                Ended(this);
        }
#endif

        /// <summary>
        /// Processes the task.
        /// </summary>
        public abstract void Process();

        public abstract TaskEndType TaskEndType
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
