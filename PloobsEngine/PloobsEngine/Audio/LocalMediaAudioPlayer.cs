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
        /// Gets or sets a value indicating whether this <see cref="BackGroundAudioPlayer"/> is loop.
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
