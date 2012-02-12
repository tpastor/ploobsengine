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
using Microsoft.Xna.Framework.Graphics;
using EngineTestes.Bilboard;

namespace EngineTestes
{
    public class CPUBillboardScreen : IScene
    {

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

            ForwardRenderTecnichDescription desc = ForwardRenderTecnichDescription.Default();
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new ForwardRenderTecnich(desc);
        }

        BillboardComponent BillboardComponent;
        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);
            BillboardComponent = new BillboardComponent();
            engine.AddComponent(BillboardComponent);
        }

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//cenario");
                TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
                ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);                
            }

            SphericalBillboard3D Billboard3D = new SphericalBillboard3D(factory.GetTexture2D("Textures\\goo"), new Vector3(100, 20, 100), Vector2.One * 0.2f);
            BillboardComponent.Billboards.Add(Billboard3D);

           
            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));
                        
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {        
            base.Draw(gameTime, render);         
        }

    }
}
