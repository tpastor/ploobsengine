#region Using Statements
using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
#endregion

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// The screen manager is a component which manages one or more GameScreen
    /// instances. It maintains a stack of screens, calls their Update and Draw
    /// methods at the appropriate times
    /// </summary>    
    /// </remarks>
    public class ScreenManager 
    {

        public ScreenManager()
        {            
        }

        #region Fields

        List<IScreen> screens = new List<IScreen>();
        List<IScreen> screensToUpdate = new List<IScreen>();        
        bool traceEnabled;        
        
        #endregion


        

        #region Properties


        /// <summary>
        /// If true, the manager prints out a list of all the screens
        /// each time it is updated. This can be useful for making sure
        /// everything is being added and removed at the right times.
        /// </summary>
        public bool TraceEnabled
        {
            get { return traceEnabled; }
            set { traceEnabled = value; }
        }


        #endregion
        

        #region Update and Draw

        /// <summary>
        /// Allows each screen to run logic.
        /// </summary>
        internal  void Update(GameTime gameTime)
        {
            // Make a copy of the master screen list, to avoid confusion if
            // the process of updating one screen adds or removes others.
            screensToUpdate.Clear();

            foreach (IScreen screen in screens)
                screensToUpdate.Add(screen);
            
            // Loop as long as there are screens waiting to be updated.
            while (screensToUpdate.Count > 0)
            {
                // Pop the topmost screen off the waiting list.
                IScreen screen = screensToUpdate[screensToUpdate.Count - 1];

                screensToUpdate.RemoveAt(screensToUpdate.Count - 1);

                // Update the screen.
                if (screen.ScreenState != ScreenState.Paused && screen.ScreenState != ScreenState.Inactive)
                    screen.iUpdate(gameTime);                
            }

            // Print debug trace?
            if (traceEnabled)
                TraceScreens();
        }


        /// <summary>
        /// Prints a list of all the screens, for debugging.
        /// </summary>
        void TraceScreens()
        {
            List<string> screenNames = new List<string>();

            foreach (IScreen screen in screens)
                screenNames.Add(screen.GetType().Name);

            Trace.WriteLine(string.Join(", ", screenNames.ToArray()));
        }



        /// <summary>
        /// Tells each screen to draw itself.
        /// </summary>
        internal void Draw(GameTime gameTime)
        {
            foreach (IScreen screen in screens)
            {
                if (screen.ScreenState == ScreenState.Hidden || screen.ScreenState == ScreenState.Inactive)
                    continue;
                
                screen.iDraw(gameTime);
            }

        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Adds a new screen to the screen manager.
        /// </summary>
        public void AddScreen(IScreen screen)
        {
            screen.screenManager = this;
            screen.IsExiting = false;

            screen.iLoadContent();
            screen.iAfterLoadContent();        

            screens.Add(screen);
        }


        /// <summary>
        /// Removes a screen from the screen manager. You should normally
        /// use GameScreen.ExitScreen instead of calling this directly, so
        /// the screen can gradually transition off rather than just being
        /// instantly removed.
        /// </summary>
        internal void RemoveScreen(IScreen screen)
        {            
            screens.Remove(screen);
            screensToUpdate.Remove(screen);
        }


        /// <summary>
        /// Expose an array holding all the screens. We return a copy rather
        /// than the real master list, because screens should only ever be added
        /// or removed using the AddScreen and RemoveScreen methods.
        /// </summary>
        public IScreen[] GetScreens()
        {
            return screens.ToArray();
        }


        #endregion
    }
}
