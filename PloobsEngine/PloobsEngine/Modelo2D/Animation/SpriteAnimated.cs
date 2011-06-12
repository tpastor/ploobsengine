using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.Modelo2D;

namespace PloobsEngine.Modelo2D
{
    public class SpriteAnimated : IModelo2D
    {   
        protected Dictionary<string, AnimationClass> Animations = new Dictionary<string, AnimationClass>();
        protected int FrameIndex = 0;        

        private int height;
        private int width;

        private string animation;
        public string Animation
        {
            get { return animation; }
            set
            {
                animation = value;
                FrameIndex = 0;
            }
        }

        public SpriteAnimated(Texture2D Texture, int Frames, int animations) : base(ModelType.Texture)
        {
            this.Texture = Texture;
            width = Texture.Width / Frames;
            height = Texture.Height / animations;
            Origin = new Vector2(width / 2, height / 2);            
        }

        public void AddAnimation(string name, int row, 
            int frames, float rotation = 0)
        {
            AnimationClass animation = new AnimationClass();
            animation.Rotation = rotation;
            Rectangle[] recs = new Rectangle[frames];
            for (int i = 0; i < frames; i++)
            {
                recs[i] = new Rectangle(i * width, 
                    (row - 1) * height, width, height);
            }
            animation.Frames = frames;
            animation.Rectangles = recs;
            Animations.Add(name, animation);
            if (Animation == null)
                Animation = name;
        }

        public List<String> GetAnimation()
        {
            return Animations.Keys.ToList();
        }

        private float timeElapsed;

        // default to 20 frames per second
        private float timeToUpdate = 0.05f;
        public int FramesPerSecond
        {
            set { timeToUpdate = (1f / value); }
        }

        public override void Update(GameTime gameTime)
        {
            timeElapsed += (float)
                gameTime.ElapsedGameTime.TotalSeconds;

            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;

                if (FrameIndex < Animations[Animation].Frames - 1)
                    FrameIndex++;
                else if (Animations[Animation].IsLooping)
                    FrameIndex = 0;
            }

            SourceRectangle = Animations[Animation].Rectangles[FrameIndex];
            Rotation = Animations[Animation].Rotation;
        }

        public Rectangle GetFrameRectangle(string animationName, int frame)
        {
            return Animations[animationName].Rectangles[frame];
        }

    }
}
