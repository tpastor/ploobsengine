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
using Microsoft.Xna.Framework;
using System.Windows.Navigation;
using PloobsEngine.Engine;
using PloobsEnginePhone7Template;

namespace PloobsFeatures
{
    public partial class StressPage : PhoneApplicationPage
    {
        public StressPage()
        {
            InitializeComponent();
            EngineStuff.InitializePloobsEngine(SharedGraphicsDeviceManager.Current, (Application.Current as App).Content);            
        }

        StressScreen FirstScreen;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            FirstScreen = new StressScreen();
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
            FirstScreen.CreateMOre();
        }

    }
}