using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Xml;
using PloobsEngine.Light;
using PloobsEngine.Cameras;
using PloobsEngine.Trigger;
using PloobsEngine.Physics;
using PloobsEngine.Audio;
using System.Runtime.Serialization;
using System.Diagnostics;
using PloobsEngine.Engine.Logger;
using PloobsEngine.Entity;
using PloobsEngine.Engine;
using PloobsEngine.Particles;
using System;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Specification of a world
    /// </summary>
    #if !WINDOWS_PHONE
    public class IWorld : ISerializable
#else
    public class IWorld 
#endif
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IWorld"/> class.
        /// </summary>
        /// <param name="PhysicWorld">The physic world.</param>
        /// <param name="Culler">The culler.</param>
        /// <param name="particleManager">The particle manager.</param>
        public IWorld(IPhysicWorld PhysicWorld, ICuller Culler, IParticleManager particleManager = null)
        {
            if (PhysicWorld == null)
            {
                ActiveLogger.LogMessage("Physic World cannot be null", LogLevel.FatalError);
                Debug.Assert(PhysicWorld != null);
                throw new Exception("Physic World cannot be null");
            }
            if (Culler == null)
            {
                ActiveLogger.LogMessage("Culler cannot be null", LogLevel.FatalError);
                Debug.Assert(Culler != null);
                throw new Exception("Culler cannot be null");
            }

            this.particleManager = particleManager;            
            this.PhysicWorld = PhysicWorld;            
            this.CameraManager = new CameraManager();            
            Dummies = new List<IDummy>();
            Lights = new List<ILight>();
            Objects = new List<IObject>();
            Triggers = new List<ITrigger>();
            SoundEmiters3D = new List<ISoundEmitter3D>();
            this.Culler = Culler;
            this.culler.world = this;
        }

        #if !WINDOWS_PHONE
        /// <summary>
        /// Initializes a new instance of the <see cref="IWorld"/> class.
        /// Desserialization
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        internal IWorld(SerializationInfo info, StreamingContext context)
        {
        }
        #endif

        protected ICuller culler;
        protected GraphicInfo graphicsInfo;
        protected GraphicFactory graphicsFactory;
        protected IContentManager contentManager;
        protected IParticleManager particleManager;

        public IParticleManager ParticleManager
        {
            get {                
                return particleManager;
            }            
        }

        protected virtual void InitWorld()
        {
            if (particleManager != null)
            {
                particleManager.GraphicInfo = graphicsInfo;
                particleManager.GraphicFactory = graphicsFactory;
            }
        }
        internal void iInitWorld()
        {
            InitWorld();
        }

        public IContentManager ContentManager
        {
            get
            {
                return contentManager;
            }
            internal set
            {
                contentManager = value;
            }
        }

        /// <summary>
        /// Gets or sets the graphics info.
        /// </summary>
        /// <value>
        /// The graphics info.
        /// </value>
        internal GraphicInfo GraphicsInfo
        {
            get { return graphicsInfo; }
            set { graphicsInfo = value; }
        }

        /// <summary>
        /// Gets or sets the graphics factory.
        /// </summary>
        /// <value>
        /// The graphics factory.
        /// </value>
        internal GraphicFactory GraphicsFactory
        {
            get { return graphicsFactory; }
            set { graphicsFactory = value; }
        }
                


        /// <summary>
        /// Adds an object to the world.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public virtual void AddObject(IObject obj)
        {
            if (obj == null)
            {
                ActiveLogger.LogMessage("Cant add null obj", LogLevel.RecoverableError);
                return ;
            }

            EntityMapper.getInstance().AddEntity(obj);
            PhysicWorld.AddObject(obj.PhysicObject);
            obj.PhysicObject.ObjectOwner = obj;
            Objects.Add(obj);            
            obj.Material.Initialization(graphicsInfo, graphicsFactory, obj);
            Culler.onObjectAdded(obj);
        }

        /// <summary>
        /// Contains the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public virtual bool ContainsObject(IObject obj)
        {
            if (obj == null)
            {
                ActiveLogger.LogMessage("Cant compare with null obj", LogLevel.RecoverableError);
                return false;
            }
            return Objects.Contains(obj);
        }

        /// <summary>
        /// Removes an object from the world.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public virtual void RemoveObject(IObject obj)
        {
            if (obj == null)
            {
                ActiveLogger.LogMessage("Cant remove with null obj", LogLevel.RecoverableError);
                return;
            }
            obj.RemoveThisObject();
            EntityMapper.getInstance().RemoveEntity(obj);
            PhysicWorld.RemoveObject(obj.PhysicObject);            
            bool resp = Objects.Remove(obj);
            if (!resp)
            {
                ActiveLogger.LogMessage("Cant remove (not found) obj: " + obj.Name, LogLevel.RecoverableError);
            }
            Culler.onObjectRemoved(obj);

        }

        /// <summary>
        /// Updates the world.
        /// </summary>
        /// <param name="gt">The gt.</param>
        protected virtual void UpdateWorld(GameTime gt)
        {
            PhysicWorld.iUpdate(gt);

            ///critical code, no logging here, just the assert. (not present in the release)
            Debug.Assert(CameraManager.ActiveCamera != null);
            CameraManager.ActiveCamera.iUpdate(gt);

            IObject[] toPass = Objects.ToArray();
            for (int i = 0; i < toPass.Count(); i++)            
            {
                toPass[i].iUpdateObject(gt, CameraManager.ActiveCamera, Lights);                
            }

            if(ParticleManager!= null)
                ParticleManager.iUpdate3D(gt, CameraManager.ActiveCamera.View, CameraManager.ActiveCamera.Projection, CameraManager.ActiveCamera.Position);

            foreach (ISoundEmitter3D item in SoundEmiters3D)
            {
                item.iUpdate(gt,CameraManager.ActiveCamera);                
            }


        }
        internal void iUpdateWorld(GameTime gt)
        {
            UpdateWorld(gt);
        }
        
        /// <summary>
        /// Adds the light.
        /// </summary>
        /// <param name="light">The light.</param>
        public virtual void AddLight(ILight light)
        {
            if (light == null)
            {
                ActiveLogger.LogMessage("Cant Add null Light", LogLevel.RecoverableError);
                return;
            }
            Lights.Add(light);
        }
        /// <summary>
        /// Removes the light.
        /// </summary>
        /// <param name="light">The light.</param>
        public virtual void RemoveLight(ILight light)
        {
            if (light == null)
            {
                ActiveLogger.LogMessage("Cant remove null Light", LogLevel.RecoverableError);
                return;
            }
            bool resp = Lights.Remove(light);
            if (!resp)
            {
                ActiveLogger.LogMessage("light not found: " + light.Name, LogLevel.Warning);
            }
        }
        /// <summary>
        /// Camera Managment
        /// </summary>
        public CameraManager CameraManager { get; internal set; }

        public IList<ITrigger> Triggers
        {
            internal set;
            get;
        }

        /// <summary>
        /// Adds the trigger.
        /// </summary>
        /// <param name="trigger">The trigger.</param>
        public virtual void AddTrigger(ITrigger trigger)
        {
            if (trigger == null)
            {
                ActiveLogger.LogMessage("Cant Add null Trigger", LogLevel.RecoverableError);
                return;
            }

            if (String.IsNullOrEmpty(trigger.Name))
            {
                ActiveLogger.LogMessage("Trigger with no Name", LogLevel.Warning);
            }

            Triggers.Add(trigger);            

            if (trigger.GhostObject != null)
                PhysicWorld.AddObject(trigger.GhostObject);

        }
        /// <summary>
        /// Removes the trigger.
        /// </summary>
        /// <param name="trigger">The trigger.</param>
        public virtual void RemoveTrigger(ITrigger trigger)
        {
            if (trigger == null)
            {
                ActiveLogger.LogMessage("Cant Remove Null Trigger", LogLevel.RecoverableError);
                return;                
            }
            bool resp = Triggers.Remove(trigger);
            if (!resp)
            {
                ActiveLogger.LogMessage("Trigger not found: " + trigger.Name, LogLevel.Warning);
            }

            if (trigger.GhostObject != null)
                PhysicWorld.RemoveObject(trigger.GhostObject);
        }
        /// <summary>
        /// Gets or sets the physic world.
        /// </summary>
        /// <value>
        /// The physic world.
        /// </value>
        public IPhysicWorld PhysicWorld { get; internal set; }        

        /// <summary>
        /// Add a Dummy to the world
        /// Its like a position, 
        /// usefull to serializable position from a world editor
        /// </summary>
        /// <param name="dummy">The dummy.</param>
        public virtual void AddDummy(IDummy dummy)
        {
            if (dummy == null)
            {
                ActiveLogger.LogMessage("Cant Add Null dummy", LogLevel.RecoverableError);
                return;
            }

            if (String.IsNullOrEmpty(dummy.Name))
            {
                ActiveLogger.LogMessage("Dummy with no Name", LogLevel.Warning);
            }
            Dummies.Add(dummy);
        }
        /// <summary>
        /// Removes the dummy.
        /// </summary>
        /// <param name="dummy">The dummy.</param>
        public virtual void RemoveDummy(IDummy dummy)
        {
            if (dummy == null)
            {
                ActiveLogger.LogMessage("Cant Remove Null dummy", LogLevel.RecoverableError);
                return;                
            }
            bool resp = Dummies.Remove(dummy);
            if (!resp)
            {
                ActiveLogger.LogMessage("Dummy not found: " + dummy.Name, LogLevel.Warning);
            }
        }
        /// <summary>
        /// Gets all the dummyes.
        /// </summary>
        /// <returns></returns>
        public IList<IDummy> Dummies
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the lights.
        /// </summary>
        public IList<ILight> Lights
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets the culler.
        /// </summary>
        /// <value>
        /// The culler.
        /// </value>
        public ICuller Culler
        {
            set
            {
                if (value == null)
                {
                    ActiveLogger.LogMessage("Cant add null culler", LogLevel.FatalError);
                    Debug.Assert(value != null);
                    throw new Exception("Cant add null culler");
                }

                this.culler = value;
                foreach (var item in Objects)
                {
                    this.culler.onObjectAdded(item);
                }
            }
            get
            {
                return culler;
            }
        }        
        
        /// <summary>
        /// Gets the objects.
        /// </summary>
        public IList<IObject> Objects
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the objects.
        /// </summary>
        public List<ISoundEmitter3D> SoundEmiters3D
        {
            get;
            protected set;
        }


        /// <summary>
        /// Adds the sound emitter.
        /// </summary>
        /// <param name="em">The em.</param>
        /// <param name="play">if set to <c>true</c> [play].</param>
        public virtual void AddSoundEmitter(ISoundEmitter3D em, bool play = false)
        {
            if (em == null)
            {
                ActiveLogger.LogMessage("Emitter is Null " + em.ToString(), LogLevel.RecoverableError);
                return;
            }
            
            SoundEmiters3D.Add(em);
            em.Apply3D();
            if(play)
                em.Play();
        }

        /// <summary>
        /// Removes the sound emitter.
        /// </summary>
        /// <param name="e">The e.</param>
        public virtual void RemoveSoundEmitter(ISoundEmitter3D em)
        {
            if (em == null)
            {
                ActiveLogger.LogMessage("Emitter is Null " + em.ToString(), LogLevel.RecoverableError);
                return;
            }
            em.Stop();
            bool resp = SoundEmiters3D.Remove(em);
            if (!resp)
            {
                ActiveLogger.LogMessage("Emitter not found: " + em.ToString(), LogLevel.Warning);
            }

        }

        #region ISerializable Members
        #if !WINDOWS_PHONE
        /// <summary>
        /// TODO
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext"/>) for this serialization.</param>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ActiveLogger.LogMessage("Serialization not implemented", LogLevel.RecoverableError);
            //info.AddValue("Objects", Objects, Objects.GetType());
            //info.AddValue("Lights", Lights, Lights.GetType());
            //info.AddValue("Triggers", Triggers, Triggers.GetType());
            //info.AddValue("SoundEmitter3D", SoundEmiters3D, SoundEmiters3D.GetType());
            //info.AddValue("Culler", Culler, Culler.GetType());
            //info.AddValue("PhysicWorld", PhysicWorld, PhysicWorld.GetType());
        }        

        #endif
        #endregion
    }
}

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      