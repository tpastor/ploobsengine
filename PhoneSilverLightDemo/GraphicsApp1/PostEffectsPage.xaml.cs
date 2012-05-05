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
using ProjectTemplate;
using System.Windows.Navigation;
using PloobsFeatures;

namespace GraphicsApp1
{
    public partial class PostEffectsPage : PhoneApplicationPage
    {
        public PostEffectsPage()
        {
            InitializeComponent();
            EngineStuff.InitializePloobsEngine(SharedGraphicsDeviceManager.Current, (Application.Current as App).Content);
        }

        PostEffectScreen FirstScreen;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            FirstScreen = new PostEffectScreen();
            EngineStuff.Current.StartScene(FirstScreen, this);
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            EngineStuff.Current.LeaveScene();
            base.OnNavigatedFrom(e);
        }

        private void bloom_Click(object sender, RoutedEventArgs e)
        {
            FirstScreen.ToggleBloom();

        }

        private void blur_Click(object sender, RoutedEventArgs e)
        {
            FirstScreen.ToggleBlur();

        }
     
        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (FirstScreen != null)
                FirstScreen.AmmountMotionBlurDelta((int)e.NewValue);
            
        }


     
    }
}