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

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            ActiveLogger.LogMessage("Serialization not implemented yet", LogLevel.RecoverableError);
        }
    }
}
