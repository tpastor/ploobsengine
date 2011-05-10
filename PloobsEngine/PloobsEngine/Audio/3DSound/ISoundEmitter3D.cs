using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Entity;
using PloobsEngine.MessageSystem;
using Microsoft.Xna.Framework.Audio;
using PloobsEngine.SceneControl;
using PloobsEngine.Cameras;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Audio
{
    /// <summary>
    /// Generic Sound 3D Emiter 
    /// </summary>
    public abstract class ISoundEmitter3D 
    {
        public ISoundEmitter3D(IContentManager cmanager,String SoundName)
        {
            System.Diagnostics.Debug.Assert(cmanager != null);
            System.Diagnostics.Debug.Assert(!String.IsNullOrEmpty(SoundName));
            SoundEffect se = cmanager.GetAsset<SoundEffect>(SoundName);
            soundEngineInstance = se.CreateInstance();
            Name = se.Name;
            Duration = se.Duration;
            listener = new AudioListener();
            emiter = new AudioEmitter();
        }

#if DEBUG
        bool isAdded = false;
#endif
        internal void Apply3D()
        {
#if DEBUG
            isAdded = true;
#endif

            soundEngineInstance.Apply3D(listener, emiter);
        }

        public TimeSpan Duration
        {
            get;
            internal set;
        }

        public String Name
        {
            get;
            internal set;
        }

        protected SoundEffectInstance soundEngineInstance;
        protected AudioEmitter emiter;
        protected AudioListener listener;

        public SoundEffectInstance SoundEngineInstance
        {
            get { return soundEngineInstance; }            
        }        

        public AudioListener Listener
        {
            get { return listener; }            
        }

        public AudioEmitter Emiter
        {
            get { return emiter; }            
        }

        /// <summary>
        /// Updates .
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="camera">The camera.</param>
        protected abstract void Update(GameTime gt, ICamera camera);
        internal void iUpdate(GameTime gt,ICamera camera)
        {
            Update(gt,camera);
        }
        /// <summary>
        /// Pauses the sound
        /// </summary>
        public virtual void Pause()
        {
#if DEBUG
            if (!isAdded)
            {
                ActiveLogger.LogMessage("Cant Play/Stop/Pause/Resume a 3D Sound before adding it to the world, Action ignored", LogLevel.RecoverableError);
                return;
            }
#endif

            soundEngineInstance.Pause();
        }
        /// <summary>
        /// Resumes the sound
        /// </summary>
        public virtual void Resume()
        {
#if DEBUG
            if (!isAdded)
            {
                ActiveLogger.LogMessage("Cant Play/Stop/Pause/Resume a 3D Sound before adding it to the world, Action ignored", LogLevel.RecoverableError);
                return;
            }
#endif

            soundEngineInstance.Resume();
        }
        /// <summary>
        /// Plays the sound
        /// </summary>
        public virtual void Play()
        {
#if DEBUG
            if (!isAdded)
            {
                ActiveLogger.LogMessage("Cant Play/Stop/Pause/Resume a 3D Sound before adding it to the world, Action ignored", LogLevel.RecoverableError);
                return;
            }
#endif

            soundEngineInstance.Play();
        }
        /// <summary>
        /// Stops the sound
        /// </summary>
        public virtual void Stop(bool imediately = true)
        {
#if DEBUG
            if (!isAdded)
            {
                ActiveLogger.LogMessage("Cant Play/Stop/Pause/Resume a 3D Sound before adding it to the world, Action ignored", LogLevel.RecoverableError);
                return;
            }
#endif

            soundEngineInstance.Stop(imediately);
        }
        /// <summary>
        /// Gets the Sound state.
        /// </summary>
        public virtual SoundState State
        {
            get
            {
                return soundEngineInstance.State;
            }
        }        
    }
}
