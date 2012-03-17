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

namespace PloobsEngine.TestSuite
{
    public class TestProcessor
    {
        public void EvaluateTestes(String path)
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
            InitialEngineDescription.Logger = new logger();
            EngineStuff EngineStuff = new EngineStuff(ref InitialEngineDescription, 
                (a) =>
                    {
                        a.AddScreen(new AlgoMainTest(AlgClass,screens));
                    }
            );

            EngineStuff.Run();
        }

        class AlgoMainTest : IScreen
        {
            IList<Type> AlgClass;
            int i = -1;
            List<IScreen> IScreen;
            public AlgoMainTest(IList<Type> AlgClass, List<IScreen> IScreen)
            {
                this.AlgClass = AlgClass;
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
                    ScreenManager.AddScreen(new ScreenMainTest(IScreen));
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
            public ScreenMainTest(List<IScreen>  IScreen)
            {
                this.IScreen = IScreen;                                
            }

            TimePassed TimePassed = new TimePassed(5, false);
            TimePassed bet = new TimePassed(1, false);
            int i = -1;
            bool firstTime = true;
            IContentManager contentManager;
            protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory, IContentManager contentManager)
            {
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
                render.RenderTextComplete("Ploobs Automated Visual Tests",new Microsoft.Xna.Framework.Vector2(50,50),Color.Red,Matrix.Identity);
            }
        }

        class logger : ILogger
            {
                #region ILogger Members

                public void Log(string Message, LogLevel logLevel)
                {
                    Console.WriteLine(logLevel);
                    Console.WriteLine(Message);
                }

                #endregion
            }

    }
}
#endif