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
        /// Creates the color texture from other two textures (you provide the way both textures are mixed)
        /// </summary>
        /// <param name="t1">The texture 1.</param>
        /// <param name="t2">The texture 2.</param>
        /// <param name="operation">The operation that will mix both textures.</param>
        /// <returns></returns>
        public Texture2D CreateColorTextureFromOthertexture(Texture2D t1, Texture2D t2, Func<Vector4, Vector4,Vector4> operation)
        {
            System.Diagnostics.Debug.Assert(t1.Width == t2.Width);
            System.Diagnostics.Debug.Assert(t1.Height == t2.Height);
            System.Diagnostics.Debug.Assert(t1.LevelCount == t2.LevelCount);
            System.Diagnostics.Debug.Assert(t1.Format == t2.Format);
            System.Diagnostics.Debug.Assert(operation != null);

            Color[] c1 = new Color[t1.Height * t1.Width];
            Color[] c2 = new Color[t2.Height * t2.Width];
            t1.GetData<Color>(c1);
            t2.SetData<Color>(c2);

            Texture2D t = factory.CreateTexture2D(t1.Height, t1.Width, t1.LevelCount > 1);
            Color[] cor = new Color[t1.Height * t1.Width];
            for (int i = 0; i < t1.Height; i++)
            {
                for (int j = 0; j < t1.Width; j++)
                {                    
                    cor[i + j * t1.Height] = Color.FromNonPremultiplied(operation(c1[i + j * t1.Height].ToVector4(),c2[i + j * t1.Height].ToVector4()));
                }
            }
            t.SetData<Color>(cor);
            return t;
        }

        /// <summary>
        /// Multiplies two color textures.
        /// </summary>
        /// <param name="t1">The t1.</param>
        /// <param name="t2">The t2.</param>
        /// <returns></returns>
        public Texture2D MultiplyColorTexture(Texture2D t1, Texture2D t2)
        {
            System.Diagnostics.Debug.Assert(t1.Width == t2.Width);
            System.Diagnostics.Debug.Assert(t1.Height == t2.Height);
            System.Diagnostics.Debug.Assert(t1.LevelCount == t2.LevelCount);
            System.Diagnostics.Debug.Assert(t1.Format == t2.Format);

            Color[] c1 = new Color[t1.Height * t1.Width];
            Color[] c2 = new Color[t2.Height * t2.Width];
            t1.GetData<Color>(c1);
            t2.SetData<Color>(c2);

            Texture2D t = factory.CreateTexture2D(t1.Height, t1.Width, t1.LevelCount > 1);
            Color[] cor = new Color[t1.Height * t1.Width];
            for (int i = 0; i < t1.Height; i++)
            {
                for (int j = 0; j < t1.Width; j++)
                {
                    Vector4 cv = c1[i + j * t1.Height].ToVector4() * c2[i + j * t1.Height].ToVector4();
                    cor[i + j * t1.Height] = Color.FromNonPremultiplied(cv);
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
