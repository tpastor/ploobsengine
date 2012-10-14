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
using BEPUphysics;
using PloobsEngine.SceneControl;
using PloobsEngine.Modelo;
using BEPUphysics.MathExtensions;
using PloobsEngine.Engine.Logger;
using BEPUphysics.BroadPhaseSystems;
using BEPUphysics.Collidables;

namespace PloobsEngine.Physics.Bepu
{

    /// <summary>
    /// Base class for Bepu entities
    /// </summary>
    public abstract class BepuEntityObject : IPhysicObject
    {   
        /// <summary>
        /// Sets the material description.
        /// </summary>
        /// <param name="materialDescription">The material description.</param>
        public void SetMaterialDescription(MaterialDescription materialDescription)
        {
            this.materialDecription = materialDescription;
            entity.Material = new BEPUphysics.Materials.Material(materialDescription.StaticFriction, materialDescription.DynamicFriction, materialDescription.Bounciness);
        }

        /// <summary>
        /// Gets the material description.
        /// </summary>
        /// <returns></returns>
        public MaterialDescription GetMaterialDescription()
        {
            return materialDecription;
        }

        /// <summary>
        /// [Utility] Recovers the physic object from entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static IPhysicObject RecoverIPhysicObjectFromEntity(BEPUphysics.Entities.Entity entity)
        {
            return (entity.CollisionInformation.Tag as IPhysicObject);
        }

        /// <summary>
        /// [Utility] Recovers the object from entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static IObject RecoverObjectFromEntity(BEPUphysics.Entities.Entity entity)
        {
            IPhysicObject phy = (entity.CollisionInformation.Tag as IPhysicObject);
            if (phy != null)
                return phy.ObjectOwner;
            return null;
        }

        /// <summary>
        /// [Utility] Recovers the object from collidable.
        /// </summary>
        /// <param name="collidable">The collidable.</param>
        /// <returns></returns>
        public static IObject RecoverObjectFromCollidable(BEPUphysics.Collidables.Collidable collidable)
        {
            IPhysicObject phy = (collidable.Tag as IPhysicObject);
            if (phy != null)
                return phy.ObjectOwner;
            return null;
        }

        public static IPhysicObject RecoverIPhysicObjectFromCollidable(BEPUphysics.Collidables.Collidable collidable)
        {
            IPhysicObject phy = (collidable.Tag as IPhysicObject);
            if (phy != null)
                return phy;
            return null;
        }


        /// <summary>
        /// Recovers iobject from broad phase entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <returns></returns>
        public static IObject RecoverObjectFromBroadPhase(BroadPhaseEntry entry)
        {
            IPhysicObject phyObj = null;
            if (entry is Collidable)
            {
                Collidable collidable = (entry as Collidable);
                phyObj = collidable.Tag as IPhysicObject;
                return phyObj.ObjectOwner;
            }
            else
            {
                return null;
            }
            
        }

        /// <summary>
        /// Recovers physicobject from broad phase entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <returns></returns>
        public static IPhysicObject RecoverIPhysicObjectFromBroadPhase(BroadPhaseEntry entry)
        {
            IPhysicObject phyObj = null;
            if (entry is Collidable)
            {
                Collidable collidable = (entry as Collidable);
                phyObj = collidable.Tag as IPhysicObject;
            }
            return phyObj;
        }      
        


        /// <summary>
        /// Initializes a new instance of the <see cref="BepuEntityObject"/> class.
        /// </summary>
        /// <param name="md">The md.</param>
        /// <param name="mass">The mass.</param>
        public BepuEntityObject(MaterialDescription md, float mass)
        {
            if (md == null)
                md = MaterialDescription.DefaultBepuMaterial();
            this.materialDecription = md;
            this.mass = mass;            
        }

        public BepuEntityObject() : this(MaterialDescription.DefaultBepuMaterial(),0)
        {            
        }

        #region IPhysicObject Members

        protected MaterialDescription materialDecription;
        protected BEPUphysics.Entities.Entity entity;
        protected float mass;
        private IObject obj;
        protected Vector3 scale = Vector3.One;

        public void LimitRotationAxis(bool x, bool y, bool z)
        {
            Matrix3X3 m = entity.LocalInertiaTensorInverse;
            if (!x)
            {
                m.M11 = 0;
                m.M12 = 0;
                m.M13 = 0;             
            }
            if (!y)
            {
                m.M21 = 0;
                m.M22 = 0;
                m.M23 = 0;                
            }
            if (!z)
            {
                m.M31 = 0;
                m.M32 = 0;
                m.M33 = 0;                
            }
            entity.LocalInertiaTensorInverse = m;
        }

        /// <summary>
        /// Gets or sets the entity.
        /// </summary>
        /// <value>
        /// The entity.
        /// </value>
        public BEPUphysics.Entities.Entity Entity
        {
            get { return entity; }
            set { entity = value; }
        }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public override Vector3 Position
        {
            get
            {
                return entity.Position;
            }
            set
            {
                entity.Position = value;
            }
        }

        /// <summary>
        /// Gets or sets the scale.
        /// CARE USING THIS, the scale will be uniform to all axis
        /// </summary>
        /// <value>
        /// The scale.
        /// </value>
        public override Vector3 Scale
        {
            get
            {
                return scale;
            }
            set
            {
                ActiveLogger.LogMessage("Cant set Scale on the fly", LogLevel.Warning);
            }
        }

        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        /// <value>
        /// The rotation.
        /// </value>
        public override Matrix Rotation
        {
            get
            {
                return Matrix.CreateFromQuaternion(entity.Orientation);
            }
            set
            {
                entity.Orientation = Quaternion.CreateFromRotationMatrix(value);
            }
        }

        /// <summary>
        /// Vector pointing to the front
        /// </summary>
        public override Vector3 FaceVector
        {
            get {
                return Rotation.Forward;                
            }
        }

        /// <summary>
        /// Gets the world matrix.
        /// </summary>
        public override Matrix WorldMatrix
        {
            get 
            {                  
                return Matrix.CreateScale(scale) * entity.WorldTransform ;               
            }
        }

        /// <summary>
        /// Gets or sets the velocity.
        /// </summary>
        /// <value>
        /// The velocity.
        /// </value>
        public override Vector3 Velocity
        {
            get
            {
                return entity.LinearVelocity;
            }
            set
            {
                this.entity.LinearVelocity = value;
            }
        }



        public override Vector3 AngularVelocity
        {
            
            get { return entity.AngularVelocity; }
            set { this.entity.AngularVelocity = value; }


        }



        public override bool isMotionLess
        {
            get
            {
                return !entity.IsDynamic;
            }
            set
            {
                if (value == false)
                    this.entity.BecomeDynamic(mass);
                else
                    this.entity.BecomeKinematic();
            }
        }

        /// <summary>
        /// Gets or sets the IObject owner.
        /// </summary>
        /// <value>
        /// The IObject owner.
        /// </value>
        public override IObject ObjectOwner
        {
            get
            {
                return obj;
            }
            set
            {
                this.obj = value;
                this.obj.OnBeingRemoved += new BeingRemoved(obj_OnBeingRemoved);    
            }
        }

        void obj_OnBeingRemoved(IObject obj)
        {
            if (entity != null)
                entity.CollisionInformation.Events.RemoveAllEvents();
            this.obj.OnBeingRemoved -= obj_OnBeingRemoved;
            this.obj = null;
        }       
        

        public override void ApplyImpulse(Vector3 position, Vector3 force)
        {
            entity.ApplyImpulse(position, force);
        }

        /// <summary>
        /// Gets the bounding box IN WORLD COORDINATES
        /// </summary>
        public override BoundingBox? BoundingBox
        {
            get { return entity.CollisionInformation.BoundingBox; }
        }       

        #endregion
    }
}
