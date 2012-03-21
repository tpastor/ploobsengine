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
using PloobsEngine.Physic2D;
using PloobsEngine.Particles;
using PloobsEngine.Engine.Logger;
using System.Diagnostics;
using PloobsEngine.Engine;
using PloobsEngine.Entity;
using Microsoft.Xna.Framework;
using PloobsEngine.Audio;
using PloobsEngine.SceneControl._2DScene.Culler;

namespace PloobsEngine.SceneControl._2DScene
{
    /// <summary>
    /// 2D World
    /// </summary>
    public class I2DWorld
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IWorld"/> class.
        /// </summary>
        /// <param name="PhysicWorld">The physic world.</param>
        /// <param name="particleManager">The particle manager.</param>
        /// <param name="culler">The culler.</param>
        public I2DWorld(I2DPhysicWorld PhysicWorld, IParticleManager particleManager = null, I2DCuller culler = null)
        {
            if (PhysicWorld == null)
            {
                ActiveLogger.LogMessage("Physic World cannot be null", LogLevel.FatalError);
                Debug.Assert(PhysicWorld != null);
                throw new Exception("Physic World cannot be null");
            }

            if (culler == null)
            {
                this.culler = new Simple2DCuller();                
            }
            else
            {
                this.culler = culler;
            }
            this.culler.world = this;
            
            this.particleManager = particleManager;            
            this.PhysicWorld = PhysicWorld;                        
            Dummies = new List<IDummy>();            
            Objects = new List<I2DObject>();
            SoundEmiters2D = new List<ISoundEmitter2D>();
            CleanUpObjectsOnDispose = true;


#if !WINDOWS_PHONE && !REACH
            Lights2D = new List<PloobsEngine.Light2D.Light2D>();
#endif

        }

        protected I2DCuller culler;
        protected GraphicInfo graphicsInfo;
        protected GraphicFactory graphicsFactory;
        protected IContentManager contentManager;
        protected IParticleManager particleManager;
        protected ICamera2D camera2D = null;

        /// <summary>
        /// Gets or sets the camera2D.
        /// </summary>
        /// <value>
        /// The camera2D.
        /// </value>
        public ICamera2D Camera2D
        {
            get { return    camera2D; }
            set { camera2D = value; }
        }

        /// <summary>
        /// Gets the particle manager.
        /// </summary>
        public IParticleManager ParticleManager
        {
            get {                
                return particleManager;
            }            
        }

        /// <summary>
        /// Inits the world.
        /// </summary>
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
#if !WINDOWS_PHONE && !REACH

        /// <summary>
        /// Adds the light.
        /// </summary>
        /// <param name="Light">The light.</param>
        public void AddLight(PloobsEngine.Light2D.Light2D Light)
        {
            System.Diagnostics.Debug.Assert(Light != null);
            Light.RenderTarget = graphicsFactory.CreateRenderTarget(Light.baseSize, Light.baseSize);
            Lights2D.Add(Light);
        }

        /// <summary>
        /// Removes the light.
        /// </summary>
        /// <param name="Light">The light.</param>
        public void RemoveLight(PloobsEngine.Light2D.Light2D Light)
        {
            Light.RenderTarget = null;
            Lights2D.Remove(Light);
        }

        /// <summary>
        /// Gets the lights 2D.
        /// </summary>
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

        /// <summary>
        /// Gets the content manager.
        /// </summary>
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
            culler.onObjectAdded(obj);

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
                culler.onObjectRemoved(obj);
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

            foreach (ISoundEmitter2D item in SoundEmiters2D)
            {
                item.iUpdate(gt, camera2D);
            }


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
        /// Gets or sets a value indicating whether [clean up objects on dispose].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [clean up objects on dispose]; otherwise, <c>false</c>.
        /// </value>
        public bool CleanUpObjectsOnDispose
        {
            set;
            get;
        }


        /// <summary>
        /// Gets the culler.
        /// </summary>
        public I2DCuller Culler
        {
            get
            {
                return culler;
            }
        }

        /// <summary>
        /// Gets the objects.
        /// </summary>
        public IList<I2DObject> Objects
        {
            get;
            protected set;
        }

        

        /// <summary>
        /// Gets the objects.
        /// </summary>
        public List<ISoundEmitter2D> SoundEmiters2D
        {
            get;
            protected set;
        }


        /// <summary>
        /// Adds the sound emitter.
        /// </summary>
        /// <param name="em">The em.</param>
        /// <param name="play">if set to <c>true</c> [play].</param>
        public virtual void AddSoundEmitter(ISoundEmitter2D em, bool play = false)
        {
            if (em == null)
            {
                ActiveLogger.LogMessage("Emitter is Null " + em.ToString(), LogLevel.RecoverableError);
                return;
            }

            SoundEmiters2D.Add(em);
            em.Apply3D();
            if (play)
                em.Play();
        }

        /// <summary>
        /// Removes the sound emitter.
        /// </summary>
        /// <param name="em">The em.</param>
        public virtual void RemoveSoundEmitter(ISoundEmitter2D em)
        {
            if (em == null)
            {
                ActiveLogger.LogMessage("Emitter is Null " + em.ToString(), LogLevel.RecoverableError);
                return;
            }
            em.Stop();
            bool resp = SoundEmiters2D.Remove(em);
            if (!resp)
            {
                ActiveLogger.LogMessage("Emitter not found: " + em.ToString(), LogLevel.Warning);
            }
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        public virtual void CleanUp()
        {
            Camera2D.CleanUp();

            foreach (var item in Objects.ToArray())
            {
                this.RemoveObject(item);
            }
            
            if (CleanUpObjectsOnDispose)
            {
                foreach (var item in Objects)
                {
                    item.CleanUp(graphicsFactory);
                }
            }

            

            foreach (var item in SoundEmiters2D.ToArray())
            {
                this.RemoveSoundEmitter(item);
            }
            

            foreach (var item in SoundEmiters2D)
            {
                item.CleanUp(graphicsFactory);
            }

            Objects.Clear();
            Camera2D = null;
            Dummies.Clear();
            SoundEmiters2D.Clear();
            particleManager = null;
            PhysicWorld = null;
            this.culler = null;
        }

    }
}
