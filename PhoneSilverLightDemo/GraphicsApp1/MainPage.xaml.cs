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

namespace PloobsFeatures
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }        
        private void imageBasic2D_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Basic2D.xaml", UriKind.Relative));
        }

        private void imageAnimation_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/GamePage.xaml", UriKind.Relative));
        }

        private void image2_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/StressPage.xaml", UriKind.Relative));
        }

        private void image3_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/EnvironmentPage.xaml", UriKind.Relative));
        }


        private void ModelManipulationTap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ModelManipulationPage.xaml", UriKind.Relative));
        }

        private void imagepicking2D_Tap(object sender, GestureEventArgs e)
        {

            NavigationService.Navigate(new Uri("/TerrainPage.xaml", UriKind.Relative));
        }

        private void Basic2DPositioning_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Basic2DPositioning.xaml", UriKind.Relative));
        }

        private void Basic2DCamera_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Basic2DCamera.xaml", UriKind.Relative));
        }

        private void Basic2DSprite_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Basic2DSprite.xaml", UriKind.Relative));
        }

        private void Basic2DParticle_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Basic2DParticles.xaml", UriKind.Relative));            
        }

        private void Basic2DPhysic_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Basic2DPhysic.xaml", UriKind.Relative));            
        }

        private void particles3D_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Particles3D.xaml", UriKind.Relative));            
        }
    }
}