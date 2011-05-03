#region Using Statements
using System;
using Microsoft.Xna.Framework;
using System.Collections;
using System.Collections.Generic;
using PloobsEngine.Audio;
using PloobsEngine.Entity;
using PloobsEngine.Engine;
using PloobsEngine.SceneControl.GUI;
#endregion

namespace PloobsEngine.SceneControl
{
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
    /// IScreen, base class fot all screens
    /// </summary>
    public abstract class IScreen
    {
        public IScreen(IGui gui)
        {
            this.gui = gui;
        }

        private IGui gui = null;

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
            updateables.Add(updateable);
        }
        

        /// <summary>
        /// Removes the IScreenUpdateable.
        /// </summary>
        /// <param name="updateable">The updateable.</param>
        public void RemoveScreenUpdateable(IScreenUpdateable updateable)
        {
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
            set { screenState = value; }
        }

       private ScreenState screenState = ScreenState.Active;
       

        /// <summary>
        /// Gets the manager that this screen belongs to.
        /// </summary>
        protected ScreenManager ScreenManager
        {
            get { return screenManager; }            
        }

        internal ScreenManager screenManager;                

        #endregion

        private GraphicInfo graphicInfo;

        /// <summary>
        /// Gets the graphic info.
        /// </summary>
        public GraphicInfo GraphicInfo
        {
            get { return graphicInfo; }
            internal set { graphicInfo = value; }
        }
        private GraphicFactory graphicFactory;

        /// <summary>
        /// Gets the graphic factory.
        /// </summary>
        public GraphicFactory GraphicFactory
        {
            get { return graphicFactory; }
            internal set { graphicFactory = value; }
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
