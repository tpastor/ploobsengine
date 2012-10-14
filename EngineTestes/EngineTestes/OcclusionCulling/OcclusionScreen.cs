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
using EngineTestes.ScreenTests;
using PloobsEngine.TestSuite;

namespace EngineTestes
{
    [TesteVisualScreen]
    public class OcclusionScreen : IScene
    {

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

            ForwardRenderTecnichDescription desc = ForwardRenderTecnichDescription.Default();
            desc.BackGroundColor = Color.CornflowerBlue;
            desc.UsePostDrawPhase = true;
            renderTech = new ForwardRenderTecnich(desc);
        }

        ForwardXNABasicShader shader1;
        ForwardXNABasicShader shader2 ;
        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//cenario");
                TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero);
                ForwardMaterial fmaterial = ForwardMaterial.DefaultForwardMaterial();
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                obj.Name = "scene";
                this.World.AddObject(obj);
            }
            
            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//uzi");
                TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, new Vector3(100,10,10), Matrix.Identity, Vector3.One * 10);
                shader1 = new ForwardXNABasicShader();
                shader1.UseOcclusionCulling = true;
                ForwardMaterial fmaterial = new ForwardMaterial(shader1);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                obj.Name = "uzzi";
                this.World.AddObject(obj); 
            }

            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//block");
                simpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Green), TextureType.DIFFUSE);
                TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, new Vector3(100, 20, 200), Matrix.Identity, new Vector3(300, 300, 5));
                shader2 = new ForwardXNABasicShader();
                shader2.UseOcclusionCulling = true;
                ForwardMaterial fmaterial = new ForwardMaterial(shader2);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                obj.Name = "Block";
                this.World.AddObject(obj);
            }


            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//block");
                simpleModel.SetTexture(Color.Red);
                TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, new Vector3(150, 20, 250), Matrix.Identity, new Vector3(300, 300, 5));
                ForwardXNABasicShader sh = new ForwardXNABasicShader();
                sh.UseOcclusionCulling = false;
                ForwardMaterial fmaterial = new ForwardMaterial(sh);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                obj.Name = "Block";
                this.World.AddObject(obj);
            }
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    ///Cria modelo com textura procedural amarela
                    SimpleModel sm = new SimpleModel(factory, "Model\\block");
                    sm.SetTexture(Color.Yellow);
                    GhostObject pi = new GhostObject(new Vector3(i * 10, 50, j * 10), Matrix.Identity, new Vector3(5));                    
                    ForwardTransparenteShader s = new ForwardTransparenteShader(0.3f);                    
                    ForwardMaterial mat = new ForwardMaterial(s);
                    IObject obj4 = new IObject(mat, sm, pi);
                    this.World.AddObject(obj4);
                }

            }
            

            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);         

            render.RenderTextComplete("Uzzi Pixels Rendered: " + shader1.PixelsRendered, new Vector2(10, 20), Color.White);
            render.RenderTextComplete("Block Pixels Rendered: " + shader2.PixelsRendered, new Vector2(10, 40), Color.White);
            
        }

    }
}
