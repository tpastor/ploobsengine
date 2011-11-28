using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Lidgren.Network;
using System.Diagnostics;

namespace PloobsEngine.NetWorking
{
    public class NetWorkEchoMessageClient
    {
        public NetWorkEchoMessageClient(String Identifier, Func<NetOutgoingMessage,NetOutgoingMessage> CreateMessage, Action<NetIncomingMessage> HandleMessageBack)
        {
            this.identifier = Identifier;
            this.CreateMessage = CreateMessage;
            this.HandleMessageBack = HandleMessageBack;
        }

        String identifier;

        public String Identifier
        {
            get { return identifier; }
            set { identifier = value; }
        }


        public Func<NetOutgoingMessage, NetOutgoingMessage> CreateMessage;
                
        public Action<NetIncomingMessage> HandleMessageBack;
        
    }
}
