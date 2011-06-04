using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEPUphysics;
using BEPUphysics.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BEPUphysics.Entities;
using PloobsEngine.SceneControl;
using PloobsEngine.Modelo;
using BEPUphysics.UpdateableSystems;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Physics.Bepu
{
    public class DetectorVolumeObject : IPhysicObject
    {                
        IObject owner;
        TriangleMeshObject mesh;
        DetectorVolume detectorVolume;

        public DetectorVolume DetectorVolume
        {
            get { return detectorVolume; }
            set { detectorVolume = value; }
        }

        public DetectorVolumeObject(BepuPhysicWorld bepuPhysicWorld,TriangleMeshObject mesh) 
        {
            this.mesh = mesh;
            detectorVolume = new DetectorVolume(mesh.StaticMesh.Mesh.Data, bepuPhysicWorld.Space.BroadPhase.QueryAccelerator);            
        }

        public override PhysicObjectTypes PhysicObjectTypes
        {
            get { return PhysicObjectTypes.DETECTOROBJECT; }
        }

        #region IPhysicObject Members

        public override Vector3 Position
        {
            get
            {
                return mesh.WorldMatrix.Translation;
            }
            set
            {
                this.mesh.Position = value;
            }
        }

        public override Vector3 Scale
        {
            get
            {
                return this.mesh.Scale;
            }
            set
            {
                this.mesh.Scale = value;
            }
        }

        public override Matrix Rotation
        {
            get
            {
                return this.mesh.Rotation;
            }
            set
            {
                this.mesh.Rotation = value;
            }
        }

        public override Vector3 FaceVector
        {
            get { return this.mesh.FaceVector; }
        }

        public override Matrix WorldMatrix
        {
            get { return this.mesh.WorldMatrix; }
        }

        public override Vector3 AngularVelocity
        {
            get { return this.mesh.AngularVelocity; }
            set { this.mesh.AngularVelocity = value; }
        }

        public override Vector3 Velocity
        {
            get
            {
                return this.mesh.Velocity;
            }
            set
            {
                this.mesh.Velocity = value;
            }
        }

        public override bool isMotionLess
        {
            get
            {
                return this.mesh.isMotionLess;
            }
            set
            {
                this.mesh.isMotionLess = value;
            }
        }

        public override IObject ObjectOwner
        {
            get
            {
                return this.mesh.ObjectOwner;
            }
            set
            {
                this.mesh.ObjectOwner = value;
            }
        }

        public override void Enable()
        {
            this.mesh.Enable();
        }

        public override void Disable()
        {
            this.mesh.Disable();
        }


        public override BoundingBox BoundingBox
        {
            get { return this.mesh.BoundingBox; }
        }

        #endregion

        public override void ApplyImpulse(Vector3 position, Vector3 force)
        {
            ActiveLogger.LogMessage("Cant Apply Impulse to Detector object", LogLevel.Warning);
        }

        #if !WINDOWS_PHONE
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            ActiveLogger.LogMessage("Serializable not implemented", LogLevel.Warning);
        }
        #endif
    }
}
