using PloobsEngine.Engine;
using PloobsEngine.SceneControl;
using PloobsEngine.Engine.Logger;
using System;

namespace IntroductionDemo4._0
{
    
    public class Demos
    {
        public Demos()
        {
            InitialEngineDescription desc = InitialEngineDescription.Default();
            ///optional parameters, the default is good for most situations
            desc.UseVerticalSyncronization = true;
            desc.isFixedGameTime = true;            
            desc.Logger = new SimpleLogger();
            desc.UnhandledException_Handler = UnhandledException;

            using (EngineStuff engine = new EngineStuff(ref desc, LoadScreen))
            {
                engine.Run();
            }
        }        
        

        static void LoadScreen(ScreenManager manager)
        {            
            manager.AddScreen(new BasicScreenDeferredDemo());
            //manager.AddScreen(new BasicScreenForwardDemo());
            //manager.AddScreen(new KeyboardInputScreen());
            //manager.AddScreen(new PointLightScreen());
            //manager.AddScreen(new SpotLightScreen());
        }

        static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("Exception: " + e.ToString());
        }
    }

    class SimpleLogger : ILogger
    {
        #region ILogger Members

        public void Log(string Message, LogLevel logLevel)
        {
            Console.WriteLine(Message + "  -  "  + logLevel.ToString());
        }

        #endregion
    }
}




