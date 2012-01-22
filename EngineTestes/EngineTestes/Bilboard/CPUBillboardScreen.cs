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
using PloobsEngine.Features.Billboard;

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

        CPUSphericalBillboardComponent BillboardComponent;

        TextCPUSphericalBillboardComponent  sBillboardComponent;
        TextCPUCylindricBillboardComponent  cBillboardComponent;
        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);
            BillboardComponent = new CPUSphericalBillboardComponent();
            engine.AddComponent(BillboardComponent);

            sBillboardComponent = new TextCPUSphericalBillboardComponent();
            engine.AddComponent(sBillboardComponent);

            cBillboardComponent = new TextCPUCylindricBillboardComponent();
            engine.AddComponent(cBillboardComponent);
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

            Billboard3D Billboard3D = new Billboard3D(factory.GetTexture2D("Textures\\grama1"), new Vector3(100, 20, 100), Vector2.One * 0.2f);
            BillboardComponent.Billboards.Add(Billboard3D);

            {
                List<Billboard> poss = new List<Billboard>();
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        float x, y;
                        x = i * 10;
                        y = j * 10;
                        poss.Add(
                            new Billboard()
                            {
                               Position = new Vector3(x, 50 , y),
                               Scale = Vector2.One * 0.01f,
                               Enabled = true
                            }
                            );
                    }
                }

                CPUBillboardModel bm = new CPUBillboardModel(factory, "Bilbs", "..\\Content\\Textures\\tree", poss.ToArray());
                ForwardAlphaTestShader cb = new ForwardAlphaTestShader(128,CompareFunction.GreaterEqual);                                
                ForwardMaterial matfor = new ForwardMaterial(cb);
                matfor.RasterizerState = RasterizerState.CullNone;
                GhostObject go = new GhostObject();                
                IObject obj2 = new IObject(matfor, bm, go);
                this.World.AddObject(obj2);


                TextBillboard3D TextBillboard3D = new TextBillboard3D("TEST 123 SPHERICAL", Color.Black, new Vector3(100), 0.5f);
                sBillboardComponent.Billboards.Add(TextBillboard3D);


                TextBillboard3D = new TextBillboard3D("TEST 123 Cylinder", Color.Black, new Vector3(200, 50, 200), 0.5f);
                cBillboardComponent.Billboards.Add(TextBillboard3D);

            }


            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));
                        
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {        
            base.Draw(gameTime, render);         
        }

    }
}
