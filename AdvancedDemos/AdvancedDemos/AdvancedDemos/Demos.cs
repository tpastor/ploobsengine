using PloobsEngine.Engine;
using PloobsEngine.SceneControl;
using PloobsEngine.Engine.Logger;
using System;
using System.Windows.Forms;

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
            //desc.useMipMapWhenPossible = false;
            //desc.UseAnisotropicFiltering = false;
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
            manager.AddScreen(new TitleScreen());
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
            //manager.AddScreen(new SSAOScreen());
            //manager.AddScreen(new DemosHomeScreen());
            //manager.AddScreen(new CPUBillboardScreen());
            //manager.AddScreen(new DecalScreen());
            //manager.AddScreen(new ExplosionScreen());
            //manager.AddScreen(new StealthEffectScreen());
            //manager.AddScreen(new AmbientEnvironmenpScreen());
            //manager.AddScreen(new ShaderIDScreen());

            //manager.AddScreen(new Physx28Screen());
            //manager.AddScreen(new Physx28MaterialScreen());
            //manager.AddScreen(new Physx28TriggerScreen());
            //manager.AddScreen(new PhysxCharacter28Screen());
            //manager.AddScreen(new BasicCloth28Screen());
            //manager.AddScreen(new FlagCloth28Screen());
            //manager.AddScreen(new PressureCloth28Screen());
            //manager.AddScreen(new Fluids28Screen());
            //manager.AddScreen(new EngineTestes.DeferredEmitterFluids28Screen());
            //manager.AddScreen(new AnimScreen());
            //manager.AddScreen(new XnaSkinnedScreen());
            //manager.AddScreen(new BoxCloth28Screen());

        }

        static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ///handle unhandled exception here (log, send to a server ....)
            //Console.WriteLine("Exception: " + e.ToString());
            MessageBox.Show(e.ExceptionObject.ToString());
        }
    }

    /// <summary>
    /// Custom log class    
    /// </summary>
    class SimpleLogger : ILogger
    {
        #region ILogger Members

        public override void Log(string Message, LogLevel logLevel)
        {
            ///handle messages logs
            if (logLevel == LogLevel.FatalError)
            {
                MessageBox.Show("Fatal Error: " + Message);
            }
            else
            {
                Console.WriteLine(Message + "  -  " + logLevel.ToString());
            }
        }

        #endregion
    }
}




