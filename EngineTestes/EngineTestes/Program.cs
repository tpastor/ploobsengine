using System;
using PloobsEngine.Engine;
using PloobsEngine.SceneControl;
using EngineTestes.LoadingScreen;
using ProjectTemplate;
using EngineTestes._2DSamples;

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
            //manager.AddScreen(new AAScreen());
            //manager.AddScreen(new TechDemoScreen());
            //manager.AddScreen(new TechDemoScreenModel());
            //manager.AddScreen(new GUIScreen());
            //manager.AddScreen(new FGUIScreen());
            //manager.AddScreen(new ForwardLoadScreen());
            //manager.AddScreen(new ForwardMeshesFromModelLoadScreen());
            //manager.AddScreen(new DeferredLoadScreen(),new LoadScene());
            //manager.AddScreen(new DebugDrawScreen());
            //manager.AddScreen(new OctreeScreen());
            //manager.AddScreen(new HybridDeferred());
            //manager.AddScreen(new VegetationForwardScreen());
            //manager.AddScreen(new VegetationDeferredScreen());            
            //manager.AddScreen(new MixReflection());                        
            //manager.AddScreen(new Basic2D());
            //manager.AddScreen(new ConstraintScreen());
            //manager.AddScreen(new IaWaypoints());
            //manager.AddScreen(new IaPathFinding());
            //manager.AddScreen(new SteerScreen());
            //manager.AddScreen(new FSMScreen());
            //manager.AddScreen(new ParalaxScreen());
            //manager.AddScreen(new RadialBluScreen());
            manager.AddScreen(new BoltSample());
        }
    }

}

