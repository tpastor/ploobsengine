using Microsoft.Xna.Framework;
using PloobsEngine.SceneControl;

namespace IntroductionDemo4._0
{
    /// <summary>
    /// Opening Movie Screen
    /// </summary>
    public class TitleScreen : MovieScreen
    {
        public TitleScreen()
            : base("ploobs_entry",Color.Blue)
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