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
using PloobsEngine.Particles;
using DPSF.ParticleSystems;

namespace AdvancedDemo4._0
{
    public class ExplosionScreen : IScene
    {
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller(),new DPSFParticleManager());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
            desc.DefferedDebug = false;
            desc.BackGroundColor = Color.Black;
            desc.UseFloatingBufferForLightMap = true;
            renderTech = new DeferredRenderTechnic(desc);
        }   


        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo,factory, contentManager);

            ExplosionParticleSystem exp = new ExplosionParticleSystem();
            DPFSParticleSystem ps = new DPFSParticleSystem("exp", exp);
            this.World.ParticleManager.AddAndInitializeParticleSystem(ps);
            
            //SimpleModel simpleModel = new SimpleModel(factory, "Model//cenario");
            //TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
            //DeferredNormalShader shader = new DeferredNormalShader();
            //DeferredMaterial fmaterial = new DeferredMaterial(shader);
            //IObject obj = new IObject(fmaterial, simpleModel, tmesh);
            //this.World.AddObject(obj);            

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

            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));

            //this.RenderTechnic.AddPostEffect(new AntiAliasingPostEffectTabula());
            this.RenderTechnic.AddPostEffect(new AntiAliasingPostEffectStalker());

        }
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                ((this.World.ParticleManager.GetParticleSystem("exp") as DPFSParticleSystem).IDPSFParticleSystem as ExplosionParticleSystem).Explode();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);
            render.RenderTextComplete("Demo : Explosion with particles", new Vector2(20, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("Press Space", new Vector2(20, 35), Color.White, Matrix.Identity);
        }

    }
}
