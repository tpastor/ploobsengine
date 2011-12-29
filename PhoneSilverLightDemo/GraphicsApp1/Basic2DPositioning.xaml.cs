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
using PloobsFeatures;
using System.Windows.Navigation;
using EngineTestes;

namespace GraphicsApp1
{
    public partial class Basic2DPositioning : PhoneApplicationPage
    {
        public Basic2DPositioning()
        {
            InitializeComponent();
            EngineStuff.InitializePloobsEngine(SharedGraphicsDeviceManager.Current, (Application.Current as App).Content);            
        }

        Basic2DPositioningScreen FirstScreen;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            FirstScreen = new Basic2DPositioningScreen();
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