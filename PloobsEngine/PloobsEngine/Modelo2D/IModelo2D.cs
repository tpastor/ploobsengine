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
    public enum ModelType
    {
        Texture,Vertices
    }     

    public abstract class IModelo2D
    {
        public IModelo2D(ModelType ModelType)
        {
            this.ModelType = ModelType;
            LayerDepth = 0;
            SourceRectangle = null;
            Rotation = 0;
        }

        public Rectangle? SourceRectangle
        {
            get;
            set;
        }

        public ModelType ModelType
        {
            get;
            internal set;
        }

        public float Rotation
        {
            get;
            set;
        }

        public Texture2D Texture
        {
            set;
            get;
        }

        public Vector2 Origin
        {
            get;
            set;
        }

        public float LayerDepth
        {
            set;
            get;
        }

        public virtual void Update(GameTime gameTime)
        {
        }
    }
}
