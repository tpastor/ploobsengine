using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using System.Diagnostics;
using PloobsEngine.SceneControl;

namespace PloobsEngine.NetWorking
{
    public class NetworkServer
    {
        IWorld world;
        NetServer server;
        Dictionary<string, NetWorkEchoMessageServer> NetWorkEcho = new Dictionary<string, NetWorkEchoMessageServer>();
        Dictionary<String, NetConnection> _conns = new Dictionary<string, NetConnection>();
        Dictionary<NetMessageType, List<Action<NetMessageType, NetIncomingMessage>>> messagehandler = new Dictionary<NetMessageType, List<Action<NetMessageType, NetIncomingMessage>>>();
        Dictionary<string, NetWorkServerObject> NetWorkObjects = new Dictionary<string, NetWorkServerObject>();

        public NetworkServer(IWorld world, String serverName = "GameServer", int port = 14242)
        {
            Debug.Assert(world != null);
            this.world = world;

            NetPeerConfiguration config = new NetPeerConfiguration(serverName);
            config.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
            config.Port = port;

            // create and start server
            server = new NetServer(config);
            server.Start();

            AddMessageHandler(NetMessageType.CreateNetworkObjectOnServer, RecieveCreateNetworkObjectOnServer);
            AddMessageHandler(NetMessageType.Echo, HandleEchoMessage);
        }

        private Dictionary<String, NetConnection> ConnectionsNames
        {
            get
            {
                lock (this)
                {
                    return _conns;
                }
            }
        }
        

        public NetOutgoingMessage CreateMessage(NetMessageType messageType)
        {
            NetOutgoingMessage mes = server.CreateMessage();
            mes.Write((short)messageType);
            return mes;
        }

        public NetOutgoingMessage CreateMessage(short messageType)
        {
            NetOutgoingMessage mes = server.CreateMessage();
            mes.Write(messageType);
            return mes;
        }

        public void SendMessageToAllClients(NetOutgoingMessage mes,NetDeliveryMethod method = NetDeliveryMethod.Unreliable)
        {
            server.SendMessage(mes, server.Connections, method,0);
        }
                
        public void CreateServerObject(NetWorkServerObject no)
        {
            Debug.Assert(no.CreateLoadObjectOrder != null);
            NetWorkObjects.Add(no.Identifier, no);
        }

        void RecieveCreateNetworkObjectOnServer(NetMessageType NetMessageType, NetIncomingMessage NetIncomingMessage)
        {
            String ident = NetIncomingMessage.ReadString();
            if (NetWorkObjects.ContainsKey(ident))
            {
                ServerIObject obj = NetWorkObjects[ident].CreateLoadObjectOrder(NetIncomingMessage);
                world.AddObject(obj);

                ///skip the message type                
                NetIncomingMessage.Position = sizeof(short) * 8;

                NetOutgoingMessage o = CreateMessage(NetWorking.NetMessageType.CreateNetworkObjectOnClient);
                o.Write(obj.GetId());
                NetOutgoingMessage mes = NetWorkObjects[ident].CreateRedistributeOrder(obj, NetIncomingMessage, o);
                this.SendMessageToAllClients(mes,NetDeliveryMethod.ReliableOrdered);
            }
        }

        public void AddNetWorkEchoMessage(NetWorkEchoMessageServer mes)
        {
            Debug.Assert(mes.AnswerMessage != null);            
            NetWorkEcho[mes.Identifier] = mes;
        }

        void HandleEchoMessage(NetMessageType NetMessageType, NetIncomingMessage NetIncomingMessage)
        {
            String ident = NetIncomingMessage.ReadString();
            long unique = NetIncomingMessage.ReadInt64();

            if(NetWorkEcho.ContainsKey(ident))
            {
                NetOutgoingMessage o = CreateMessage(NetWorking.NetMessageType.Echo);
                o.Write(ident);
                o.Write(unique);
                SendMessageToAllClients(NetWorkEcho[ident].AnswerMessage(NetIncomingMessage,o),NetDeliveryMethod.ReliableOrdered);
            }
        }    

        public void AddMessageHandler(NetMessageType messageType, Action<NetMessageType, NetIncomingMessage> handler)
        {
            if (!messagehandler.ContainsKey(messageType))
            {
                messagehandler[messageType] = new List<Action<NetMessageType, NetIncomingMessage>>();
                messagehandler[messageType].Add(handler);
            }
            else
            {
                messagehandler[messageType].Add(handler);
            }
        }

        public virtual void SyncAllClients()
        {
            foreach (var item in  world.Objects)
            {
                if (item.PhysicObject.isMotionLess == false)
                {
                    NetOutgoingMessage mes = this.CreateMessage(NetMessageType.PhysicInternalSync);
                    mes.WriteEntitySync(item.GetId(), item.PhysicObject.Position, item.PhysicObject.Rotation, item.PhysicObject.Velocity, item.PhysicObject.AngularVelocity);
                    this.SendMessageToAllClients(mes, NetDeliveryMethod.Unreliable);
                }
            }
        }


        public void ProccessMessageSync()
        {
            NetIncomingMessage msg;
            while ((msg = server.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryRequest:
                        bool conFinded = false;
                        foreach (var item in server.Connections)
                        {
                            if (item.RemoteEndpoint.Address.Equals(msg.SenderEndpoint.Address))
                            {
                                conFinded = true;
                                break;
                            }
                        }
                        if (!conFinded)
                        {
                            Console.WriteLine("ClientDiscovered");
                            server.SendDiscoveryResponse(null, msg.SenderEndpoint);                            
                        }
                        break;
                    case NetIncomingMessageType.VerboseDebugMessage:
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.ErrorMessage:
                        Console.WriteLine(msg.ReadString());
                        break;
                    case NetIncomingMessageType.StatusChanged:
                            NetConnectionStatus status = (NetConnectionStatus)msg.ReadByte();
                            if (status == NetConnectionStatus.Connected)
                            {
                                ConnectionsNames.Add(msg.SenderEndpoint.ToString(), msg.SenderConnection);
                            }
                            else if(status == NetConnectionStatus.Disconnecting)
                            {
                                ConnectionsNames.Remove(msg.SenderEndpoint.ToString());
                            }

                            break;
         
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
    }
}
