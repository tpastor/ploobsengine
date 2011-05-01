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
            InitialEngineDescription desc = new InitialEngineDescription("PLoobsDemos",800, 600, false, Microsoft.Xna.Framework.Graphics.GraphicsProfile.HiDef, false, false, false);

            using (EngineStuff engine = new EngineStuff(ref desc, LoadScreen))
            {
                engine.Run();
            }

        }

        static void LoadScreen(ScreenManager manager)
        {
            //manager.AddScreen(new FirstScreen());
            //manager.AddScreen(new DeferredScreen());
            //manager.AddScreen(new DeferredLoadScreen());
            //manager.AddScreen(new DeferredAnimatedScreen());
            //manager.AddScreen(new CharacterScreen());
            //manager.AddScreen(new ParticleScreen());
            //manager.AddScreen(new PostEffectScreen());
            //manager.AddScreen(new SoundScreen());
            //manager.AddScreen(new TransparentForwardScreen());
            //manager.AddScreen(new BilboardScreen());
            //manager.AddScreen(new TerrainScreen());
            //manager.AddScreen(new TransparentDeferredScreen());
            manager.AddScreen(new AAScreen());
            //manager.AddScreen(new TechDemoScreen());
            //manager.AddScreen(new TechDemoScreenModel());
            
        }
    }

}

