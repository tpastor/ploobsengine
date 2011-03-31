using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Entity;


namespace PloobsEngine.MessageSystem
{
    /// <summary>    
    /// Helper to Handle some message
    /// </summary>
    public class SystemRecieverMessage : IRecieveMessageEntity, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemRecieverMessage"/> class.
        /// </summary>
        public SystemRecieverMessage()
        {
            EntityMapper.getInstance().AddEntity(this);
        }
        
        #region IRecieveMessageEntity Members

        /// <summary>
        /// Handles a message from determined sender type.
        /// </summary>
        /// <param name="type">Sender type.</param>
        /// <returns></returns>
        public bool HandleThisMessageType(SenderType type)
        {
            return type == SenderType.SYSTEM;
        }

        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="mes">The mes.</param>
        public virtual void HandleMessage(Message mes)
        {
            
        }

        #endregion

        #region IEntity Members
        int id;
        /// <summary>
        /// return the entity id
        /// </summary>
        /// <returns>
        /// the id
        /// </returns>
        public int getId()
        {
            return id;
        }

        /// <summary>
        /// sets the id
        /// </summary>
        /// <param name="id"></param>
        public void setId(int id)
        {
            this.id = id;
        }

        #endregion
    
        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void  Dispose()
        {
            EntityMapper.getInstance().RemoveEntity(this);
        }

        #endregion
}
}
