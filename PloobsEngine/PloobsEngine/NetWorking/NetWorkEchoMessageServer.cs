using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Lidgren.Network;
using System.Diagnostics;

namespace PloobsEngine.NetWorking
{
    public class NetWorkEchoMessageServer
    {
        public NetWorkEchoMessageServer(String Identifier, Func<NetIncomingMessage, NetOutgoingMessage, NetOutgoingMessage> AnswerMessage)
        {
            this.identifier = Identifier;
            this.AnswerMessage = AnswerMessage;            
        }

        String identifier;

        public String Identifier
        {
            get { return identifier; }
            set { identifier = value; }
        }

        public Func<NetIncomingMessage, NetOutgoingMessage, NetOutgoingMessage> AnswerMessage;
        
    }
}
