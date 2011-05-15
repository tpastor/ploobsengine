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
using PloobsEngine.Audio;
using Microsoft.Xna.Framework.Media;
using PloobsEngine.Input;

namespace AdvancedDemo4._0
{
    /// <summary>
    /// SoundScreen, Second Sample About 3D Follower Object
    /// </summary>
    public class FollowerObjectSoundScreen : IScene
    {
        LightThrowBepu lt;
        ObjectFollower3DSound sound2;

        /// <summary>
        /// Sets the world and render technich.
        /// </summary>
        /// <param name="renderTech">The render tech.</param>
        /// <param name="world">The world.</param>
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(-0.97f,true), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();            
            ///lights wont saturate, try to add lots of light in the same place, it wont be all white 
            ///This option save processing time, but make thinks a little ugly (if you will use lots of lights)
            ///Also when turn of, will wont be able to use hdr post effect correctely
            desc.UseFloatingBufferForLightMap = false;            
            renderTech = new DeferredRenderTechnic(desc);
        }

        protected override void CleanUp(EngineStuff engine)
        {
            sound2.Stop(true);
            engine.RemoveComponent("SkyBox");
            lt.CleanUp();
            base.CleanUp(engine);
        }

        /// <summary>
        /// Init Screen
        /// </summary>
        /// <param name="GraphicInfo">The graphic info.</param>
        /// <param name="engine"></param>
        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);

            SkyBox skybox = new SkyBox();
            engine.AddComponent(skybox);

            SoundMasterOptionDescription sod = engine.GetSoundMasterOptionDescription();
            sod.MasterVolume = 0.9f;
            sod.DistanceScale = 50;
            engine.SetSoundMasterOptionDescription(ref sod);
        }
        
        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//cenario");
                TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
                DeferredNormalShader shader = new DeferredNormalShader();
                DeferredMaterial fmaterial = new DeferredMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);
            }

            {
                SimpleModel sm2 = new SimpleModel(factory, "Model\\ball");
                sm2.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Red, false), TextureType.DIFFUSE);
                DeferredNormalShader nd = new DeferredNormalShader();
                IMaterial m = new DeferredMaterial(nd);
                SphereObject pi2 = new SphereObject(new Vector3(100,50,0), 1, 10, 10, MaterialDescription.DefaultBepuMaterial());
                IObject o = new IObject(m, sm2, pi2);
                this.World.AddObject(o);

                sound2 = new ObjectFollower3DSound(contentManager, "Songs/pianosong", o);                
                this.World.AddSoundEmitter(sound2,true);                
            }

            lt = new LightThrowBepu(this.World, factory);

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


            CameraFirstPerson cam = new CameraFirstPerson(MathHelper.ToRadians(30), MathHelper.ToRadians(-10), new Vector3(200, 150, 250), GraphicInfo.Viewport);
            this.World.CameraManager.AddCamera(cam);

            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//grassCube");
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);

        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);

            render.RenderTextComplete("Demo 7-22: 3D Sound Attached to an Object ", new Vector2(10, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("The Red Ball emits Sound =P, go near it and far from it", new Vector2(10, 35), Color.White, Matrix.Identity);
            render.RenderTextComplete("Throw a ball at it", new Vector2(10, 55), Color.White, Matrix.Identity);            
        }

    }
}
