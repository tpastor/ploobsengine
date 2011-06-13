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
