using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using PloobsEngine.Physics;
using PloobsEngine.Modelo;
using PloobsEngine.Material;
using PloobsEngine.Engine;
using PloobsEngine.Physics.Bepu;
using Microsoft.Xna.Framework;
using PloobsEngine.Cameras;
using PloobsEngine.Light;
using Microsoft.Xna.Framework.Input;
using PloobsEngine.Features;
using PloobsEngine.Commands;
using PloobsEngine.SceneControl.GUI;
using TomShane.Neoforce.Controls;

namespace AdvancedDemo4._0
{
    /// <summary>
    /// GUI on forward rendering
    /// </summary>
    public class FGUIScreen : IScene
    {
        bool okClicked = false;

        /// <summary>
        /// /// NEED TO PASS THE GUI IMPLEMENTATION TO THE BASE CLASS
        /// </summary>
        public FGUIScreen() : base(new NeoforceGui()) { }

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

            ForwardRenderTecnichDescription desc = new ForwardRenderTecnichDescription();
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new ForwardRenderTecnich(desc);
        }
        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);

            SkyBox skybox = new SkyBox();
            engine.AddComponent(skybox);

            engine.IsMouseVisible = true;
        }

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo,factory, contentManager);

            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//cenario");
                TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
                ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);
            }

            NeoforceGui guiManager = this.Gui as NeoforceGui;
            System.Diagnostics.Debug.Assert(guiManager != null);

            // Create and setup Window control.
            Window window = new Window(guiManager.Manager);
            window.Init();
            window.Text = "Getting Started";
            window.Width = 480;
            window.Height = 200;
            window.Center();
            window.Visible = true;

            // Create Button control and set the previous window as its parent.
            Button button = new Button(guiManager.Manager);
            button.Init();
            button.Text = "OK";
            button.Width = 72;
            button.Height = 24;
            button.Left = (window.ClientWidth / 2) - (button.Width / 2);
            button.Top = window.ClientHeight - button.Height - 8;
            button.Anchor = Anchors.Bottom;
            button.Parent = window;
            button.Click += new TomShane.Neoforce.Controls.EventHandler(button_Click);

            // Add the window control to the manager processing queue.
            guiManager.Manager.Add(window);         

            this.World.CameraManager.AddCamera(new CameraFirstPerson(false,GraphicInfo));

            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//grassCube");
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);
        }        

        void button_Click(object sender, TomShane.Neoforce.Controls.EventArgs e)
        {
            Button b = (Button)sender;       
            ///use it ....
            okClicked = true;
        }

        protected override void CleanUp(EngineStuff engine)
        {
            engine.RemoveComponent("SkyBox");
            base.CleanUp(engine);
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);
            render.RenderTextComplete("Demo 16-22:Gui Forward Sample", new Vector2(10, 15), Color.White, Matrix.Identity);            
            if(okClicked)
                render.RenderTextComplete("Ok Clicked", new Vector2(10, 35), Color.White, Matrix.Identity);
        }

    }
}
