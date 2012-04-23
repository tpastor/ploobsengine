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
            renderTech = new ForwardRenderTecnich(desc);
        }

        ForwardXNABasicShader shader1;
        ForwardXNABasicShader shader2 ;
        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//cenario");
                TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
                ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());                
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);
            }
            
            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//uzi");
                TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, new Vector3(100,10,10), Matrix.Identity, Vector3.One * 10, MaterialDescription.DefaultBepuMaterial());
                shader1 = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                shader1.UseOcclusionCulling = true;
                ForwardMaterial fmaterial = new ForwardMaterial(shader1);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj); 
            }

            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//block");
                TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, new Vector3(100, 20, 200), Matrix.Identity, new Vector3(5,300,300), MaterialDescription.DefaultBepuMaterial());
                shader2 = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                shader2.UseOcclusionCulling = true;
                ForwardMaterial fmaterial = new ForwardMaterial(shader2);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);
            }

            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            render.RenderTextComplete("Uzzi Pixels Rendered: " + shader1.PixelsRendered, new Vector2(10, 20), Color.White, Matrix.Identity);
            render.RenderTextComplete("Block Pixels Rendered: " + shader2.PixelsRendered, new Vector2(10, 40), Color.White, Matrix.Identity);

            base.Draw(gameTime, render);         
        }

    }
}
