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

namespace PloobsEngine.Physics.Bepu
{

    /// <summary>
    /// Base class for Bepu entities
    /// </summary>
    public abstract class BepuEntityObject : IPhysicObject
    {        

        /// <summary>
        /// [Utility] Recovers the object from entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static IObject RecoverObjectFromEntity(BEPUphysics.Entities.Entity entity)
        {
            IPhysicObject phy = (entity.Tag as IPhysicObject);
            if (phy != null)
                return phy.ObjectOwner;
            return null;
        }

        /// <summary>
        /// Sets the material description.
        /// </summary>
        /// <param name="materialDescription">The material description.</param>
        public void SetMaterialDescription(MaterialDescription materialDescription)
        {
            this.materialDecription = materialDescription;
            entity.Material = new BEPUphysics.Materials.Material(materialDescription.StaticFriction, materialDescription.DinamicFriction, materialDescription.Bounciness);
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
            return (entity.Tag as IPhysicObject);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="BepuEntityObject"/> class.
        /// </summary>
        /// <param name="md">The md.</param>
        /// <param name="mass">The mass.</param>
        public BepuEntityObject(MaterialDescription md, float mass)
        {
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

        public BEPUphysics.Entities.Entity Entity
        {
            get { return entity; }
            set { entity = value; }
        }

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

        public override Vector3 FaceVector
        {
            get {
                return Rotation.Forward;                
            }
        }

        public override Matrix WorldMatrix
        {
            get 
            {                  
                return Matrix.CreateScale(scale) * entity.WorldTransform ;               
            }
        }

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
        }
        
        public override void Enable()
        {
            entity.IsActive = true;
        }

        public override void Disable()
        {
            entity.IsActive = false;
        }

        public override void ApplyImpulse(Vector3 position, Vector3 force)
        {
            entity.ApplyImpulse(position, force);
        }

        public override BoundingBox BoundingBox
        {
            get { return entity.CollisionInformation.BoundingBox; }
        }       

        #endregion
    }
}
