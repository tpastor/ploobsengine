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
using PloobsEngine.Loader;

namespace AdvancedDemo4._0
{
    /// <summary>
    /// Gui on Deferred Rendering
    /// </summary>
    public class DGUIScreen : IScene
    {
        bool okClicked = false;

        /// <summary>
        /// NEED TO PASS THE GUI IMPLEMENTATION TO THE BASE CLASS
        /// </summary>
        public DGUIScreen() : base(new NeoforceGui()) { }

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();            
            desc.UseFloatingBufferForLightMap = false;            
            renderTech = new DeferredRenderTechnic(desc);
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
                ///Create the xml file model extractor
                ///Loads a XML file that was export by our 3DS MAX plugin
                ExtractXmlModelLoader ext = new ExtractXmlModelLoader("Content//ModelInfos//", "Model//", "Textures//");
                this.AttachCleanUpAble(ext);
                ///Extract all the XML info (Model,Cameras, ...)
                ModelLoaderData data = ext.Load(factory, GraphicInfo, "ilha");
                ///Create the WOrld Loader
                ///Convert the ModelLoaderData in World Entities
                WorldLoader wl = new WorldLoader(); ///all default                
                wl.LoadWorld(factory, GraphicInfo, World, data);
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

            


            #region NormalLight
            DirectionalLightPE ld1 = new DirectionalLightPE(Vector3.Left, Color.White);
            DirectionalLightPE ld2 = new DirectionalLightPE(Vector3.Right, Color.White);
            DirectionalLightPE ld3 = new DirectionalLightPE(Vector3.Backward, Color.White);
            DirectionalLightPE ld4 = new DirectionalLightPE(Vector3.Forward, Color.White);
            DirectionalLightPE ld5 = new DirectionalLightPE(Vector3.Down, Color.White);
            float li = 0.5f;
            ld1.LightIntensity = li;
            ld2.LightIntensity = li;
            ld3.LightIntensity = li;
            ld4.LightIntensity = li;
            ld5.LightIntensity = li;
            this.World.AddLight(ld1);
            this.World.AddLight(ld2);
            this.World.AddLight(ld3);
            this.World.AddLight(ld4);
            this.World.AddLight(ld5);
            #endregion

            this.World.CameraManager.AddCamera(new CameraFirstPerson(false,GraphicInfo));

            this.RenderTechnic.AddPostEffect(new AntiAliasingPostEffectStalker());

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
            render.RenderTextComplete("Demo 15-22:Gui Deferred Sample", new Vector2(10, 15), Color.White, Matrix.Identity);
            if (okClicked)
                render.RenderTextComplete("Ok Clicked", new Vector2(10, 35), Color.White, Matrix.Identity);
        }

    }
}
