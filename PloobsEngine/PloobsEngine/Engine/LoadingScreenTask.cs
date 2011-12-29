#region License
/*
    PloobsEngine Game Engine Version 0.3 Beta
    Copyright (C) 2011  Ploobs

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.SceneControl;
using PloobsEngine.Utils;
using Microsoft.Xna.Framework;
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

        public override void Result(IAsyncResult result)
        {
            LoadingScreen.ScreenManager.RemoveScreen(LoadingScreen);
            LoadingScreen.ScreenManager.AddScreen(ToLoadScreen,null,false);            
        }

        public override void Process()
        {
            ToLoadScreen.iInitScreen(ToLoadScreen.GraphicInfo, engine);
            ToLoadScreen.iLoadContent(ToLoadScreen.GraphicInfo, ToLoadScreen.GraphicFactory, contentManager);
            ToLoadScreen.iAfterLoadContent(contentManager, ToLoadScreen.GraphicInfo, ToLoadScreen.GraphicFactory);
            ToLoadScreen.IsLoaded = true;
        }

        public override TaskEndType TaskEndType
        {
            get { return Features.TaskEndType.ON_NEXT_UPDATE; }
        }

        #endregion
    }


}
