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
using PloobsEngine.Utils;
using XNAnimation;
using PloobsEngine.Engine;

namespace PloobsEngine.Modelo.Animation
{
    /// <summary>
    /// Concrete Animated Model
    /// </summary>
    public class AnimatedModel : IAnimatedModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnimatedModel"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="modelName">Name of the model.</param>
        /// <param name="diffuseTextureName">Name of the diffuse texture.</param>
        /// <param name="bumpTextureName">Name of the bump texture.</param>
        /// <param name="specularTextureName">Name of the specular texture.</param>
        /// <param name="glowTextureName">Name of the glow texture.</param>
        public AnimatedModel(GraphicFactory factory, String modelName, String diffuseTextureName = null, String bumpTextureName = null, String specularTextureName = null, String glowTextureName = null) :
            base(factory,modelName,diffuseTextureName,bumpTextureName,specularTextureName,glowTextureName)
        {
            LoadModel(factory, out BatchInformations, out TextureInformations);
        }     

        #region IModelo Members

        private float modelRadius;
        protected SkinnedModel skinnedModel;

        protected override void LoadModel(GraphicFactory factory, out BatchInformation[][] BatchInformations, out TextureInformation[][] TextureInformations)
        {
            skinnedModel = factory.GetAnimatedModel(Name);
            ModelBuilderHelper.Extract(factory, out BatchInformations, out TextureInformations, skinnedModel.Model,_diffuseName,_bumpName,_specularName,_glowName,isInternal);
            BoundingSphere sphere = new BoundingSphere();
            foreach (var item in skinnedModel.Model.Meshes)
            {
                sphere = BoundingSphere.CreateMerged(sphere, item.BoundingSphere);
            }
            modelRadius = sphere.Radius;                     
        }

        public override float GetModelRadius()
        {
            return modelRadius;
        }
        
        public Model GetAnimatedModel()
        {
            return skinnedModel.Model;
        }

        public Matrix[] getBonesTransformation()
        {
            Matrix[] m = new Matrix[skinnedModel.Model.Bones.Count];
            skinnedModel.Model.CopyAbsoluteBoneTransformsTo(m);
            return m;
        }


        /// <summary>
        /// Gets the animation Information from Model
        /// It can be for example the Bone hierarchy of the model
        /// The result need to be casted (vary toooo much between animation APIs, better to keep as Object)
        /// </summary>
        /// <returns></returns>
        public override object GetAnimation()
        {
            return skinnedModel;
        }
        
        /// <summary>
        /// Gets the model tag.
        /// </summary>
        /// <returns></returns>
        public object getModelTag()
        {
            return skinnedModel.Model.Tag;
        }

     
        /// <summary>
        /// Gets the mesh number.
        /// </summary>
        public override  int MeshNumber
        {
            get { return skinnedModel.Model.Meshes.Count; }
        }

        public override void CleanUp(GraphicFactory factory)
        {            

            base.CleanUp(factory);
        }
                      
        #endregion
    }
}