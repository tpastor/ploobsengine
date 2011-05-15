using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.MessageSystem;
using PloobsEngine.Events;

namespace PloobsEngine.Trigger
{
    public abstract class GenericEventHandler : IRecieveMessageEntity , IEventHandler
    {        
        #region IEntity Members

        private int _id;
        public int GetId()
        {
            return _id;
        }

        public void SetId(int id)
        {
            this._id = id;
        }

        #endregion

        #region IRecieveMessageEntity Members

        public bool HandleThisMessageType(SenderType type)
        {
            return type == SenderType.EVENT;
        }

        public void HandleMessage(Message mes)
        {
            Process(mes);
        }

        #endregion

        #region IEventHandler Members

        public void Process(Message mes)
        {

            HandleEvent(mes.Content as IEvent<Object>);
        }

        public abstract void HandleEvent(IEvent<Object> evt);
        #endregion
    }
}
