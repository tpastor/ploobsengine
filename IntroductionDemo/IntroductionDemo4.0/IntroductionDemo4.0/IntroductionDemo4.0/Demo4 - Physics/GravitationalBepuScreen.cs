using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using PloobsEngine.Physics;
using PloobsEngine;
using PloobsEngine.Cameras;
using PloobsEngine.Input;
using Microsoft.Xna.Framework;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.Modelo;
using PloobsEngine.Material;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Utils;
using PloobsEngine.Features;
using PloobsEngine.Commands;
using PloobsEngine.Light;
using PloobsEngine.Physic.PhysicObjects.BepuObject;
using BEPUphysics.UpdateableSystems.ForceFields;
using PloobsEngine.Engine;

namespace IntroductionDemo4._0
{
    /// <summary>
    /// Gravitation Screen
    /// Shows how to use a specific feature of Bepu Physics
    /// </summary>
    public class GravitationalBepuScreen : IScene
    {        
        ICamera cam;
        LightThrowBepu lt;

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(0,true,1,true), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
            desc.UseFloatingBufferForLightMap = true;
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new DeferredRenderTechnic(desc);
        }

        protected override void InitScreen(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);

            ///Add the Input Component
            ///InputAdvanced is responsible for abstracting the xna input layer.            
            InputAdvanced inp = new InputAdvanced();
            engine.AddComponent(inp);
        }


        protected override void LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory, IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);                                          
            
            {
                SimpleModel sm = new SimpleModel(factory,"..\\Content\\Model\\ball");
                sm.SetTexture(factory.CreateTexture2DColor(1,1, Color.Blue),TextureType.DIFFUSE);                
                IPhysicObject pi = new SphereObject(new Vector3(0, 0, 0),1,10,30,MaterialDescription.DefaultBepuMaterial());
                pi.isMotionLess = true;
                DeferredNormalShader shader = new DeferredNormalShader();
                
                IMaterial mat = new DeferredMaterial(shader);
                IObject obj3 = new IObject(mat, sm,pi);
                this.World.AddObject(obj3);

                BepuPhysicWorld physicWorld;
                physicWorld = this.World.PhysicWorld as BepuPhysicWorld;
                System.Diagnostics.Debug.Assert(physicWorld != null);                
                var field = new GravitationalFieldObject(new InfiniteForceFieldShape(), obj3.Position, 66730 / 2f, 10000, physicWorld);
                ///This Method is from BepuPhysicWorld not from th IPhysicObject
                ///You can use everithing from Bepu using this object instead of the interface
                ///but take care, the engine dont know about THIS !!! it does not manage these things
                physicWorld.Space.Add(field);            

            }           

            int numColumns = 7;
            int numRows = 7;
            int numHigh = 7;
            float separation = 3;
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    for (int k = 0; k < numHigh; k++)
                    {
                        SimpleModel sm = new SimpleModel(factory, "..\\Content\\Model\\cubo");
                        sm.SetTexture(factory.CreateTexture2DColor(1, 1, Color.White), TextureType.DIFFUSE);
                        BoxObject pi = new BoxObject(new Vector3(separation * i - numRows * separation / 2, 40 + k * separation, separation * j - numColumns * separation / 2), 1, 1, 1, 5, new Vector3(1), Matrix.Identity, MaterialDescription.DefaultBepuMaterial());                        
                        pi.Entity.LinearDamping = 0;
                        pi.Entity.AngularDamping = 0;
                        DeferredNormalShader shader = new DeferredNormalShader();

                        IMaterial mat = new DeferredMaterial(shader);
                        IObject obj3 = new IObject(mat, sm, pi);
                        this.World.AddObject(obj3);

                        pi.Entity.LinearVelocity = new Vector3(30, 0, 0);
                    }
                }
            }

            cam = new CameraFirstPerson(GraphicInfo.Viewport);            
            cam.FarPlane = 3000;

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

            this.World.CameraManager.AddCamera(cam);
            
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);
            render.RenderTextComplete("Demo: Planetary Gravity (BEPU)", new Vector2(GraphicInfo.Viewport.Width - 315, 15), Color.White,Matrix.Identity);                                    
        }

        protected override void CleanUp(EngineStuff engine)
        {
            lt.CleanUp();
        }
    }
}

