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
    public class BilboardScreen : IScene
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
         
            SimpleModel simpleModel = new SimpleModel(factory, "Model//cenario");
            TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
            DeferredNormalShader shader = new DeferredNormalShader();
            DeferredMaterial fmaterial = new DeferredMaterial(shader);
            IObject obj = new IObject(fmaterial, simpleModel, tmesh);
            this.World.AddObject(obj);

            //{
            //    List<Vector3> poss = new List<Vector3>();
            //    for (int i = 0; i < 10; i++)
            //    {
            //        for (int j = 0; j < 10; j++)
            //        {
            //            float x, y;
            //            x = i * 100;
            //            y = j * 100;
            //            poss.Add(new Vector3(x, 5, y));
            //        }
            //    }
            //    StaticBilboardModel bm = new StaticBilboardModel(factory, "Bilbs", "..\\Content\\Textures\\tree", poss);
            //    DeferredCilindricGPUBilboardShader cb = new DeferredCilindricGPUBilboardShader();
            //    DeferredMaterial matfor = new DeferredMaterial(cb);
            //    GhostObject go = new GhostObject();
            //    IObject obj2 = new IObject(matfor, bm, go);
            //    this.World.AddObject(obj2);
            //}

            //{
            //    List<Vector3> poss = new List<Vector3>();
            //    for (int i = 0; i < 10; i++)
            //    {
            //        for (int j = 0; j < 10; j++)
            //        {
            //            float x, y;
            //            x = i * 100;
            //            y = j * 100;
            //            poss.Add(new Vector3(x, 5, y));
            //        }
            //    }
            //    StaticBilboardModel bm = new StaticBilboardModel(factory, "Bilbs", "..\\Content\\Textures\\tree", poss);
            //    DeferredSphericalBilboardShader cb = new DeferredSphericalBilboardShader();
            //    DeferredMaterial matfor = new DeferredMaterial(cb);
            //    GhostObject go = new GhostObject();
            //    IObject obj2 = new IObject(matfor, bm, go);
            //    this.World.AddObject(obj2);
            //}

            //            {
            //    List<Vector3> poss = new List<Vector3>();
            //    for (int i = 0; i < 10; i++)
            //    {
            //        for (int j = 0; j < 10; j++)
            //        {
            //            float x, y;
            //            x = i * 100;
            //            y = j * 100;
            //            poss.Add(new Vector3(x, 5, y));
            //        }
            //    }
            //    StaticBilboardModel bm = new StaticBilboardModel(factory, "Bilbs", "Textures\\grama1", poss);
            //    DeferredProceduralAnimatedcilindricBilboardShader cb = new DeferredProceduralAnimatedcilindricBilboardShader();
            //    DeferredMaterial matfor = new DeferredMaterial(cb);
            //    GhostObject go = new GhostObject();
            //    IObject obj2 = new IObject(matfor, bm, go);
            //    this.World.AddObject(obj2);
            //}

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
                StaticBilboardModel bm = new StaticBilboardModel(factory, "Bilbs", "Textures\\wizard", poss);
                DeferredAnimatedTextureShader cb = new DeferredAnimatedTextureShader(3, 100, BilboardType.Cilindric);
                cb.Amplitude = 0f;
                cb.Scale = new Vector2(50, 50);
                cb.Atenuation = new Vector4(0.4f, 0.4f, 0.4f, 1);
                DeferredMaterial matfor = new DeferredMaterial(cb);
                GhostObject go = new GhostObject(new Vector3(0, 10, 0), Matrix.Identity, Vector3.One);                
                IObject obj2 = new IObject(matfor, bm, go);
                this.World.AddObject(obj2);
            }

            //{
            //    List<BilboardInstance> poss = new List<BilboardInstance>();
            //    for (int i = 0; i < 10; i++)
            //    {
            //        for (int j = 0; j < 10; j++)
            //        {
            //            float x, y;
            //            x = i * 100;
            //            y = j * 100;
            //            BilboardInstance bi = new BilboardInstance();
            //            bi.Scale = new Vector2(100, 100);
            //            bi.Position = new Vector3(x, 5, y);
            //            poss.Add(bi);
            //        }
            //    }

            //    InstancedBilboardModel bm = new InstancedBilboardModel(factory, "Bilbs", "..\\Content\\Textures\\tree",poss.ToArray());
            //    DeferredInstancedBilboardShader cb = new DeferredInstancedBilboardShader(BilboardType.Cilindric);
            //    DeferredMaterial matfor = new DeferredMaterial(cb);
            //    GhostObject go = new GhostObject();
            //    IObject obj2 = new IObject(matfor, bm, go);
            //    this.World.AddObject(obj2);
            //}


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

    }
}
