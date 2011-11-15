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
using BEPUphysics.Entities;
using BEPUphysics.Entities.Prefabs;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Physics.Bepu
{
    public class SphereObject : BepuEntityObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SphereObject"/> class.
        /// </summary>
        /// <param name="pos">The pos.</param>
        /// <param name="raio">The raio.</param>
        /// <param name="mass">The mass.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="materialDescription">The material description.</param>
        public SphereObject(Vector3 pos, float raio, float mass,float scale,MaterialDescription materialDescription)
            : base(materialDescription,mass)
        {
            this.scale = new Vector3(scale);
            entity = new Sphere(pos, raio * scale, mass);
            SetMaterialDescription(materialDescription);
        }
        
        public override PhysicObjectTypes PhysicObjectTypes
        {
            get { return PhysicObjectTypes.SPHEREOBJECT; }
        }

        #if !WINDOWS_PHONE
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            ActiveLogger.LogMessage("Serialization not implemented yet", LogLevel.RecoverableError);
        }
#endif
    }
}
