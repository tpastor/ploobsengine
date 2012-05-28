using System;
using PloobsEngine.Engine;
using PloobsEngine.SceneControl;

namespace EngineTestes
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            InitialEngineDescription desc = InitialEngineDescription.Default();

            using (EngineStuff engine = new EngineStuff(ref desc, LoadScreen))
            {
                engine.Run();
            }

        }

        static void LoadScreen(ScreenManager manager)
        {            
            manager.AddScreen(new NetworkingScreen());            
        }
    }

}

