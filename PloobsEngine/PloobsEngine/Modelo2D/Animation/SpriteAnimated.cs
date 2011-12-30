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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.Modelo2D;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Modelo2D
{
    /// <summary>
    /// Model used for sprite animation
    /// </summary>
    public class SpriteAnimated : IModelo2D
    {
        private Dictionary<string, AnimationClass> animations = new Dictionary<string, AnimationClass>();

        public Dictionary<string, AnimationClass> Animations
        {
            get { return animations; }            
        }

        private int FrameIndex = 0;
        private bool running = false;
        private int height;
        private int width;
        private float timeElapsed;
        private string animation;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteAnimated"/> class.
        /// </summary>
        /// <param name="Texture">The texture.</param>
        /// <param name="Frames">number of frames.</param>
        /// <param name="animations">number of animations.</param>
        public SpriteAnimated(Texture2D Texture, int Frames, int animations) : base(ModelType.Texture)
        {
            this.Texture = Texture;
            width = Texture.Width / Frames;
            height = Texture.Height / animations;
            Origin = new Vector2(width / 2, height / 2);            
        }

        /// <summary>
        /// Adds a specific sprite animation
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="row">The row.</param>
        /// <param name="frames">The frames.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="isLooping">if set to <c>true</c> [is looping].</param>
        /// <param name="fps">The FPS.</param>
        public void AddAnimation(string name, int row,
            int frames, float rotation = 0,bool isLooping = true, float fps = 1/0.05f)
        {
            AnimationClass ac = new AnimationClass(name,isLooping, fps);
            ac.Rotation = rotation;
            Rectangle[] recs = new Rectangle[frames];
            for (int i = 0; i < frames; i++)
            {
                recs[i] = new Rectangle(i * width, 
                    (row - 1) * height, width, height);
            }
            ac.Frames = frames;
            ac.Rectangles = recs;
            animations.Add(name, ac);
            if (this.animation == null)
            {
                this.animation = name;
                SourceRectangle = animations[this.animation].Rectangles[FrameIndex];
                Rotation = animations[this.animation].Rotation;                
            }
        }

        

        
        public override void Update(GameTime gameTime)
        {
            if (running)
            {
                timeElapsed += (float)
                    gameTime.ElapsedGameTime.TotalSeconds;

                if (timeElapsed > animations[animation].timeToUpdate)
                {
                    timeElapsed -= animations[animation].timeToUpdate;

                    if (FrameIndex < animations[animation].Frames - 1)
                        FrameIndex++;
                    else if (animations[animation].IsLooping)
                        FrameIndex = 0;
                }

                SourceRectangle = animations[animation].Rectangles[FrameIndex];
                Rotation = animations[animation].Rotation;
            }
        }

        /// <summary>
        /// Changes the current animation.
        /// </summary>
        /// <param name="animationName">Name of the animation.</param>
        /// <param name="frameToStart">The frame to start.</param>
        /// <param name="start">if set to <c>true</c> [start].</param>
        public void ChangeAnimation(String animationName,int frameToStart =0, bool start = true)
        {
            animation = animationName;
            FrameIndex = frameToStart;
            timeElapsed = 0;
            SourceRectangle = animations[animation].Rectangles[FrameIndex];
            Rotation = animations[animation].Rotation;
            running = start;        
        }

        /// <summary>
        /// Pauses the current animation.
        /// </summary>
        public void PauseCurrentAnimation()
        {
            running = false;
        }
        /// <summary>
        /// Plays the current animation.
        /// </summary>
        public void PlayCurrentAnimation()
        {
            running = true;
        }

        /// <summary>
        /// Sets the index of the current frame.
        /// </summary>
        /// <param name="frameIndex">Index of the frame.</param>
        public void SetCurrentFrameIndex(int frameIndex)
        {
            if (frameIndex < animations[animation].Frames)
            {
                FrameIndex = frameIndex;
            }
            else
            {
                throw new ArgumentOutOfRangeException("frameIndex > Maximum frames in the current animation");
            }
        }

        /// <summary>
        /// Gets the index of the current frame.
        /// </summary>
        /// <returns></returns>
        public int GetCurrentFrameIndex()
        {
            return FrameIndex;
        }

        /// <summary>
        /// Gets the current animation.
        /// </summary>
        /// <returns></returns>
        public AnimationClass GetCurrentAnimation()
        {
            return animations[animation];
        }

        /// <summary>
        /// Gets the frame rectangle of the current animation.
        /// </summary>
        /// <param name="animationName">Name of the animation.</param>
        /// <param name="frame">The frame.</param>
        /// <returns></returns>
        public Rectangle GetFrameRectangle(string animationName, int frame)
        {
            if (frame < animations[animationName].Frames)
            {
                return animations[animationName].Rectangles[frame];
            }
            else
            {
                throw new ArgumentOutOfRangeException("frameIndex > Maximum frames in the animation");
            }
            
        }

    }
}
