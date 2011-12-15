using PloobsEngine.Engine;
using PloobsEngine.SceneControl;
using PloobsEngine.Engine.Logger;
using System;
using EngineTestes;
using PloobsEnginePhone7Template;

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
            //desc.UseVerticalSyncronization = true;
            //desc.isFixedGameTime = true;
            //desc.isMultiSampling = true;            
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

            ///add the first screen here        
            //manager.AddScreen(new Basic2DParticle());
            manager.AddScreen(new Basic2DPhysicScreen());
            //manager.AddScreen(new Basic2DSpriteScreen());
            //manager.AddScreen(new Basic2DCameraScreen());
            //manager.AddScreen(new Basic2DPositioningScreen());
            //manager.AddScreen(new Basic2DScreen());
            //manager.AddScreen(new TemplateForwardScreen());
            //manager.AddScreen(new AnimationScreen());

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




