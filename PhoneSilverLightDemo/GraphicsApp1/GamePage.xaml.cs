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
using PloobsEnginePhone7Template;

namespace PloobsFeatures
{
    public partial class GamePage : PhoneApplicationPage
    {        
        public GamePage()
        {
            InitializeComponent();
            EngineStuff.InitializePloobsEngine(SharedGraphicsDeviceManager.Current, (Application.Current as App).Content);            
        }

        AnimationScreen1 FirstScreen;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            FirstScreen = new AnimationScreen1();
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
            FirstScreen.TogleRotation();
       
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            FirstScreen.TogleWireFrame();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            FirstScreen.TogleIllumination();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            FirstScreen.TogleAnimation();
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(FirstScreen != null)
                FirstScreen.MultiplySpeed((float)e.NewValue);
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            FirstScreen.TogleModel();
        }
    }
}
