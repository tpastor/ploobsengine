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
    public delegate void VideoEnded();

    public  class MovieScreen : IScreen
    {
        public MovieScreen(String location, Color backGoundColor)
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

        public VideoEnded VideoEndedDelegate
        {
            set
            {
                this.videoEnded += value;
            }
        }

        protected override void  LoadContent(Engine.GraphicInfo GraphicInfo, Engine.GraphicFactory factory, IContentManager contentManager)
        {
             base.LoadContent(GraphicInfo, factory, contentManager);            
             videoPlayer = new VideoPlayer();
             myVideoFile = contentManager.GetAsset<Video>(location);
             videoPlayer.IsLooped = false;            

        }
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


        public void Play()
        {
            if (started)
                videoPlayer.Play(myVideoFile);
            else
                play = true;

        }
        public void Stop()
        {
            if (started)
                videoPlayer.Stop();
            else
                play = false;
        }
        public void Resume()
        {
            if (started)
                videoPlayer.Resume();
        }
        public void setVolume(float vol)
        {
            if (started)
            videoPlayer.Volume = vol;
            ActiveLogger.LogMessage("Cant set volume if the movie did not started", LogLevel.RecoverableError);
        }
        public MediaState getState()
        {
            if (started)
            return videoPlayer.State;
            ActiveLogger.LogMessage("Cant get state if the movie did not started", LogLevel.RecoverableError);
            return MediaState.Stopped;
        }

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

        public virtual void VideoEnded()
        {
        }

        public virtual void UpdateMovie(GameTime gt)
        {
        }

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
