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

namespace EngineTestes
{
    public class FirstScreen : IScene
    {

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic[] renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

            ForwardRenderTecnichDescription desc = new ForwardRenderTecnichDescription(Color.CornflowerBlue);            
            renderTech = new IRenderTechnic[] { new ForwardRenderTecnich(desc) };
        }

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo,factory, contentManager);            

            SimpleModel simpleModel = new SimpleModel(contentManager, "Model//cenario");
            TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One,MaterialDescription.DefaultBepuMaterial());            
            XNABasicShader shader = new XNABasicShader(XNABasicShaderDescription.Default());            
            ForwardMaterial fmaterial = new ForwardMaterial(shader);            
            IObject obj = new IObject(fmaterial,simpleModel,tmesh);
            this.World.AddObject(obj); 

            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo.Viewport));                              
        }

    }
}
