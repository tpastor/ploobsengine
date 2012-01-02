using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using PloobsEngine.Engine.Logger;
using PloobsEngine.Entity;
using PloobsEngine.Physics;
using System.Diagnostics;
using PloobsEngine.Cameras;
using PloobsEngine.Light;
using PloobsEngine.Trigger;
using PloobsEngine.Audio;

namespace PloobsEngine.NetWorking
{
    public class NetworkServerWorld : IWorld
    {
#if WINDOWS
        /// <summary>
        /// Initializes a new instance of the <see cref="IWorld"/> class.
        /// </summary>
        /// <param name="PhysicWorld">The physic world.</param>
        /// <param name="multiThread">if set to <c>true</c> [mult thread].</param>
        public NetworkServerWorld(IPhysicWorld PhysicWorld,bool multiThread = false)
#else
        public IWorld(IPhysicWorld PhysicWorld)
#endif
        {
            if (PhysicWorld == null)
            {
                ActiveLogger.LogMessage("Physic World cannot be null", LogLevel.FatalError);
                Debug.Assert(PhysicWorld != null);
                throw new Exception("Physic World cannot be null");
            }
            
            this.particleManager = particleManager;            
            this.PhysicWorld = PhysicWorld;            
            this.CameraManager = new CameraManager();            
            Dummies = new List<IDummy>();
            Lights = new List<ILight>();
            Objects = new List<IObject>();
            Triggers = new List<ITrigger>();
            SoundEmiters3D = new List<ISoundEmitter3D>();            
#if WINDOWS
            this.multThreading = multiThread;
#endif
        }

        public override void AddObject(IObject obj)
        {
            if (obj == null)
            {
                ActiveLogger.LogMessage("Cant add null obj", LogLevel.RecoverableError);
                return;
            }

            EntityMapper.getInstance().AddEntity(obj);
            PhysicWorld.AddObject(obj.PhysicObject);
            obj.PhysicObject.ObjectOwner = obj;
            Objects.Add(obj);            

        }

        
        public override void RemoveObject(IObject obj)
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
        }

        public void Update(Microsoft.Xna.Framework.GameTime gt)
        {
            UpdateWorld(gt);
        }

        protected override void UpdateWorld(Microsoft.Xna.Framework.GameTime gt)
        {
            PhysicWorld.iUpdate(gt);
        }

        public override void AddSoundEmitter(Audio.ISoundEmitter3D em, bool play = false)
        {
            if (em == null)
            {
                ActiveLogger.LogMessage("Emitter is Null " + em.ToString(), LogLevel.RecoverableError);
                return;
            }
            SoundEmiters3D.Add(em);            
        }
        public override void RemoveSoundEmitter(Audio.ISoundEmitter3D em)
        {
            if (em == null)
            {
                ActiveLogger.LogMessage("Emitter is Null " + em.ToString(), LogLevel.RecoverableError);
                return;
            }            
            bool resp = SoundEmiters3D.Remove(em);
            if (!resp)
            {
                ActiveLogger.LogMessage("Emitter not found: " + em.ToString(), LogLevel.Warning);
            }
        }

    }
}
