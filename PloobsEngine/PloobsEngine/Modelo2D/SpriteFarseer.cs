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
using PloobsEngine.Physics2D.Farseer;

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
