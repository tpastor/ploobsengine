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
using BEPUphysics.Entities;
using Microsoft.Xna.Framework;
using BEPUphysics.DataStructures;
using BEPUphysics.Entities.Prefabs;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Physics.Bepu
{
    public class BoxObject : BepuEntityObject
    {
        public BoxObject(Vector3 pos, float xlen , float ylen , float zlen, float mass = 10, Vector3? scale = null, Matrix? orientation = null, MaterialDescription materialDescription = null)
            : base(materialDescription, mass)
        {
            if (!orientation.HasValue)
                orientation = Matrix.Identity;

            if (!scale.HasValue)
                scale = Vector3.One;

            this.scale = scale.Value;
            entity = new Box(pos, xlen * this.scale.X, ylen * this.scale.Y, zlen * this.scale.Z, mass);
            entity.Orientation = Quaternion.CreateFromRotationMatrix(orientation.Value);            
        }        
        
        public override PhysicObjectTypes PhysicObjectTypes
        {
            get
            {
                return PhysicObjectTypes.BOXOBJECT;
            }
        }

#if WINDOWS
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            ActiveLogger.LogMessage("Serialization not implemented yet", LogLevel.RecoverableError);
        }
#endif

    }
}
