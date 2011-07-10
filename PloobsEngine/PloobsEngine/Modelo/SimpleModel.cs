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

namespace PloobsEngine.Modelo
{
    /// <summary>
    /// Implementation of the most simple Model in the engine
    /// </summary>
    public class SimpleModel : IModelo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleModel"/> class.
        /// </summary>
        /// <param name="factory">The graphic factory.</param>
        /// <param name="modelName">Name of the model.</param>
        /// <param name="diffuseTextureName">Name of the diffuse texture.</param>
        /// <param name="bumpTextureName">Name of the bump texture.</param>
        /// <param name="specularTextureName">Name of the specular texture.</param>
        /// <param name="glowTextureName">Name of the glow texture.</param>
        /// <param name="CallLoadContent">if set to <c>true</c> [call load content].</param>
       public SimpleModel(GraphicFactory factory,String modelName, String diffuseTextureName = null, String bumpTextureName = null, String specularTextureName = null, String glowTextureName = null)
            : base(factory, modelName, false)
        {
            this._diffuseName = diffuseTextureName;
            this._bumpName = bumpTextureName;
            this._glowName = glowTextureName;
            this._specularName = specularTextureName;
            LoadModel(factory, out BatchInformations, out TextureInformations);
        }

        internal SimpleModel(GraphicFactory factory, String modelName, bool isInternal, String diffuseTextureName = null)
            : base(isInternal,factory, modelName,false)
        {
            this._diffuseName = diffuseTextureName;
            LoadModel(factory, out BatchInformations, out TextureInformations);
        }
        string _glowName = null;
        string _diffuseName = null;
        string _bumpName = null;
        string _specularName = null;
        private Model model;        
        private float modelRadius;

        protected override void LoadModel(GraphicFactory factory, out BatchInformation[][] BatchInformations, out TextureInformation[][] TextureInformation)
        {
            model = factory.GetModel(this.Name,isInternal);
            ModelBuilderHelper.Extract(factory,out BatchInformations, out TextureInformation,this.Name,_diffuseName,_bumpName,_specularName,_glowName,isInternal);            
            
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
    }
}
