using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.SceneControl;
using PloobsEngine.Engine;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Modelo
{
    public class CustomModel : IModelo
    {
       public CustomModel(GraphicFactory factory, String modelName, BatchInformation[] binfo,Texture2D diffuse = null, Texture2D bump = null, Texture2D specular = null, Texture2D glow= null, int meshpartIndex = 0)
           : base(factory, modelName, CUSTOM, CUSTOM, CUSTOM, CUSTOM, false)
       {
           this.meshpartIndex = meshpartIndex;
           BatchInformations = new BatchInformation[1][];
           BatchInformations[0] = binfo;
                      
           BoundingSphere bs = new BoundingSphere();
           for (int i = 0; i < binfo.Count(); i++)
           {
               float radius;
               Vector3 center;
               ModelBuilderHelper.ExtractModelRadiusAndCenter(binfo[i], out radius, out center);                
               bs = BoundingSphere.CreateMerged(bs,new BoundingSphere(center,radius));
           }
           modelRadius = bs.Radius;

           SetTexture(diffuse, TextureType.DIFFUSE);
           SetTexture(bump, TextureType.BUMP);
           SetTexture(specular, TextureType.SPECULAR);
           SetTexture(glow, TextureType.GLOW);           
       }
     
        private int meshpartIndex = 0;
        private Model model;        
        private float modelRadius;

        protected override void LoadBatchInfo(GraphicFactory factory, out BatchInformation[][] BatchInformations)
        {
            model = factory.GetModel(this.Name, isInternal);
            ModelBuilderHelper.Extract(model, out BatchInformations);

            if (diffuse == null)            
            {
                BasicEffect newVariable = model.Meshes[0].MeshParts[meshpartIndex].Effect as BasicEffect;
                if (newVariable != null)
                {
                    diffuse = newVariable.Texture;
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
            get { return 1; }
        }

        public override float GetModelRadius()
        {
            return modelRadius;
        }

        public override BatchInformation[] GetBatchInformation(int meshNumber)
        {
            if (meshNumber != 0)
            {
                ActiveLogger.LogMessage("This Model has only one BatchInformation " + Name, LogLevel.RecoverableError);
            }
            return BatchInformations[0];   
        }
    }
}
