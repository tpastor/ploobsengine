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
using EngineTestes;
using System.Windows.Navigation;

namespace PloobsFeatures
{
    public partial class ModelManipulation : PhoneApplicationPage
    {
        public ModelManipulation()
        {
                    InitializeComponent();
            EngineStuff.InitializePloobsEngine(SharedGraphicsDeviceManager.Current, (Application.Current as App).Content);            
        }

        TechTruthScreen FirstScreen;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            FirstScreen = new TechTruthScreen();
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
            FirstScreen.ManipulateIllumination();
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (FirstScreen != null)
            {
                FirstScreen.ChangeFogProperties((float)e.NewValue);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            FirstScreen.BloomEnable = !FirstScreen.BloomEnable;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            FirstScreen.Wireframe = !FirstScreen.Wireframe; 
        }
    }
}