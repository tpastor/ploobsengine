using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PloobsEngineIntegration;
using PloobsEngine.SceneControl;
using PloobsEngine.Engine;
using Microsoft.Xna.Framework;
using EngineTestes;

namespace WpfIntegration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public EngineStuff Game
        {
            get;
            private set;
        }

        public MainWindow()
        {
            InitializeComponent();
            InitialEngineDescription desc = new InitialEngineDescription("PLoobsDemos", 800, 600, false, Microsoft.Xna.Framework.Graphics.GraphicsProfile.HiDef, false, false, false);
            Game = new EngineStuff(ref desc, LoadScreen);            
            DataContext = this;

#if !RESIZABLE
            this.ResizeMode = ResizeMode.NoResize;
#endif
        }

        void LoadScreen(ScreenManager manager)
        {
            manager.AddScreen(new DeferredScreen());            
        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
