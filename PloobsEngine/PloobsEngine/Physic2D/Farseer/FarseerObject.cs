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
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Common;
using Microsoft.Xna.Framework;
using FarseerPhysics.Common.Decomposition;
using FarseerPhysics.Factories;
using FarseerPhysics.Common.PolygonManipulation;
using PloobsEngine.Modelo2D;

namespace PloobsEngine.Physic2D.Farseer
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

        public FarseerObject(FarseerWorld world, IModelo2D model, float density = 1, BodyType BodyType = BodyType.Dynamic)
            : this(world,model.Texture,density,BodyType)
        {
            
        }

        public FarseerObject(FarseerWorld world, Texture2D texture,float density = 1, BodyType BodyType = BodyType.Dynamic, float colapseDistance = 4)
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
            Origin = -centroid - new Vector2(texture.Width / 2, texture.Height/ 2); ;

            //We simplify the vertices found in the texture.
            textureVertices = SimplifyTools.ReduceByDistance(textureVertices, colapseDistance);

            //Since it is a concave polygon, we need to partition it into several smaller convex polygons
            List<Vertices> list = BayazitDecomposer.ConvexPartition(textureVertices);
                        
            //scale the vertices from graphics space to sim space
            Vector2 vertScale = new Vector2(ConvertUnits.ToSimUnits(1));
            foreach (Vertices vertices in list)
            {
                vertices.Scale(ref vertScale);
            }

            //Create a single body with multiple fixtures
            body = BodyFactory.CreateCompoundPolygon(world.World, list, density, BodyType);
            body.BodyType = BodyType;
            body.CollisionCategories = Category.All;
            body.CollidesWith = Category.All;
            body.Enabled = false;
        }

        public override Physic2DType Physic2DType
        {
            get
            {
                return Physic2D.Physic2DType.Physic;
            }            
        }

        private Body body;

        public Body Body
        {
            get { return body; }
            set { body = value; }
        }        

        #region I2DPhysicObject Members


        public override Vector2 LinearVelocity
        {
            get
            {
                return body.LinearVelocity;
            }
            set
            {
                body.LinearVelocity = value;
            }
        }

        public override float AngularVelocity
        {
            get
            {
                return body.AngularVelocity;
            }
            set
            {
                body.AngularVelocity = value;
            }
        }
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

        public override void ApplyForce(Vector2 force, Vector2? point = null)
        {
            if (point == null)
            {
                body.ApplyForce(force);
            }
            else
            {
                body.ApplyForce(force, point.Value);
            }
        }

        #endregion
    }
}
