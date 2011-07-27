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
using BEPUphysics.Constraints.TwoEntity.Joints;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Physic.Constraints.BepuConstraint
{
    public class BepuPhysicConstraint : IPhysicConstraint
    {

        protected Joint joint;

        public Joint Joint
        {
            get { return joint; }
            set { joint = value; }
        }




        public override Microsoft.Xna.Framework.Vector3 Position
        {
            get
            {
                return Position;
            }
            set
            {
                Position = value;
            }
        }

        public override Physics.IPhysicObject BodyA
        {
            set;
            get;
        }

        public override Physics.IPhysicObject BodyB
        {
            set;
            get;
        }


        public override String PhysicConstraintType
        {
            set;
            get;
        }

#if !WINDOWS_PHONE
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            ActiveLogger.LogMessage("Serialization not implemented yet", LogLevel.RecoverableError);
        }
#endif
    }
}
