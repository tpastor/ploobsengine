using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using PloobsEngine.Engine;
using Microsoft.Xna.Framework;
using System.Windows.Navigation;
using AdvancedDemo4._0;
using ProjectTemplate;
using PloobsFeatures;

namespace GraphicsApp1
{
    public partial class GeoClipTerrainPage : PhoneApplicationPage
    {
        public GeoClipTerrainPage()
        {
            InitializeComponent();
            EngineStuff.InitializePloobsEngine(SharedGraphicsDeviceManager.Current, (Application.Current as App).Content);            
        }

        TerrainGeoClipMap FirstScreen;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            FirstScreen = new TerrainGeoClipMap();
            EngineStuff.Current.StartScene(FirstScreen, this);            
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            EngineStuff.Current.LeaveScene();
            base.OnNavigatedFrom(e);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            FirstScreen.releaseobjs();  
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            FirstScreen.wireframe = !FirstScreen.wireframe;
        }
    }
}