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
using BEPUphysics.Paths.PathFollowing;
using Microsoft.Xna.Framework;
using BEPUphysics.Entities;
using PloobsEngine.Modelo;
using PloobsEngine.SceneControl;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Physics.Bepu
{
    /// <summary>
    /// Specialized Object Proper to Movimentation
    /// </summary>
    public class ObjectMover : IPhysicObject
    {
        public ObjectMover(BepuEntityObject entityObject)
        {
            bepuEntityObject = entityObject;
            mover = new EntityMover(entityObject.Entity);
            rotator = new EntityRotator(entityObject.Entity);            
        }

        public ObjectMover(BepuEntityObject entityObject,bool VelocityMotor)
        {
            bepuEntityObject = entityObject;
            mover = new EntityMover(entityObject.Entity);
            rotator = new EntityRotator(entityObject.Entity);
            if(VelocityMotor)
                mover.LinearMotor.Settings.Mode = BEPUphysics.Constraints.TwoEntity.Motors.MotorMode.VelocityMotor;
        }

        private EntityMover mover;

        public EntityMover Mover
        {
            get { return mover; }
            set { mover = value; }
        }
        private EntityRotator rotator;

        public EntityRotator Rotator
        {
            get { return rotator; }
            set { rotator = value; }
        }
        private BepuEntityObject bepuEntityObject;

        public BepuEntityObject BepuEntityObject
        {
            get { return bepuEntityObject; }
            set { bepuEntityObject = value; }
        }

        public Vector3 GoalVelocity
        {
            set
            {
                mover.LinearMotor.Settings.VelocityMotor.GoalVelocity = value;
            }
            get
            {
                return mover.LinearMotor.Settings.VelocityMotor.GoalVelocity;
            }
        }

        public Quaternion GoalOrientation
        {
            set
            {
                rotator.TargetOrientation = value;
            }
            get
            {
                return rotator.TargetOrientation;
            }
        }




        #region IPhysicObject Members

        public override Vector3 Position
        {
            get
            {
                return bepuEntityObject.Position;
            }
            set
            {
                this.bepuEntityObject.Position = value;
            }
        }

        public override Vector3 Scale
        {
            get
            {
                return bepuEntityObject.Scale;
            }
            set
            {
                this.bepuEntityObject.Scale = value;
            }
        }

        public override Matrix Rotation
        {
            get
            {
                return this.bepuEntityObject.Rotation;
            }
            set
            {
                this.bepuEntityObject.Rotation = value;
            }
        }

        public override Vector3 FaceVector
        {
            get { return bepuEntityObject.FaceVector; }
        }

        public override Matrix WorldMatrix
        {
            get { return this.bepuEntityObject.WorldMatrix; }
        }

        public override Vector3 AngularVelocity
        {
            get { return this.bepuEntityObject.AngularVelocity; }
            set { this.bepuEntityObject.AngularVelocity = value; }
        }

        public override Vector3 Velocity
        {
            get
            {
                return this.bepuEntityObject.Velocity;
            }
            set
            {
                this.bepuEntityObject.Velocity = value;
            }
        }

        public override bool isMotionLess
        {
            get
            {
                return this.bepuEntityObject.isMotionLess;
            }
            set
            {
                this.bepuEntityObject.isMotionLess = value;
            }
        }

        public override IObject ObjectOwner
        {
            get
            {
                return this.bepuEntityObject.ObjectOwner;
            }
            set
            {
                this.bepuEntityObject.ObjectOwner = value;
            }
        }

        public override PhysicObjectTypes PhysicObjectTypes
        {
            get { return PhysicObjectTypes.SPECIALIZEDMOVER; }
        }

        
        public override void ApplyImpulse(Vector3 position, Vector3 force)
        {
            bepuEntityObject.ApplyImpulse(position,force);

        }

        public override BoundingBox? BoundingBox
        {
            get { return bepuEntityObject.BoundingBox; }
        }

#if WINDOWS
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            ActiveLogger.LogMessage("Serialization not implemented yet", LogLevel.RecoverableError);
        }
#endif

        #endregion
    }
}
