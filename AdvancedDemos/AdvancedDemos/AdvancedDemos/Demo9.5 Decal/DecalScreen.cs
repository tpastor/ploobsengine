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

using Microsoft.Xna.Framework.Graphics;

namespace AdvancedDemo4._0
{
    [PloobsEngine.TestSuite.TesteVisualScreen]
    public class DecalScreen : IScene
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

            ///add Decal Component
            DecalComponent = new DecalComponent();
            engine.AddComponent(DecalComponent);
        }

        DecalComponent DecalComponent;
        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory, IContentManager contentManager)
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

            CameraFirstPerson cam = new CameraFirstPerson(GraphicInfo);
            //cam.FarPlane = 100;
            this.World.CameraManager.AddCamera(cam);

            lt = new LightThrowBepu(this.World, factory);


            {
                ///Add Decal To the Decal Component
                ///Texture
                Texture2D texture = factory.GetTexture2D("Textures//goo");
                ///The projection Matrix
                var projection = Matrix.CreatePerspectiveFieldOfView(cam.FieldOfView / 20, cam.AspectRatio, 1, 2000);

                ///The view Matrix
                var view = Matrix.CreateLookAt(
                    new Vector3(500, 300, 200),
                    new Vector3(0, 0, 0),
                    Vector3.Up
                    );
                Decal decal = new Decal(texture, view, projection);
                DecalComponent.Decals.Add(decal);
            }
            {
                ///Add Decal To the Decal Component
                ///Texture
                Texture2D texture = factory.GetTexture2D("Textures//goo");
                ///The projection Matrix
                var projection = Matrix.CreatePerspectiveFieldOfView(cam.FieldOfView / 10, cam.AspectRatio, 1, 2000);

                ///The view Matrix
                var view = Matrix.CreateLookAt(
                    new Vector3(500, 500, 700),
                    new Vector3(-200, 50, -10),
                    Vector3.Up
                    );
                Decal decal = new Decal(texture, view, projection);
                DecalComponent.Decals.Add(decal);
            }

            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//grasscube");
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);

        }
        LightThrowBepu lt;
        protected override void CleanUp(EngineStuff engine)
        {
            engine.RemoveComponent(DecalComponent.MyName);
            engine.RemoveComponent(SkyBox.MyName);
            lt.CleanUp();
            base.CleanUp(engine);
        }
        int x = 0;
        int z = 0;
        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);
            render.RenderTextComplete("Demo : Simple Deferred Decal System ", new Vector2(20, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("Shows how to \"hack\" the Deferred Render System", new Vector2(20, 35), Color.White, Matrix.Identity);
            render.RenderTextComplete("Use O P and K L to change Decal View matrix target", new Vector2(20, 55), Color.White, Matrix.Identity);
        }
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.O))
            {
                z += 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                z -= 1;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.K))
            {
                x += 1;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.L))
            {
                x -= 1;
            }


            ///The view Matrix
            var view = Matrix.CreateLookAt(
                new Vector3(500, 500, 700),
                new Vector3(x, 50, z),
                Vector3.Up
                );

            DecalComponent.Decals[0].view = view;

            base.Update(gameTime);
        }

    }
}
