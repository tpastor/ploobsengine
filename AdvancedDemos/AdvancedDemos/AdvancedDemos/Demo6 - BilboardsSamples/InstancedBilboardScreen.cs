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
    /// <summary>
    /// Instanced Bilboards
    /// The technich presented lets you add looooooots (yeah, looooooots) of bilboards in a scene    
    /// </summary>
    [PloobsEngine.TestSuite.TesteVisualScreen]
    public class InstancedBilboardScreen : IScene
    {
        /// <summary>
        /// Sets the world and render technich.
        /// </summary>
        /// <param name="renderTech">The render tech.</param>
        /// <param name="world">The world.</param>
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());
            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();            
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
        }

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo,factory, contentManager);

            SimpleModel simpleModel = new SimpleModel(factory, "Model//block");
            simpleModel.SetTexture(factory.CreateTexture2DColor(1,1,Color.White),TextureType.DIFFUSE);
            BoxObject tmesh = new BoxObject(new Vector3(0,10,0), 1, 1,1,50,new  Vector3(5000,1,5000),Matrix.Identity, MaterialDescription.DefaultBepuMaterial());
            tmesh.isMotionLess = true;
            DeferredNormalShader shader = new DeferredNormalShader();
            DeferredMaterial fmaterial = new DeferredMaterial(shader);
            IObject obj = new IObject(fmaterial, simpleModel, tmesh);
            this.World.AddObject(obj);            
            
            {
                List<BilboardInstance> poss = new List<BilboardInstance>();
                for (int i = -10 ; i < 20; i++)
                {
                    for (int j = -10; j < 20; j++)
                    {
                        float x, y;
                        x = i * 100;
                        y = j * 100;
                        BilboardInstance bi = new BilboardInstance();
                        bi.Scale = new Vector2(100, 100);
                        bi.Position = new Vector3(x, 5, y);
                        poss.Add(bi);
                    }
                }

                ///same as before, just the name of the class change
                ///You can change the position of a individual bilboard using bm.GetBilboardInstances(), updating the structure recieved and 
                ///using bm.SetBilboardInstances(). Dont do this every frame pls =P
                ///You can change lots of parameters of the DeferredInstancedBilboardShader, check it
                InstancedBilboardModel bm = new InstancedBilboardModel(factory, "Bilbs", "..\\Content\\Textures\\tree",poss.ToArray());                
                DeferredInstancedBilboardShader cb = new DeferredInstancedBilboardShader(BilboardType.Cilindric);                
                DeferredMaterial matfor = new DeferredMaterial(cb);
                GhostObject go = new GhostObject();
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


            CameraFirstPerson cam = new CameraFirstPerson(MathHelper.ToRadians(-50), MathHelper.ToRadians(-15), new Vector3(-200, 300, 250), GraphicInfo);
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
            render.RenderTextComplete("Instanced Bilboard Sample", new Vector2(10, 15), Color.White, Matrix.Identity);
        }

    }
}
