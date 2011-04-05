using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.SceneControl;

namespace PloobsEngine.Modelo
{
    public class SimpleModel : IModelo
    {
        public SimpleModel(IContentManager contentManager,String modelName, String diffuseTextureName = null, String bumpTextureName = null, String specularTextureName = null, String glowTextureName = null)
            : base(contentManager,modelName, diffuseTextureName, bumpTextureName, specularTextureName, glowTextureName)
        {

        }

        internal SimpleModel(IContentManager contentManager,String modelName, bool isInternal,String diffuseTextureName = null)
            : base(isInternal,contentManager, modelName, diffuseTextureName, null, null, null)
        {
        }

        
        private Model model;        
        private float modelRadius;

        protected override void LoadBatchInfo(SceneControl.IContentManager cmanager, out BatchInformation[][] BatchInformations)
        {
            model = cmanager.GetAsset<Model>(this.Name, isInternal);
            ModelBuilderHelper.Extract(model, out BatchInformations);

            if (diffuse == null)            
            {
                if (model.Meshes[0].Effects[0] is BasicEffect)
                {
                    diffuse = (model.Meshes[0].Effects[0] as BasicEffect).Texture;
                    _diffuseName = CUSTOM;
                }
            }

            
            BoundingSphere sphere = new BoundingSphere();
            foreach (var item in model.Meshes)
            {
                BoundingSphere.CreateMerged(sphere, item.BoundingSphere);
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

        public override BatchInformation[] GetBatchInformation(int meshNumber)
        {
            return BatchInformations[meshNumber];   
        }
    }
}
