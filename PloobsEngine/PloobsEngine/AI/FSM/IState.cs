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
using PloobsEngine.SceneControl;
using PloobsEngine.Events;
using Microsoft.Xna.Framework;

namespace PloobsEngine.IA
{
    /// <summary>
    /// Interface to represent an abstract State in the ploobs finite state machine
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// called on this state is entered
        /// </summary>
        void Init();
        /// <summary>
        /// called to find a state change is needed.
        /// Return null if no state change is needed
        /// </summary>
        /// <returns></returns>
        String NextState();
        /// <summary>
        /// Called all the frames
        /// </summary>
        /// <param name="gameTime"></param>
        void UpdateState(GameTime gameTime);
        /// <summary>
        /// Called when leaving this state
        /// </summary>
        void Finish();
        /// <summary>
        /// Name of the state (unique)
        /// </summary>
        String Name
        {
            get;
        }
    }
}
