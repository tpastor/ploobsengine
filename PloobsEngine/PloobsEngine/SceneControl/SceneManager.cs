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
        internal ScreenManager(ref GraphicInfo GraphicInfo, GraphicFactory factory, IContentManager contentManager, RenderHelper render, EngineStuff engine)
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
        /// <param name="loadAndInitScreen">if set to <c>true</c> [load and init definitiveScreen].</param>
        public void AddScreen(IScreen definitiveScreen, IScreen LoadingScreen = null, bool loadAndInitScreen = true)
        {
            
            System.Diagnostics.Debug.Assert(definitiveScreen != null);            
#if WINDOWS_PHONE
            definitiveScreen.Page = engine.PhoneApplicationPage;
#endif
            if (LoadingScreen != null)
            {
                LoadingScreen.screenManager = this;
                LoadingScreen.graphicFactory = GraphicFactory;
                LoadingScreen.graphicInfo = GraphicInfo;                
                if (loadAndInitScreen)
                {
                    LoadingScreen.iInitScreen(GraphicInfo, engine);
                    LoadingScreen.iLoadContent(GraphicInfo, GraphicFactory, contentManager);
                    LoadingScreen.iAfterLoadContent(contentManager, GraphicInfo, GraphicFactory);
                    LoadingScreen.IsLoaded = true;
                }
                screens.Add(LoadingScreen);
                LoadingScreen.ScreenState = ScreenState.Active;

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
                if (loadAndInitScreen)
                {
                    definitiveScreen.iInitScreen(GraphicInfo, engine);
                    definitiveScreen.iLoadContent(GraphicInfo, GraphicFactory, contentManager);
                    definitiveScreen.iAfterLoadContent(contentManager, GraphicInfo, GraphicFactory);
                }
                screens.Add(definitiveScreen);
                definitiveScreen.ScreenState = ScreenState.Active;
            }
        }

        /// <summary>
        /// Removes a screen from the screen manager. 
        /// </summary>
        /// <param name="screen">The screen.</param>
        /// <param name="cleanUp">if set to <c>true</c> [clean up the screen].</param>
        public void RemoveScreen(IScreen screen, bool cleanUp = true)
        {
            System.Diagnostics.Debug.Assert(screen != null);

            if(cleanUp)
                screen.RemoveThisScreen(engine);
            screen.ScreenState = ScreenState.Inactive;
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
