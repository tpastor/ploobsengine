using System;
using PloobsEngine.Engine;
using PloobsEngine.SceneControl;
using EngineTestes.LoadingScreen;
using ProjectTemplate;
using EngineTestes._2DSamples;
using AdvancedDemo4._0;

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
            desc.useMipMapWhenPossible = true;
            desc.UseAnisotropicFiltering = true;
            desc.isMultiSampling = true;            

            using (EngineStuff engine = new EngineStuff(ref desc, LoadScreen))
            {
                engine.Run();
            }

        }

        static void LoadScreen(ScreenManager manager)
        {
            manager.AddScreen(new Basic2DNeoforce());
            //manager.AddScreen(new Primitive2D());
            //manager.AddScreen(new Teste2D());
            //manager.AddScreen(new SponzaScreen());
            //manager.AddScreen(new ForwardMaterialsScreen());
            //manager.AddScreen(new PostEffectScreen());            
            //manager.AddScreen(new MobilePhysicScreen());
            //manager.AddScreen(new MultScreen());
            //manager.AddScreen(new FirstScreen());
            //manager.AddScreen(new DeferredScreen());
            //manager.AddScreen(new DeferredLoadScreen());
            //manager.AddScreen(new DeferredAnimatedScreen());
            //manager.AddScreen(new ForwardAnimatedScreen());
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
            //manager.AddScreen(new BoltSample());
            //manager.AddScreen(new ShatterSample());
            //manager.AddScreen(new ExplosionScreen());
            //manager.AddScreen(new StealthEffectScreen());
            //manager.AddScreen(new StealthShaderScreen());
            //manager.AddScreen(new GammaCorrectionScreen());
        }
    }

}

