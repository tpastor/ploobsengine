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

namespace EngineTestes
{
    public class DeferredScreen : IScene
    {
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic[] renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
            desc.DefferedDebug = false;
            desc.UseFloatingBufferForLightMap = true;
            renderTech = new DeferredRenderTechnic[] { new DeferredRenderTechnic(desc) };
        }
        OceanShader waterShader;

        protected override void Update(GameTime gameTime)
        {
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

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo,factory, contentManager);

            ///Create the water
            ///Water is just a Shader that Creats a Water like texture and binds it to a model
            //{
            //    IModelo sm = new SimpleModel(factory, "Model\\block");
            //    Matrix trans = Matrix.CreateTranslation(new Vector3(100, 50, 0));
            //    Plane plano = Plane.Transform(new Plane(0, 1, 0, 0), trans);
            //    IPhysicObject pi = new BoxObject(Vector3.Zero, 1, 1, 1, 5, new Vector3(1300, 0.1f, 1300), trans, MaterialDescription.DefaultBepuMaterial());
            //    pi.isMotionLess = true;
            //    ///Water shader, will refract and reflect according to the plano passed in the parameter
            //    ///Using default Parameters, there are lots of things that can be changed. See WaterCompleteShader 
            //    WaterCompleteShader shader = new WaterCompleteShader(800, 600, plano, 0);
            //    shader.SpecularIntensity = 0.01f;
            //    shader.SpecularPower = 50;
            //    IMaterial mat = new DeferredMaterial(shader);
            //    IObject obj4 = new IObject(mat, sm, pi);
            //    this.World.AddObject(obj4);
            //}

            
            //SimpleModel simpleModel = new SimpleModel(factory, "Model//cenario");
            //TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
            //NormalDeferred shader = new NormalDeferred();
            //DeferredMaterial fmaterial = new DeferredMaterial(shader);
            //IObject obj = new IObject(fmaterial, simpleModel, tmesh);
            //this.World.AddObject(obj);

            //OceanModel wm = new OceanModel(factory, "WaterModel", Vector3.Zero, 256, 256);
            //GhostObject go = new GhostObject(Vector3.Zero, Matrix.Identity, Vector3.One * 2);
            //waterShader = new OceanShader("Textures/cubemap");
            //waterShader.SetDefault();
            //DeferredMaterial wmaterial = new DeferredMaterial(waterShader);
            //IObject obj = new IObject(wmaterial, wm, go);
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

            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo.Viewport));

            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//cubemap");
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);

        }

    }
}
