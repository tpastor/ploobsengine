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
using PloobsFeatures;
using System.Windows.Navigation;
using AdvancedDemo4._0;

namespace GraphicsApp1
{
    public partial class Particles3D : PhoneApplicationPage
    {
        public Particles3D()
        {
            InitializeComponent();
            EngineStuff.InitializePloobsEngine(SharedGraphicsDeviceManager.Current, (Application.Current as App).Content);            
        }

        ParticleScreen FirstScreen;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            FirstScreen = new ParticleScreen();
            EngineStuff.Current.StartScene(FirstScreen, this);            
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            EngineStuff.Current.LeaveScene();
            base.OnNavigatedFrom(e);
        }
    }
}