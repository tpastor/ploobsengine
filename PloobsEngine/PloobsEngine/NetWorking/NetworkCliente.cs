using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Xna.Framework;
using PloobsEngine.Physics;
using System.Diagnostics;
using PloobsEngine.SceneControl;
using PloobsEngine.Entity;

namespace PloobsEngine.NetWorking
{
    
    public class NetworkCliente
    {

        NetClient client;
        NetConnection servercon;
        IWorld world;
        Dictionary<string, NetWorkClientObject> NetWorkObjects = new Dictionary<string, NetWorkClientObject>();
        Dictionary<PhysicObjectTypes, Action<NetIncomingMessage, IObject>> Synchandlers = new Dictionary<PhysicObjectTypes, Action<NetIncomingMessage, IObject>>();
        Dictionary<NetMessageType, List<Action<NetMessageType, NetIncomingMessage>>> messagehandler = new Dictionary<NetMessageType, List<Action<NetMessageType, NetIncomingMessage>>>();

        public NetworkCliente(IWorld world, String serverName = "GameServer", int port = 14242, bool waitForConnect = true)
        {
            Debug.Assert(world != null);
            this.world = world;
            NetPeerConfiguration config = new NetPeerConfiguration(serverName);
            config.EnableMessageType(NetIncomingMessageType.DiscoveryResponse);

            client = new NetClient(config);
            client.Start();
            client.DiscoverLocalPeers(port);

            if (waitForConnect)
            {
                waitConnection();
            }

            AddMessageHandler(NetMessageType.CreateNetworkObjectOnClient, RecieveCreateNetworkObjectOnClient);
            AddMessageHandler(NetMessageType.PhysicInternalSync, StartRecieveSyncPhysicMessages);
        }


        public void AddMessageHandler(NetMessageType messageType , Action<NetMessageType, NetIncomingMessage> handler)
        {
            if(!messagehandler.ContainsKey(messageType))
            {
                messagehandler[messageType] = new List<Action<NetMessageType,NetIncomingMessage>>();
                messagehandler[messageType].Add(handler);
            }
            else
            {
                   messagehandler[messageType].Add(handler);
            }
        }

        public NetOutgoingMessage CreateMessage(NetMessageType messageType)
        {
            NetOutgoingMessage mes = client.CreateMessage();
            mes.Write((short)messageType);
            return mes;
        }


        public NetOutgoingMessage CreateMessage(short messageType)
        {
            NetOutgoingMessage mes = client.CreateMessage();
            mes.Write(messageType);
            return mes;
        }

        public void SendMessage(NetOutgoingMessage mes,NetDeliveryMethod method = NetDeliveryMethod.Unreliable)
        {
            if (servercon == null)
                waitConnection();

            if (servercon.Status == NetConnectionStatus.Disconnected)
            {
                PloobsEngine.Engine.Logger.ActiveLogger.LogMessage("Connection lost " + servercon.ToString(), Engine.Logger.LogLevel.Warning);
                client.Connect(servercon.RemoteEndpoint);
                waitConnection();
                
            }

            client.SendMessage(mes, servercon, method);
        }

        public void ProcessMessageSync()
        {
            if (servercon == null)
                waitConnection();

            NetIncomingMessage msg;
            while ((msg = client.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.Data:                        
                        NetMessageType type =(NetMessageType) msg.ReadInt16();
                        if (messagehandler[type] != null)
                        {
                            foreach (var item in messagehandler[type])
                            {
                                item(type, msg);
                            }                            
                        }                        
                        break;
                }
            }
        }
        
        private void waitConnection()
        {
            NetIncomingMessage msg;
            while (true)
            {
                while ((msg = client.ReadMessage()) != null)
                {
                    switch (msg.MessageType)
                    {
                        case NetIncomingMessageType.DiscoveryResponse:
                            servercon = client.Connect(msg.SenderEndpoint);
                            if (servercon != null)
                                return;
                            break;
                    }
                }
                Thread.Sleep(100);
            }
        }

        public void CreateNetWorkObject(NetWorkClientObject no)
        {
            Debug.Assert(no.CreateRemoteObjectOrder != null);
            Debug.Assert(no.HandleRemoteObjectOrder != null);

            NetOutgoingMessage om = this.CreateMessage(NetMessageType.CreateNetworkObjectOnServer);
            om.Write(no.Identifier);
            SendMessage(no.CreateRemoteObjectOrder(om),NetDeliveryMethod.ReliableOrdered);
            if(!NetWorkObjects.ContainsKey(no.Identifier))
                NetWorkObjects.Add(no.Identifier, no);
        }
                
        void RecieveCreateNetworkObjectOnClient(NetMessageType NetMessageType, NetIncomingMessage NetIncomingMessage)
        {
            int id = NetIncomingMessage.ReadInt32();
            String ident = NetIncomingMessage.ReadString();
            if (NetWorkObjects.ContainsKey(ident))
            {                
                IObject obj = NetWorkObjects[ident].HandleRemoteObjectOrder(NetIncomingMessage, id);
                world.AddObject(obj);
            }
        }
               

        public void RegisterMessageSync(PhysicObjectTypes type , Action<NetIncomingMessage,IObject> handler)
        {            
            Debug.Assert(handler!=null);
            Synchandlers.Add(type, handler);
        }

        void StartRecieveSyncPhysicMessages(NetMessageType NetMessageType, NetIncomingMessage NetIncomingMessage)
        {            
            int id = NetIncomingMessage.ReadInt32();
            IObject obj = (IObject)EntityMapper.getInstance().getEntity(id);

            PhysicObjectTypes tp = obj.PhysicObject.PhysicObjectTypes;
            if(Synchandlers.ContainsKey(obj.PhysicObject.PhysicObjectTypes))
            {
                Synchandlers[tp](NetIncomingMessage,obj);
            }
        }
    }
}

