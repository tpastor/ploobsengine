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
using ProjectTemplate;
using Microsoft.Win32;
using WinFormsContentLoading;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Modelo;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.Material;
using PloobsEngine.Physics;
using System.IO;

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
            InitialEngineDescription desc = InitialEngineDescription.Default();
            Game = new EngineStuff(ref desc, LoadScreen);            
            DataContext = this;

#if !RESIZABLE
            this.ResizeMode = ResizeMode.NoResize;
#endif
        }

        EditorTestScreen EditorTestScreen = new EditorTestScreen();
        void LoadScreen(ScreenManager manager)
        {
            manager.AddScreen(EditorTestScreen);            
        }

        ContentBuilder ContentBuilder = new ContentBuilder();

        void MakeCopy(String source, String destiny)
        {
            foreach (String item in Directory.EnumerateFiles(source))
            {
                int i = item.LastIndexOf("\\");
                i = i == 0 ? 0 : i + 1;
                String name = item.Substring(i);
                File.Copy(item, destiny + name, true);
            }

            foreach (String item in Directory.EnumerateDirectories(source))
            {
                int i = item.LastIndexOf("\\");
                i = i == 0 ? 0 : i + 1;
                String name = item.Substring(i);
                Directory.CreateDirectory(destiny +  name);
                MakeCopy(item, destiny + name + "\\");
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OpenFileDialog = new OpenFileDialog();
            OpenFileDialog.Filter = "X Files (*.x)|*.x";
            OpenFileDialog.ShowDialog();            
            String fileName = OpenFileDialog.FileName;
            ContentBuilder.Add(fileName, "Model",null,"ModelProcessor");
            String buildError = ContentBuilder.Build();
            if (string.IsNullOrEmpty(buildError))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Content/Loaded");
                MakeCopy(ContentBuilder.OutputDirectory, Directory.GetCurrentDirectory() + "/Content/Loaded/");                
                                
                SimpleModel simpleModel = new SimpleModel(EditorTestScreen.GraphicFactory, "Loaded/Model");
                ///Physic info (position, rotation and scale are set here)
                TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Microsoft.Xna.Framework.Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
                ///Shader info (must be a deferred type)
                DeferredNormalShader shader = new DeferredNormalShader();
                ///Material info (must be a deferred type also)
                DeferredMaterial fmaterial = new DeferredMaterial(shader);
                ///The object itself
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                ///Add to the world
                EditorTestScreen.World.AddObject(obj);

            }
            else
            {
                // If the build failed, display an error message.
                MessageBox.Show(buildError, "Error");
            }


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
