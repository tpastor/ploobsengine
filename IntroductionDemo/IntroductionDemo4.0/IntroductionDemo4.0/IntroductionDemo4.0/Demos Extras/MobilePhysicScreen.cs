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

namespace IntroductionDemo4._0
{
    public class MobilePhysicScreen : IScene
    {
                
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(-9.8f), new SimpleCuller());

            ForwardRenderTecnichDescription desc = ForwardRenderTecnichDescription.Default();
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new ForwardRenderTecnich(desc);
        }

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            BepuPhysicWorld.ApplyHighStabilitySettings(this.World.PhysicWorld as BepuPhysicWorld);

            base.LoadContent(GraphicInfo, factory, contentManager);

            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//cenario");
                TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
                ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);               
                
            }
            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//uzi");
                simpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Green), TextureType.DIFFUSE);
                MobileMeshObject tmesh = new MobileMeshObject(simpleModel, new Vector3(100, 100, 10), Matrix.Identity, Vector3.One * 10, MaterialDescription.DefaultBepuMaterial(),BEPUphysics.CollisionShapes.MobileMeshSolidity.DoubleSided,10);
                //TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, new Vector3(100, 100, 10), Matrix.Identity, Vector3.One * 50, MaterialDescription.DefaultBepuMaterial());
                ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);
                shader.BasicEffect.EnableDefaultLighting();
            }

            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//uzi");
                simpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Yellow), TextureType.DIFFUSE);
                MobileMeshObject tmesh = new MobileMeshObject(simpleModel, new Vector3(150, 100, 10), Matrix.Identity, Vector3.One * 20, MaterialDescription.DefaultBepuMaterial(), BEPUphysics.CollisionShapes.MobileMeshSolidity.DoubleSided, 10);
                //TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, new Vector3(100, 100, 10), Matrix.Identity, Vector3.One * 50, MaterialDescription.DefaultBepuMaterial());
                ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);
                shader.BasicEffect.EnableDefaultLighting();

            }

            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//uzi");
                simpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Red), TextureType.DIFFUSE);
                MobileMeshObject tmesh = new MobileMeshObject(simpleModel, new Vector3(200, 100, 10), Matrix.Identity, Vector3.One * 15, MaterialDescription.DefaultBepuMaterial(), BEPUphysics.CollisionShapes.MobileMeshSolidity.DoubleSided, 10);
                //TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, new Vector3(100, 100, 10), Matrix.Identity, Vector3.One * 50, MaterialDescription.DefaultBepuMaterial());
                ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);
                shader.BasicEffect.EnableDefaultLighting();
            }
            
            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));            
        }
        

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);

            render.RenderTextComplete("Demo: Physic Triangle Mesh Mobile Sample", new Vector2(GraphicInfo.Viewport.Width - 515, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("Uzzies guns are Triangle Meshes", new Vector2(GraphicInfo.Viewport.Width - 515, 35), Color.White, Matrix.Identity);
        }

    }
}
