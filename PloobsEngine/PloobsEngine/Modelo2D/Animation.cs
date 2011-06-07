using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Modelo2D
{
    public class Animation
    {
        Rectangle[] frames;
        float frameLength = 1f / 5f;
        float timer = 0f;
        int currentFrame = 0;

        /// <summary>
        /// Gets of sets the FPS of the animation
        /// </summary>
        public int FramesPerSecond
        {
            get { return (int)(1f / frameLength); }
            set { frameLength = 1f / (float)value; }
        }

        /// <summary>
        /// Gets the Rectangle containing the current frame of animation
        /// </summary>
        public Rectangle CurrentFrame
        {
            get { return frames[currentFrame]; }
        }

        /// <summary>
        /// Creates an animation object
        /// </summary>
        /// <param name="width"> the total width of the input image</param>
        /// <param name="height"> the height of the input image</param>
        /// <param name="numFrames"> the number of frames in the sprite-sheet</param>
        /// <param name="xOffset"> the X origin of the sprite-sheet</param>
        /// <param name="yOffset"> the Y origin of the sprite-sheet</param>
        public Animation(int width, int height, int numFrames, int xOffset, int yOffset)
        {
            frames = new Rectangle[numFrames];
            int frameWidth = width / numFrames;
            for (int i = 0; i < numFrames; i++)
            {
                frames[i] = new Rectangle(xOffset + (frameWidth * i), yOffset,
                                          frameWidth, height);
            }
        }

        /// <summary>
        /// update the animation
        /// </summary>
        /// <param name="elapsed"> seconds since the last frame</param>
        public void Update(float elapsed)
        {
            timer += elapsed;

            if (timer >= frameLength)
            {
                timer = 0f;
                currentFrame = (currentFrame + 1) % frames.Length;
            }
        }

        /// <summary>
        /// resets the animation
        /// </summary>
        public void Reset()
        {
            currentFrame = 0;
            timer = 0f;
        }
    }
}
