using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.SceneControl;
using PloobsEngine.Utils;
using Microsoft.Xna.Framework;
using XNAnimation;
using PloobsEngine.Features;

namespace PloobsEngine.Engine
{    
    internal class LoadingScreenTask : ITask
    {

        IScreen LoadingScreen;
        IScreen ToLoadScreen;
        IContentManager contentManager;
        EngineStuff engine;
        public LoadingScreenTask(IScreen ToLoadScreen, IContentManager contentManager, EngineStuff engine, IScreen LoadingScreen)
        {
            this.ToLoadScreen = ToLoadScreen;
            this.engine = engine;
            this.contentManager = contentManager;
            this.LoadingScreen = LoadingScreen;
        }

        #region ITask Members

        public void Result(IAsyncResult result)
        {
            LoadingScreen.ScreenManager.RemoveScreen(LoadingScreen);
            LoadingScreen.ScreenManager.AddScreen(ToLoadScreen,null,false);            
        }

        public void Process()
        {
            ToLoadScreen.iInitScreen(ToLoadScreen.GraphicInfo, engine);
            ToLoadScreen.iLoadContent(ToLoadScreen.GraphicInfo, ToLoadScreen.GraphicFactory, contentManager);
            ToLoadScreen.iAfterLoadContent(contentManager, ToLoadScreen.GraphicInfo, ToLoadScreen.GraphicFactory);
            ToLoadScreen.IsLoaded = true;
        }

        public TaskEndType TaskEndType
        {
            get { return Features.TaskEndType.ON_NEXT_UPDATE; }
        }

        #endregion
    }


}
