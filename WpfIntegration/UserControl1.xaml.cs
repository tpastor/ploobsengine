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
using System.Windows.Interop;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngineIntegration;

namespace WpfIntegration
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(GamePanel_Loaded);
        }

        public Game Game
        {
            get { return (Game)GetValue(GameProperty); }
            set { SetValue(GameProperty, value); }
        }

        public static readonly DependencyProperty GameProperty = DependencyProperty.Register("Game", typeof(Game), typeof(UserControl1));


        void GamePanel_Loaded(object sender, RoutedEventArgs e)
        {
            if (ReferenceEquals(Game, null))
            {
                return;
            }

            GameReflector.CreateGame(Game, _image);
            //Set the back buffer for the D3DImage, since unlocking it without one will thrown and exception
            SetD3DImageBackBuffer(CreateRenderTarget(1, 1));

#if RESIZABLE
            //Register for size changed if using a RenderTarget2D for drawing
            _image.SizeChanged += new SizeChangedEventHandler(OnSizeChanged);
#endif
            //Register for Rendering to perform updates and drawing
            System.Windows.Media.CompositionTarget.Rendering += new EventHandler(OnRendering);
        }

        private void OnRendering(object sender, EventArgs e)
        {
            _d3DImage.Lock();
            //Update and draw the game, then invalidate the D3DImage
            Game.Tick();
            _d3DImage.AddDirtyRect(new Int32Rect(0, 0, _d3DImage.PixelWidth, _d3DImage.PixelHeight));
            _d3DImage.Unlock();

            Window.GetWindow(this).Title = Game.Window.Title;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Can't create a RenderTarget2D with dimensions smaller than zero
            if ((int)_image.ActualWidth <= 0 || (int)_image.ActualHeight <= 0)
            {
                return;
            }

            var renderTarget = CreateRenderTarget((int)_image.ActualWidth, (int)_image.ActualHeight);
            if (!ReferenceEquals(renderTarget, null))
            {
                //Direct the game's drawing to the newly created RenderTarget2D,
                //whose surface will be displayed in the D3DImage
                Game.GraphicsDevice.SetRenderTarget(renderTarget);
                SetD3DImageBackBuffer(renderTarget);
            }
        }

        private RenderTarget2D CreateRenderTarget(int width, int height)
        {
            return new RenderTarget2D(Game.GraphicsDevice, width, height, false, SurfaceFormat.Color, DepthFormat.Depth24);
        }

        private void SetD3DImageBackBuffer(RenderTarget2D renderTarget)
        {
            _d3DImage.Lock();
#if RESIZABLE
            //Get the surface from the RenderTarget2D if using one for drawing
            _d3DImage.SetBackBuffer(D3DResourceType.IDirect3DSurface9, GameReflector.GetRenderTargetSurface(renderTarget));
#else
            //If not using a RenderTarget for drawing, get the surface from the GraphicsDevice
            _d3DImage.SetBackBuffer(D3DResourceType.IDirect3DSurface9, GameReflector.GetGraphicsDeviceSurface(Game.GraphicsDevice));
#endif
            _d3DImage.Unlock();
        }
    }
}
