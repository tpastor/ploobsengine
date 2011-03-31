using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Engine.Input
{
    public class SimpleConcreteInputPlayable : InputPlayable
    {
        private StateKey state;
        private Keys key;
        private KeyStateChange callback;
        private EntityType type;

        public SimpleConcreteInputPlayable(StateKey state , Keys key ,KeyStateChange callback , EntityType type )
        {
            this.state = state;
            this.key = key;
            this.callback = callback;
            this.type = type;

        }

        #region InputPlayable Members

        public StateKey StateKey
        {
            get
            {
                return state;
            }
            
        }

        public Microsoft.Xna.Framework.Input.Keys Keys
        {
            get { return key; }
        }

        public KeyStateChange KeyStateChange
        {
            get { return callback; }
        }

        #endregion

        #region InputPlayable Members


        public EntityType EntityType
        {
            get { return type; }
        }

        #endregion
    }
}
