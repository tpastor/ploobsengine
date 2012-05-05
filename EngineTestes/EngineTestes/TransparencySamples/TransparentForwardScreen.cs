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
    public class TransparentForwardScreen : IScene
    {
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());
            //world.Culler.SortObjectsByDistanceToCamera = true;
            ForwardRenderTecnichDescription desc = ForwardRenderTecnichDescription.Default();
            desc.UsePostDrawPhase = true;
            renderTech = new ForwardRenderTecnich(desc);
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
            ForwardXNABasicShader shader = new ForwardXNABasicShader();
            ForwardMaterial fmaterial = new ForwardMaterial(shader);
            IObject obj = new IObject(fmaterial, simpleModel, tmesh);
            this.World.AddObject(obj);

            ///Varios blocos transparentes
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    ///Cria modelo com textura procedural amarela
                    SimpleModel sm = new SimpleModel(factory, "Model\\block");
                    sm.SetTexture(factory.CreateTexture2DColor(1,1, Color.Yellow), TextureType.DIFFUSE);                    
                    ///Objeto fisico Ghost (nao tem colisao, nem formato)
                    GhostObject pi = new GhostObject(new Vector3(i * 10, 50, j * 10), Matrix.Identity, new Vector3(5));
                    pi.isMotionLess = true;
                    ForwardTransparenteShader s = new ForwardTransparenteShader();                    
                    s.TransparencyLevel = 0.3f;
                    ForwardMaterial mat = new ForwardMaterial(s);
                    IObject obj4 = new IObject(mat, sm, pi);
                    this.World.AddObject(obj4);
                }

            }
            

            this.World.CameraManager.AddCamera(new CameraFirstPerson(true,GraphicInfo));

            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//cubemap");
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);

        }

    }
}
