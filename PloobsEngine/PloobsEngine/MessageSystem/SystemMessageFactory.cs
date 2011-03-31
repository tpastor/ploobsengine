using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.MessageSystem
{
    /// <summary>
    /// Helper to create system messages
    /// </summary>
    public class SystemMessageFactory
    {
        /// <summary>
        /// Not found message.
        /// </summary>
        /// <param name="reciever">The reciever.</param>
        /// <param name="notfoundid">The notfoundid.</param>
        /// <returns></returns>
        public static Message NotFoundReciever(int reciever,int notfoundid)
        {
            return new Message(PrincipalConstants.MessageDeliverId,reciever,null,Priority.VERYLOW,0,SenderType.SYSTEM,notfoundid,PrincipalConstants.RecieverNotFound);            
        }

    }
}
