using System;
using PloobsEngine.Engine;
using PloobsEngine.SceneControl;

namespace EngineTestes
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            InitialEngineDescription desc = new InitialEngineDescription(800,600,false,Microsoft.Xna.Framework.Graphics.GraphicsProfile.Reach,false,true);

            using (EngineStuff engine = new EngineStuff(ref desc, LoadScreen))
            {
                engine.Run();
            }

        }

        static void LoadScreen(ScreenManager manager)
        {
            manager.AddScreen(new FirstScreen());
        }
    }
#endif
}

