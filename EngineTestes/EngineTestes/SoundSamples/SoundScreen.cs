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

namespace EngineTestes
{
    public class SoundScreen : IScene
    {
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
            desc.DefferedDebug = false;
            desc.UseFloatingBufferForLightMap = false;            
            renderTech = new DeferredRenderTechnic(desc);
        }
    
        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);

            SkyBox skybox = new SkyBox();
            engine.AddComponent(skybox);

            SoundMasterOptionDescription sod = engine.GetSoundMasterOptionDescription();
            sod.MasterVolume = 0.9f;
            sod.DistanceScale = 200;
            engine.SetSoundMasterOptionDescription(ref sod);
        }

        //SoundAudioPlayer ap;
        //SimpleSoundEffect se;
        Static3DSound sound;
        IContentManager contentManager;

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo,factory, contentManager);
            this.contentManager = contentManager;
    
            SimpleModel simpleModel = new SimpleModel(factory, "Model//cenario");
            TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
            DeferredNormalShader shader = new DeferredNormalShader();
            DeferredMaterial fmaterial = new DeferredMaterial(shader);
            IObject obj = new IObject(fmaterial, simpleModel, tmesh);
            this.World.AddObject(obj);            
            
            //ap = new SoundAudioPlayer(contentManager);
            //ap.AddSoundToRepository("Songs/bye", "bye");

            //se = new SimpleSoundEffect(contentManager, "Songs/alarm");

            //LocalMediaAudioPlayer lm = new LocalMediaAudioPlayer();
            //AlbumCollection ac = lm.MediaLibrary.Albums;
            //lm.PlayAlbum(ac[0]);
            
            sound = new Static3DSound(contentManager, "Songs/pianosong", Vector3.Zero);
            this.World.AddSoundEmitter(sound, true);

            //ObjectFollower3DSound sound2 = new ObjectFollower3DSound(contentManager, "Songs/pianosong", obj);

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

            this.World.CameraManager.AddCamera(new CameraFirstPerson(true,GraphicInfo.Viewport));

            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//cubemap");
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);

        }

        protected override void Update(GameTime gameTime)
        {
            //if (Keyboard.GetState().IsKeyDown(Keys.Space))
            //{
            //    se.Play();
            //}

            //if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            //{
            //    se.Pause();
            //}

            //if (Keyboard.GetState().IsKeyDown(Keys.RightAlt))
            //{
            //    ap.PlaySoundEffect("bye");
            //}
            //if (Keyboard.GetState().IsKeyDown(Keys.RightShift))
            //{
            //    ap.StopSoundEffect("bye");
            //}

            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
            {
                this.World.RemoveSoundEmitter(sound);
                sound = new Static3DSound(contentManager, "Songs/pianosong", Vector3.Zero);
                this.World.AddSoundEmitter(sound, true);
                
                sound.Play();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.RightControl))
            {
                sound.Stop(true);
            }
            

            base.Update(gameTime);
        }

    }
}
