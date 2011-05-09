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

namespace AdvancedDemo4._0
{
    public class WaterCompleteScreen : IScene
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
        }

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory, IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            ///Create the water
            ///Water is just a Shader that Creats a Water like texture and binds it to a model
            {
                IModelo sm = new SimpleModel(factory, "Model\\block");
                Matrix trans = Matrix.CreateTranslation(new Vector3(0, 50, 0));
                Plane plano = Plane.Transform(new Plane(0, 1, 0, 0), trans);
                ///The water is VERRRRY big, the reflection and refraction will looks like blocked, its normal, increasy the size of the reflection/refrection buffer to correct this (bigger cost)
                ///We normally use the SAME transformation applyied to the plane (if not stranges affects can happen)
                IPhysicObject pi = new BoxObject(new Vector3(0, 50, 0), 1, 1, 1, 5, new Vector3(3000, 0.1f, 3000), trans, MaterialDescription.DefaultBepuMaterial());
                pi.isMotionLess = true;
                ///Water shader, will refract and reflect according to the plano passed in the parameter
                ///Using default Parameters, there are lots of things that can be changed. See WaterCompleteShader 
                DeferredWaterCompleteShader shader = new DeferredWaterCompleteShader(800, 600, plano, 0.1f);
                shader.SpecularIntensity = 0.01f;
                shader.SpecularPower = 50;
                IMaterial mat = new DeferredMaterial(shader);
                IObject obj4 = new IObject(mat, sm, pi);
                this.World.AddObject(obj4);
            }

            ExtractXmlModelLoader ext = new ExtractXmlModelLoader("Content//ModelInfos//", "Model//", "Textures//");
            ModelLoaderData data = ext.Load(factory, GraphicInfo, "shadow");
            WorldLoader wl = new WorldLoader();
            wl.OnCreateIObject += new CreateIObject(wl_OnCreateIObject);
            wl.OnCreateILight += new CreateILight(wl_OnCreateILight);
            wl.LoadWorld(factory, GraphicInfo, World, data);
            
            #region NormalLight
            DirectionalLightPE ld1 = new DirectionalLightPE(Vector3.Left, Color.White);
            DirectionalLightPE ld2 = new DirectionalLightPE(Vector3.Right, Color.White);
            DirectionalLightPE ld3 = new DirectionalLightPE(Vector3.Backward, Color.White);
            DirectionalLightPE ld4 = new DirectionalLightPE(Vector3.Forward, Color.White);
            DirectionalLightPE ld5 = new DirectionalLightPE(Vector3.Down, Color.White);
            DirectionalLightPE ld6 = new DirectionalLightPE(Vector3.Up, Color.White);
            float li = 0.5f;
            ld1.LightIntensity = li;
            ld2.LightIntensity = li;
            ld3.LightIntensity = li;
            ld4.LightIntensity = li;
            ld5.LightIntensity = li;
            ld6.LightIntensity = li;
            this.World.AddLight(ld1);
            this.World.AddLight(ld2);
            this.World.AddLight(ld3);
            this.World.AddLight(ld4);
            this.World.AddLight(ld5);
            this.World.AddLight(ld6);
            #endregion

            this.World.CameraManager.AddCamera(new CameraFirstPerson(true, GraphicInfo.Viewport));

            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//grassCube");
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);

        }

        ILight wl_OnCreateILight(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ILight li)
        {
            return null;
        }
        IObject wl_OnCreateIObject(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ModelInformation mi)
        {            
            return WorldLoader.CreateOBJ(world, factory, ginfo, mi);
        }


        protected override void CleanUp(EngineStuff engine)
        {
            engine.RemoveComponent("SkyBox");
            base.CleanUp(engine);
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);
            render.RenderTextComplete("Water Sample", new Vector2(10, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("Dynamic Reflection and Refraction", new Vector2(10, 35), Color.White, Matrix.Identity);
        }

    }
}
