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
using Microsoft.Xna.Framework;

namespace PloobsEngine.IA
{
    public class StateMachine
    {
        public StateMachine()
        {        
        }
        
        private IDictionary<String,IState> _states = new Dictionary<String,IState>();
        private String currentState = null;
        private bool newInState = true;

        public void AddState(IState state)
        {
            if (currentState == null)
                currentState = state.Name;
            _states.Add(state.Name, state);
        }

        public void SetCurrentState(String name)
        {
            this.currentState = name;
        }
        public IState GetCurrentState()
        {
            return _states[currentState];
        }

        public void UpdateFSM(GameTime gameTime)
        {
            if (newInState)
            {
                _states[currentState].Init();
                newInState = false;
            }
            _states[currentState].UpdateState(gameTime);
            string next = _states[currentState].NextState();
            if (next != null)
            {
                _states[currentState].Finish();
                currentState = next;
                newInState = true;
            }
        }
    }
}
