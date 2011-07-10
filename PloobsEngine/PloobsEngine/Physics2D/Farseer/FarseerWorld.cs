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

namespace PloobsEngine.Physics2D.Farseer
{
    public class FarseerWorld : I2DPhysicWorld
    {
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

        protected override void Update(Microsoft.Xna.Framework.GameTime gt)
        {            
            // variable time step but never less then 30 Hz
            world.Step(Math.Min((float)gt.ElapsedGameTime.TotalSeconds, (1f / 30f)));               
        }

        public override void AddObject(I2DPhysicObject obj)
        {
            if (obj is FarseerObject)
            {
                FarseerObject ob = obj as FarseerObject;
                ob.Body.UserData = obj;
                ob.Body.Enabled = true;                
            }
            else if (obj is GhostObject)
            {
                obj.Enabled = true;
            }
        }

        public override void RemoveObject(I2DPhysicObject obj)
        {
            if (obj is FarseerObject)
            {
                FarseerObject ob = obj as FarseerObject;
                world.RemoveBody(ob.Body);
            }
            else if (obj is GhostObject)
            {
                obj.Enabled = false;
            }
        }
    }
}
