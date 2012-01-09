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
using Microsoft.Xna.Framework.Media;

namespace PloobsEngine.Audio
{
    /// <summary>
    /// MusicPlayer
    /// IMPORTANT
    /// On Windows, MediaLibrary can find songs only if the Windows Media Player previously found songs on the system.
    /// This means that Windows Media Player first must search the system for music before any songs can be accessed through MediaLibrary.
    /// </summary>
    public class LocalMediaAudioPlayer
    {
        private MediaLibrary sampleMediaLibrary = new MediaLibrary();

        public MediaLibrary MediaLibrary
        {
            get { return sampleMediaLibrary; }
            set { sampleMediaLibrary = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="LocalMediaAudioPlayer"/> is loop.
        /// </summary>
        /// <value>
        ///   <c>true</c> if loop; otherwise, <c>false</c>.
        /// </value>
        public bool Loop
        {
            get
            {
                return MediaPlayer.IsRepeating;
            }
            set
            {
                MediaPlayer.IsRepeating = value;
            }
        }

        /// <summary>
        /// Starts the queue of songs
        /// </summary>
        public void StartQueue()
        {
            MediaPlayer.IsRepeating = false;
            MediaPlayer.Play(sampleMediaLibrary.Songs);
        }

        /// <summary>
        /// Gets the song collection.
        /// </summary>
        public SongCollection SongCollection
        {
            get
            { return sampleMediaLibrary.Songs; }
        }


        /// <summary>
        /// Plays the album.
        /// </summary>
        /// <param name="album">The album.</param>
        public void PlayAlbum(Album album)
        {
            MediaPlayer.Play(album.Songs);
        }

        /// <summary>
        /// Plays the specific music.
        /// </summary>
        /// <param name="name">The name.</param>
        public void PlaySpecificMusic(String name)
        {
            MediaPlayer.Play(sampleMediaLibrary.Songs.First((a) => a.Name == name));
        }

        public void PlaySpecificMusic(Song song)
        {
            MediaPlayer.Play(song);            
        }

        /// <summary>
        /// Advances one music in queue.
        /// </summary>
        public void AdvanceOneMusicInQueue()
        {
            MediaPlayer.MoveNext();
        }

        /// <summary>
        /// Backs one music in queue.
        /// </summary>
        public void BackOneMusicInQueue()
        {
            MediaPlayer.MovePrevious();
        }

        /// <summary>
        /// Current state of the current music.
        /// </summary>
        /// <returns></returns>
        public MediaState CurrentMusicState()
        {
            return MediaPlayer.State;
        }

        /// <summary>
        /// Pauses the current music.
        /// </summary>
        public void PauseCurrentMusic()
        {
            MediaPlayer.Pause();
        }

        /// <summary>
        /// Resumes the current music.
        /// </summary>
        public void ResumeCurrentMusic()
        {
            MediaPlayer.Resume();
        }

        /// <summary>
        /// Stops the current music.
        /// </summary>
        public void StopCurrentMusic()
        {
            MediaPlayer.Stop();
        }


    }
}
