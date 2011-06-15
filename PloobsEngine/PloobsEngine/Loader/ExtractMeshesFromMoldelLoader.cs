using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.Modelo;

namespace PloobsEngine.Loader
{
    public class ExtractMeshesFromMoldelLoader : IModelLoader
    {
        #region IModelLoader Members

        public ModelLoaderData Load(Engine.GraphicFactory factory, Engine.GraphicInfo info, string Name)
        {
            ModelLoaderData ModelLoaderData = new ModelLoaderData();
            Model model = factory.GetModel(Name);
            Matrix[] m = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(m);            
                        
            for (int i = 0; i < model.Meshes.Count; i++)
            {
                String name = model.Meshes[i].Name.Substring(5);                

                for (int j = 0; j < model.Meshes[i].MeshParts.Count; j++)
                {                        
                        Matrix tr = m[model.Meshes[i].ParentBone.Index];

                        Vector3 scale;
                        Vector3 pos;
                        Quaternion ori;
                        tr.Decompose(out scale, out ori, out pos);

                        ObjectInformation mi = new ObjectInformation();
                        mi.modelName = name;
                        mi.meshPartIndex = j;
                        mi.meshIndex = i;
                        mi.position = pos;
                        mi.scale = scale;
                        mi.rotation = ori;

                        mi.ellasticity = -1;
                        mi.dinamicfriction = -1;
                        mi.staticfriction = -1;
                        mi.collisionType = "TriangleMesh";
                        mi.mass = 10;

                        ModelBuilderHelper.Extract(m, model.Meshes[i].MeshParts[j], out mi.batchInformation);                        
                        mi.batchInformation.ModelLocalTransformation = m[model.Meshes[i].ParentBone.Index];

                        mi.textureInformation = new Modelo.TextureInformation(false, factory, null, null, null, null);
                        mi.textureInformation.LoadTexture();

                        Effect effect = model.Meshes[i].MeshParts[j].Effect;                
                        BasicEffect BasicEffect = effect as BasicEffect;
                        if(BasicEffect != null)
                            mi.textureInformation.SetTexture(BasicEffect.Texture, TextureType.DIFFUSE);
                        ModelLoaderData.ModelMeshesInfo.Add(mi);                    
                }
            }


            return ModelLoaderData;
        }

        #endregion
    }
}
