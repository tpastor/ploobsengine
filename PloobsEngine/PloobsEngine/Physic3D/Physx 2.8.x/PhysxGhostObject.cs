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
using System.Xml;
using PloobsEngine.Utils;
using PloobsEngine.Modelo;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Physics
{    

    /// <summary>
    /// Fake Physic object, not simulated
    /// </summary>
    public class PhysxGhostObject : IPhysicObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PhysxGhostObject"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="orientation">The orientation.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="bb">The bb.</param>
     public PhysxGhostObject(Vector3 position, Matrix orientation, Vector3 scale, BoundingBox? bb = null) 
     {
         this.pos = position;
         this.ori = orientation;
         this.scale = scale;
         this.bb = bb;
     }

     /// <summary>
     /// Initializes a new instance of the GhostObject class.
     /// DEfault Object in 0,0,0 identity rotation and 1,1,1 scale
     /// </summary>
     /// <param name="bb">The bb.</param>
     public PhysxGhostObject(BoundingBox? bb = null)         
     {
         this.pos = Vector3.Zero;
         this.ori = Matrix.Identity;
         this.scale = Vector3.One;
         this.bb = bb;
     }

     bool internalMatrix = false;
     private Vector3 pos;
     private Matrix ori;
     private Matrix internalWorld;
     private BoundingBox? bb =null;
     Vector3 scale;

     public override Vector3 Scale
     {
         get
         {
             return scale;
         }
         set
         {
             this.scale = value;
         }
     }
    
     public override Vector3 Position
     {
         get
         {
             return pos ;
         }
         set
         {
             this.pos = value;
         }
     }
     public override Matrix Rotation
     {
         get
         {
             return ori;
         }
         set
         {
             this.ori = value;
         }
     }
     public override BoundingBox? BoundingBox
     {
         get
         {
             if (bb.HasValue)
             {
                 Vector3 min =  bb.Value.Min;
                 Vector3 max =  bb.Value.Max;
                 return new BoundingBox(Vector3.Transform(min, WorldMatrix), Vector3.Transform(max, WorldMatrix));
             }
             return bb;
         }         
     }

     public void SetBoundingBox(BoundingBox bb)
     {
         this.bb = bb;
     }

     public override bool isMotionLess
     {
         get
         {
             return true;
         }
         set
         {
             if (value == false)
             {
                 ActiveLogger.LogMessage("Ghost is always motion less", LogLevel.RecoverableError);
             }
         }
     }
     
     public override Vector3 Velocity
     {
         get
         {
             return Vector3.Zero;
         }
         set
         {
             ActiveLogger.LogMessage("Ghost does not have velocity", LogLevel.RecoverableError);
         }
     }

     public void setInternalWorldMatrix(Matrix mat)
     {
             internalMatrix = true;
             internalWorld = mat;
     }

     public override Matrix WorldMatrix
     {
         get
         {
             if (internalMatrix)
             {
                 return internalWorld;
             }
             else
             {
                 return Matrix.CreateScale(Scale) * Rotation * Matrix.CreateTranslation(Position);
             }
         }         

     }

#if WINDOWS
     public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
     {
         ActiveLogger.LogMessage("Serialization not implemented yet", LogLevel.RecoverableError);
     }
#endif
        
        public override PhysicObjectTypes PhysicObjectTypes
        {
            get { return PhysicObjectTypes.GHOST; }
        }

        public override Vector3 FaceVector
        {
            get { return ori.Forward; }
        }

        public override SceneControl.IObject ObjectOwner
        {
            get;
            set;

        }

        public override void ApplyImpulse(Vector3 position, Vector3 force)
        {
        }

        public override Vector3 AngularVelocity
        {
            get
            {
                return Vector3.Zero;
            }
            set
            {
                
            }
        }
    }
}
