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
using PloobsEngine.Physics;
using System.Runtime.Serialization;

namespace PloobsEngine.Physic.Constraints
{
    /// <summary>
    /// Specification of a physic Constraint
    /// </summary>
#if WINDOWS
    public abstract class IPhysicConstraint : ISerializable
#else
    public abstract class IPhysicConstraint 
#endif
    {   
        public abstract PhysicConstraintType PhysicConstraintType { get; }

        #region ISerializable Members
#if WINDOWS
        public abstract void GetObjectData(SerializationInfo info, StreamingContext context);
#endif

        #endregion
    }
    public enum PhysicConstraintType
    {
        JOINT,
        SOLVER,
    }


    
}
