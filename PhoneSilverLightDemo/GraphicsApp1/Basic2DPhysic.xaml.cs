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
using PloobsFeatures;
using EngineTestes;

namespace GraphicsApp1
{
    public partial class Basic2DPhysic : PhoneApplicationPage
    {
        public Basic2DPhysic()
        {
            InitializeComponent();            
            EngineStuff.InitializePloobsEngine(SharedGraphicsDeviceManager.Current, (Application.Current as App).Content);            
        }

        Basic2DPhysicScreen FirstScreen;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            FirstScreen = new Basic2DPhysicScreen();
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