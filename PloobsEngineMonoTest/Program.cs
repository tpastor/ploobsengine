using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Engine;
using PloobsEngine.SceneControl;
using EngineTestes;

namespace PloobsEngineTest
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            InitialEngineDescription desc = InitialEngineDescription.Default();
            desc.useMipMapWhenPossible = true;                        
            //desc.isMultiSampling = true;                        

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
}
