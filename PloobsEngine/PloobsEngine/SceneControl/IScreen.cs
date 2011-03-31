///based on the XNA DEMO Screen Managment
#region Using Statements
using System;
using Microsoft.Xna.Framework;
using System.Collections;
using System.Collections.Generic;
using PloobsEngine.Audio;
using PloobsEngine.Entity;
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
       /// Gets or sets a value indicating whether this instance is exiting.
       /// If you set to True the screen will be removed as soon as possible
       /// </summary>
       /// <value>
       /// 	<c>true</c> if this instance is exiting; otherwise, <c>false</c>.
       /// </value>
        public bool IsExiting
        {
            get { return isExiting; }
            protected internal set
            {                
                isExiting = value;                
            }
        }

        bool isExiting = false;


        /// <summary>
        /// Gets the manager that this screen belongs to.
        /// </summary>
        protected ScreenManager ScreenManager
        {
            get { return screenManager; }            
        }

        internal ScreenManager screenManager;                

        #endregion

        /// <summary>
        /// Load graphics content for the screen.
        /// </summary>
        protected virtual void LoadContent() { }        
        internal void iLoadContent()
        {
            LoadContent();
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
                item.Update(gameTime);
            }

            if (IsExiting)
            {
                ExitScreen();
            }           
        }
                
        internal void iDraw(GameTime gameTime)
        {
            Draw(gameTime);
        }
        /// <summary>
        /// This is called when the screen should draw itself.
        /// </summary>
        protected abstract void Draw(GameTime gameTime);
        
        
        /// <summary>
        /// Kill the screen        
        /// </summary>
        public void ExitScreen()
        {
            CleanUp();
            ScreenManager.RemoveScreen(this);                            
        }

        /// <summary>
        /// Cleans up resources that dont are exclusive of the screen        
        /// </summary>
        protected virtual void CleanUp()
        {
        }        

    }
}
