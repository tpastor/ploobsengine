using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Engine;
using EngineTestes;
using PloobsEngine.SceneControl;

namespace GraphicsApp1
{
    public partial class GamePage : PhoneApplicationPage
    {        
        public GamePage()
        {
            InitializeComponent();
            EngineStuff.InitializePloobsEngine(SharedGraphicsDeviceManager.Current, (Application.Current as App).Content);            
        }

        FirstScreen FirstScreen;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            FirstScreen = new FirstScreen();
            EngineStuff.Current.StartScene(FirstScreen, this);            
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            EngineStuff.Current.LeaveScene();
            base.OnNavigatedFrom(e);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Texture2D tex =  FirstScreen.GraphicFactory.CreateTexture2DRandom(1, 1);
            IObject obj =  FirstScreen.World.Objects[0];
            obj.Modelo.SetTexture(tex, PloobsEngine.Modelo.TextureType.DIFFUSE);
        }
    }
}
