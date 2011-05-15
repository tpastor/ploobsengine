using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.DataStructure;
using PloobsEngine.Entity;
using PloobsEngine.Components;


namespace PloobsEngine.MessageSystem
{
    /// <summary>
    /// Responsible to Deliver the messages to the Senders
    /// o interessante eh q eu uso uma interface combinada com um delegate (fico muito loco , vo patentia ...)
    /// funciona assim , voce extende um interface q tem um metodo do tipo CanIhandleThisMessage q recebe um
    /// tipo de mensagem , se ele consegue dar handler ele devolve um delegate pra o tratador de mensagem q ele implementou
    /// .... veja o exemplo do Sampleagente q ficara mais facil de entender
    /// o que importa eh q a pessoa implementa apenas 2 funcoezinhas e ja ta OK para enviar
    /// e receber mensagens e o sistema ja sabe como trata-las...
    /// o sistema faz o "roteamento" das mensagens pra encontrar o destinatario
    /// eh possivel fazer broadcast e enviar apenas para um grupo usando as tags 
    /// USEI components (nassa arquitetura) pra ficar mais facil de "plugar"    
    /// </summary>

    public class MessageDeliver :  IComponent
    {
        public MessageDeliver() {  }
        public MessageDeliver( int Num)  { numMessagerdeliveredByFrame = Num; }
        private static GameTime gt = new GameTime();
        private static PriorityQueueB<Message> fila = new PriorityQueueB<Message>(new MessageComparer());
        private static PriorityQueueB<Message> delayedfila = new PriorityQueueB<Message>(new DelayComparer());
        private static int numMessagerdeliveredByFrame = 2; // numero de mensagems q podem ser entregues por frame (deslentiar) , so vale para as q n tem tempo

        /// <summary>
        /// Gets or sets the messagerdelivered by frame.
        /// </summary>
        /// <value>
        /// The num messagerdelivered by frame.
        /// </value>
        public int NumMessagerdeliveredByFrame
        {
            get { return numMessagerdeliveredByFrame; }
            set { numMessagerdeliveredByFrame = value;}
        }
        
        /// <summary>
        /// Deliver the messages
        /// </summary>
        /// <param name="gt"></param>
        protected override void Update(GameTime gtt)
        {
            gt = gtt;

            ///primeiro Todas as com tempo 0
            while (true)
            {
                Message m = delayedfila.Peek();
                if (m == null)
                    break;
                if (gtt.TotalGameTime.TotalMilliseconds > m.Timetodeliver)
                {
                    deliver(delayedfila.Pop());
                }
                else
                {
                    break;
                }
            }            

            /// o resto respeitando o limite por frame
            for (int i = 0; i < numMessagerdeliveredByFrame; i++)
            {
                if (fila.Count == 0)
                    break;
    
            Message m = fila.Pop();

            deliver(m);
                
            }

        }

        /// <summary>
        /// Check is performed NOW,if the message is send later problems may occur
        /// </summary>
        /// <param name="mem">The mem.</param>
        /// <returns></returns>
        public static bool SendMessageWithChecking(Message mem)
        {
            if (EntityMapper.getInstance().CheckEntity(mem.Receiver))
            {
                SendMessage(mem);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Put the message in the Sender Queue
        /// </summary>
        /// <param name="mem">The mem.</param>
        public static void SendMessage(Message mem)
        {
            if (mem.Timetodeliver == 0)
            {
                fila.Push(mem);
                
            }
            else if (mem.Timetodeliver < 0)
            {
                deliver(mem);

            }
            else
            {
                double tempo = mem.Timetodeliver + gt.TotalGameTime.TotalMilliseconds;
                mem.Timetodeliver = tempo;
                delayedfila.Push(mem);
            }
            
        }

        private static void deliver(Message m)
        {
            if (m.Tag != null)
            {
                if (m.Tag == "all")
                {
                    foreach (IRecieveMessageEntity var in EntityMapper.getInstance().GetAllRecieveEntities())
                    {
                       
                            if (var.HandleThisMessageType(m.SenderType) != false)
                            {
                                var.HandleMessage(m);
                            }
                     

                    }
                    return;
                }

                IList<IRecieveMessageEntity> list = EntityMapper.getInstance().GetTagRecieveEntity(m.Tag);
                foreach (IRecieveMessageEntity var in list)
                {

                    if (var.HandleThisMessageType(m.SenderType) != false)
                    {
                        var.HandleMessage(m);
                    }
                }
            }
            else
            {
                if (EntityMapper.getInstance().CheckEntity(m.Receiver))
                {
                    IRecieveMessageEntity ag = EntityMapper.getInstance().getRecieveEntity(m.Receiver);
                    if (ag.HandleThisMessageType(m.SenderType) != false)
                    {
                        ag.HandleMessage(m);
                    }
                }
                else
                {
                    if (m.Check == Checks.CHECK_DELIVERY)
                    {
                        SendMessageWithChecking(SystemMessageFactory.NotFoundReciever(m.Sender, m.Receiver));
                    }
                }


            }
        }


        #region IReciever Members

        public static readonly String MyName = "MessageDeliver";

        /// <summary>
        /// Gets The component Name.
        /// </summary>
        /// <returns></returns>
        public override string getMyName()
        {
            return MyName;
        }

        #endregion

        /// <summary>
        /// Gets the type of the component.
        /// </summary>
        /// <value>
        /// The type of the component.
        /// </value>
        public override ComponentType ComponentType
        {
            get { return ComponentType.UPDATEABLE; }
        }


    }

    /// <summary>
    /// Message Comparer
    /// </summary>
    internal class MessageComparer : IComparer<Message>
    {
        /// <summary>
        /// Compares the messages
        /// </summary>
        /// <param name="nodeA">The node A.</param>
        /// <param name="nodeb">The nodeb.</param>
        /// <returns></returns>
        public int Compare(Message nodeA, Message nodeb )
        {
            if (nodeA.Prioridade > nodeb.Prioridade)
                return 1;
            if (nodeA.Prioridade < nodeb.Prioridade)
                return -1;
            return 0;
        }

    }

    /// <summary>
    /// Delay Comparer
    /// </summary>
    internal class DelayComparer : IComparer<Message>
    {
        /// <summary>
        /// Compare the delays of the messages
        /// </summary>
        /// <param name="nodeA">The node A.</param>
        /// <param name="nodeb">The nodeb.</param>
        /// <returns></returns>
        public int Compare(Message nodeA, Message nodeb)
        {
            if (nodeA.Timetodeliver < nodeb.Timetodeliver)
                return 1;
            if (nodeA.Timetodeliver > nodeb.Timetodeliver)
                return -1;
            return 0;
        }

    }


}
