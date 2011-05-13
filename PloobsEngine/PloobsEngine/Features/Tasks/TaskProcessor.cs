using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Components;
using System.IO;

namespace PloobsEngine.Features
{
    /// <summary>
    /// Task Command Executor
    /// </summary>
    public class TaskProcessor :  IComponent
    {        
        private delegate void Task();
        /// <summary>
        /// The Name of the Component
        /// </summary>
        public static readonly String MyName = "TaskProcessor";

        private List<ITask> finished = new List<ITask>();

        protected List<ITask> Finished
        {
            get {
                lock (finished)
                {
                    return finished;
                }                
            }
            
        }

        /// <summary>
        /// Starts the task.
        /// </summary>
        /// <param name="task">The task.</param>
        public void StartTask(ITask task)
        {
            Task t = new Task(task.Process);
            if (task.TaskEndType == TaskEndType.INSTANT)
            {                
                t.BeginInvoke(new AsyncCallback(task.Result), null);
            }
            else if (task.TaskEndType == TaskEndType.ON_NEXT_UPDATE)
            {                
                t.BeginInvoke(new AsyncCallback(onfinished), task);                
            }
        }

        private void onfinished(IAsyncResult ar)
        {
            ITask task = ar.AsyncState as ITask;
            System.Diagnostics.Debug.Assert(task != null);

            Finished.Add(task);
        }

        protected override void Update(GameTime gt)
        {
            base.Update(gt);
            if (Finished.Count != 0)
            {
                foreach (var item in Finished)
                {
                    item.Result(null);
                }
                Finished.Clear();
            }
        }

        /// <summary>
        /// Gets the type of the component type.
        /// </summary>
        /// <value>
        /// The type of the component.
        /// </value>
        public override ComponentType ComponentType
        {
            get { return ComponentType.UPDATEABLE; }
        }

        #region IReciever Members

        /// <summary>
        /// The name of the reciever
        /// MUST BE UNIQUE
        /// </summary>
        /// <returns></returns>
        public override  string getMyName()
        {
            return MyName;
        }

        #endregion


     
    }


}
