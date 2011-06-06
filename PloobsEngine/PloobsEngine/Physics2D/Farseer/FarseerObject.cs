using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Common;
using Microsoft.Xna.Framework;
using FarseerPhysics.Common.Decomposition;
using FarseerPhysics.Factories;
using FarseerPhysics.Common.PolygonManipulation;

namespace PloobsEngine.Physics2D.Farseer
{
    public class FarseerObject : I2DPhysicObject
    {
        public FarseerObject(FarseerWorld world, Shape shape, BodyType BodyType = BodyType.Dynamic)
        {            
            body = new Body(world.World);
            body.BodyType = BodyType;
            body.CreateFixture(shape);
            body.Enabled = false;            
        }

        public FarseerObject(Body body)
        {
            this.body = body;
            body.Enabled = false;
        }

        public FarseerObject(FarseerWorld world, Vertices vertices, float density = 1, BodyType BodyType = BodyType.Dynamic)
        {
            Shape shape = new PolygonShape(vertices, density);
            body = new Body(world.World);
            body.BodyType = BodyType;
            body.CreateFixture(shape);
            body.Enabled = false;
        }       
        

        public FarseerObject(FarseerWorld world, Texture2D texture, BodyType BodyType = BodyType.Dynamic)
        {            
             //Create an array to hold the data from the texture
            uint[] data = new uint[texture.Width * texture.Height];
      
             //Transfer the texture data to the array
            texture.GetData(data);

            //Find the vertices that makes up the outline of the shape in the texture
            Vertices textureVertices = PolygonTools.CreatePolygon(data, texture.Width);

            //The tool return vertices as they were found in the texture.
            //We need to find the real center (centroid) of the vertices for 2 reasons:

            //1. To translate the vertices so the polygon is centered around the centroid.
            Vector2 centroid = -textureVertices.GetCentroid();
            textureVertices.Translate(ref centroid);

            //2. To draw the texture the correct place.
            Origin = -centroid;

            //We simplify the vertices found in the texture.
            textureVertices = SimplifyTools.ReduceByDistance(textureVertices, 4f);

            //Since it is a concave polygon, we need to partition it into several smaller convex polygons
            List<Vertices> list = BayazitDecomposer.ConvexPartition(textureVertices);

            //Adjust the scale of the object for WP7's lower resolution
            float _scale;
#if WINDOWS_PHONE
            _scale = 0.6f;
#else
            _scale = 1f;
#endif

            //scale the vertices from graphics space to sim space
            Vector2 vertScale = new Vector2(ConvertUnits.ToSimUnits(1)) * _scale;
            foreach (Vertices vertices in list)
            {
                vertices.Scale(ref vertScale);
            }

            //Create a single body with multiple fixtures
            body = BodyFactory.CreateCompoundPolygon(world.World, list, 1f, BodyType.Dynamic);
            body.BodyType = BodyType;
            body.CollisionCategories = Category.All;
            body.CollidesWith = Category.All;

        }

        private Body body;

        public Body Body
        {
            get { return body; }
            set { body = value; }
        }        

        #region I2DPhysicObject Members

        public override bool isDynamic
        {
            get
            {
                return !body.IsStatic;
            }
            set
            {                
                    body.IsStatic = !value;
            }
        }

        public override Microsoft.Xna.Framework.Vector2 Position
        {
            get
            {
                return ConvertUnits.ToDisplayUnits(body.Position);
            }
            set
            {
                body.Position = ConvertUnits.ToSimUnits(value);
            }
        }

        public override float Rotation
        {
            get
            {
                return body.Rotation;
            }
            set
            {
                body.Rotation = value;
            }
        }

        public override bool Enabled
        {
            get
            {
                return body.Enabled;
            }
            internal set
            {
                body.Enabled = value;
            }
        }

        #endregion
    }
}
