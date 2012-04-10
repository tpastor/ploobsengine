using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Modelo;
using PloobsEngine.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.SceneControl;

namespace AdvancedDemo4._0
{
    public class FullAnimatedModel : IModelo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref=" XModel "/> class.
        /// </summary>
        /// <param name="factory">The graphic factory.</param>
        /// <param name="modelName">Name of the model.</param>
        /// <param name="diffuseTextureName">Name of the diffuse texture.</param>
        /// <param name="bumpTextureName">Name of the bump texture.</param>
        /// <param name="specularTextureName">Name of the specular texture.</param>
        /// <param name="glowTextureName">Name of the glow texture.</param>
        /// <param name="forceFromDisk">if set to <c>true</c> [force from disk].</param>
       public  FullAnimatedModel (GraphicFactory factory,String modelName, String diffuseTextureName = null, String bumpTextureName = null, String specularTextureName = null, String glowTextureName = null, bool forceFromDisk = false)
            : base(factory, modelName, false)
        {
            this.forceFromDisk = forceFromDisk;            
            this._diffuseName = diffuseTextureName;
            this._bumpName = bumpTextureName;
            this._glowName = glowTextureName;
            this._specularName = specularTextureName;
            LoadModel(factory, out BatchInformations, out TextureInformations);
        }

        string _glowName = null;
        string _diffuseName = null;
        string _bumpName = null;
        string _specularName = null;
        public Model model;        
        private float modelRadius;
        bool forceFromDisk ;

        protected override void LoadModel(GraphicFactory factory, out BatchInformation[][] BatchInformations, out TextureInformation[][] TextureInformation)
        {
            model = factory.GetModel(this.Name, false, false);
            ModelBuilderHelper.Extract(factory,out BatchInformations, out TextureInformation,model,_diffuseName,_bumpName,_specularName,_glowName,false);            
            
            BoundingSphere sphere = new BoundingSphere();
            foreach (var item in model.Meshes)
            {
                sphere = BoundingSphere.CreateMerged(sphere, item.BoundingSphere);
            }
            modelRadius = sphere.Radius;         
        }

        public override int MeshNumber
        {
            get { return model.Meshes.Count; }
        }

        public override float GetModelRadius()
        {
            return modelRadius;
        }

        public void UpdateTransformations()
        {
            Matrix[] boneabsolute = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(boneabsolute);

            for (int j = 0; j < model.Meshes.Count; j++)
            {
                ModelMesh mesh = model.Meshes[j];
                for (int i = 0; i < mesh.MeshParts.Count; i++)
                {
                    BatchInformations[j][i].ModelLocalTransformation = boneabsolute[mesh.ParentBone.Index];
                }
            }
        }


        public override void CleanUp(GraphicFactory factory)
        {
            for (int i = 0; i < MeshNumber; i++)
            {
                foreach (var item in TextureInformations[i])
                {
                    factory.ReleaseAsset(item.DiffuseMapName);
                    factory.ReleaseAsset(item.BumpMapName);
                    factory.ReleaseAsset(item.GlowName);
                    factory.ReleaseAsset(item.SpecularMapName);
                }
            }
            factory.ReleaseAsset(Name);            
        }

    }
}
