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
    public class TechDemoScreen : IScene
    {
                
        DirectionalLightPE l;

        
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(-0.9f,true,1,true), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
            desc.DefferedDebug = false;
            desc.UseFloatingBufferForLightMap = true;
            ShadowLightMap css = new ShadowLightMap(ShadowFilter.PCF3x3,1024,DirectionalShadowFilteringType.PCF7x7,512,0.95f);                        
            desc.DeferredLightMap = css;            
            renderTech = new DeferredRenderTechnic(desc);
        }

        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);

            SkyBox skybox = new SkyBox();
            engine.AddComponent(skybox);

            InputAdvanced ia = new InputAdvanced();
            engine.AddComponent(ia);

        }        

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            ExtractXmlModelLoader ext = new ExtractXmlModelLoader("Content//ModelInfos//", "Model//", "Textures//");
            ModelLoaderData data = ext.Load(factory, GraphicInfo, "techdemo");
            WorldLoader wl = new WorldLoader();
            wl.OnCreateIObject += new CreateIObject(wl_OnCreateIObject);
            wl.OnCreateILight += new CreateILight(wl_OnCreateILight);
            wl.LoadWorld(factory, GraphicInfo, World, data);

            LightThrowBepu lt = new LightThrowBepu(this.World, factory);
            {
                l = new DirectionalLightPE(new Vector3(0.2f, -1, 0.2f), Color.White);
                l.CastShadown = true;
                float lii = 0.6f;
                l.LightIntensity = lii;
                this.World.AddLight(l);
            }

            #region NormalLight
            //DirectionalLightPE ld1 = new DirectionalLightPE(Vector3.Left, Color.White);
            //DirectionalLightPE ld2 = new DirectionalLightPE(Vector3.Right, Color.White);
            //DirectionalLightPE ld3 = new DirectionalLightPE(Vector3.Backward, Color.White);
            //DirectionalLightPE ld4 = new DirectionalLightPE(Vector3.Forward, Color.White);
            //DirectionalLightPE ld5 = new DirectionalLightPE(Vector3.Down, Color.White);
            //DirectionalLightPE ld6 = new DirectionalLightPE(Vector3.Up, Color.White);
            //float li = 0.5f;
            //ld1.LightIntensity = li;
            //ld2.LightIntensity = li;
            //ld3.LightIntensity = li;
            //ld4.LightIntensity = li;
            //ld5.LightIntensity = li;
            //ld6.LightIntensity = li;
            //this.World.AddLight(ld1);
            //this.World.AddLight(ld2);
            //this.World.AddLight(ld3);
            //this.World.AddLight(ld4);
            //this.World.AddLight(ld5);
            //this.World.AddLight(ld6);

            #endregion

            CameraFirstPerson cam = new CameraFirstPerson(GraphicInfo.Viewport);
            cam.FarPlane = 1000;
            cam.MoveSpeed *= 5;
            this.World.CameraManager.AddCamera(cam);

            this.RenderTechnic.AddPostEffect(new AntiAliasingPostEffect());

        }

        ILight wl_OnCreateILight(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ILight li)
        {
            return null;
        }        

        IObject[] wl_OnCreateIObject(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ObjectInformation[] oi)
        {
            foreach (var mi in oi)
            {
                if (!mi.HasTexture(TextureType.DIFFUSE))
                {
                    mi.textureInformation.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Red), TextureType.DIFFUSE);
                }

                if (mi.HasTexture(TextureType.BUMP))
                {
                    mi.textureInformation.SetNullTexture(TextureType.BUMP);
                }    
            }           

            
            return WorldLoader.CreateOBJ(world, factory, ginfo, oi);
        }


    }
}
