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
        /// Gets the current screen state.
        /// </summary>
        public ScreenState ScreenState
        {
            get { return screenState; }
            set { screenState = value; }
        }

       ScreenState screenState = ScreenState.Active;


        /// <summary>
        /// There are two possible reasons why a screen might be transitioning
        /// off. It could be temporarily going away to make room for another
        /// screen that is on top of it, or it could be going away for good.
        /// This property indicates whether the screen is exiting for real:
        /// if set, the screen will automatically remove itself as soon as the
        /// transition finishes.
        /// </summary>
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


        #region Initialization


        /// <summary>
        /// Load graphics content for the screen.
        /// </summary>
        public virtual void LoadContent()
        {
                     
        }

        /// <summary>
        /// Called after all the screens LoadContent is called
        /// </summary>
        public virtual void AfterLoadContent()
        {
        }

        

        #endregion


        #region Update and Draw


        /// <summary>
        /// Allows the screen to run logic, such as updating the transition position.
        /// Unlike HandleInput, this method is called regardless of whether the screen
        /// is active, hidden, or in the middle of a transition.
        /// </summary>
        public virtual void Update(GameTime gameTime)
        {
            //this.otherScreenHasFocus = otherScreenHasFocus;
            
            foreach (var item in updateables)
            {
                item.Update(gameTime);
            }

            if (IsExiting)
            {                
                  ExitScreen();                                 
            }           
        }
        
        /// <summary>
        /// This is called when the screen should draw itself.
        /// </summary>
        public virtual void Draw(GameTime gameTime)
        {
            
        }

        #endregion

        #region Public Methods


        /// <summary>
        /// Kill the screen
        /// Should be overrided for cleanup
        /// </summary>
        public void ExitScreen()
        {
            CleanUp();
            ScreenManager.RemoveScreen(this);                            
        }

        /// <summary>
        /// Cleans up everything.
        /// Called when the screen is removed
        /// </summary>
        public virtual void CleanUp()
        {
        }

        #endregion

    }
}
