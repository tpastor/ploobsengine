using Microsoft.Xna.Framework;
using PloobsEngine.SceneControl;
using System;

namespace AdvancedDemo4._0
{
    /// <summary>
    /// Opening Movie Screen
    /// </summary>
    public class TitleScreen : MovieScreen
    {
        public TitleScreen()
            : base("ploobs_entry", Color.Black)
        {
        }

        public override void VideoEnded()
        {
            ScreenManager.RemoveScreen(this);
            DemosHomeScreen menu = new DemosHomeScreen();
            ScreenManager.AddScreen(menu);
        }

    }
}
