using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using PloobsEngine.Engine;
using PloobsEngine.SceneControl;
using EngineTestes;
using EngineTestes._2DSamples;

namespace EnginePhoneTests
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {        
        EngineStuff engine; 

        public Game1()
        {
            InitialEngineDescription desc = InitialEngineDescription.Default();
            desc.isMultiSampling = true;

            engine = new EngineStuff(this, ref desc, LoadScreen);

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);
        }

        void LoadScreen(ScreenManager manager)
        {
            //manager.AddScreen(new AnimationScreen());
            manager.AddScreen(new FirstScreen());
            //manager.AddScreen(new Basic2D());
            //manager.AddScreen(new InputGestureScreen());
            //manager.AddScreen(new Picking2D());
            //manager.AddScreen(new Picking3D());
            //manager.AddScreen(new CamScreen());

        }
        
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            engine.LoadContent();
            base.LoadContent();
        }

        

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            engine.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            engine.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
