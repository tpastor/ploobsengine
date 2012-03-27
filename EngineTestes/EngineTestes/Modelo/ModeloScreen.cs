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

namespace EngineTestes
{
    public class ModeloScreen : IScene
    {

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

            ForwardRenderTecnichDescription desc = ForwardRenderTecnichDescription.Default();
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new ForwardRenderTecnich(desc);
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

            {
                TModelo<VertexPositionColor> simpleModel = new TModelo<VertexPositionColor>(factory,
                    new VertexPositionColor[] 
                    {
                       new VertexPositionColor(new Vector3(50,10,0),Color.Green),
                       new VertexPositionColor(new Vector3(10,10,0),Color.Green),
                       new VertexPositionColor(new Vector3(0,100,0),Color.Green)
                    }
                    , null,
                    Matrix.Identity, null,-1,false,1,BufferUsage.None);

                TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, new Vector3(100,10,10), Matrix.Identity, Vector3.One * 2, MaterialDescription.DefaultBepuMaterial());
                ForwardXNABasicShader shader = new ForwardXNABasicShader();                
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);

                shader.BasicEffect.TextureEnabled = false;
                shader.BasicEffect.VertexColorEnabled = true;
            }

            {
                TConstructiveModelo<VertexPositionColor> simpleModel = new TConstructiveModelo<VertexPositionColor>(factory,Matrix.CreateTranslation(new Vector3(100)),null,-1,false,BufferUsage.None);
                simpleModel.AddVertex(new VertexPositionColor(new Vector3(50,10,0),Color.Red));
                simpleModel.AddVertex(new VertexPositionColor(new Vector3(100,10,0),Color.Red));
                simpleModel.AddVertex(new VertexPositionColor(new Vector3(0, 100, 0), Color.Red));
                simpleModel.BuildModel();
                
                TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, new Vector3(100, 10, 10), Matrix.Identity, Vector3.One * 2, MaterialDescription.DefaultBepuMaterial());
                ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);

                shader.BasicEffect.TextureEnabled = false;
                shader.BasicEffect.VertexColorEnabled = true;
            }


            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));

            this.RenderTechnic.AddPostEffect(new BlackWhitePostEffect());
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {        
            base.Draw(gameTime, render);         
        }

    }
}
