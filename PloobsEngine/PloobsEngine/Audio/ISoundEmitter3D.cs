using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Entity;
using PloobsEngine.MessageSystem;
using Microsoft.Xna.Framework.Audio;
using PloobsEngine.SceneControl;

namespace PloobsEngine.Audio
{
    /// <summary>
    /// Generic Sound 3D Emiter 
    /// </summary>
    public interface ISoundEmitter3D : IEntity, IRecieveMessageEntity
    {

        /// <summary>
        /// Gets or sets the world.
        /// </summary>
        /// <value>
        /// The world.
        /// </value>
        IWorld World {set; get;}
        
        /// <summary>
        /// Sets the emitter position.
        /// </summary>
        /// <param name="position">The position.</param>
        void setEmitterPosition(Vector3 position);
        /// <summary>
        /// Sets the listener position.
        /// </summary>
        /// <param name="position">The position.</param>
        void setListenerPosition(Vector3 position);
        /// <summary>
        /// Updates .
        /// </summary>
        /// <param name="gt">The gt.</param>
        void Update(GameTime gt);
        /// <summary>
        /// Pauses the sound
        /// </summary>
        void Pause();
        /// <summary>
        /// Resumes the sound
        /// </summary>
        void Resume();
        /// <summary>
        /// Plays the sound
        /// </summary>
        void Play();
        /// <summary>
        /// Stops the sound
        /// </summary>
        void Stop();
        /// <summary>
        /// Gets the Sound state.
        /// </summary>
        SoundState State
        {
            get;
        }
    
    }
}
