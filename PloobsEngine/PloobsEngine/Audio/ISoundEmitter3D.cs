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

namespace PloobsEngine.Audio
{
    /// <summary>
    /// Generic Sound 3D Emiter 
    /// </summary>
    public abstract class ISoundEmitter3D 
    {   
        
        /// <summary>
        /// Sets the emitter position.
        /// </summary>
        /// <param name="position">The position.</param>
        public abstract void setEmitterPosition(Vector3 position);
        /// <summary>
        /// Sets the listener position.
        /// </summary>
        /// <param name="position">The position.</param>
        public abstract void setListenerPosition(Vector3 position);
        /// <summary>
        /// Updates .
        /// </summary>
        /// <param name="gt">The gt.</param>
        protected abstract void Update(GameTime gt, ICamera camera);
        internal void iUpdate(GameTime gt,ICamera camera)
        {
            Update(gt,camera);
        }
        /// <summary>
        /// Pauses the sound
        /// </summary>
        public abstract void Pause();
        /// <summary>
        /// Resumes the sound
        /// </summary>
        public abstract void Resume();
        /// <summary>
        /// Plays the sound
        /// </summary>
        public abstract void Play();
        /// <summary>
        /// Stops the sound
        /// </summary>
        public abstract void Stop();
        /// <summary>
        /// Gets the Sound state.
        /// </summary>
        public abstract SoundState State
        {
            get;
        }        
    }
}
