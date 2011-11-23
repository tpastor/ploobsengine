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
using PloobsProjectTemplate;

namespace EngineTestes
{
    public class CustomModelScreen : IScene
    {

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

            ForwardRenderTecnichDescription desc = ForwardRenderTecnichDescription.Default();
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new ForwardRenderTecnich(desc);
        }

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory, IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);
            {
                 //supose you have these: (just an idiot sample -- dummy values)
            int[] indices = new int[] {0,1,2,1,2,3};
            Vector3[] vecs = new Vector3[] { new Vector3(0),new Vector3(4),new Vector3(2),new Vector3(1)};

            ///This is not good, cause it is very cpu intensive
            ///try to update the vertices array instead of creating one every time
            VertexPositionTexture[] vertices = new VertexPositionTexture[vecs.Length];
            for (int i = 0; i < vecs.Length; i++)
            {
                VertexPositionTexture v = new VertexPositionTexture(vecs[i],Vector2.Zero);
                vertices[i] = v;
            }


              //TO CONSTRUCT
            ////Creating the terrain
            VoxelTerrainModel VoxelTerrainModel = new PloobsProjectTemplate.VoxelTerrainModel(factory, "ANY_NAME", "Textures//goo", indices, vertices);
            TriangleMeshObject tm = new TriangleMeshObject(VoxelTerrainModel,Vector3.Zero,Matrix.Identity,Vector3.One,MaterialDescription.DefaultBepuMaterial());
            DeferredNormalShader shader2 = new DeferredNormalShader();
            IMaterial mat = new DeferredMaterial(shader2);
            IObject obj2 = new IObject(mat,VoxelTerrainModel,tm);


            ///TO UPDATE
            ///PHYSIC
            ///to update the triangle mesh date (updated version of indices/vertices ....)
            tm.StaticMesh.Mesh.Data.Indices = indices; 
            tm.StaticMesh.Mesh.Data.Vertices = vecs;

            //if you NOT changed the indices (slower)
            tm.StaticMesh.Mesh.Tree.Reconstruct();
            //if you changed the indices (faster)
            tm.StaticMesh.Mesh.Tree.Refit();


            
            ///MODEL (see the VoxelTerrainModel  ) implementation
            VoxelTerrainModel.Vertices = vertices;
            VoxelTerrainModel.Indices = indices ;
            }

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
                TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, new Vector3(100, 10, 10), Matrix.Identity, Vector3.One * 10, MaterialDescription.DefaultBepuMaterial());
                ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);
            }

            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo.Viewport));

            this.RenderTechnic.AddPostEffect(new BlackWhitePostEffect());
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);
        }

    }
}
