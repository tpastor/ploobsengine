#if WINDOWS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using PloobsEngine.Engine;
using PloobsEngine.SceneControl;
using PloobsEngine.Utils;
using System.IO;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine.Logger;
using Microsoft.Xna.Framework.Graphics;

namespace PloobsEngine.TestSuite
{
    public class TestProcessor
    {
        public void EvaluateTestes(String path, String logPath = null, String pathToScreenShot = null)
        {
            if(Path.IsPathRooted(path) == false)
                path = Path.Combine(Environment.CurrentDirectory, path);
            Assembly u = Assembly.LoadFile(path);
            IList<Type> Screens = new List<Type>();
            IList<Type> AlgClass = new List<Type>();
            foreach (var item in u.GetTypes())
            {
                foreach (var att in item.GetCustomAttributes(false))
                {
                    if (att is TesteVisualScreen)
                    {
                        Screens.Add(item);                        
                    }
                    if (att is TesteAlgorithmClass)
                    {
                        AlgClass.Add(item);
                    }
                }
            }

            List<IScreen> screens = new List<IScreen>();
            foreach (var item in Screens)
            {
                screens.Add((IScreen) Activator.CreateInstance(item));
            }

            InitialEngineDescription InitialEngineDescription= InitialEngineDescription.Default();
            InitialEngineDescription.Logger = new logger(logPath);
            InitialEngineDescription.UnhandledException_Handler =
                (a,b) =>
                {                    
                    ActiveLogger.LogMessage("Fatal Error: " + b.ExceptionObject, LogLevel.FatalError);
                    Environment.Exit(-1);
                };

            EngineStuff EngineStuff = new EngineStuff(ref InitialEngineDescription, 
                (a) =>
                    {
                        a.AddScreen(new AlgoMainTest(AlgClass, screens, pathToScreenShot));
                    }
            );

            EngineStuff.Run();
            Environment.Exit(0);
        }

        class AlgoMainTest : IScreen
        {
            IList<Type> AlgClass;
            int i = -1;
            List<IScreen> IScreen;
            String pathToScreenShot;
            public AlgoMainTest(IList<Type> AlgClass, List<IScreen> IScreen,String pathToScreenShot)
            {
                this.AlgClass = AlgClass;
                this.pathToScreenShot = pathToScreenShot;
                this.IScreen = IScreen;
            }

            EngineStuff EngineStuff;
            protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
            {
                this.EngineStuff = engine;
                base.InitScreen(GraphicInfo, engine);
            }

            protected override void Update(GameTime gameTime)
            {
                i += 1;
                if (i == AlgClass.Count)
                {
                    ScreenManager.RemoveScreen(this);
                    ScreenManager.AddScreen(new ScreenMainTest(IScreen, pathToScreenShot));
                }
                else
                {
                    object o = Activator.CreateInstance(AlgClass[i]);
                    if (o is IAlgoTest)
                    {
                        IAlgoTest a = o as IAlgoTest;
                        a.EngineStuff = EngineStuff;
                    }

                    foreach (var item in AlgClass[i].GetMethods())
                    {
                        foreach (var at in item.GetCustomAttributes(false))
                        {
                            if (at is TesteAlgorithmMethod)
                            {
                                item.Invoke(o, null);
                            }
                        }
                    }

                }
                base.Update(gameTime);
            }

            protected override void Draw(Microsoft.Xna.Framework.GameTime gameTime, RenderHelper render)
            {
                render.RenderTextComplete("Ploobs Automated Algorithm Tests", new Microsoft.Xna.Framework.Vector2(50, 50), Color.Red, Matrix.Identity);
            }
        }

        class ScreenMainTest : IScreen
        {
            List<IScreen> IScreen;
            String pathToScreenShot;

            public ScreenMainTest(List<IScreen>  IScreen,String pathToScreenShot)
            {
                this.pathToScreenShot = pathToScreenShot;
                this.IScreen = IScreen;                                
            }

            TimePassed TimePassed = new TimePassed(5);
            TimePassed bet = new TimePassed(1);
            int i = -1;
            bool firstTime = true;
            IContentManager contentManager;
            RenderTarget2D RenderTarget2D;
            bool takeScreen = false;
            protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory, IContentManager contentManager)
            {
                RenderTarget2D = factory.CreateRenderTarget(GraphicInfo.BackBufferWidth, GraphicInfo.BackBufferHeight);
                this.contentManager = contentManager;
                base.LoadContent(GraphicInfo, factory, contentManager);
            }
            protected override void Update(Microsoft.Xna.Framework.GameTime gameTime)
            {
                if(firstTime)
                {
                    bet.InitOrRestart(gameTime);
                    firstTime = false;
                }


                if(bet.hasPassed(gameTime))
                {
                    i +=1;
                    if (i == IScreen.Count)
                        Environment.Exit(0);
                    ScreenManager.AddScreen(IScreen[i]);
                    TimePassed.InitOrRestart(gameTime);
                    ActiveLogger.LogMessage("Added Screen: " + IScreen[i].GetType().AssemblyQualifiedName, LogLevel.Info);
                    if(pathToScreenShot!=null)
                        takeScreen = true;
                }

                if (TimePassed.hasPassed(gameTime))
                {                    
                    ScreenManager.RemoveScreen(IScreen[i]);
                    bet.InitOrRestart(gameTime);
                    ActiveLogger.LogMessage("Removed Screen: " + IScreen[i].GetType().AssemblyQualifiedName,LogLevel.Info);

                    foreach (var item in contentManager.DumpAssetReferenceCount())
                    {
                        ActiveLogger.LogMessage("Assets Dump: " + item.Key + " : " + item.Value, LogLevel.Info);
                    }

                    System.Diagnostics.Debug.Assert(contentManager.DumpAssetReferenceCount().Count == 0);

                }

                base.Update(gameTime);
            }

            protected override void Draw(Microsoft.Xna.Framework.GameTime gameTime, RenderHelper render)
            {        
                if (takeScreen)
                {
                    render.PushRenderTarget(RenderTarget2D);
                    screenManager.GetScreens()[1].iDraw(gameTime, render);
                    render.PopRenderTarget();
                    takeScreen = false;
                    String sabeTo= Path.Combine(pathToScreenShot,screenManager.GetScreens()[1].GetType().Name + ".png");
                    RenderTarget2D.SaveAsPng(new FileStream(sabeTo, FileMode.Create), GraphicInfo.BackBufferWidth, GraphicInfo.BackBufferHeight);
                }
                render.RenderTextComplete("Ploobs Automated Visual Tests", new Microsoft.Xna.Framework.Vector2(50, 50), Color.Red, Matrix.Identity);
            }
        }

        class logger : ILogger
            {
            StreamWriter StreamWriter;
            public logger(string path)
            {
                string format = "MMM_d_HHmmyyyy";
                if (!String.IsNullOrEmpty(path))
                {
                    path = Path.Combine(path, "PloobsEngine_Build_" + DateTime.Now.ToString(format) + ".log");
                }
                else
                {
                    path = "PloobsEngine_Build_" + DateTime.Now.ToString(format) + ".log";
                }
                StreamWriter = new System.IO.StreamWriter(path, true);
                StreamWriter.WriteLine("Testing PloobsEngine " + DateTime.Now.ToUniversalTime());
            }
            ~logger()
            {
                StreamWriter.Flush();
                StreamWriter.Close();
            }

                #region ILogger Members
                            
                public override void Log(string Message, LogLevel logLevel)
                {
                    StreamWriter.WriteLine(logLevel + " : " + CurrentSceneType + " : " + Message);
                    StreamWriter.Flush();
                }

                #endregion
            }

    }
}
#endif