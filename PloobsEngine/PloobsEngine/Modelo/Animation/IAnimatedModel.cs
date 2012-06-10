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
using Microsoft.Xna.Framework;
using PloobsEngine.SceneControl;
using PloobsEngine.Engine;

namespace PloobsEngine.Modelo.Animation
{
    /// <summary>
    /// New Kind of Model specific for Animations
    /// </summary>
    public abstract class IAnimatedModel : IModelo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IAnimatedModel"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="modelName">Name of the model.</param>
        /// <param name="diffuseTextureName">Name of the diffuse texture.</param>
        /// <param name="bumpTextureName">Name of the bump texture.</param>
        /// <param name="specularTextureName">Name of the specular texture.</param>
        /// <param name="glowTextureName">Name of the glow texture.</param>
        public IAnimatedModel(GraphicFactory factory, String modelName, String diffuseTextureName, String bumpTextureName = null, String specularTextureName = null, String glowTextureName = null)
            : base(factory, modelName,false)
        {
            this._diffuseName = diffuseTextureName;
            this._bumpName = bumpTextureName;
            this._glowName = glowTextureName;
            this._specularName = specularTextureName;
        }

        protected string _glowName = null;
        protected string _diffuseName = null;
        protected string _bumpName = null;
        protected string _specularName = null;

        /// <summary>
        /// Gets the animation Information from Model
        /// It can be for example the Bone hierarchy of the model
        /// The result need to be casted (vary toooo much between animation APIs, better to keep as Object)
        /// </summary>
        /// <returns></returns>
        public abstract Object GetAnimation();

        /// <summary>
        /// Cleans up the IModelo
        /// </summary>
        /// <param name="factory"></param>
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

            base.CleanUp(factory);
        }
    }
}
