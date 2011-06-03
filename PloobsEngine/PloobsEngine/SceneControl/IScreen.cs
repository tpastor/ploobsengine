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
        public IScreen(IGui gui = null)
        {
            this.gui = gui;
            IsLoaded = false;
        }
               

        private Dictionary<IInput, BindKeyCommand> KeyBinds = new Dictionary<IInput, BindKeyCommand>();
        private Dictionary<IInput, BindMouseCommand> MouseBinds = new Dictionary<IInput, BindMouseCommand>();
        public OnScreenChangeState OnScreenChangeState = null;

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
        /// <summary>
        /// Cleans up resources that dont are exclusive of the screen        
        /// </summary>
        protected virtual void CleanUp(EngineStuff engine)
        {            
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

            IScreenUpdateable[] updts = updateables.ToArray();

            for (int i = 0; i < updts.Length; i++)
            {
                updts[i].iCleanUp();
            }

            if (gui != null)
                gui.iDispose();
        }        

    }
}
