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
#region Using Statements
using System;
using Microsoft.Xna.Framework;
using System.Collections;
using System.Collections.Generic;
using PloobsEngine.Audio;
using PloobsEngine.Entity;
using PloobsEngine.Engine;
using PloobsEngine.SceneControl.GUI;
using PloobsEngine.Input;
using PloobsEngine.Commands;
#endregion

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Called when Screen state change
    /// </summary>
    /// <param name="screen">The screen.</param>
    public delegate void OnScreenChangeState(IScreen screen);

    /// <summary>
    /// Enum describes the screen transition state.
    /// </summary>
    public enum ScreenState
    {
        /// <summary>
        /// Updates and Draw called (normal operation)
        /// </summary>
        Active,
        /// <summary>
        /// Updates is not called
        /// </summary>
        Paused,
        /// <summary>
        /// Draw is not called
        /// </summary>
        Hidden,
        /// <summary>
        /// draw and update not called
        /// </summary>
        Inactive
    }

    /// <summary>
    /// IScreen, base class for all screens
    /// </summary>
    public abstract class IScreen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IScreen"/> class.
        /// </summary>
        /// <param name="gui">The GUI Component, if null you cant use GUI in this screen.</param>
        #if !WINDOWS_PHONE
        public IScreen(IGui gui = null)
        {
            this.gui = gui;
            IsLoaded = false;
            CleanUpWhenRemoved = true;
            CleanupAbles = new List<ICleanupAble>();
        }
        #else
public IScreen()
        {
            this.gui = null;
            IsLoaded = false;
            CleanupAbles = new List<ICleanupAble>();
            CleanUpWhenRemoved = true;
        }
#endif

#if WINDOWS
        private Dictionary<IInput, BindKeyCommand> KeyBinds = new Dictionary<IInput, BindKeyCommand>();
        private Dictionary<IInput, BindMouseCommand> MouseBinds = new Dictionary<IInput, BindMouseCommand>();        
#elif !XBOX
        private Dictionary<IInput, BindGestureCommand> GestureBinds= new Dictionary<IInput, BindGestureCommand>();
#endif
        /// <summary>
        /// called on OnScreenStateChange 
        /// </summary>
        public OnScreenChangeState OnScreenChangeState = null;

        /// <summary>
        /// Attach a cleanupable to the screen
        /// </summary>
        /// <param name="ICleanupAble">The Icleanupable.</param>
        public void AttachCleanUpAble(ICleanupAble ICleanupAble)
        {
            System.Diagnostics.Debug.Assert(ICleanupAble != null);
            this.CleanupAbles.Add(ICleanupAble);
        }

        private IList<ICleanupAble> CleanupAbles
        {
            get;
            set;
        }

        public bool CleanUpWhenRemoved
        {
            get;
            set;
        }

#if WINDOWS
        /// <summary>
        /// Binds the KeyBoard input.
        /// </summary>
        /// <param name="ipk">The InputPlayableKeyBoard.</param>
        public void BindInput(InputPlayableKeyBoard ipk)
        {
            System.Diagnostics.Debug.Assert(ipk != null);
            BindKeyCommand bkc = new BindKeyCommand(ipk, BindAction.ADD);
            KeyBinds.Add(ipk,bkc);
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bkc);
        }



        /// <summary>
        /// Binds the MouseBottom input.
        /// </summary>
        /// <param name="ipk">The InputPlaybleMouseBottom.</param>
        public void BindInput(InputPlaybleMouseBottom ipk)
        {
            System.Diagnostics.Debug.Assert(ipk != null);
            BindMouseCommand bkc = new BindMouseCommand(ipk, BindAction.ADD);
            MouseBinds.Add(ipk, bkc);
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bkc);
        }

        /// <summary>
        /// Binds the MousePosition input.
        /// </summary>
        /// <param name="ipk">The InputPlaybleMousePosition.</param>
        public void BindInput(InputPlaybleMousePosition ipk)
        {
            System.Diagnostics.Debug.Assert(ipk != null);
            BindMouseCommand bkc = new BindMouseCommand(ipk, BindAction.ADD);
            MouseBinds.Add(ipk, bkc);
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bkc);
        }

        /// <summary>
        /// Removes the KeyBoard binding.
        /// </summary>
        /// <param name="ipk">The InputPlayableKeyBoard.</param>
        public void RemoveInputBinding(InputPlayableKeyBoard ipk)
        {
            System.Diagnostics.Debug.Assert(ipk != null);
            BindKeyCommand bc = KeyBinds[ipk];
            if (bc != null)
            {
                bc.BindAction = BindAction.REMOVE;
                CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bc);
            }
        }

        /// <summary>
        /// Removes the MouseBottom binding.
        /// </summary>
        /// <param name="ipk">The InputPlaybleMouseBottom.</param>
        public void RemoveInputBinding(InputPlaybleMouseBottom ipk)
        {
            System.Diagnostics.Debug.Assert(ipk != null);
            BindMouseCommand bc = MouseBinds[ipk];
            if (bc != null)
            {
                bc.BindAction = BindAction.REMOVE;
                CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bc);
            }
        }

        /// <summary>
        /// Removes the MousePosition bindings.
        /// </summary>
        /// <param name="ipk">The InputPlaybleMousePosition.</param>
        public void RemoveInputBinding(InputPlaybleMousePosition ipk)
        {
            System.Diagnostics.Debug.Assert(ipk != null);
            BindMouseCommand bc = MouseBinds[ipk];
            if (bc != null)
            {
                bc.BindAction = BindAction.REMOVE;
                CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bc);
            }
        }
#endif

        /// <summary>
        /// Gets a value indicating whether this instance is loaded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is loaded; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoaded
        {
            get;
            internal set;
        }

        private IGui gui = null;

        /// <summary>
        /// Gets the GUI interface.
        /// REMEMBER, you need to provide an implementation in the Screen Constructor, if you dont, you cant access this property
        /// </summary>
        protected IGui Gui
        {
            get {
                System.Diagnostics.Debug.Assert(gui != null, "To use the Gui You must specify a implementation in the the IScreen Constructor");
                return gui; 
            }            
        }
                
        private List<IScreenUpdateable> updateables = new List<IScreenUpdateable>();

        /// <summary>
        /// Attach one IScreenUpdateable to this screen
        /// </summary>
        /// <param name="updateable">The updateable.</param>
        public void AddScreenUpdateable(IScreenUpdateable updateable)
        {
            System.Diagnostics.Debug.Assert(updateable != null);
            updateables.Add(updateable);
        }


        /// <summary>
        /// Removes the IScreenUpdateable.
        /// </summary>
        /// <param name="updateable">The updateable.</param>
        /// <param name="callCleanUp">if set to <c>true</c> [call clean up].</param>
        public void RemoveScreenUpdateable(IScreenUpdateable updateable, bool callCleanUp = false)
        {
            System.Diagnostics.Debug.Assert(updateable != null);
            if(callCleanUp)
                updateable.iCleanUp();
            updateables.Remove(updateable);
        }

        
        #region Properties       
        

        /// <summary>
        /// Gets or sets the state of the screen.
        /// </summary>
        /// <value>
        /// The state of the screen.
        /// </value>
        public ScreenState ScreenState
        {
            get { return screenState; }
            set {
                if (OnScreenChangeState != null && value != screenState)
                {
                    screenState = value;
                    OnScreenChangeState(this);
                }
                else
                {
                    screenState = value;
                }
            }
        }

       private ScreenState screenState = ScreenState.Inactive;       

        /// <summary>
        /// Gets the manager that this screen belongs to.
        /// </summary>
        public ScreenManager ScreenManager
        {
            get { return screenManager; }            
        }

        internal ScreenManager screenManager;                

        #endregion

        internal GraphicInfo graphicInfo;

        /// <summary>
        /// Gets the graphic info.
        /// </summary>
        public GraphicInfo GraphicInfo
        {
            get { return graphicInfo; }           
        }

        internal GraphicFactory graphicFactory;

        /// <summary>
        /// Gets the graphic factory.
        /// </summary>
        public GraphicFactory GraphicFactory
        {
            get { return graphicFactory; }            
        }

        /// <summary>
        /// Load graphics content for the screen.
        /// </summary>
        protected virtual void LoadContent(GraphicInfo GraphicInfo,GraphicFactory factory , IContentManager contentManager) { }
        internal void iLoadContent(GraphicInfo GraphicInfo,GraphicFactory factory, IContentManager contentManager)
        {
            LoadContent(GraphicInfo,factory,contentManager);
        }

        /// <summary>
        /// Init Screen
        /// </summary>
        /// <param name="GraphicInfo">The graphic info.</param>
        /// <param name="engine">The engine.</param>
        protected virtual void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine) { }
        internal void iInitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            if (gui != null)
                gui.iInitialize(engine, GraphicFactory, GraphicInfo);

            InitScreen(GraphicInfo, engine);
        }


        internal void iAfterLoadContent(IContentManager manager, Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            AfterLoadContent(manager,ginfo,factory);
        }
        /// <summary>
        /// Called after all the screens LoadContent is called
        /// </summary>
        protected virtual void AfterLoadContent(IContentManager manager, Engine.GraphicInfo ginfo, Engine.GraphicFactory factory) { }

        
        internal void iUpdate(GameTime gameTime)
        {
            Update(gameTime);
        }
        /// <summary>
        /// Update the Screen
        /// </summary>
        protected virtual void Update(GameTime gameTime)
        {
            if (gui != null)
                gui.iUpdate(gameTime);

            IScreenUpdateable[] updts = updateables.ToArray();

            for (int i = 0; i < updts.Length; i++)
            {
                updts[i].iUpdate(gameTime);
            }            
            
        }

        internal void iDraw(GameTime gameTime, RenderHelper render)
        {
            if (gui != null)
                gui.iBeginDraw(gameTime, render, graphicInfo);

            Draw(gameTime,render);

            if (gui != null)
                gui.iEndDraw(gameTime,render,graphicInfo);
        }
        /// <summary>
        /// This is called when the screen should draw itself.
        /// </summary>
        protected abstract void Draw(GameTime gameTime, RenderHelper render);


        internal void RemoveThisScreen(EngineStuff engine)
        {
            CleanUp(engine);
        }

#if WINDOWS_PHONE
        public void BindInput(InputPlaybleGesture ipk)
        {
            System.Diagnostics.Debug.Assert(ipk != null);
            BindGestureCommand bkc = new BindGestureCommand(ipk, BindAction.ADD);
            GestureBinds.Add(ipk, bkc);
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bkc);
        }

        public void RemoveInputBinding(InputPlaybleGesture ipk)
        {
            System.Diagnostics.Debug.Assert(ipk != null);
            BindGestureCommand bc = GestureBinds[ipk];
            if (bc != null)
            {
                bc.BindAction = BindAction.REMOVE;
                CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bc);
            }
        }
#endif

        /// <summary>
        /// Cleans up resources that dont are exclusive of the screen        
        /// </summary>
        protected virtual void CleanUp(EngineStuff engine)
        {
            if (CleanUpWhenRemoved)
            {
#if WINDOWS
                foreach (var item in KeyBinds.Values)
                {
                    item.BindAction = BindAction.REMOVE;
                    CommandProcessor.getCommandProcessor().SendCommandAssyncronous(item);
                }
                foreach (var item in MouseBinds.Values)
                {
                    item.BindAction = BindAction.REMOVE;
                    CommandProcessor.getCommandProcessor().SendCommandAssyncronous(item);
                }
#elif WINDOWS_PHONE
            foreach (var item in GestureBinds.Values)
            {
                item.BindAction = BindAction.REMOVE;
                CommandProcessor.getCommandProcessor().SendCommandAssyncronous(item);
            }
            
#endif

                IScreenUpdateable[] updts = updateables.ToArray();

                for (int i = 0; i < updts.Length; i++)
                {
                    updts[i].iCleanUp();
                }

                if (gui != null)
                {
                    gui.iDispose();
                    gui = null;
                }

                foreach (var item in CleanupAbles)
                {
                    item.CleanUp(graphicFactory);
                }
                CleanupAbles.Clear();
#if DEBUG
            GC.Collect();
            GC.WaitForPendingFinalizers();
#endif
            }
        }        

    }
}
