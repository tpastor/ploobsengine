using PloobsEngine.Engine;
using PloobsEngine.SceneControl;
using PloobsEngine.Engine.Logger;
using System;
using EngineTestes;
using PloobsEnginePhone7Template;
using IntroductionDemo4._0;
using AdvancedDemo4._0;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectTemplate
{
    /// <summary>
    /// Engine entry point
    /// </summary>
    public class EngineSetup
    {
        public EngineSetup()
        {
            ///Create the default Engine Description
            InitialEngineDescription desc = InitialEngineDescription.Default();
            ///optional parameters, the default is good for most situations                                    
            desc.isFixedGameTime = true;                
            desc.UseVerticalSyncronization = true;
            desc.isMultiSampling = true;
            desc.BackBufferWidth = 800;
            desc.BackBufferHeight = 600;           
            desc.useMipMapWhenPossible = false;
            desc.Logger = new SimpleLogger();
            desc.UnhandledException_Handler = UnhandledException;
            ///start the engine
            using (EngineStuff engine = new EngineStuff(ref desc, LoadScreen))
            {
                ///start the engine internal flow
                engine.Run();
            }
        }


        static void LoadScreen(ScreenManager manager)
        {
            
            manager.AddScreen(new DemosHomeScreen());

            ///add the first screen here        
            //manager.AddScreen(new Basic2DParticle());
            //manager.AddScreen(new Basic2DPhysicScreen());
            //manager.AddScreen(new Basic2DSpriteScreen());
            //manager.AddScreen(new Basic2DCameraScreen());
            //manager.AddScreen(new Basic2DPositioningScreen());
            //manager.AddScreen(new Basic2DScreen());
            //manager.AddScreen(new TemplateForwardScreen());
            //manager.AddScreen(new AnimationScreen());
            //manager.AddScreen(new MilkShakeFormatScreen());
            //manager.AddScreen(new ColladaFormatScreen());
            //manager.AddScreen(new TerrainGeoClipMap());
            //manager.AddScreen(new DynamicEnvMapScreen());            
            //manager.AddScreen(new MotionForwardScreen());            

        }

        static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ///handle unhandled excetption here (log, send to a server ....)
            Console.WriteLine("Exception: " + e.ToString());
        }
    }

    /// <summary>
    /// Custom log class
    /// When using the Release version of the engine, the log wont be used by the engine.
    /// </summary>
    class SimpleLogger : ILogger
    {
        #region ILogger Members

        public void Log(string Message, LogLevel logLevel)
        {
            ///handle messages logs the way you want here
            Console.WriteLine(Message + "  -  " + logLevel.ToString());
        }

        #endregion
    }
}




