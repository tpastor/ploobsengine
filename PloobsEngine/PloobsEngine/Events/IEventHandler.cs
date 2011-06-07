using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Entity;
using PloobsEngine.MessageSystem;

namespace PloobsEngine.Events
{
    public interface IEventHandler : IEntity
    {
        void Process(Message mes);
    }
}
