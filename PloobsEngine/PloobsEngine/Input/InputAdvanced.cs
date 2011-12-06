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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PloobsEngine.Components;
#if WINDOWS_PHONE
using Microsoft.Xna.Framework.Input.Touch;
#endif

namespace PloobsEngine.Input
{

    public enum EntityType
    {
        IOBJECT, CAMERA, COMPONENT, TOOLS
    }

#if WINDOWS
    /// <summary>
    /// Mouse Buttoms
    /// </summary>
    public enum MouseButtons
    {
        LeftButton = 0,
        MiddleButton = 1,
        RightButton = 2,
        XButton1 = 3,
        XButton2 = 4,    
        None = 5
    }
#endif


    /// <summary>
    /// Component responsible for processing Keyboard and Mouse 
    /// </summary>
    public class InputAdvanced : IComponent
    {

        public static readonly String MyName = "InputAdvanced";
        
#if XBOX360
        private Dictionary<Buttons, float>[] buttonCache;
        private GamePadState[] currentPadState;
        private GamePadState[] previousPadState;
#elif WINDOWS
        private IDictionary<Keys, InputPlayableKeyBoard> _Mapper = new Dictionary<Keys, InputPlayableKeyBoard>();
        private Dictionary<Keys, float> keyCache;
        private KeyboardState currentKeyState;
        private KeyboardState previousKeyState;
        private int _id;

        private Dictionary<MouseButtons, float> mouseCache;
        private MouseState currentMouseState;
        private MouseState previousMouseState;
#elif WINDOWS_PHONE
        private Dictionary<GestureType, List<InputPlaybleGesture>> _gestureMapper = new Dictionary<GestureType, List<InputPlaybleGesture>>();
        bool addedgesture = false;
        internal void BindGesture(InputPlaybleGesture ip, BindAction ba)
        {
            if (ba == BindAction.ADD)
            {
                addedgesture = true;
                if(_gestureMapper.ContainsKey(ip.GestureType))
                {
                    _gestureMapper[ip.GestureType].Add(ip);
                }
                else
                {
                    
                    TouchPanel.EnabledGestures = TouchPanel.EnabledGestures | ip.GestureType;
                    List<InputPlaybleGesture> gest = new List<InputPlaybleGesture>();
                    gest.Add(ip);
                    _gestureMapper.Add(ip.GestureType, gest);
                }
                   
            }
            else if (ba == BindAction.REMOVE)
            {
                List<InputPlaybleGesture> gest = _gestureMapper[ip.GestureType];
                gest.Remove(ip);
            }
        }

        internal void ProcessGesture()
        {
            if (addedgesture)
            {
                while (TouchPanel.IsGestureAvailable)
                {
                    GestureSample sample = TouchPanel.ReadGesture();
                    if (_gestureMapper.ContainsKey(sample.GestureType))
                    {
                        foreach (var item in _gestureMapper[sample.GestureType])
                        {
                            if (processMask(item.InputMask))
                            {
                                item.FireEvent(sample);
                            }
                        }
                    }
                }

            }
        }
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="InputAdvanced"/> class.
        /// </summary>
        /// <param name="es">The es.</param>
        public InputAdvanced()
            
        {                    

#if XBOX360
            buttonCache = new Dictionary<Buttons, float>[4]
            {
                new Dictionary<Buttons, float>(), new Dictionary<Buttons, float>(),
                new Dictionary<Buttons, float>(), new Dictionary<Buttons, float>()
            };
            currentPadState = previousPadState = new GamePadState[4];
            for (int i = 0; i < 4; i++)
            {
                previousPadState[i] = currentPadState[i] = 
                    GamePad.GetState(((PlayerIndex)Enum.GetValues(PlayerIndex)[i]));
            }

            foreach (Buttons button in Enum.GetValues(Buttons))
                buttonCache.Add(button, 0.0f);
#elif WINDOWS
            keyCache = new Dictionary<Keys, float>();
            previousKeyState = currentKeyState = Keyboard.GetState();
            mouseCache = new Dictionary<MouseButtons, float>();
            previousMouseState = currentMouseState = Mouse.GetState();
            mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);

            foreach (Keys key in Enum.GetValues(typeof(Keys)))
                keyCache.Add(key, 0.0f);
            foreach (MouseButtons mb in Enum.GetValues(typeof(MouseButtons)))
                mouseCache.Add(mb, 0.0f);
#elif WINDOWS_PHONE
#endif
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        protected override void Update(GameTime gameTime)
        {
            float elapsedTime =
                (float)(gameTime.ElapsedGameTime.TotalSeconds);
#if XBOX360
            previousPadState = currentPadState;
            for (int i = 0; i < currentPadState.Length; i++)
            {
                currentPadState[i] = 
                    GamePad.GetState(((PlayerIndex)Enum.GetValues(PlayerIndex)[i]));
            }

            foreach (Buttons b in Enum.GetValues(typeof(Buttons)))
            {
                if (IsButtonDown(b))
                    buttonCache[b] += elapsedTime;
                else
                    buttonCache[b] = 0.0f;
            }
#elif WINDOWS
            // We set the Previous states to the Current states
            // since the Current states will be updated with
            // the ACTUAL Current states.
            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            // We also take in the latest mouse position.
            mousePosition.X = currentMouseState.X;
            mousePosition.Y = currentMouseState.Y;

            // When iterating through the enumeration of type 'Keys',
            // we find if the key is down; if it is then we add
            // the elapsed game time to it, otherwise we set to zero.
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                if (IsKeyDown(key))
                    keyCache[key] += elapsedTime;
                else
                    keyCache[key] = 0.0f;
            }

            // The same thing is done with the 'MouseButtons' enum.
            foreach (MouseButtons mb in Enum.GetValues(typeof(MouseButtons)))
            {
                if (IsMouseButtonDown(mb))
                    mouseCache[mb] += elapsedTime;
                else
                    mouseCache[mb] = 0.0f;
            }
#elif WINDOWS_PHONE
#endif

#if WINDOWS
            ProcessKeys();
            ProcessMouse();
#elif WINDOWS_PHONE
            ProcessGesture();
#endif
            
        }

#if XBOX360
        public float TimePressed(Buttons button, PlayerIndex player)
        {
            return buttonCache[(int)player][button];
        }

        public bool IsButtonPress(Buttons button, PlayerIndex player)
        {
            return (currentPadState[(int)player].IsButtonDown(button) &&
                    previousPadState[(int)player].IsButtonUp(button));
        }

        public bool IsButtonRelease(Buttons button, PlayerIndex player)
        {
            return (currentPadState[(int)player].IsButtonUp(button) &&
                    previousPadState[(int)player].IsButtonDown(button));
        }

        public bool IsButtonDown(Buttons button, PlayerIndex player)
        {
            return currentPadState[(int)player].IsButtonDown(button);
        }

        public bool IsButtonUp(Buttons button, PlayerIndex player)
        {
            return currentPadState[(int)player].IsButtonUp(button);
        }

        public Vector2 LeftThumbstick(PlayerIndex player)
        {
            return currentPadState[(int)player].Thumbsticks.Left;
        }

        public Vector2 RightThumbstick(PlayerIndex player)
        {
            return currentPadState[(int)player].Thumbsticks.Right;
        }
#elif WINDOWS
        #region Mouse Input
        // Mouse Buttons Elapsed Time
        
        private float TimePressed(MouseButtons mb)
        {
            return mouseCache[mb];
        }

        // Latest Mouse Position as Vector2.
        private Vector2 mousePosition;
        private Vector2 MousePosition
        {
            get { return mousePosition; }
        }

        // Normalized Delta ScrollWheelValue.
        private float ScrollWheelDelta
        {
            get
            {
                return MathHelper.Clamp((currentMouseState.ScrollWheelValue -
                                         previousMouseState.ScrollWheelValue),
                                        -1,
                                        1);
            }
        }

        // Newly Pressed Button
        private bool IsMouseButtonPress(MouseButtons mb)
        {
            return (IsMouseButtonDown(mb) &&
                IsMouseButtonState(mb, ButtonState.Released,
                    ref previousMouseState));
        }

        // Newly Released Button
        private bool IsMouseButtonRelease(MouseButtons mb)
        {
            return (IsMouseButtonUp(mb) &&
                IsMouseButtonState(mb, ButtonState.Pressed,
                    ref previousMouseState));
        }

        // Continuously Pressed Button.
        private bool IsMouseButtonDown(MouseButtons mb)
        {
            return IsMouseButtonState(mb, ButtonState.Pressed,
                ref currentMouseState);
        }

        // Continuously Released Button.
        private bool IsMouseButtonUp(MouseButtons mb)
        {
            return IsMouseButtonState(mb, ButtonState.Released,
                ref currentMouseState);
        }

        // Base method
        private bool IsMouseButtonState(MouseButtons mb,
            ButtonState state, ref MouseState mouseState)
        {
            switch (mb)
            {
                case MouseButtons.LeftButton:
                    return (mouseState.LeftButton == state);
                case MouseButtons.MiddleButton:
                    return (mouseState.MiddleButton == state);
                case MouseButtons.RightButton:
                    return (mouseState.RightButton == state);
                case MouseButtons.XButton1:
                    return (mouseState.XButton1 == state);
                case MouseButtons.XButton2:
                    return (mouseState.XButton2 == state);
            }
            return false;
        }
        #endregion

        #region Keyboard Input
        // Keyboard Keys Elapsed Time
        private float TimePressed(Keys key)
        {
            return keyCache[key];
        }

        // Newly Pressed Key
        private bool IsKeyPress(Keys key)
        {
            return (currentKeyState.IsKeyDown(key) &&
                    previousKeyState.IsKeyUp(key));
        }

        // Newly Released Key
        private bool IsKeyRelease(Keys key)
        {
            return (currentKeyState.IsKeyUp(key) &&
                    previousKeyState.IsKeyDown(key));
        }

        // Continuously Pressed Key
        private bool IsKeyDown(Keys key)
        {
            return currentKeyState.IsKeyDown(key);
        }

        // Continuously Released Key
        private bool IsKeyUp(Keys key)
        {
            return currentKeyState.IsKeyUp(key);
        }
        #endregion
#elif WINDOWS_PHONE
#endif

        #region IReciever Members

        /// <summary>
        /// The name of the reciever
        /// MUST BE UNIQUE
        /// </summary>
        /// <returns></returns>
        public override string getMyName()
        {
            return MyName;
        }

        #endregion
               

        #region ProcessInputs
        private InputMask mask  = InputMask.GALL;

        internal void TurnOffInputMask(InputMask mask)
        {
            this.mask = this.mask & ~mask;
            this.mask = this.mask | InputMask.GSYSTEM;
        }


        internal void TurnOnInputMask(InputMask mask)
        {
            this.mask = this.mask | mask;
        }

        #if WINDOWS
        internal void Clear()
        {

         _keysMapperDown = new List<InputPlayableKeyBoard>();
        _keysMapperPress = new List<InputPlayableKeyBoard>();
        _keysMapperUp = new List<InputPlayableKeyBoard>();
        _keysMapperRelease = new List<InputPlayableKeyBoard>();

        _mouseMapperDown = new List<InputPlaybleMouseBottom>();
        _mouseMapperPress = new List<InputPlaybleMouseBottom>();
        _mouseMapperUp = new List<InputPlaybleMouseBottom>();
        _mouseMapperRelease = new List<InputPlaybleMouseBottom>();
        _mouseMapperPosition = new List<InputPlaybleMousePosition>();

        }


        internal void BindMousePosition(InputPlaybleMousePosition ip, BindAction ba)
        {
            if (ba == BindAction.ADD)
            {
              _mouseMapperPosition.Add(ip);             
            }
            else if (ba == BindAction.REMOVE)
            {
                _mouseMapperPosition.Remove(ip);
            }
        }

        internal void BindMouseBottom(InputPlaybleMouseBottom ip, BindAction ba)
        {
            if (ba == BindAction.ADD)
            {
                if (ip.StateKey == StateKey.DOWN)
                {
                    _mouseMapperDown.Add(ip);
                }
                else if (ip.StateKey == StateKey.PRESS)
                {
                    _mouseMapperPress.Add(ip);

                }
                else if (ip.StateKey == StateKey.RELEASE)
                {
                    _mouseMapperRelease.Add(ip);
                }
                else if (ip.StateKey == StateKey.UP)
                {
                    _mouseMapperUp.Add(ip);
                }
            }
            else if (ba == BindAction.REMOVE)
            {
                if (ip.StateKey == StateKey.DOWN)
                {
                    _mouseMapperDown.Remove(ip);
                }
                else if (ip.StateKey == StateKey.PRESS)
                {
                    _mouseMapperPress.Remove(ip);

                }
                else if (ip.StateKey == StateKey.RELEASE)
                {
                    _mouseMapperRelease.Remove(ip);
                }
                else if (ip.StateKey == StateKey.UP)
                {
                    _mouseMapperUp.Remove(ip);
                }
            }
            
            

        }

        internal void BindKey(InputPlayableKeyBoard ip, BindAction ba)
        {
            if (ba == BindAction.ADD)
            {
                if (ip.StateKey == StateKey.DOWN)
                {
                    _keysMapperDown.Add(ip);
                }
                else if (ip.StateKey == StateKey.PRESS)
                {
                    _keysMapperPress.Add(ip);

                }
                else if (ip.StateKey == StateKey.RELEASE)
                {
                    _keysMapperRelease.Add(ip);
                }
                else if (ip.StateKey == StateKey.UP)
                {
                    _keysMapperUp.Add(ip);
                }
            }
            else if (ba == BindAction.REMOVE)
            {
                if (ip.StateKey == StateKey.DOWN)
                {
                    _keysMapperDown.Remove(ip);
                }
                else if (ip.StateKey == StateKey.PRESS)
                {
                    _keysMapperPress.Remove(ip);

                }
                else if (ip.StateKey == StateKey.RELEASE)
                {
                    _keysMapperRelease.Remove(ip);
                }
                else if (ip.StateKey == StateKey.UP)
                {
                    _keysMapperUp.Remove(ip);
                }
            }            
            
        }
#endif
        private bool processMask(InputMask m)
        {
            if ((m & mask) == m )
            {
                return true;
            }
            return false;
        }

#if WINDOWS
        private void ProcessMouse()
        {            
            foreach (InputPlaybleMouseBottom ip in _mouseMapperDown)
            {
                if (this.IsMouseButtonDown(ip.MouseButtons))
                { 
                    if(processMask(ip.InputMask))
                    ip.FireEvent(currentMouseState);
                    
                }
            }
            foreach (InputPlaybleMouseBottom ip in _mouseMapperPress)
            {
                if (this.IsMouseButtonPress(ip.MouseButtons))
                {
                    if (processMask(ip.InputMask))
                        ip.FireEvent(currentMouseState);
                    
                }
            }
            foreach (InputPlaybleMouseBottom ip in _mouseMapperRelease)
            {
                if (this.IsMouseButtonRelease(ip.MouseButtons))
                {
                    if (processMask(ip.InputMask))
                        ip.FireEvent(currentMouseState);
                    
                }
            }
            foreach (InputPlaybleMouseBottom ip in _mouseMapperUp)
            {
                if (this.IsMouseButtonUp(ip.MouseButtons))
                {
                    if (processMask(ip.InputMask))
                        ip.FireEvent(currentMouseState);
                    
                }
            }
            foreach (InputPlaybleMousePosition ip in _mouseMapperPosition)
            {
                if (processMask(ip.InputMask))
                ip.FireEvent(currentMouseState);             
            }

        }

        #elif WINDOWS_PHONE
#endif

        /// <summary>
        /// Gets the type of the component type.
        /// </summary>
        /// <value>
        /// The type of the component.
        /// </value>
        public override ComponentType ComponentType
        {
            get { return ComponentType.UPDATEABLE; }
        }

#if WINDOWS
        private void ProcessKeys()
        {
            foreach (InputPlayableKeyBoard ip in _keysMapperDown)
            {
                if (!processMask(ip.InputMask))
                      continue;

                bool allOk = true;
                foreach (var item in ip.Keys)
                {
                    if (!this.IsKeyDown(item))
                    {
                        allOk = false;
                        break;
                    }                    
                }

                if (allOk)
                    {                        
                        ip.FireEvent();
                    }
            }
            foreach (InputPlayableKeyBoard ip in _keysMapperPress)
            {
                if (!processMask(ip.InputMask))
                    continue;

                bool allOk = true;
                foreach (var item in ip.Keys)
                {
                    if (!this.IsKeyPress(item))
                    {
                        allOk = false;
                        break;
                    }
                }

                if (allOk)
                {
                    ip.FireEvent();
                }
            }
            foreach (InputPlayableKeyBoard ip in _keysMapperRelease)
            {
                if (!processMask(ip.InputMask))
                    continue;

                bool allOk = true;
                foreach (var item in ip.Keys)
                {
                    if (!this.IsKeyRelease(item))
                    {
                        allOk = false;
                        break;
                    }
                }

                if (allOk)
                {
                    ip.FireEvent();
                }                
            }
            foreach (InputPlayableKeyBoard ip in _keysMapperUp)
            {
                if (!processMask(ip.InputMask))
                    continue;

                bool allOk = true;
                foreach (var item in ip.Keys)
                {
                    if (!this.IsKeyUp(item))
                    {
                        allOk = false;
                        break;
                    }
                }

                if (allOk)
                {
                    ip.FireEvent();
                }                
               
            }  
        }
        
        private IList<InputPlayableKeyBoard> _keysMapperDown = new List<InputPlayableKeyBoard>();
        private IList<InputPlayableKeyBoard> _keysMapperPress = new List<InputPlayableKeyBoard>();
        private IList<InputPlayableKeyBoard> _keysMapperUp = new List<InputPlayableKeyBoard>();
        private IList<InputPlayableKeyBoard> _keysMapperRelease = new List<InputPlayableKeyBoard>();

        private IList<InputPlaybleMouseBottom> _mouseMapperDown = new List<InputPlaybleMouseBottom>();
        private IList<InputPlaybleMouseBottom> _mouseMapperPress = new List<InputPlaybleMouseBottom>();
        private IList<InputPlaybleMouseBottom> _mouseMapperUp = new List<InputPlaybleMouseBottom>();
        private IList<InputPlaybleMouseBottom> _mouseMapperRelease = new List<InputPlaybleMouseBottom>();
        private IList<InputPlaybleMousePosition> _mouseMapperPosition = new List<InputPlaybleMousePosition>();
        #elif WINDOWS_PHONE
#endif
        #endregion

    }

    /// <summary>
    /// Possibles Binding Options
    /// </summary>
    public enum BindAction
    {
        /// <summary>
        /// add a bind
        /// </summary>
        ADD,
        /// <summary>
        /// Remove a bind
        /// </summary>
        REMOVE
    }

    /// <summary>
    /// Input Chanels that can be used
    /// When you register a Command, it will be bind to a mask, you can turn the mask of and all the binds of this mask are 
    /// turned off, you can turn a mask on and ....
    /// Usefull When you have Menus and Screens, and you need to change between then, just set the corresponding inputs
    /// to diferent masks and turn one on and the other off when necessary
    /// </summary>
    public enum InputMask : ulong
    {
        G1 = 0x000000000001,
        G2 = 0x000000000010,
        G3 = 0x000000000100,
        G4 = 0x000000001000,
        G5 = 0x000000010000,
        GPICKING = 0x000000100000,
        GINTRO = 0x000001000000,
        GMENU = 0x000010000000,
        /// <summary>
        /// All Binds ON
        /// </summary>
        GALL = 0x111111111111,
        /// <summary>
        /// NONE BINDS ON
        /// </summary>
        GNONE = 0x0000000000000,
        GCamera = 0x1000000000000,
        /// <summary>
        /// ALWAYS ON, no matter what the mask forced
        /// </summary>
        GSYSTEM = 0x0100000000000
    }
}
