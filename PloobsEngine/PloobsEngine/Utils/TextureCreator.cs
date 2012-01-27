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
using PloobsEngine;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine;

namespace PloobsEngine.Utils
{
    /// <summary>
    /// Create Procedural textures
    /// </summary>
    internal class TextureCreator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextureCreator"/> class.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="factory">The factory.</param>
        public TextureCreator(GraphicInfo info,GraphicFactory factory)
        {
            this.info = info;
            this.factory = factory;
        }
        GraphicInfo info;
        GraphicFactory factory;

        /// <summary>
        /// Creates the color texture. (one color all texture)
        /// squared texture
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="texCor">The tex cor.</param>
        /// <returns></returns>
        public Texture2D CreateColorTexture(int size, Color texCor)
        {
            return CreateColorTexture(size, size, texCor);
        }

        /// <summary>
        /// Creates the color texture. (one color all texture)
        /// rectangular texture
        /// </summary>
        /// <param name="sizex">The sizex.</param>
        /// <param name="sizey">The sizey.</param>
        /// <param name="texCor">The tex cor.</param>
        /// <param name="mipmap">if set to <c>true</c> [mipmap].</param>
        /// <returns></returns>
        public Texture2D CreateColorTexture(int sizex,int sizey,Color texCor,bool mipmap = false)
        {
            Texture2D t = factory.CreateTexture2D(sizex, sizey, mipmap);
            Color[] cor = new Color[sizex * sizey];
            for (int i = 0; i < sizex; i++)
            {
                for (int j = 0; j < sizey; j++)
                {
                    cor[i + j * sizex] = texCor;
                }
            }

            t.SetData<Color>(cor);            
            return t;
        }

        /// <summary>
        /// Creates the complete random color texture.
        /// squared size
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public Texture2D CreateCompleteRandomColorTexture(int size)
        {
            return CreateCompleteRandomColorTexture(size, size);
        }

        /// <summary>
        /// Creates the complete random color texture.
        /// </summary>
        /// <param name="sizex">The sizex.</param>
        /// <param name="sizey">The sizey.</param>
        /// <param name="mipmap">if set to <c>true</c> [mipmap].</param>
        /// <returns></returns>
        public Texture2D CreateCompleteRandomColorTexture(int sizex,int sizey,bool mipmap = false)
        {
            Texture2D t = factory.CreateTexture2D(sizex, sizey,mipmap);
            Color[] cor = new Color[sizex * sizey];
            for (int i = 0; i < sizex; i++)
            {
                for (int j = 0; j < sizey; j++)
                {
                    cor[i + j * sizex] = StaticRandom.RandomColor();
                }
            }

            t.SetData<Color>(cor);
            //t.Save("bla.png", ImageFileFormat.Png);
            return t;
        }

        /// <summary>
        /// Creates the black and white random texture. like random, but with the
        /// same color on all rgb channels
        /// square
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public Texture2D CreateBlackAndWhiteRandomTexture(int size)
        {
            return CreateBlackAndWhiteRandomTexture(size, size);
        }


        /// <summary>
        /// Creates the black and white random texture. like random, but with the
        /// same color on all rgb channels
        /// </summary>
        /// <param name="sizex">The sizex.</param>
        /// <param name="sizey">The sizey.</param>
        /// <param name="mipmap">if set to <c>true</c> [mipmap].</param>
        /// <returns></returns>
        public Texture2D CreateBlackAndWhiteRandomTexture(int sizex,int sizey,bool mipmap = false)
        {
            Texture2D t = factory.CreateTexture2D(sizex, sizey,mipmap);
            Color[] cor = new Color[sizex * sizey];
            for (int i = 0; i < sizex; i++)
            {
                for (int j = 0; j < sizey; j++)
                {
                    float staticRandomRandom = StaticRandom.Random();
                    cor[i + j * sizex] = new Color(staticRandomRandom, staticRandomRandom, staticRandomRandom);
                }
            }

            t.SetData<Color>(cor);
            //t.Save("bla.png", ImageFileFormat.Png);
            return t;
        }

        /// <summary>
        /// Creates the perlin noise texture.
        /// squared
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="frequencia">The frequencia.</param>
        /// <param name="amplitude">The amplitude.</param>
        /// <param name="persistence">The persistence.</param>
        /// <param name="octave">The octave.</param>
        /// <returns></returns>
        public Texture2D CreatePerlinNoiseTexture(int size, float frequencia, float amplitude, float persistence, int octave)
        {
            return CreatePerlinNoiseTexture(size, size, frequencia, amplitude, persistence, octave);
        }

        /// <summary>
        /// Creates the perlin noise texture.
        /// </summary>
        /// <param name="sizex">The sizex.</param>
        /// <param name="sizey">The sizey.</param>
        /// <param name="frequencia">The frequencia.</param>
        /// <param name="amplitude">The amplitude.</param>
        /// <param name="persistence">The persistence.</param>
        /// <param name="octave">The octave.</param>
        /// <param name="mipmap">if set to <c>true</c> [mipmap].</param>
        /// <returns></returns>
        public Texture2D CreatePerlinNoiseTexture(int sizex, int sizey,float frequencia, float amplitude, float persistence, int octave,bool mipmap = false)
        {
            PerlinNoise pn = new PerlinNoise(sizex, sizey);
            Texture2D t = factory.CreateTexture2D(sizex, sizey,mipmap);
            Color[] cor = new Color[sizex * sizey];
            for (int i = 0; i < sizex; i++)
            {
                for (int j = 0; j < sizey; j++)
                {
                    float value = pn.GetRandomHeight(i, j, 1, frequencia, amplitude, persistence, octave);
                    value =  0.5f * (1 + value);
                    cor[i + j * sizex] = new Color(value,value,value);
                }
            }

            t.SetData<Color>(cor);
            return t;            
        }
    }
}
