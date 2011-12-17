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
    /// <summary>
    /// Simplest Bilboard Sample
    /// </summary>
    public class NormalBilboardScreen : IScene
    {
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();            
            desc.UseFloatingBufferForLightMap = false;            
            renderTech = new DeferredRenderTechnic(desc);
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

            SimpleModel simpleModel = new SimpleModel(factory, "Model//block");
            simpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.White), TextureType.DIFFUSE);
            BoxObject tmesh = new BoxObject(new Vector3(0, 10, 0), 1, 1, 1, 50, new Vector3(5000, 1, 5000), Matrix.Identity, MaterialDescription.DefaultBepuMaterial());
            tmesh.isMotionLess = true;
            DeferredNormalShader shader = new DeferredNormalShader();
            DeferredMaterial fmaterial = new DeferredMaterial(shader);
            IObject obj = new IObject(fmaterial, simpleModel, tmesh);
            this.World.AddObject(obj);            
            
            {
                List<Vector3> poss = new List<Vector3>();
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        float x, y;
                        x = i * 100;
                        y = j * 100;
                        poss.Add(new Vector3(x, 5, y));
                    }
                }
                ///Bilboard Model
                StaticBilboardModel bm = new StaticBilboardModel(factory, "Bilbs", "..\\Content\\Textures\\tree", poss);
                ///Bilboard Shader, this one is otimized for Cilindrical Bilboard only
                DeferredCilindricGPUBilboardShader cb = new DeferredCilindricGPUBilboardShader();
                cb.Atenuation = new Vector4(0.8f, 0.8f, 0.8f, 1);
                DeferredMaterial matfor = new DeferredMaterial(cb);
                ///You can Change Parameters (position, scale ...) of ALL bilboards changing the World Matrix of the physical Object
                ///for Ghost Object we can use go.setInternalWorldMatrix.
                GhostObject go = new GhostObject();
                go.Position = new Vector3(200, -10f, 0);
                IObject obj2 = new IObject(matfor, bm, go);
                this.World.AddObject(obj2);
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


            CameraFirstPerson cam = new CameraFirstPerson(MathHelper.ToRadians(-60), MathHelper.ToRadians(-10), new Vector3(-200, 150, 250), GraphicInfo);
            this.World.CameraManager.AddCamera(cam);

            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//grassCube");
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);
        }

        protected override void CleanUp(EngineStuff engine)
        {
            engine.RemoveComponent("SkyBox");
            base.CleanUp(engine);
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);
            render.RenderTextComplete("Demo 12-22:Normal Bilboard Sample, otimized Cilindrical bilboard", new Vector2(10, 15), Color.White, Matrix.Identity);
        }


    }
}
