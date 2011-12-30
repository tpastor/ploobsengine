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
using Microsoft.Xna.Framework;
using FarseerPhysics.Collision;

namespace PloobsEngine.Physic2D.Farseer
{
    /// <summary>
    /// 2D farseer physic world implementation
    /// </summary>
    public class FarseerWorld : I2DPhysicWorld
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FarseerWorld"/> class.
        /// </summary>
        /// <param name="gravity">The gravity.</param>
        public FarseerWorld(Vector2 gravity)
        {
            world = new World(gravity);
        
#if WINDOWS || XBOX
            ConvertUnits.SetDisplayUnitToSimUnitRatio(24f);
      
#elif WINDOWS_PHONE
            ConvertUnits.SetDisplayUnitToSimUnitRatio(16f);      
#endif
        }

        private World world;

        public World World
        {
            get { return world; }
            set { world = value; }
        }

        /// <summary>
        /// Updates
        /// </summary>
        /// <param name="gt">The gt.</param>
        protected override void Update(Microsoft.Xna.Framework.GameTime gt)
        {            
            // variable time step but never less then 30 Hz
            world.Step(Math.Min((float)gt.ElapsedGameTime.TotalSeconds, (1f / 30f)));               
        }

        /// <summary>
        /// Adds the object.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public override void AddObject(I2DPhysicObject obj)
        {
            if (obj.Physic2DType == Physic2DType.Physic)
            {
                FarseerObject ob = obj as FarseerObject;
                ob.Body.UserData = obj;
                ob.Body.Enabled = true;                
            }
            else if (obj.Physic2DType == Physic2DType.Ghost)
            {
                obj.Enabled = true;
            }
        }

        /// <summary>
        /// Removes the object.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public override void RemoveObject(I2DPhysicObject obj)
        {
            if (obj.Physic2DType == Physic2DType.Physic)
            {
                FarseerObject ob = obj as FarseerObject;
                world.RemoveBody(ob.Body);
            }
            else if (obj.Physic2DType == Physic2DType.Ghost)
            {
                obj.Enabled = false;
            }
        }

        /// <summary>
        /// Test a Point agains the world
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public override I2DPhysicObject Picking(Vector2 point)
        {          
             Fixture fix =  world.TestPoint(point);
            if(fix == null)
                return null;
            else
            return fix.Body.UserData as I2DPhysicObject;
        }

        List<I2DPhysicObject> objs = new List<I2DPhysicObject>();
        /// <summary>
        /// Tests the AABB against the world.
        /// </summary>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns></returns>
        public override List<I2DPhysicObject> TestAABB(Vector2 min, Vector2 max)
        {
            objs.Clear();
            AABB a = new AABB(ref min, ref max);
            world.QueryAABB(
                (p) =>
                {
                    if (p.Body.UserData != null)
                    {
                        I2DPhysicObject I2DPhysicObject = (I2DPhysicObject)p.Body.UserData;
                        if(!objs.Contains(I2DPhysicObject))
                             objs.Add(I2DPhysicObject);
                    }
                    return true;
                }

            , ref a);
            return objs;
        }
    }
}
