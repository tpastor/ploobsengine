using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine;
using PloobsEngine.Cameras;
using PloobsEngine.Input;
using PloobsEngine.Light;
using PloobsEngine.Material;
using PloobsEngine.Modelo;
using PloobsEngine.Physics;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.SceneControl;
using PloobsEngine.Utils;
using PloobsEngine.Features;
using PloobsEngine.Engine;

namespace IntroductionDemo4._0
{
    /// <summary>
    /// Stress Test Screen
    ///THROW THE BALLS IN THE BLOCKS !!!!!!
    /// </summary>
    public class StressBepuScreen : IScene
    {        
        ICamera cam;
        LightThrowBepu lt;
        float drawfps;
        float combinedfps;

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(0, true), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
            desc.UseFloatingBufferForLightMap = true;
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new DeferredRenderTechnic(desc);
        }

        protected override void InitScreen(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);
                        
            engine.AddComponent(new FPSCounter());

            ///IF the component already exist, it wont be added, the safest way is to get if from here
            FPSCounter fpsCounter = engine.GetComponent<FPSCounter>("FPSCounter");

            fpsCounter.CombinedFps += new FpsEvent(fpsCounter_CombinedFps);
            fpsCounter.DrawFps += new FpsEvent(fpsCounter_DrawFps);
        }

        void fpsCounter_DrawFps(float fps)
        {
            this.drawfps = fps;
        }

        void fpsCounter_CombinedFps(float fps)
        {
            this.combinedfps = fps;
        }

        protected override void LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory, IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);            

            #region Models

            ///Some Physic World Parameters
            ///No accuracy (speed up the simulation) and no gravity
            BepuPhysicWorld physicWorld = this.World.PhysicWorld as BepuPhysicWorld;
            System.Diagnostics.Debug.Assert(physicWorld != null);
            physicWorld.Space.Solver.IterationLimit = 1; //Essentially no sustained contacts, so don't need to worry about accuracy.            

            int numColumns = 10;
            int numRows = 10;
            int numHigh = 15;
         
            ///1500 box
            ///1500 luzes

            ///CREATE LOOOOTS OF OBJECTS AND LIGHTS
            float separation = 3;
            for (int i = 0; i < numRows; i++)
                for (int j = 0; j < numColumns; j++)
                    for (int k = 0; k < numHigh; k++)
                    {
                        SimpleModel sm = new SimpleModel(factory,"..\\Content\\Model\\cubo");
                        sm.SetTexture(factory.CreateTexture2DColor(1,1, StaticRandom.RandomColor()), TextureType.DIFFUSE);
                        MaterialDescription md = MaterialDescription.DefaultBepuMaterial();
                        md.Bounciness = 1;
                        BoxObject pi = new BoxObject(new Vector3(separation * i, k * separation, separation * j), 1,1,1, 1,new Vector3(1),Matrix.Identity,md);                                                
                        pi.Entity.AngularDamping = 0f; //It looks cooler if boxes don't slowly stop spinning!                                               
                        IShader shader = new DeferredNormalShader();
                        IMaterial mat = new DeferredMaterial(shader);
                        IObject obj5 = new IObject(mat, sm, pi);
                        this.World.AddObject(obj5);
                        
                        ///Light is atached to the blocks
                        MoveablePointLight pl = new MoveablePointLight(pi, StaticRandom.RandomColor(), 5, 1);
                        this.World.AddLight(pl);
                        
                    }

            #endregion            

            cam = new CameraFirstPerson(GraphicInfo);            
            cam.FarPlane = 3000;
            this.World.CameraManager.AddCamera(cam);


            ///THROW THE BALLS IN THE BLOCKS !!!!!!
            lt = new LightThrowBepu(this.World, factory);         

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

        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);
            render.RenderTextComplete("FPS Draw " + drawfps, new Vector2(40, 20), Color.White, Matrix.Identity);
            render.RenderTextComplete("FPS Combined " + combinedfps, new Vector2(40, 40), Color.White, Matrix.Identity);

            render.RenderTextComplete("Demo: Stress Test (BEPU)", new Vector2(GraphicInfo.Viewport.Width - 315, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("Left click to launch balls", new Vector2(GraphicInfo.Viewport.Width - 315, 40), Color.White,Matrix.Identity);            
        }


        protected override void CleanUp(EngineStuff engine)
        {
            ///if you wont want to remove the component itself, you should at least remove the events
            engine.RemoveComponent("FPSCounter");
            
            lt.CleanUp();
        }
    }
}

