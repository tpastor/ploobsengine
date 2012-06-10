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
using Microsoft.Xna.Framework.Audio;
using PloobsEngine.SceneControl;
using PloobsEngine.Engine;

namespace PloobsEngine.Audio
{
    /// <summary>
    /// Simple effect Sound effect class
    /// This instance is not shared. It is completely independent
    /// </summary>
    public class SimpleSoundEffect
    {
        SoundEffect mySoundEffect;
        SoundEffectInstance mySoundEffectInstance;
        String internalName;
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleSoundEffect"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="name">The name.</param>
        /// <param name="volume">The volume.</param>
        /// <param name="pitch">The pitch(-1 to 1).</param>
        /// <param name="pan">The pan (-1 to 1).</param>
        /// <param name="isLooped">if set to <c>true</c> [is looped].</param>
        public SimpleSoundEffect(GraphicFactory factory, string name, float volume = 1, float pitch = 0, float pan = 0, bool isLooped = false)
        {
            System.Diagnostics.Debug.Assert(factory != null);
            System.Diagnostics.Debug.Assert(!String.IsNullOrEmpty(name));
            System.Diagnostics.Debug.Assert(volume >= -1 && volume <= 1);
            System.Diagnostics.Debug.Assert(pitch >= -1 && pitch <= 1);
            System.Diagnostics.Debug.Assert(pan >= -1 && pan <= 1);

            internalName = name;
            this.mySoundEffect = factory.GetAsset<SoundEffect>(name);
            this.Duration = mySoundEffect.Duration;
            this.Name = mySoundEffect.Name;
            this.mySoundEffectInstance = mySoundEffect.CreateInstance();
            mySoundEffectInstance.Pan = pan;
            mySoundEffectInstance.Pitch = pitch;
            mySoundEffectInstance.Volume = volume;
            mySoundEffectInstance.IsLooped = isLooped;
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
        /// Gets my sound effect instance.
        /// </summary>
        public SoundEffectInstance MySoundEffectInstance
        {
            get { return mySoundEffectInstance; }
        }

        /// <summary>
        /// Forces the play.
        /// </summary>
        public void ForcePlay()
        {
            this.mySoundEffect.Play();
        }
        /// <summary>
        /// Plays this instance.
        /// </summary>
        public void Play()
        {

            this.mySoundEffectInstance.Play();
        }
        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            this.mySoundEffectInstance.Stop();

        }

        /// <summary>
        /// Pauses this instance.
        /// </summary>
        public void Pause()
        {
            this.mySoundEffectInstance.Pause();
        }

        /// <summary>
        /// Resumes this instance.
        /// </summary>
        public void Resume()
        {
            this.mySoundEffectInstance.Resume();
        }

        /// <summary>
        /// Cleans up.
        /// Not called by the engine
        /// </summary>
        public void CleanUp()
        {
            mySoundEffect.Dispose();
        }

        /// <summary>
        /// Cleans up.
        /// Not called by the engine
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="removeSoundGeneratorAlso">if set to <c>true</c> [remove sound generator also].</param>
        public void CleanUp(GraphicFactory factory,bool removeSoundGeneratorAlso = true)
        {
            if(removeSoundGeneratorAlso)
                factory.ReleaseAsset(internalName);
            mySoundEffect.Dispose();
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        public SoundState State
        {
            get
            {
                return this.mySoundEffectInstance.State;                
            }
        }

        /// <summary>
        /// Sets the volume.
        /// </summary>
        /// <param name="volume">The volume.</param>
        public void setVolume(float volume)
        {
            System.Diagnostics.Debug.Assert(volume >= -1 && volume <= 1);
            this.mySoundEffectInstance.Volume = volume;
        }
    }
}
