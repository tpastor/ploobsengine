using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.MessageSystem;

namespace PloobsEngine.Events
{
    public class EventManager
    {
        private IDictionary<String, IEventHandler> _handlers = new Dictionary<String, IEventHandler>();
        private IDictionary<String, SenderType> _codType = new Dictionary<String, SenderType>();
        private IList<SenderType> _handledTypes = new List<SenderType>();

        public void AddEventHandler(String MessageCod, SenderType type, IEventHandler handler)
        {
            _codType.Add(MessageCod, type);
            _handlers.Add(MessageCod, handler);
            _handledTypes.Add(type);

        }
        public void RemoveHandlers(String cod)
        {
            _handlers.Remove(cod);
            _handledTypes.Remove(_codType[cod]);
            _codType.Remove(cod);
        }


        public void ClearAll()
        {
            _handlers.Clear();
            _handledTypes.Clear();
            _codType.Clear();

        }

        public void HandleEvents(Message mes)
        {
            IEventHandler eh = null;
            _handlers.TryGetValue(mes.Cod, out eh);
            if (eh != null)
                eh.Process(mes);
        }

        public bool HandleThisType(SenderType type)
        {
            return _handledTypes.Contains(type);
        }
    }
}
