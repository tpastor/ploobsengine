#region License
/*
    PloobsEngine Game Engine Version 0.3 Beta
    Copyright (C) 2011  Ploobs

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.SceneControl;
using PloobsEngine.Engine;
using PloobsEngine.Engine.Logger;
using PloobsEngine.Loader;

namespace PloobsEngine.Modelo
{
    /// <summary>
    /// Model implementation responsible for holding a specific mesh of a larger model
    /// Used by our World loader (from 3ds exporter)
    /// </summary>
    public class CustomModel : IModelo
    {
        public CustomModel(GraphicFactory factory, String modelName, int meshindex, BatchInformation[] binfo, Texture2D diffuse = null)
            : base(factory, modelName,false)
        {
            System.Diagnostics.Debug.Assert(binfo != null);
            System.Diagnostics.Debug.Assert(binfo.Count() != 0);
            BatchInformations = new BatchInformation[1][];
            BatchInformations[0] = binfo;

            BoundingSphere bs = new BoundingSphere();
            for (int i = 0; i < binfo.Count(); i++)
            {
                float radius;
                Vector3 center;
                ModelBuilderHelper.ExtractModelRadiusAndCenter(binfo[i], out radius, out center);
                bs = BoundingSphere.CreateMerged(bs, new BoundingSphere(center, radius));
            }
            modelRadius = bs.Radius;

            TextureInformations = new TextureInformation[1][];
            TextureInformations[0] = new TextureInformation[binfo.Count()];
            
            for (int j = 0; j < TextureInformations[0].Count(); j++)
            {
                TextureInformations[0][j] = new TextureInformation(isInternal, factory);
                TextureInformations[0][j].LoadTexture();
                if (diffuse == null)
                {
                    if (model != null)
                    {
                        BasicEffect effect = model.Meshes[meshindex].MeshParts[j].Effect as BasicEffect;
                        if (effect != null)
                        {
                            TextureInformations[0][j].SetTexture(effect.Texture, TextureType.DIFFUSE);
                        }
                    }
                }
                else
                {
                       SetTexture(diffuse, TextureType.DIFFUSE,meshindex,j);
                }            
            }
        }

        public CustomModel(GraphicFactory factory, ObjectInformation[] loader)
            : base(factory, loader[0].modelName, false)
        {
            System.Diagnostics.Debug.Assert(loader!= null);
            System.Diagnostics.Debug.Assert(loader.Count() != 0);
            BatchInformations = new BatchInformation[1][];
            BatchInformations[0] = new BatchInformation[loader.Count()];

            TextureInformations = new TextureInformation[1][];
            TextureInformations[0] = new TextureInformation[loader.Count()];

            BoundingSphere bs = new BoundingSphere();
            for (int i = 0; i < loader.Count(); i++)
            {
                BatchInformations[0][i] = loader[i].batchInformation;
                TextureInformations[0][i] = loader[i].textureInformation;

                float radius;
                Vector3 center;
                ModelBuilderHelper.ExtractModelRadiusAndCenter(BatchInformations[0][i], out radius, out center);
                bs = BoundingSphere.CreateMerged(bs, new BoundingSphere(center, radius));
            }            
            modelRadius = bs.Radius;
        }        

        public CustomModel(GraphicFactory factory, String modelName, BatchInformation BatchInformation, TextureInformation TextureInformation, int meshindex = 0, int meshpartIndex = 0)        
            : base(factory, modelName, false)
       {

           System.Diagnostics.Debug.Assert(BatchInformation != null);
           System.Diagnostics.Debug.Assert(TextureInformation != null);

           BatchInformations = new BatchInformation[1][];
           BatchInformations[0] = new BatchInformation[1];
           BatchInformations[0][0] = BatchInformation;
                      
           BoundingSphere bs = new BoundingSphere();           
           {
               float radius;
               Vector3 center;
               ModelBuilderHelper.ExtractModelRadiusAndCenter(BatchInformation, out radius, out center);                
               bs = BoundingSphere.CreateMerged(bs,new BoundingSphere(center,radius));
           }
           modelRadius = bs.Radius;

           TextureInformations = new TextureInformation[1][];
           TextureInformations[0] = new TextureInformation[1];
           TextureInformations[0][0] = TextureInformation;

           if (TextureInformation.getTexture(TextureType.DIFFUSE) == null)
           {
               if (model != null)
               {
                   BasicEffect effect = model.Meshes[meshindex].MeshParts[meshpartIndex].Effect as BasicEffect;
                   if (effect != null)
                   {
                       TextureInformations[0][0].SetTexture(effect.Texture, TextureType.DIFFUSE);
                   }
               }
           }           
       }         
        
        private Model model;        
        private float modelRadius;

        protected override void LoadModel(GraphicFactory factory, out BatchInformation[][] BatchInformations, out TextureInformation[][] TextureInformations)
        {
            //code not called !!!
            //Here for future usage.

            model = factory.GetModel(this.Name, isInternal);
            ModelBuilderHelper.Extract(model, out BatchInformations);
            
            BoundingSphere sphere = new BoundingSphere();
            foreach (var item in model.Meshes)
            {
                BoundingSphere.CreateMerged(sphere, item.BoundingSphere);
            }
            modelRadius = sphere.Radius;

            TextureInformations = new TextureInformation[1][];
            TextureInformations[0] = new TextureInformation[1];
            TextureInformations[0][0] = new TextureInformation(isInternal, factory);
            TextureInformations[0][0].LoadTexture();
        }

        public override int MeshNumber
        {
            get { return BatchInformations.Count(); }
        }

        public override float GetModelRadius()
        {
            return modelRadius;
        }

        public override void CleanUp(GraphicFactory factory)
        {            
        }

    }
}
