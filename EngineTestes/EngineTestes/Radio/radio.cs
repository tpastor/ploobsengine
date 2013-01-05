using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Entity;
using PloobsEngine.MessageSystem;

namespace EngineTestes.Radio
{
    public class Radio : IRecieveMessageEntity
    {
        public const String RadioGeneralChannel = "RadioGeneralChannel";

        public Radio()
        {            
            EntityMapper.getInstance().AddEntity(this);
            EntityMapper.getInstance().AddgrouptagRecieveEntity(RadioGeneralChannel, this);
        }

        
        public void AddChannel(String Channel)
        {
            EntityMapper.getInstance().AddgrouptagRecieveEntity(Channel, this);
        }

        public void RemoveChannel(String Channel)
        {
            EntityMapper.getInstance().RemovegrouptagRecieveEntity(Channel, this);
        }

        public void SendMessage(String title, String Message, String Channel = RadioGeneralChannel)
        {
            Message m = new Message(GetId(), -1, Channel, Priority.HIGH, 0, SenderType.NORMAL, Message, title);
            MessageDeliver.SendMessage(m);
        }

        #region IRecieveMessageEntity Members
        public event Action<Message> MessageHandler;
        public bool HandleThisMessageType(SenderType type)
        {
            return true;
        }

        public void HandleMessage(Message mes)
        {
            if (MessageHandler != null)
                MessageHandler(mes);
        }

        #endregion

        #region IEntity Members
        long id;
        public long GetId()
        {
            return id;
        }

        public void SetId(long id)
        {
            this.id = id;
        }

        #endregion

    }
}
