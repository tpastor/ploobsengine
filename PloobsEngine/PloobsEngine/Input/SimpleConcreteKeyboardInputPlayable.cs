using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace PloobsEngine.Input
{
    public class SimpleConcreteKeyboardInputPlayable : InputPlayableKeyBoard
    {
        private StateKey state;
        private Keys[] key;
        private KeyStateChange callback;
        private EntityType type;
        private InputMask mask;

        public SimpleConcreteKeyboardInputPlayable(StateKey state , Keys key ,KeyStateChange callback , EntityType type )
        {
            this.state = state;
            this.key = new Keys[1];
            this.key[0] = key;
            this.callback = callback;
            this.type = type;
            this.mask = InputMask.GSYSTEM;

        }

        public SimpleConcreteKeyboardInputPlayable(StateKey state, Keys key, KeyStateChange callback, EntityType type, InputMask mask)
        {
            this.state = state;
            this.key = new Keys[1];
            this.key[0] = key;
            this.callback = callback;
            this.type = type;
            this.mask = mask;

        }

        /// <summary>
        /// For Combo
        /// </summary>
        /// <param name="state"></param>
        /// <param name="key"></param>
        /// <param name="callback"></param>
        /// <param name="type"></param>
        /// <param name="mask"></param>
        public SimpleConcreteKeyboardInputPlayable(StateKey state, Keys[] key, KeyStateChange callback, EntityType type, InputMask mask)
        {
            this.state = state;
            this.key = key;
            this.callback = callback;
            this.type = type;
            this.mask = mask;

        }

        /// <summary>
        /// For Combo
        /// </summary>
        /// <param name="state"></param>
        /// <param name="key"></param>
        /// <param name="callback"></param>
        /// <param name="type"></param>
        /// <param name="mask"></param>
        public SimpleConcreteKeyboardInputPlayable(StateKey state, Keys[] key, KeyStateChange callback, EntityType type)
        {
            this.state = state;
            this.key = key;            
            this.callback = callback;
            this.type = type;
            this.mask = InputMask.GSYSTEM;

        }

        #region InputPlayable Members

        public StateKey StateKey
        {
            get
            {
                return state;
            }
            
        }

        public Microsoft.Xna.Framework.Input.Keys[] Keys
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

        #region InputPlayableKeyBoard Members


        public InputMask InputMask
        {
            get { return mask; }
        }

        #endregion
    }
}
