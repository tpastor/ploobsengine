using EngineTestes;
using PloobsEngine.Engine;
using PloobsEngine.SceneControl;
using System;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;

namespace PloobsWindowsStore
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var factory = new MonoGame.Framework.GameFrameworkViewSource<EngineStuff>(GameFactory);
            Windows.ApplicationModel.Core.CoreApplication.Run(factory);
        }

        static void GameFactory( EngineStuff engine, IActivatedEventArgs args)
        {
            InitialEngineDescription desc = InitialEngineDescription.Default();
            desc.useMipMapWhenPossible = true;
            engine.Initialize(ref desc, LoadScreen);            
        }

        static void LoadScreen(ScreenManager manager)
        {
            manager.AddScreen(new FirstScreen());
        }

    }
}
