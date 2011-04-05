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

namespace EngineTestes
{
    public class DeferredScreen : IScene
    {
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic[] renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
            desc.DefferedDebug = true;
            desc.UseFloatingBufferForLightMap = false;
            renderTech = new DeferredRenderTechnic[] { new DeferredRenderTechnic(desc) };
        }

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo,factory, contentManager);            

            SimpleModel simpleModel = new SimpleModel(contentManager, "Model//cenario");
            TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One,MaterialDescription.DefaultBepuMaterial());
            NormalDeferred shader = new NormalDeferred();
            DeferredMaterial fmaterial = new DeferredMaterial(shader);            
            IObject obj = new IObject(fmaterial,simpleModel,tmesh);
            this.World.AddObject(obj);

            #region NormalLight
            DirectionalLight ld1 = new DirectionalLight(Vector3.Left, Color.White);
            DirectionalLight ld2 = new DirectionalLight(Vector3.Right, Color.White);
            DirectionalLight ld3 = new DirectionalLight(Vector3.Backward, Color.White);
            DirectionalLight ld4 = new DirectionalLight(Vector3.Forward, Color.White);
            DirectionalLight ld5 = new DirectionalLight(Vector3.Down, Color.White);
            float li = 0.4f;
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

            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo.Viewport));                              
        }

    }
}
