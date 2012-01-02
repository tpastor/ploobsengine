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

namespace PloobsEngine.Modelo2D
{
    /// <summary>
    /// Type of the model
    /// </summary>
    public enum ModelType
    {
        Texture,Vertices
    }

    /// <summary>
    /// 2D model specification
    /// </summary>
    public abstract class IModelo2D
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IModelo2D"/> class.
        /// </summary>
        /// <param name="ModelType">Type of the model.</param>
        public IModelo2D(ModelType ModelType)
        {
            this.ModelType = ModelType;
            LayerDepth = 0;
            SourceRectangle = null;
            Rotation = 0;
        }

        /// <summary>
        /// Gets or sets the source rectangle (if texture type)
        /// Null otherwise
        /// </summary>
        /// <value>
        /// The source rectangle.
        /// </value>
        public Rectangle? SourceRectangle
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the type of the model.
        /// </summary>
        /// <value>
        /// The type of the model.
        /// </value>
        public ModelType ModelType
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        /// <value>
        /// The rotation.
        /// </value>
        public float Rotation
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the texture.
        /// </summary>
        /// <value>
        /// The texture.
        /// </value>
        public Texture2D Texture
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets the origin of the model (center of the object normally, in local coordinates)
        /// </summary>
        /// <value>
        /// The origin.
        /// </value>
        public Vector2 Origin
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the layer depth.
        /// same concept of Photoshop =P
        /// </summary>
        /// <value>
        /// The layer depth.
        /// </value>
        public float LayerDepth
        {
            set;
            get;
        }

        /// <summary>
        /// Updates the modelo.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public virtual void Update(GameTime gameTime)
        {
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public virtual void CleanUp(PloobsEngine.Engine.GraphicFactory factory)
        {
            String fullName = Texture.Tag as String;            
            if (fullName != null)
            {
                factory.ReleaseAsset(fullName);
            }
            if (fullName == "CREATED")
            {
                Texture.Dispose();
            }
            
        }
             
    }
}
