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
            //desc.UseVerticalSyncronization = true;
            //desc.isFixedGameTime = true;
            //desc.isMultiSampling = true;
            //desc.useMipMapWhenPossible = true;
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
            //manager.AddScreen(new TitleScreen());
            //manager.AddScreen(new DeferredLoadScreen());
            //manager.AddScreen(new BumpSpecularDemo());                        
            //manager.AddScreen(new EnvMapScreen());                        
            //manager.AddScreen(new ParalaxScreen());                        
            //manager.AddScreen(new TransparentDeferredScreen());                        
            //manager.AddScreen(new SoundScreen());
            //manager.AddScreen(new FollowerObjectSoundScreen());                        
            //manager.AddScreen(new TerrainScreen());                        
            //manager.AddScreen(new ParticleScreen());                        
            //manager.AddScreen(new AnimatedBilboardScreen());                        
            //manager.AddScreen(new InstancedBilboardScreen());                        
            //manager.AddScreen(new NormalBilboardScreen());                        
            //manager.AddScreen(new ProceduralAnimatedBilboardScreen());            
            //manager.AddScreen(new DeferredAnimatedScreen());            
            //manager.AddScreen(new FGUIScreen(),new LoadingScreen());            
            //manager.AddScreen(new DGUIScreen());              
            //manager.AddScreen(new NoiseScreen());            
            //manager.AddScreen(new PerlinNoiseScreen());            
            //manager.AddScreen(new ProceduralTextureScreen());            
            //manager.AddScreen(new OceanScreen());            
            //manager.AddScreen(new WaterCompleteScreen());            
            //manager.AddScreen(new DeferredDirectionaldShadowScreen());            
            manager.AddScreen(new SSAOScreen());
            //manager.AddScreen(new DemosHomeScreen());
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




