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
using System.Windows.Navigation;
using Microsoft.Xna.Framework;
using AdvancedDemo4._0;

namespace PloobsFeatures
{
    public partial class TerrainPage : PhoneApplicationPage
    {
        public TerrainPage()
        {
            InitializeComponent();
            EngineStuff.InitializePloobsEngine(SharedGraphicsDeviceManager.Current, (Application.Current as App).Content);            
        }

        TerrainScreen FirstScreen;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            FirstScreen = new TerrainScreen();
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

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (FirstScreen != null)
                FirstScreen.recreateTerrain((int)e.NewValue);
        }

    }
}