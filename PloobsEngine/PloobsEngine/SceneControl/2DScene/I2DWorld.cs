using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Physics2D;
using PloobsEngine.Particles;
using PloobsEngine.Engine.Logger;
using System.Diagnostics;
using PloobsEngine.Engine;
using PloobsEngine.Entity;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl._2DScene
{
    public class I2DWorld
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IWorld"/> class.
        /// </summary>
        /// <param name="PhysicWorld">The physic world.</param>
        /// <param name="particleManager">The particle manager.</param>
        public I2DWorld(I2DPhysicWorld PhysicWorld, IParticleManager particleManager = null)
        {
            if (PhysicWorld == null)
            {
                ActiveLogger.LogMessage("Physic World cannot be null", LogLevel.FatalError);
                Debug.Assert(PhysicWorld != null);
                throw new Exception("Physic World cannot be null");
            }
            
            this.particleManager = particleManager;            
            this.PhysicWorld = PhysicWorld;                        
            Dummies = new List<IDummy>();            
            Objects = new List<I2DObject>();
            MaterialSortedObjects = new Dictionary<Type, List<I2DObject>>();
            
#if !WINDOWS_PHONE
            Lights2D = new List<PloobsEngine.Light2D.Light2D>();
#endif

        }
        
        protected GraphicInfo graphicsInfo;
        protected GraphicFactory graphicsFactory;
        protected IContentManager contentManager;
        protected IParticleManager particleManager;
        protected ICamera2D camera2D = null;

        public ICamera2D Camera2D
        {
            get { return camera2D; }
            set { camera2D = value; }
        }

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
            if (camera2D == null)
            {
                camera2D = new Camera2D(graphicsInfo);
            }
        }
#if !WINDOWS_PHONE

        public void AddLight(PloobsEngine.Light2D.Light2D Light)
        {
            Light.RenderTarget = graphicsFactory.CreateRenderTarget(Light.baseSize, Light.baseSize);
            Lights2D.Add(Light);
        }

        public void Remove(PloobsEngine.Light2D.Light2D Light)
        {
            Light.RenderTarget = null;
            Lights2D.Remove(Light);
        }

        public List<PloobsEngine.Light2D.Light2D> Lights2D
        {
            get;
            internal set;
        }
#endif


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
        public virtual void AddObject(I2DObject obj)
        {
            if (obj == null)
            {
                ActiveLogger.LogMessage("Cant add null obj", LogLevel.RecoverableError);
                return ;
            }

            EntityMapper.getInstance().AddEntity(obj);
            obj.Material.Initialization(GraphicsInfo, GraphicsFactory, obj);
            PhysicWorld.AddObject(obj.PhysicObject);
            obj.PhysicObject.Owner = obj;
            Objects.Add(obj);

            if (!MaterialSortedObjects.ContainsKey(obj.Material.GetType()))
            {
                MaterialSortedObjects[obj.Material.GetType()] = new List<I2DObject>();
            }
            MaterialSortedObjects[obj.Material.GetType()].Add(obj);
        }

        /// <summary>
        /// Contains the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public virtual bool ContainsObject(I2DObject obj)
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
        public virtual void RemoveObject(I2DObject obj)
        {
            if (obj == null)
            {
                ActiveLogger.LogMessage("Cant remove with null obj", LogLevel.RecoverableError);
                return;
            }
            obj.RemoveThisObject();
            EntityMapper.getInstance().RemoveEntity(obj);
            PhysicWorld.RemoveObject(obj.PhysicObject);
            obj.PhysicObject.Owner = null;            
            bool resp = Objects.Remove(obj);
            if (!resp)
            {
                ActiveLogger.LogMessage("Cant remove (not found) obj: " + obj.Name, LogLevel.RecoverableError);
            }
            else
            {
                MaterialSortedObjects[obj.Material.GetType()].Remove(obj);
            }

        }

        /// <summary>
        /// Updates the world.
        /// </summary>
        /// <param name="gt">The gt.</param>
        protected virtual void UpdateWorld(GameTime gt)
        {

            camera2D.Update(gt);

            PhysicWorld.iUpdate(gt);
            
            I2DObject[] toPass = Objects.ToArray();
            for (int i = 0; i < toPass.Count(); i++)            
            {
                toPass[i].iUpdateObject(gt);                
            }

            if(ParticleManager!= null)
                ParticleManager.iUpdate2D(gt, camera2D.View, camera2D.SimProjection);
        }
        internal void iUpdateWorld(GameTime gt)
        {
            UpdateWorld(gt);
        }
        
        
        /// <summary>
        /// Gets or sets the physic world.
        /// </summary>
        /// <value>
        /// The physic world.
        /// </value>
        public I2DPhysicWorld PhysicWorld { get; internal set; }        

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
        /// Gets the objects.
        /// </summary>
        public IList<I2DObject> Objects
        {
            get;
            protected set;
        }

        public Dictionary<Type, List<I2DObject>> MaterialSortedObjects
        {
            get;
            protected set;
        }       


    }
}
