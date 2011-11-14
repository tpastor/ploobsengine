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
using PloobsEngine.Engine;
using PloobsEngine.Utils;
using FarseerPhysics.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Collision.Shapes;
using PloobsEngine.Physic2D.Farseer;

namespace PloobsEngine.Modelo2D
{
    public class SpriteFarseer : IModelo2D
    {
        static FarseerAsset2DCreator assetCreator = null;

        public SpriteFarseer(GraphicFactory factory, Vertices vertices, Color color, float materialScale = 1) :base(ModelType.Texture)
        {
            if(assetCreator ==null)
                assetCreator  = new FarseerAsset2DCreator(factory);

            Texture = assetCreator.CreateTextureFromVertices(vertices,color, materialScale);
            Origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
        }

        public SpriteFarseer(GraphicFactory factory, PolygonShape shape, Color color, float materialScale = 1)
            : base(ModelType.Texture)
        {
            if (assetCreator == null)
                assetCreator = new FarseerAsset2DCreator(factory);

            Texture = assetCreator.CreateTextureFromVertices(shape.Vertices, color, materialScale);
            Origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
        }

        public SpriteFarseer(GraphicFactory factory, CircleShape shape, Color color, float materialScale = 1)
            : base(ModelType.Texture)
        {
            if (assetCreator == null)
                assetCreator = new FarseerAsset2DCreator(factory);

            Texture = assetCreator.CreateCircleTexture(shape.Radius, color, materialScale);
            Origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
        }

        public SpriteFarseer(GraphicFactory factory, float radius, Color color, float materialScale = 1)
            : base(ModelType.Texture)
        {
            if (assetCreator == null)
                assetCreator = new FarseerAsset2DCreator(factory);

            Texture = assetCreator.CreateCircleTexture(radius, color, materialScale);
            Origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
        }

        public SpriteFarseer(Texture2D texture)
            : base(ModelType.Texture)
        {
            Texture = texture;
            Origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
        }
    }
}
