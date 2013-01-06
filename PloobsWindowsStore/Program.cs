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
            var factory = new MonoGame.Framework.GameFrameworkViewSourceByDelegate<EngineStuff>(GameFactory);
            Windows.ApplicationModel.Core.CoreApplication.Run(factory);
        }

        static EngineStuff GameFactory(CoreApplicationView voceview, IActivatedEventArgs args)
        {
            InitialEngineDescription desc = InitialEngineDescription.Default();
            desc.useMipMapWhenPossible = true;
            EngineStuff engine = new EngineStuff(ref desc, LoadScreen);
            return engine;
        }

        static void LoadScreen(ScreenManager manager)
        {
            manager.AddScreen(new FirstScreen());
        }

    }
}
