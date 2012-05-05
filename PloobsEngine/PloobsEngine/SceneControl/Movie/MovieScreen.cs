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
#if !WINDOWS_PHONE && !REACH
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// When video Ended
    /// </summary>
    public delegate void VideoEnded();

    /// <summary>
    /// Screen responsible for playing video
    /// Extend it 
    /// </summary>
    public  class MovieScreen : IScreen
    {
        public MovieScreen(String location, Color backGoundColor) : base(null)
        {
            this.location = location;
            this.backGoundColor = backGoundColor;
        }

        protected Color backGoundColor;
        private string location;
        protected Video myVideoFile;
        protected VideoPlayer videoPlayer;        
        protected bool started = false;
        protected bool play = true;        
        private VideoEnded videoEnded;

        /// <summary>
        /// Sets the video ended delegate.
        /// </summary>
        /// <value>
        /// The video ended delegate.
        /// </value>
        public VideoEnded VideoEndedDelegate
        {
            set
            {
                this.videoEnded += value;
            }
        }

        /// <summary>
        /// Load graphics content for the screen.
        /// </summary>
        /// <param name="GraphicInfo"></param>
        /// <param name="factory"></param>
        /// <param name="contentManager"></param>
        protected override void  LoadContent(Engine.GraphicInfo GraphicInfo, Engine.GraphicFactory factory, IContentManager contentManager)
        {
             base.LoadContent(GraphicInfo, factory, contentManager);            
             videoPlayer = new VideoPlayer();
             myVideoFile = contentManager.GetAsset<Video>(location);            
             videoPlayer.IsLooped = false;            

        }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is looped.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is looped; otherwise, <c>false</c>.
        /// </value>
        public bool IsLooped
        {
            get
            {
                return videoPlayer.IsLooped;
            }
            set
            {
                videoPlayer.IsLooped = value;
            }
        }


        /// <summary>
        /// Plays this instance.
        /// </summary>
        public void Play()
        {
            if (started)
                videoPlayer.Play(myVideoFile);
            else
                play = true;

        }
        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            if (started)
                videoPlayer.Stop();
            else
                play = false;
        }
        /// <summary>
        /// Resumes this instance.
        /// </summary>
        public void Resume()
        {
            if (started)
                videoPlayer.Resume();
        }
        /// <summary>
        /// Sets the volume.
        /// </summary>
        /// <param name="vol">The vol.</param>
        public void setVolume(float vol)
        {
            if (started)
            videoPlayer.Volume = vol;
            ActiveLogger.LogMessage("Cant set volume if the movie did not started", LogLevel.RecoverableError);
        }
        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <returns></returns>
        public MediaState getState()
        {
            if (started)
            return videoPlayer.State;
            ActiveLogger.LogMessage("Cant get state if the movie did not started", LogLevel.RecoverableError);
            return MediaState.Stopped;
        }

        /// <summary>
        /// Update the Screen
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void  Update(GameTime gameTime)	
        {
            started = true;
            if(play)
            {            
                videoPlayer.Play(myVideoFile);
                play = false;
             
            }

            if (videoPlayer.State == MediaState.Stopped)
            {
                if (videoEnded != null)
                    videoEnded();
                VideoEnded();
            }

            this.UpdateMovie(gameTime);
            base.Update(gameTime);            
        }

        /// <summary>
        /// Videoes the ended.
        /// </summary>
        public virtual void VideoEnded()
        {
        }

        /// <summary>
        /// Updates the movie.
        /// </summary>
        /// <param name="gt">The gt.</param>
        public virtual void UpdateMovie(GameTime gt)
        {
        }

        /// <summary>
        /// Cleans up resources that dont are exclusive of the screen
        /// </summary>
        /// <param name="engine"></param>
        protected override void CleanUp(Engine.EngineStuff engine)
        {
            if (CleanUpWhenRemoved)
            {
                videoPlayer.Dispose();
                engine.ContentManager.ReleaseAsset(location);
                base.CleanUp(engine);
            }
        }

        /// <summary>
        /// This is called when the screen should draw itself.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="render"></param>
        protected override void  Draw(GameTime gameTime, RenderHelper render)
        {
 	       render.Clear(backGoundColor);
           Texture2D videoTexture = null;
           if (videoPlayer.State != MediaState.Stopped)
               videoTexture = videoPlayer.GetTexture();
           
            // Draw the video, if we have a texture to draw.
            if (videoTexture != null)
            {
                render.RenderTextureComplete(videoTexture,Color.White,GraphicInfo.FullScreenRectangle,Matrix.Identity);
            }            
        }

    }
}
#endif