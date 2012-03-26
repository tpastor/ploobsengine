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
using PloobsEngine.Loader;

namespace AdvancedDemo4._0
{
    /// <summary>
    /// SoundScreen
    /// Mess of Sounds =P    
    /// </summary>
    [PloobsEngine.TestSuite.TesteVisualScreen]
    public class SoundScreen : IScene
    {
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

            ///Controls some master option of the engine sound player
            SoundMasterOptionDescription sod = engine.GetSoundMasterOptionDescription();
            ///between 0 and 1
            sod.MasterVolume = 0.9f;
            ///varies according to your game scale
            ///Used in 3D sounds
            sod.DistanceScale = 200;
            engine.SetSoundMasterOptionDescription(ref sod);
        }

        SoundAudioPlayer ap;
        SimpleSoundEffect se;
        Static3DSound sound;
        LocalMediaAudioPlayer lm;

        /// <summary>
        /// Load content for the screen.
        /// </summary>
        /// <param name="GraphicInfo"></param>
        /// <param name="factory"></param>
        /// <param name="contentManager"></param>
        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo,factory, contentManager);

            {
                ///Create the xml file model extractor
                ///Loads a XML file that was export by our 3DS MAX plugin
                ExtractXmlModelLoader ext = new ExtractXmlModelLoader("Content//ModelInfos//", "Model//", "Textures//");
                this.AttachCleanUpAble(ext);
                ///Extract all the XML info (Model,Cameras, ...)
                ModelLoaderData data = ext.Load(factory, GraphicInfo, "ilha");
                ///Create the WOrld Loader
                ///Convert the ModelLoaderData in World Entities
                WorldLoader wl = new WorldLoader(); ///all default                
                wl.LoadWorld(factory, GraphicInfo, World, data);
            }


            ///Create and add a sound to the SoundAudioPlayer
            ap = new SoundAudioPlayer(factory);
            ap.AddSoundToRepository("Songs/bye", "bye");

            ///Create a sound effect without the SoundAudioPlayer (internaly the SoundAudioPlayer is a container of SimpleSoundEffect -- and some stuffs more)
            se = new SimpleSoundEffect(factory, "Songs/alarm");

            ///Load the Sounds that you hear in your Microsoft Media Player
            ///Just loading the first album found =P
            lm = new LocalMediaAudioPlayer();
            AlbumCollection ac = lm.MediaLibrary.Albums;
            lm.PlayAlbum(ac[0]);
            
            ///Creating a static 3D sound in the 0,0,0 (Bellow the island tree, go there and hear the sound getting louder)
            sound = new Static3DSound(factory, "Songs/pianosong", Vector3.Zero);            
            this.World.AddSoundEmitter(sound, true);

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

            CameraFirstPerson cam = new CameraFirstPerson(MathHelper.ToRadians(30), MathHelper.ToRadians(-10), new Vector3(200, 150, 250), GraphicInfo);
            this.World.CameraManager.AddCamera(cam);

            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//grassCube");
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);

        }

        protected override void Update(GameTime gameTime)
        {
            ///WE COULD USE THE INPUTADVANCED, but we did not.
            ///Sometimes the state of a key remains down more than one frame
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                se.Play();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                se.Pause();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.RightAlt))
            {
                ap.PlaySoundEffect("bye");
            }
            if (Keyboard.GetState().IsKeyDown(Keys.RightShift))
            {
                ap.StopSoundEffect("bye");
            }
            if (Keyboard.GetState().IsKeyDown(Keys.RightControl))
            {
                sound.Stop(true);
            }           

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);

            render.RenderTextComplete("Demo 6-22: Sound Mess, lots of sounds together ", new Vector2(10, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("Space/Enter to Play/Pause the Sound Effect: " + se.State, new Vector2(10, 35), Color.White, Matrix.Identity);
            render.RenderTextComplete("RightAlt/RightShif to Play/Stop the AudioPlayer Bye effect: " + ap.SoundState("bye"), new Vector2(10, 55), Color.White, Matrix.Identity);
            render.RenderTextComplete("RightControl to Stop the 3D sound (Source is bellow the tree, go near there):" + sound.State, new Vector2(10, 75), Color.White, Matrix.Identity);
        }

        protected override void CleanUp(EngineStuff engine)
        {
            se.Stop();            
            ap.StopSoundEffect("bye");
            ap.RemoveAllSounds();
            sound.Stop(true);
            lm.StopCurrentMusic();            

            engine.RemoveComponent("SkyBox");
            base.CleanUp(engine);
        }

    }
}
