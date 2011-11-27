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
using PloobsEngine.Loader;
using PloobsEngine.Input;
using PloobsEngine.Utils;

namespace EngineTestes
{
    public class SponzaScreen : IScene
    {

        Vec3Interpolator inter = new Vec3Interpolator();
        DirectionalLightPE ld1;

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
            renderTech = new DeferredRenderTechnic(desc);
        }
     

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo,factory, contentManager);

            ExtractXmlModelLoader ext = new ExtractXmlModelLoader("Content//ModelInfos//", "Model//", "Textures//");
            ModelLoaderData data = ext.Load(factory, GraphicInfo, "sponza");
            WorldLoader wl = new WorldLoader();
            wl.LoadWorld(factory, GraphicInfo, World, data);            

            LightThrowBepu lt = new LightThrowBepu(this.World, factory);

            #region NormalLight
            DirectionalLightPE ld1 = new DirectionalLightPE(Vector3.Left, Color.White);
            DirectionalLightPE ld2 = new DirectionalLightPE(Vector3.Right, Color.White);
            DirectionalLightPE ld3 = new DirectionalLightPE(Vector3.Backward, Color.White);
            DirectionalLightPE ld4 = new DirectionalLightPE(Vector3.Forward, Color.White);
            DirectionalLightPE ld5 = new DirectionalLightPE(Vector3.Down, Color.White);
            float li = 0.1f;
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

            //ld1 = new DirectionalLightPE(new Vector3(0.2f, -1, 0.2f), Color.White);
            //ld1.CastShadown = true;
            //float li = 0.9f;
            //ld1.LightIntensity = li;
            //this.World.AddLight(ld1);
            
            #endregion

            CameraFirstPerson cam = new CameraFirstPerson(GraphicInfo.Viewport);
            cam.MoveSpeed *= 5;
            this.World.CameraManager.AddCamera(cam);
            
            /////Interpolator to change lightDirection
            //inter.Start(new Vector3(0, -1, 0), new Vector3(1, -1, 1), 3, true);

            this.RenderTechnic.AddPostEffect(new AntiAliasingPostEffect());
            this.RenderTechnic.AddPostEffect(new BloomPostEffect());

        }


    }
}
