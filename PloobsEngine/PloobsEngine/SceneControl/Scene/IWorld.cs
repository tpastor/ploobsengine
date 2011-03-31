using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Xml;
using PloobsEngine.Particle3D;
using PloobsEngine.Light;
using PloobsEngine.Cameras;
using PloobsEngine.Trigger;
using PloobsEngine.Physics;
using PloobsEngine.Audio;
using System.Runtime.Serialization;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Specification of a world
    /// </summary>
    public abstract class IWorld : ISerializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IWorld"/> class.
        /// </summary>
        /// <param name="PhysicWorld">The physic world.</param>
        /// <param name="ParticleManager">The particle manager.</param>
        /// <param name="Culler">The culler.</param>
        public IWorld(IPhysicWorld PhysicWorld, ParticleManager ParticleManager, ICuller Culler)
        {
            this.PhysicWorld = PhysicWorld;
            this.ParticleManager = ParticleManager;
            this.CameraManager = new CameraManager();
            this.Culler = Culler;
        }

        /// <summary>
        /// Gets the particle manager instance.
        /// </summary>
        public abstract ParticleManager ParticleManager
        {
            get;
            internal set;
        }

        /// <summary>
        /// Adds an object to the world.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public abstract int AddObject(IObject obj);

        /// <summary>
        /// Removes an object from the world.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public virtual void RemoveObject(IObject obj)
        {
            obj.RemoveThisObject();
        }

        /// <summary>
        /// Updates the world.
        /// </summary>
        /// <param name="gt">The gt.</param>
        protected abstract void UpdateWorld(GameTime gt);
        internal void iUpdateWorld(GameTime gt)
        {
            UpdateWorld(gt);
        }

        /// <summary>
        /// Contains the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public abstract bool Contain(IObject obj);        
        /// <summary>
        /// Adds the light.
        /// </summary>
        /// <param name="light">The light.</param>
        /// <returns></returns>
        public abstract int AddLight(ILight light);
        /// <summary>
        /// Removes the light.
        /// </summary>
        /// <param name="light">The light.</param>
        public abstract void RemoveLight(ILight light);
        /// <summary>
        /// Camera Managment
        /// </summary>
        public abstract CameraManager CameraManager { get; internal set; }
        /// <summary>
        /// Adds the trigger.
        /// </summary>
        /// <param name="trigger">The trigger.</param>
        public abstract void AddTrigger(ITrigger trigger);
        /// <summary>
        /// Removes the trigger.
        /// </summary>
        /// <param name="trigger">The trigger.</param>
        public abstract void RemoveTrigger(ITrigger trigger);
        /// <summary>
        /// Gets or sets the physic world.
        /// </summary>
        /// <value>
        /// The physic world.
        /// </value>
        public abstract IPhysicWorld PhysicWorld { get; internal set; }        

        /// <summary>
        /// Add a Dummy to the world
        /// Its like a position, 
        /// usefull to serializable position from a world editor
        /// </summary>
        /// <param name="dummy">The dummy.</param>
        public abstract void AddDummy(IDummy dummy);
        /// <summary>
        /// Removes the dummy.
        /// </summary>
        /// <param name="dummy">The dummy.</param>
        public abstract void RemoveDummy(IDummy dummy);
        /// <summary>
        /// Gets all the dummyes.
        /// </summary>
        /// <returns></returns>
        public abstract IList<IDummy> GetDummyes();
        
        /// <summary>
        /// Gets or sets the culler.
        /// </summary>
        /// <value>
        /// The culler.
        /// </value>
        public ICuller Culler
        {
            internal set;
            get;
        }
        
        /// <summary>
        /// Ray test
        /// </summary>
        /// <param name="raio">Ray do Xna</param>
        /// <param name="method">considerar ou nao objetos imoveis</param>
        /// <param name="maxDistance">0 para qualquer distancia</param>
        /// <returns></returns>
        public abstract RayTestInfo SegmentIntersect(Ray raio, SegmentInterceptMethod method, float maxDistance);
        /// <summary>
        /// Gets the lights.
        /// </summary>
        public abstract IList<ILight> Lights
        {
            get;
        }
        /// <summary>
        /// Gets the objects.
        /// </summary>
        public abstract IList<IObject> Objects
        {
            get;
        }

        /// <summary>
        /// Adds the sound emitter.
        /// </summary>
        /// <param name="em">The em.</param>
        public abstract void AddSoundEmitter(ISoundEmitter3D em);        
        /// <summary>
        /// Removes the sound emitter.
        /// </summary>
        /// <param name="e">The e.</param>
        public abstract void RemoveSoundEmitter(ISoundEmitter3D e);

        #region ISerializable Members

        public abstract void GetObjectData(SerializationInfo info, StreamingContext context);        

        #endregion
    }
}
