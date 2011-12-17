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

namespace EngineTestes
{
    public class MobilePhysicScreen : IScene
    {

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new DeferredRenderTechnic(desc);
        }

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
                MobileMeshObject tmesh = new MobileMeshObject(simpleModel, new Vector3(100, 100, 10), Matrix.Identity, Vector3.One * 50, MaterialDescription.DefaultBepuMaterial(),BEPUphysics.CollisionShapes.MobileMeshSolidity.DoubleSided,10);
                //TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, new Vector3(100, 100, 10), Matrix.Identity, Vector3.One * 50, MaterialDescription.DefaultBepuMaterial());
                ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj); 
            }

            new LightThrowBepu(this.World, factory);
            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));            
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {        
            base.Draw(gameTime, render);         
        }

    }
}
