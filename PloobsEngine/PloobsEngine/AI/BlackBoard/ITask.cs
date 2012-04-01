using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.IA
{
    public abstract class ITask
    {
        public ITask(String taskName)
        {
            this.QueueNameHandler = taskName;
        }

        public String QueueNameHandler
        {
            private set;
            get;
        }
    }
}
