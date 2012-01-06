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
using PloobsEngine.Engine;
using System.Windows.Navigation;
using EngineTestes;

namespace PloobsFeatures
{
    public partial class Basic2D : PhoneApplicationPage
    {
        public Basic2D()
        {
            InitializeComponent();
            EngineStuff.InitializePloobsEngine(SharedGraphicsDeviceManager.Current, (Application.Current as App).Content);            
        }

        Basic2DScreen FirstScreen;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            FirstScreen = new Basic2DScreen();
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
            FirstScreen.AtractorStrengh(1);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            FirstScreen.AtractorStrengh(-1);
        }

        bool trace = false;
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (trace)
            {
                FirstScreen.Trace(false);
                (sender as Button).Content = "Track";
            }
            else
            {
                FirstScreen.Trace(true);
                (sender as Button).Content = "Untrack";
            }
            trace = !trace;
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            FirstScreen.ChangeMode();
        }
    }
}