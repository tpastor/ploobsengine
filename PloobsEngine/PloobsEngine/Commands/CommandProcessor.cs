#region License
/*
    PloobsEngine Game Engine Version 0.3 Beta
    Copyright (C) 2011  Ploobs

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Engine.Logger;

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
            //this is removed in Release version, critical code here
            System.Diagnostics.Debug.Assert(command != null,"command cannot be null");
            System.Diagnostics.Debug.Assert(!String.IsNullOrEmpty(recieverId), "reciver id cannot be null");
            SendCommand(command, recieverId);
        }

        /// <summary>
        /// Sends a command Now
        /// </summary>
        /// <param name="command">The command.</param>
        public void SendCommandSyncronous(ICommand command)
        {
            System.Diagnostics.Debug.Assert(command != null, "command cannot be null");
            System.Diagnostics.Debug.Assert(!String.IsNullOrEmpty(command.TargetName), "TargetName of the command cannot be null");
            SendCommand(command, command.TargetName);
        }

        /// <summary>
        /// Schedules a Command to be send when possible
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="recieverId">The reciever id.</param>
        public void SendCommandAssyncronous(ICommand command, String recieverId)
        {
            System.Diagnostics.Debug.Assert(command != null, "command cannot be null");
            System.Diagnostics.Debug.Assert(!String.IsNullOrEmpty(recieverId), "Reciever id cannot be null");

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
            System.Diagnostics.Debug.Assert(command != null, "command cannot be null");
            System.Diagnostics.Debug.Assert(!String.IsNullOrEmpty(command.TargetName), "TargetName of the command cannot be null");
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
                ActiveLogger.LogMessage("Reciver not found. Are you sure you add the " + command.TargetName + " component, If in releas, the program will continue and will discard the Command", LogLevel.FatalError);
                System.Diagnostics.Debug.Assert(false, "Reciver not found. Are you sure you add the " + command.TargetName + " component");
                return;             
            }
            Object obj = _idToReciever[recieverId];
            command.isetTarget(obj);
            command.iexecute();            
        }

    }
}
