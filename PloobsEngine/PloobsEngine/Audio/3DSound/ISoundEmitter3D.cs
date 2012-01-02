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
using Microsoft.Xna.Framework;
using PloobsEngine.Entity;
using PloobsEngine.MessageSystem;
using Microsoft.Xna.Framework.Audio;
using PloobsEngine.SceneControl;
using PloobsEngine.Cameras;
using PloobsEngine.Engine.Logger;
using PloobsEngine.Engine;

namespace PloobsEngine.Audio
{
    /// <summary>
    /// Generic Sound 3D Emiter 
    /// </summary>
    public abstract class ISoundEmitter3D 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ISoundEmitter3D"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="SoundName">Name of the sound.</param>
        public ISoundEmitter3D(GraphicFactory factory,String SoundName)
        {
            System.Diagnostics.Debug.Assert(factory != null);
            System.Diagnostics.Debug.Assert(!String.IsNullOrEmpty(SoundName));

            this.internalName = SoundName;
            SoundEffect se = factory.GetAsset<SoundEffect>(SoundName);                        
            soundEngineInstance = se.CreateInstance();

            Name = se.Name;
            Duration = se.Duration;
            listener = new AudioListener();
            emiter = new AudioEmitter();
        }
        String internalName;

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

        /// <summary>
        /// Cleans up.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="removeSoundGeneratorAlso">if set to <c>true</c> [remove sound generator also].</param>
        public void CleanUp(GraphicFactory factory, bool removeSoundGeneratorAlso = true)
        {
            if (removeSoundGeneratorAlso)
            {
                factory.ReleaseAsset(internalName);
            }
            soundEngineInstance.Dispose();
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
