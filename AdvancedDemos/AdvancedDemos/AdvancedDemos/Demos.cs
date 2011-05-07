using PloobsEngine.Engine;
using PloobsEngine.SceneControl;
using PloobsEngine.Engine.Logger;
using System;

namespace AdvancedDemo4._0
{    
    /// <summary>
    /// Introduction Demos entry point
    /// </summary>
    public class Demos
    {
        public Demos()
        {
            InitialEngineDescription desc = InitialEngineDescription.Default();
            ///optional parameters, the default is good for most situations
            desc.UseVerticalSyncronization = true;
            desc.isFixedGameTime = true;
            desc.isMultiSampling = true;
            desc.Logger = new SimpleLogger();
            desc.UnhandledException_Handler = UnhandledException;
            ///start the engine
            using (EngineStuff engine = new EngineStuff(ref desc, LoadScreen))
            {
                engine.Run();
            }
        }        
        

        static void LoadScreen(ScreenManager manager)
        {            
            ///add the title screen
            //manager.AddScreen(new DeferredLoadScreen());
            manager.AddScreen(new BumpSpecularDemo());                        
        }

        static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ///handle unhandled exception here (log, send to a server ....)
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
            ///handle messages logs
            Console.WriteLine(Message + "  -  "  + logLevel.ToString());
        }

        #endregion
    }
}




