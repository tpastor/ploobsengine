///based on the XNA DEMO Screen Managment
#region Using Statements
using System;
using Microsoft.Xna.Framework;
using System.Collections;
using System.Collections.Generic;
using PloobsEngine.Audio;
using PloobsEngine.Entity;
using PloobsEngine.Engine;
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
                
        private IList<IScreenUpdateable> updateables = new List<IScreenUpdateable>();

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

        public GraphicInfo GraphicInfo
        {
            get { return graphicInfo; }
            internal set { graphicInfo = value; }
        }
        private GraphicFactory graphicFactory;

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
        /// <param name="contentManager">The content manager.</param>
        protected virtual void InitScreen(GraphicInfo GraphicInfo, IContentManager contentManager) { }
        internal void iInitScreen(GraphicInfo GraphicInfo, IContentManager contentManager)
        {
            InitScreen(GraphicInfo, contentManager);
        }
        

        internal void iAfterLoadContent()
        {
            AfterLoadContent();
        }
        /// <summary>
        /// Called after all the screens LoadContent is called
        /// </summary>
        protected virtual void AfterLoadContent() { }

        
        internal void iUpdate(GameTime gameTime)
        {
            Update(gameTime);
        }
        /// <summary>
        /// Update the Screen
        /// </summary>
        protected virtual void Update(GameTime gameTime)
        {
            foreach (var item in updateables)
            {
                item.iUpdate(gameTime);
            }
            
        }

        internal void iDraw(GameTime gameTime, RenderHelper render)
        {
            Draw(gameTime,render);
        }
        /// <summary>
        /// This is called when the screen should draw itself.
        /// </summary>
        protected abstract void Draw(GameTime gameTime, RenderHelper render);


        internal void RemoveThisScreen()
        {
            CleanUp();
        }        
        /// <summary>
        /// Cleans up resources that dont are exclusive of the screen        
        /// </summary>
        protected virtual void CleanUp()
        {
        }        

    }
}
