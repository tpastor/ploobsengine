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

namespace PloobsEngine.Utils
{
    /// <summary>
    /// Perlin Noise
    /// </summary>
    public class PerlinNoise
    {
        /// <summary>
        /// Perlin Noise Constructot
        /// </summary>
        /// <param name="width">max</param>
        /// <param name="height">max</param>
        public PerlinNoise(int width, int height)
        {
            this.MAX_WIDTH = width;
            this.MAX_HEIGHT = height;                
        }

        public int MAX_WIDTH = 256;
        public int MAX_HEIGHT = 256;
                
        /// <summary>
        /// Gets the value for a specific X and Y coordinate
        /// results in range [-1, 1] * maxHeight
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="MaxHeight"></param>
        /// <param name="Frequency"></param>
        /// <param name="Amplitude"></param>
        /// <param name="Persistance"></param>
        /// <param name="Octaves"></param>
        /// <returns></returns>
        public float GetRandomHeight(float X, float Y, float MaxHeight,
            float Frequency, float Amplitude, float Persistance,
            int Octaves)
        {
            GenerateNoise();
            float FinalValue = 0.0f;
            for (int i = 0; i < Octaves; ++i)
            {
                FinalValue += GetSmoothNoise(X * Frequency, Y * Frequency) * Amplitude;
                Frequency *= 2.0f;
                Amplitude *= Persistance;
            }
            if (FinalValue < -1.0f)
            {
                FinalValue = -1.0f;
            }
            else if (FinalValue > 1.0f)
            {
                FinalValue = 1.0f;
            }
            return FinalValue * MaxHeight;
        }

        //This function is a simple bilinear filtering function which is good (and easy) enough.        
        private float GetSmoothNoise(float X, float Y)
        {
            float FractionX = X - (int)X;
            float FractionY = Y - (int)Y;
            int X1 = ((int)X + MAX_WIDTH) % MAX_WIDTH;
            int Y1 = ((int)Y + MAX_HEIGHT) % MAX_HEIGHT;
            //for cool art deco looking images, do +1 for X2 and Y2 instead of -1...
            int X2 = ((int)X + MAX_WIDTH - 1) % MAX_WIDTH;
            int Y2 = ((int)Y + MAX_HEIGHT - 1) % MAX_HEIGHT;
            float FinalValue = 0.0f;
            FinalValue += FractionX * FractionY * Noise[X1, Y1];
            FinalValue += FractionX * (1 - FractionY) * Noise[X1, Y2];
            FinalValue += (1 - FractionX) * FractionY * Noise[X2, Y1];
            FinalValue += (1 - FractionX) * (1 - FractionY) * Noise[X2, Y2];
            return FinalValue;
        }

        float[,] Noise;
        bool NoiseInitialized = false;
        /// <summary>
        /// create a array of randoms
        /// </summary>
        private void GenerateNoise()
        {
            if (NoiseInitialized)                //A boolean variable in the class to make sure we only do this once
                return;
            Noise = new float[MAX_WIDTH, MAX_HEIGHT];    //Create the noise table where MAX_WIDTH and MAX_HEIGHT are set to some value>0            
            for (int x = 0; x < MAX_WIDTH; ++x)
            {
                for (int y = 0; y < MAX_HEIGHT; ++y)
                {
                    Noise[x, y] = ((float)(StaticRandom.Random()) - 0.5f) * 2.0f;  //Generate noise between -1 and 1
                }
            }
            NoiseInitialized = true;
        }

    }
}
