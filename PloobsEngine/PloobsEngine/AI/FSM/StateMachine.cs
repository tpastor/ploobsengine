using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;

namespace PloobsEngine.IA
{
    public class StateMachine
    {
        public StateMachine(IObject obj)
        {
            this.obj = obj;
        }

        private IObject obj;
        private IDictionary<String,IState> _states = new Dictionary<String,IState>();
        private String currentState = null;
        private bool newInState = true;

        public void AddState(String name, IState state)
        {
            _states.Add(name, state);
        }

        public void SetCurrentState(String name)
        {
            this.currentState = name;
        }

        public void UpdateFSM()
        {

            if (newInState)
            {
                _states[currentState].Init(obj);
                newInState = false;
            }
            _states[currentState].UpdateState();
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
