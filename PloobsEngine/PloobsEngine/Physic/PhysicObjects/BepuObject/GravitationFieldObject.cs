using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using BEPUphysics.UpdateableSystems.ForceFields;
using PloobsEngine.Physics;

namespace PloobsEngine.Physic.PhysicObjects.BepuObject
{
    public class GravitationalFieldObject : ForceField
    {
        /// <summary>
        /// Creates a gravitational field.
        /// </summary>
        /// <param name="physicWorld">The physic world (cast the IWorld IphysicWorld property).</param>
        /// <param name="shape">Shape representing the volume of the force field.</param>
        /// <param name="origin">Location that entities will be pushed toward.</param>
        /// <param name="multiplier">Represents the gravitational constant of the field times the effective mass at the center of the field.</param>
        /// <param name="maxForce">Maximum force the field can apply.</param>
        public GravitationalFieldObject(BepuPhysicWorld physicWorld, ForceFieldShape shape, Vector3 origin, float multiplier, float maxForce)
            : base(shape, physicWorld.Space.BroadPhase.QueryAccelerator)
        {            
            
            Multiplier = multiplier;
            Origin = origin;
            MaxForce = maxForce;
        }

        /// <summary>
        /// Gets or sets the maximum force that can be applied by the field.
        /// </summary>
        public float MaxForce { get; set; }

        /// <summary>
        /// Gets or sets the gravitational constant of the field times the effective mass at the center of the field.
        /// </summary>
        public float Multiplier { get; set; }

        /// <summary>
        /// Gets or sets the center of the field that entities will be pushed toward.
        /// </summary>
        public Vector3 Origin { get; set; }


        /// <summary>
        /// Calculates the gravitational force to apply to the entity.
        /// </summary>
        /// <param name="e">Target of the impulse.</param>
        /// <param name="dt">Time since the last frame in simulation seconds.</param>
        /// <param name="impulse">Force to apply at the given position.</param>
        protected override void CalculateImpulse(BEPUphysics.Entities.Entity e, float dt, out Vector3 impulse)
        {
            //Could use a linear dropoff for a slightly faster calculation (divide by lengthSquared instead of length^3)
            //The third length power here is to normalize the direction r.
            Vector3 r = e.Position - Origin;
            float length = r.Length();
            float force = dt * Math.Min(MaxForce, Multiplier * e.Mass / (length * length * length));
            impulse = -force * r;
        }
    }
}
