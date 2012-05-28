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
using PloobsEngine.Physic.PhysicObjects.BepuObject;
using BEPUphysics.UpdateableSystems.ForceFields;
using EngineTestes.Post;

namespace EngineTestes
{
    public class PostEffectScreen : IScene
    {
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();            
            desc.UseFloatingBufferForLightMap = true;
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
            base.LoadContent(GraphicInfo,factory, contentManager);
            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//cenario");
                TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
                DeferredNormalShader shader = new DeferredNormalShader();
                DeferredMaterial fmaterial = new DeferredMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);
            }

            {
                SimpleModel sm = new SimpleModel(factory, "..\\Content\\Model\\block");
                sm.SetTexture(factory.CreateTexture2DColor(1, 1, Color.White), TextureType.DIFFUSE);
                BoxObject pi = new BoxObject(new Vector3(100, 40, 0), 1, 1, 1, 25, new Vector3(100, 10, 100), Matrix.Identity, MaterialDescription.DefaultBepuMaterial());
                DeferredNormalShader shader = new DeferredNormalShader();
                IMaterial mat = new DeferredMaterial(shader);
                IObject obj3 = new IObject(mat, sm, pi);
                this.World.AddObject(obj3);
            }

            

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

            //HSVEffect hsv = new HSVEffect(new Vector4(), new Vector4(1.5f,1,1,1));
            //this.RenderTechnic.AddPostEffect(hsv);

            //BackgroundTexture bt = new BackgroundTexture("Textures//Bubble");
            //this.RenderTechnic.AddPostEffect(bt);

            //SharpenPostEffect sharpen = new SharpenPostEffect();
            //this.RenderTechnic.AddPostEffect(sharpen);

            //DreamPostEffect dp = new DreamPostEffect();
            //this.RenderTechnic.AddPostEffect(dp);

            //NightPostEffect np = new NightPostEffect();
            //this.RenderTechnic.AddPostEffect(np);

            //HeatPostEffect h = new HeatPostEffect();
            //this.RenderTechnic.AddPostEffect(h);

            //FogPostEffect fog = new FogPostEffect(1, 1000);
            //fog.FogColor = Color.Red;
            //this.RenderTechnic.AddPostEffect(fog);

            //BlackWhitePostEffect b = new BlackWhitePostEffect();
            //this.RenderTechnic.AddPostEffect(b);

            //AntiAliasingPostEffect aa = new AntiAliasingPostEffect();
            //this.RenderTechnic.AddPostEffect(aa);

            //DepthCueingPostEffect dc = new DepthCueingPostEffect();
            //this.RenderTechnic.AddPostEffect(dc);

            //GaussianBlurPostEffect gb = new GaussianBlurPostEffect();
            //this.RenderTechnic.AddPostEffect(gb);

            //RadialBlurPostEffect rb = new RadialBlurPostEffect();
            //this.RenderTechnic.AddPostEffect(rb);

            //BloomPostEffect bloomPostEffect = new BloomPostEffect();
            //this.RenderTechnic.AddPostEffect(bloomPostEffect);

            //DephtOfFieldPostEffect dof = new DephtOfFieldPostEffect();
            //this.RenderTechnic.AddPostEffect(dof);

            //HdrPostEffect hdr = new HdrPostEffect();
            //this.RenderTechnic.AddPostEffect(hdr);

            SSAOPostEffect ssao = new SSAOPostEffect();
            //ssao.OutputONLYSSAOMAP = true;
            ssao.WhiteCorrection = 0.7f;
            ssao.Intensity = 5;
            ssao.Diffscale = 0.5f;
            ssao.BlurMode = BlurMode.NONE;
            this.RenderTechnic.AddPostEffect(ssao);

            //SunPostEffect sun = new SunPostEffect();            
            //this.RenderTechnic.AddPostEffect(sun);

            //AdvancedTone PostEffectTest = new AdvancedTone();
            //this.RenderTechnic.AddPostEffect(PostEffectTest);

            //GlowPostEffect glow = new GlowPostEffect();
            //this.RenderTechnic.AddPostEffect(glow);

            //UnderWater nt = new UnderWater();
            //this.RenderTechnic.AddPostEffect(nt);
            
            CameraFirstPerson cam = new CameraFirstPerson(GraphicInfo);
            cam.MoveSpeed *= 5;
            this.World.CameraManager.AddCamera(cam);

            LightThrowBepu lt = new LightThrowBepu(this.World, factory);

            //SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//grasscube");
            //CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);

        }       

    }
}
