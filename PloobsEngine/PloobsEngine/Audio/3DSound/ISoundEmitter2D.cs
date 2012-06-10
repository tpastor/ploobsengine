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
using PloobsEngine.SceneControl._2DScene;
using PloobsEngine.Engine;

namespace PloobsEngine.Audio
{
    /// <summary>
    /// Generic Sound 2D Emiter 
    /// </summary>
    public abstract class ISoundEmitter2D 
    {
        String internalName;
        public ISoundEmitter2D(GraphicFactory cmanager, String SoundName)
        {
            System.Diagnostics.Debug.Assert(cmanager != null);
            System.Diagnostics.Debug.Assert(!String.IsNullOrEmpty(SoundName));

            this.internalName = SoundName;
            SoundEffect se = cmanager.GetAsset<SoundEffect>(SoundName);                        
            soundEngineInstance = se.CreateInstance();

            Name = se.Name;
            Duration = se.Duration;
            listener = new AudioListener();
            emiter = new AudioEmitter();
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
        /// Gets the duration.
        /// </summary>
        public TimeSpan Duration
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public String Name
        {
            get;
            internal set;
        }

        /// <summary>
        /// SoundEffectInstance soundEngineInstance
        /// </summary>
        protected SoundEffectInstance soundEngineInstance;
        /// <summary>
        /// AudioEmitter emiter
        /// </summary>
        protected AudioEmitter emiter;
        /// <summary>
        /// AudioListener listener
        /// </summary>
        protected AudioListener listener;

        /// <summary>
        /// Gets the sound engine instance.
        /// </summary>
        public SoundEffectInstance SoundEngineInstance
        {
            get { return soundEngineInstance; }            
        }

        /// <summary>
        /// Gets the listener.
        /// </summary>
        public AudioListener Listener
        {
            get { return listener; }            
        }

        /// <summary>
        /// Gets the emiter.
        /// </summary>
        public AudioEmitter Emiter
        {
            get { return emiter; }            
        }

        /// <summary>
        /// Updates .
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="camera">The camera.</param>
        protected abstract void Update(GameTime gt, ICamera2D camera);
        internal void iUpdate(GameTime gt, ICamera2D camera)
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
