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
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Input;
using PloobsEngine.Modelo.Animation;

namespace EngineTestes
{
    public class TechTruthScreen : IScene
    {
        private RotatingCamera cam;

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            BepuPhysicWorld BepuPhysicWorld = new BepuPhysicWorld(-9.7f);
            SimpleCuller SimpleCuller = new SimpleCuller();
            world = new IWorld(BepuPhysicWorld, SimpleCuller);

            ForwardRenderTecnichDescription desc = ForwardRenderTecnichDescription.Default();
            desc.BackGroundColor = Color.CornflowerBlue;
            desc.UsePostEffect = true;
            renderTech = new ForwardRenderTecnich(desc);
        }

        bool illuminationstate = true;
        public void ManipulateIllumination()
        {
            if (illuminationstate)
            {                
                shader.BasicEffect.PreferPerPixelLighting = true;                

                shader.BasicEffect.LightingEnabled = true;
                shader.BasicEffect.AmbientLightColor = new Vector3(0.1f, 0.1f, 0.1f);

                shader.BasicEffect.SpecularColor = new Vector3(1, 1, 1);
                shader.BasicEffect.SpecularPower = 24;

                // Set direction of light here, not position!
                shader.BasicEffect.DirectionalLight0.Direction = new Vector3(-1, -1, -1);
                shader.BasicEffect.DirectionalLight0.DiffuseColor = new Vector3(0.5f, 0.2f, 0.4f);
                shader.BasicEffect.DirectionalLight0.SpecularColor = new Vector3(1, 0, 0);
                shader.BasicEffect.DirectionalLight0.Enabled = true;

                // Set direction of light here, not position!
                shader.BasicEffect.DirectionalLight1.Direction = new Vector3(1, 1, 1);
                shader.BasicEffect.DirectionalLight1.DiffuseColor = new Vector3(0.6f, 0.3f, 0.2f);
                shader.BasicEffect.DirectionalLight1.SpecularColor = new Vector3(0, 1, 0);
                shader.BasicEffect.DirectionalLight1.Enabled = true;

                // Set direction of light here, not position!
                shader.BasicEffect.DirectionalLight2.Direction = new Vector3(-1, -1, 0);
                shader.BasicEffect.DirectionalLight2.DiffuseColor = new Vector3(0.4f, 0.3f, 0.6f);
                shader.BasicEffect.DirectionalLight2.SpecularColor = new Vector3(0, 0, 1);
                shader.BasicEffect.DirectionalLight2.Enabled = true;

            }
            else
            {
                shader.BasicEffect.EnableDefaultLighting();
            }
            illuminationstate = !illuminationstate;
        }

        public void ChangeFogProperties(float value)
        {
            shader.BasicEffect.FogEnd = value;
            shader2.EnvironmentMapEffect.FogEnd = value; 
        }

        ForwardXNABasicShader shader;
        ForwardEnvironmentShader shader2;
        RasterizerState RasterizerState;
        BloomPostEffect BloomPostEffect;
        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory, IContentManager contentManager)
        {
            RasterizerState = new Microsoft.Xna.Framework.Graphics.RasterizerState();
            RasterizerState.FillMode = FillMode.WireFrame;

            BloomPostEffect = new BloomPostEffect();
            BloomPostEffect.Enabled = false;
            this.RenderTechnic.AddPostEffect(BloomPostEffect);

        
            base.LoadContent(GraphicInfo, factory, contentManager);

            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//cena");
                TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());

                ForwardXNABasicShaderDescription ForwardXNABasicShaderDescription = ForwardXNABasicShaderDescription.Default();
                ForwardXNABasicShaderDescription.EnableTexture = true;
                ForwardXNABasicShaderDescription.EnableLightning = true;
                ForwardXNABasicShaderDescription.DefaultLightning = true;

                shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription);                                
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                
                shader.Initialize(GraphicInfo, factory, obj);                
                this.World.AddObject(obj);                      
            }


            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//cilos");
                ///tem mais de um cilo neste mesh, tem q setar as texturas de todo mundo ....
                Texture2D tex = factory.CreateTexture2DColor(1, 1, Color.White);
                for (int i = 0; i < simpleModel.MeshNumber; i++)
                {
                    simpleModel.SetTexture(tex, TextureType.DIFFUSE,i,0);    
                }
                
                TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());                
                shader2 = new ForwardEnvironmentShader(factory.GetTextureCube("Textures//cubeMap"),1,false);
                ForwardMaterial fmaterial = new ForwardMaterial(shader2);

                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);
            }

            shader.BasicEffect.FogEnabled = true;
            shader.BasicEffect.FogColor = new Vector3(0.1f, 0.1f, 0.1f); // Dark grey
            shader.BasicEffect.FogStart = 30;
            shader.BasicEffect.FogEnd = 1000;

            shader2.EnvironmentMapEffect.FogEnabled = true;
            shader2.EnvironmentMapEffect.FogColor = new Vector3(0.1f, 0.1f, 0.1f); // Dark grey
            shader2.EnvironmentMapEffect.FogStart = 30;
            shader2.EnvironmentMapEffect.FogEnd = 1000; 

            cam = new RotatingCamera(this);
            this.World.CameraManager.AddCamera(cam);
        }
                
        private bool wireframe = false;

        public bool Wireframe
        {
            get
            {
                return wireframe;
            }
            set
            {
                wireframe = value;
            }

        }

        public bool BloomEnable
        {
            get
            {
                return BloomPostEffect.Enabled;
            }
            set
            {
                BloomPostEffect.Enabled = value;
            }

        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {           
                if (wireframe)
                    render.PushRasterizerState(RasterizerState);
                base.Draw(gameTime, render);
                if (wireframe)
                    render.PopRasterizerState();

            render.RenderTextComplete("PloobsEngine on Windows Phone7", new Vector2(20, 10), Color.Red, Matrix.Identity);
        }

    }
}
