using System;
using System.Collections.Generic;
using System.Text;

namespace PloobsEngine.MessageSystem
{
     /// <summary>
     /// Message To Be sent
     /// </summary>
    public class Message
    {
        private Checks check = Checks.DONT_CHECK_DELIVERY;

        /// <summary>
        /// Gets or sets the check.
        /// This is used by the Message deliver to decide id it will check if the
        /// Reciever exists
        /// </summary>
        /// <value>
        /// The check.
        /// </value>
        public Checks Check
        {
            get { return check; }
            set { check = value; }
        }

        private int sender;

        /// <summary>
        /// Gets or sets the sender ID.
        /// </summary>
        /// <value>
        /// The sender.
        /// </value>
        public int Sender
        {
            get { return sender; }
            set { sender = value; }
        }
        private int receiver;

        /// <summary>
        /// Gets or sets the receiver ID.
        /// </summary>
        /// <value>
        /// The receiver.
        /// </value>
        public int Receiver
        {
            get { return receiver; }
            set { receiver = value; }
        }
        private string tag;

        /// <summary>
        /// Gets or sets the tag of this message.
        /// If tag is set, the message will be send to the TAG group, and not to the
        /// reciever
        /// </summary>
        /// <value>
        /// The tag.
        /// </value>
        public string Tag
        {
            get { return tag; }
            set { tag = value; }
        }
        private Priority prioridade;

        /// <summary>
        /// Gets or sets the Priority.
        /// </summary>
        /// <value>
        /// The prioridade.
        /// </value>
        public Priority Prioridade
        {
            get { return prioridade; }
            set { prioridade = value; }
        }
        private double timetodeliver;

        /// <summary>
        /// Gets or sets the timetodeliver.
        /// 0 for now
        /// </summary>
        /// <value>
        /// The timetodeliver.
        /// </value>
        public double Timetodeliver
        {
            get { return timetodeliver; }
            set { timetodeliver = value; }
        }
        private SenderType type;

        /// <summary>
        /// Gets or sets the type of the sender.
        /// </summary>
        /// <value>
        /// The type of the sender.
        /// </value>
        public SenderType SenderType
        {
            get { return type; }
            set { type = value; }
        }
        private Object content; //consertar , mas n sei como mas n poderia ser object aqui , em c++ eu usaria um void pointer ....

        /// <summary>
        /// Gets or sets the content of the message.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public Object Content
        {
            get { return content; }
            set { content = value; }
        }
        private string cod;

        /// <summary>
        /// Gets or sets the message code.
        /// </summary>
        /// <value>
        /// The cod.
        /// </value>
        public string Cod
        {
            get { return cod; }
            set { cod = value; }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="sender">Id of the sender, use -1 to be an undentified sender</param>
        /// <param name="receiver">Reciever ID, use -1 to send to a Tag group</param>
        /// <param name="tag">Tag (used to send to a group of entities), if the message will be send to just one reciver, use null here</param>
        /// <param name="pri">Priority</param>
        /// <param name="timetodeliver">Time to send the message in MILISECONDS( Use 0 to send in THIS frame and -1 to send NOW)</param>
        /// <param name="type">Message Type; The engine DONT use this field for NOTHING, its for user control</param>
        /// <param name="Content">Message Content</param>
        /// <param name="cod">Message Code, The engine DONT use this field, its for user control</param>
        public Message(int sender, int receiver, string tag , Priority pri , int timetodeliver , SenderType type ,Object Content, string cod)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.tag = tag;
            this.prioridade = pri;
            this.timetodeliver = timetodeliver;
            this.type = type;
            this.content = Content;
            this.cod = cod;
        }

    }

    /// <summary>
    /// Message Priority
    /// </summary>
    public enum Priority
    {
        /// <summary>
        /// urgent
        /// </summary>
        URGENT = 10 ,
        /// <summary>
        /// Hight
        /// </summary>
        HIGH  = 5,
        /// <summary>
        /// Medium
        /// </summary>
        MEDIUM = 3 ,
        /// <summary>
        /// Low
        /// </summary>
        LOW = 2 ,
        /// <summary>
        /// Very low
        /// </summary>
        VERYLOW = 0

    }

    /// <summary>
    /// Avaliable Sender Types
    /// </summary>
    public enum SenderType
    {
        SYSTEM = 0 , NORMAL , IA ,HUD,SOUND,EVENT,MAIL,OBJECT
    }

    /// <summary>
    /// Types of checking
    /// </summary>
    public enum Checks
    {
        CHECK_DELIVERY,DONT_CHECK_DELIVERY
    }

     

}
