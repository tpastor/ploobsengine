using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.Commands
{
    /// <summary>
    /// Reponsible for sending Commands
    /// </summary>
    public class CommandProcessor
    {

        #region Singleton

        private static CommandProcessor cp = null;

        private CommandProcessor()
        {
        }

        /// <summary>
        /// Gets the command processor.
        /// </summary>
        /// <returns></returns>
        public static CommandProcessor getCommandProcessor()
        {
            if(cp == null)
            {
                cp = new CommandProcessor();
            }
            return cp;
        }
#endregion

        private static IDictionary<String, IReciever> _idToReciever = new Dictionary<String,IReciever>();
        private static IDictionary<String, IList<ICommand>> _assyncronousCommands = new Dictionary<String, IList<ICommand>>();
        private static List<ICommand> _pendingCommandsToAdd = new List<ICommand>();

        /// <summary>
        /// Send a command NOW
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="recieverId">The reciever id.</param>
        public void SendCommandSyncronous(ICommand command, String recieverId)
        {
            SendCommand(command, recieverId);
        }

        /// <summary>
        /// Sends a command Now
        /// </summary>
        /// <param name="command">The command.</param>
        public void SendCommandSyncronous(ICommand command)
        {
            SendCommand(command, command.TargetName);
        }

        /// <summary>
        /// Schedules a Command to be send when possible
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="recieverId">The reciever id.</param>
        public void SendCommandAssyncronous(ICommand command, String recieverId)
        {
            if (!processing)
            {
                IList<ICommand> list;

                if (!_assyncronousCommands.ContainsKey(recieverId))
                {
                    list = new List<ICommand>();
                }
                else
                {
                    list = _assyncronousCommands[recieverId];
                }

                list.Add(command);
                _assyncronousCommands[recieverId] = list;
            }
            else
            {
                _pendingCommandsToAdd.Add(command);
            }

        }

        /// <summary>
        /// Schedules a Command to be send when possible
        /// </summary>
        /// <param name="command">The command.</param>
        public void SendCommandAssyncronous(ICommand command)
        {
            this.SendCommandAssyncronous(command, command.TargetName);
        }

        private static bool processing;

        /// <summary>
        /// Process the Assyncronous Commands        
        /// </summary>
        internal void ProcessCommands()
        {
            processing = true;
            foreach (string recieverId in _assyncronousCommands.Keys)
            {
                IList<ICommand> list = _assyncronousCommands[recieverId];
                foreach (ICommand command in list)
                {
                    SendCommand(command, recieverId);
                }

            }
            _assyncronousCommands.Clear();
            processing = false;

            foreach (ICommand item in _pendingCommandsToAdd)
	        {
                this.SendCommandAssyncronous(item);
	        }
            _pendingCommandsToAdd.Clear();
        }

        /// <summary>
        /// Registers the specified reciever.
        /// </summary>
        /// <param name="rec">The rec.</param>
        internal void Register(IReciever rec)
        {
            _idToReciever.Add(rec.getMyName(), rec);
        }

        /// <summary>
        /// Unregister a specified reciever
        /// </summary>
        /// <param name="rec">The rec.</param>
        internal void UnRegister(IReciever rec)
        {
            _idToReciever.Remove(rec.getMyName());
        }

        private void SendCommand(ICommand command, String recieverId)
        {
            if (!_idToReciever.ContainsKey(recieverId))
            {
                throw new Exception("Reciever Id nao Encontrado : " + recieverId);
             
            }
            Object obj = _idToReciever[recieverId];
            command.isetTarget(obj);
            command.iexecute();            
        }



    }
}
