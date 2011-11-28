using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;

namespace PloobsEngine.Audio
{
    /// <summary>
    /// Song MusicPlayer
    /// IMPORTANT
    /// On Windows, MediaLibrary can find songs only if the Windows Media Player previously found songs on the system.
    /// This means that Windows Media Player first must search the system for music before any songs can be accessed through MediaLibrary.
    /// </summary>
    public static class BackGroundAudioPlayer
    {   
        private static MediaLibrary sampleMediaLibrary = new MediaLibrary();


        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BackGroundAudioPlayer"/> is loop.
        /// </summary>
        /// <value>
        ///   <c>true</c> if loop; otherwise, <c>false</c>.
        /// </value>
        public static bool Loop
        {
            get
            {
                return MediaPlayer.IsRepeating ;
            }
            set
            {
                MediaPlayer.IsRepeating = value;
            }
        }

        /// <summary>
        /// Starts the queue of songs
        /// </summary>
        public static void StartQueue()
        {
            MediaPlayer.IsRepeating = false;
            MediaPlayer.Play(sampleMediaLibrary.Songs);
        }

        /// <summary>
        /// Gets the song collection.
        /// </summary>
        public static SongCollection SongCollection
        {
            get
            { return sampleMediaLibrary.Songs; }
       }

        /// <summary>
        /// Plays the specific music.
        /// </summary>
        /// <param name="name">The name.</param>
        public static void PlaySpecificMusic(String name)
        {
            MediaPlayer.Play(sampleMediaLibrary.Songs.First((a) => a.Name == name));
        }

        /// <summary>
        /// Advances one music in queue.
        /// </summary>
        public static void AdvanceOneMusicInQueue()
        {
            MediaPlayer.MoveNext();
        }

        /// <summary>
        /// Backs one music in queue.
        /// </summary>
        public static void BackOneMusicInQueue()
        {
            MediaPlayer.MovePrevious();
        }

        /// <summary>
        /// Current state of the current music.
        /// </summary>
        /// <returns></returns>
        public static MediaState CurrentMusicState()
        {
            return MediaPlayer.State;
        }

        /// <summary>
        /// Pauses the current music.
        /// </summary>
        public static void PauseCurrentMusic()
        {
            MediaPlayer.Pause();
            
        }

        /// <summary>
        /// Resumes the current music.
        /// </summary>
        public static void ResumeCurrentMusic()
        {
            MediaPlayer.Resume();
        }

        /// <summary>
        /// Stops the current music.
        /// </summary>
        public static void StopCurrentMusic()
        {         
            MediaPlayer.Stop();
        }
        

    }
}
