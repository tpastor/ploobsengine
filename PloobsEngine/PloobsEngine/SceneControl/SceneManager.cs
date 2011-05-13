#region Using Statements
using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using PloobsEngine.Engine;
using PloobsEngine.Engine.Logger;
using System.Threading;
using PloobsEngine.Features;
using PloobsEngine.Commands;
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
        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenManager"/> class.
        /// </summary>
        /// <param name="GraphicInfo">The graphic info.</param>
        /// <param name="factory">The factory.</param>
        /// <param name="contentManager">The content manager.</param>
        /// <param name="render">The render.</param>
        /// <param name="engine">The engine.</param>
        internal ScreenManager(ref GraphicInfo GraphicInfo, GraphicFactory factory,IContentManager contentManager, RenderHelper render,EngineStuff engine)
        {
            this.GraphicInfo = GraphicInfo;
            this.GraphicFactory = factory;
            this.contentManager = contentManager;
            this.render = render;
            this.engine = engine;
        }
        
        #region Fields

        EngineStuff engine;
        List<IScreen> screens = new List<IScreen>();
        List<IScreen> screensToUpdate = new List<IScreen>();
        internal GraphicInfo GraphicInfo;
        internal GraphicFactory GraphicFactory;
        internal IContentManager contentManager;
        internal RenderHelper render;
        
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
                
                screen.iDraw(gameTime,render);
            }

        }

        #endregion


        #region Public Methods
        
        /// <summary>
        /// Adds a new screen to the screen manager.
        /// </summary>
        /// <param name="definitiveScreen">The definitive screen.</param>
        /// <param name="LoadingScreen">The loading screen.</param>
        public void AddScreen(IScreen definitiveScreen, IScreen LoadingScreen = null)
        {
            if (LoadingScreen != null)
            {
                LoadingScreen.screenManager = this;
                LoadingScreen.graphicFactory = GraphicFactory;
                LoadingScreen.graphicInfo = GraphicInfo;
                LoadingScreen.iInitScreen(GraphicInfo, engine);
                LoadingScreen.iLoadContent(GraphicInfo, GraphicFactory, contentManager);
                LoadingScreen.iAfterLoadContent(contentManager, GraphicInfo, GraphicFactory);
                screens.Add(LoadingScreen);

                definitiveScreen.screenManager = this;
                definitiveScreen.graphicFactory = GraphicFactory;
                definitiveScreen.graphicInfo = GraphicInfo;
                TaskCommand tc = new TaskCommand(new LoadingScreenTask(definitiveScreen, contentManager, engine, LoadingScreen));
                CommandProcessor.getCommandProcessor().SendCommandAssyncronous(tc);
            }
            else
            {
                definitiveScreen.screenManager = this;
                definitiveScreen.graphicFactory = GraphicFactory;
                definitiveScreen.graphicInfo = GraphicInfo;
                definitiveScreen.iInitScreen(GraphicInfo, engine);
                definitiveScreen.iLoadContent(GraphicInfo, GraphicFactory, contentManager);
                definitiveScreen.iAfterLoadContent(contentManager, GraphicInfo, GraphicFactory);
                screens.Add(definitiveScreen);
            }
        }

        /// <summary>
        /// Removes a screen from the screen manager. You should normally
        /// use GameScreen.ExitScreen instead of calling this directly, so
        /// the screen can gradually transition off rather than just being
        /// instantly removed.
        /// </summary>
        public void RemoveScreen(IScreen screen)
        {
            if (screen == null)
            {
                ActiveLogger.LogMessage("cant remove null screen", LogLevel.RecoverableError);                
            }

            screen.RemoveThisScreen(engine);
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
