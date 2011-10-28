﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace PloobsEngine.NetWorking
{
    public class NetworkServer
    {
        Dictionary<String, NetConnection> _conns = new Dictionary<string, NetConnection>();

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

        public NetworkServer(String serverName = "GameServer", int port = 14242)
        {
            NetPeerConfiguration config = new NetPeerConfiguration(serverName);
            config.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
            config.Port = port;

            // create and start server
            server = new NetServer(config);
            server.Start();
        }

        NetServer server;        

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

        Dictionary<NetMessageType, List<Action<NetMessageType, NetIncomingMessage>>> messagehandler = new Dictionary<NetMessageType, List<Action<NetMessageType, NetIncomingMessage>>>();

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