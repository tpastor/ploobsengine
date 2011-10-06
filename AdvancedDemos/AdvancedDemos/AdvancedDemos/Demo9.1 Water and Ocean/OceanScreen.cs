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

namespace AdvancedDemo4._0
{
    public class OceanScreen : IScene
    {
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller(), null, true);

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();            
            desc.UseFloatingBufferForLightMap = true;
            renderTech = new DeferredRenderTechnic(desc);
        }
        DeferredWaterShader waterShader;

        protected override void Update(GameTime gameTime)
        {
            ///Could use the InputAdvanced, but we choose not this time
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                waterShader.WaterAmount = 0;
                waterShader.BumpHeight = .1f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                waterShader.WaterAmount = 1;

                waterShader.ShallowWaterColor = Color.DarkSeaGreen;
                waterShader.DeepWaterColor = Color.Brown;
                waterShader.ReflectionColor = Color.DarkGray;
                waterShader.BumpHeight = .1f;


            }

            if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                //shader2.ShallowWaterColor = Color.Gold;
                //shader2.DeepWaterColor = Color.Blue;
                //shader2.ReflectionColor = Color.White;                

                waterShader.BumpSpeed = new Vector2(0, .5f);
                waterShader.FresnelPower = 5;
                waterShader.WaterAmount = 2;
                waterShader.HDRMultiplier = 25;
                waterShader.BumpHeight = .7f;
                waterShader.WaveAmplitude = .1f;
                waterShader.WaterAmount = .1f;
                waterShader.WaveFrequency = .3f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D4))
            {
                waterShader.WaveFrequency = .5f;
                waterShader.WaveAmplitude = .3f;
                waterShader.BumpHeight = .1f;

            }

            if (Keyboard.GetState().IsKeyDown(Keys.D5))
            {
                waterShader.WaveFrequency = .1f;
                waterShader.WaveAmplitude = .1f;
                waterShader.BumpHeight = .1f;
                waterShader.BumpHeight = .1f;

            }

            if (Keyboard.GetState().IsKeyDown(Keys.D6))
            {
                waterShader.WaveFrequency = .1f;
                waterShader.WaveAmplitude = .5f;
                waterShader.BumpHeight = .1f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D7))
            {
                waterShader.WaveFrequency = .1f;
                waterShader.WaveAmplitude = 2f;
                waterShader.BumpHeight = 1f;

            }

            if (Keyboard.GetState().IsKeyDown(Keys.D8))
            {
                waterShader.WaveFrequency = 0;
                waterShader.WaveAmplitude = 0;
                waterShader.BumpHeight = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D9))
            {
                waterShader.WaveFrequency = 0;
                waterShader.WaveAmplitude = 0;
                waterShader.BumpHeight = 1;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.D0))
            {
                waterShader.SetDefault();

            }
            base.Update(gameTime);
        }

        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);

            SkyBox skybox = new SkyBox();
            engine.AddComponent(skybox);
        }

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory, IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            OceanModel wm = new OceanModel(factory, "WaterModel", Vector3.Zero, 256, 256);
            GhostObject go = new GhostObject(new Vector3(-300,0,-300), Matrix.Identity, Vector3.One * 2);
            waterShader = new DeferredWaterShader("Textures/cubemap");
            waterShader.SetDefault();
            DeferredMaterial wmaterial = new DeferredMaterial(waterShader);
            IObject obj = new IObject(wmaterial, wm, go);
            this.World.AddObject(obj); 

            #region NormalLight
            DirectionalLightPE ld1 = new DirectionalLightPE(Vector3.Left, Color.White);
            DirectionalLightPE ld2 = new DirectionalLightPE(Vector3.Right, Color.White);
            DirectionalLightPE ld3 = new DirectionalLightPE(Vector3.Backward, Color.White);
            DirectionalLightPE ld4 = new DirectionalLightPE(Vector3.Forward, Color.White);
            DirectionalLightPE ld5 = new DirectionalLightPE(Vector3.Down, Color.White);
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

            this.RenderTechnic.AddPostEffect(new AntiAliasingPostEffectStalker());


            CameraFirstPerson cam = new CameraFirstPerson(MathHelper.ToRadians(-120), MathHelper.ToRadians(-5), new Vector3(-70, 70, 90), GraphicInfo.Viewport);
            this.World.CameraManager.AddCamera(cam);

            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//cubemap");
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);

        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);
            render.RenderTextComplete("Demo 18-22:Procedural Ocean Sample", new Vector2(10, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("Press Keys 1 to 9 to change water parameters", new Vector2(10, 35), Color.White, Matrix.Identity);
            render.RenderTextComplete("Experiment changing some parameters on the code", new Vector2(10, 55), Color.White, Matrix.Identity);
        }

        protected override void CleanUp(EngineStuff engine)
        {
            engine.RemoveComponent("SkyBox");
            base.CleanUp(engine);
        }

    }
}
