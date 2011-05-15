using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.MessageSystem;
using PloobsEngine.Entity;

namespace PloobsEngine.Events
{
    /// <summary>
    /// Evento Simples com alvos simples
    /// </summary>
    public class SimpleEvent : IEvent<object>
    {
        public SimpleEvent(String code)
        {
            this.code = code;
        }
        protected string code;
        protected object _source;

        protected IList<IEntity> _targets = new List<IEntity>();

        #region IEvent Members

        public virtual void FireEvent(object source)
        {
            this._source = source;
            foreach (IEntity item in _targets)
            {
                Message me = new Message(PrincipalConstants.EventSenderId, item.GetId(), null, Priority.LOW, 0, SenderType.EVENT, this, this.Code);
                MessageDeliver.SendMessage(me);
            }
        }

        public void AddTarget(IRecieveMessageEntity entity)
        {
            _targets.Add(entity);
        }

        public void RemoveTarget(IRecieveMessageEntity entity)
        {
            _targets.Remove(entity);
        }

        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }

        #endregion

        #region IEvent Members


        public object GetEventSource()
        {
            return _source;
        }

        #endregion

    }
}
