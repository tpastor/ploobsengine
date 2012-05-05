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
using IntroductionDemo4._0;
using EngineTestes;

namespace GraphicsApp1
{
    public partial class CrowdPage : PhoneApplicationPage
    {
        public CrowdPage()
        {
            InitializeComponent();
            EngineStuff.InitializePloobsEngine(SharedGraphicsDeviceManager.Current, (Application.Current as App).Content);
        }

        RVOScreen FirstScreen;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
         
            FirstScreen = new RVOScreen();
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