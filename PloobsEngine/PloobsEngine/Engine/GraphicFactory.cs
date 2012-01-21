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
using PloobsEngine.SceneControl;
using PloobsEngine.Utils;
using Microsoft.Xna.Framework;
using XNAnimation;

namespace PloobsEngine.Engine
{
    /// <summary>
    /// Creates everything related to Graphics.
    /// FunctionNameConvention:
    /// Use Get... To load something and Create... to create something
    /// </summary>
    public class GraphicFactory
    {
        internal GraphicsDevice device;
        internal IContentManager contentManager;
        internal SpriteBatch SpriteBatch;
        GraphicInfo ginfo;        
        RenderTargetPool RenderTargetPool;
        TextureCreator texCreator;
        internal RenderHelper render;

        internal GraphicFactory(GraphicInfo ginfo, GraphicsDevice device, IContentManager contentManager)
        {
            this.device = device;
            this.ginfo = ginfo;
            this.contentManager = contentManager;
            SpriteBatch = new SpriteBatch(device);
            RenderTargetPool = new RenderTargetPool(device);
            texCreator = new TextureCreator(ginfo, this);            
        }

        /// <summary>
        /// Gets the basic effect.
        /// </summary>
        /// <returns></returns>
        public BasicEffect GetBasicEffect()
        {
            return new BasicEffect(device);
        }

        /// <summary>
        /// Gets the alpha test effect.
        /// </summary>
        /// <returns></returns>
        public AlphaTestEffect GetAlphaTestEffect()
        {
            return new AlphaTestEffect(device);
        }

        /// <summary>
        /// Gets the skinned effect.
        /// </summary>
        /// <returns></returns>
        public SkinnedEffect GetSkinnedEffect()
        {
            return new SkinnedEffect(device);
        }

        /// <summary>
        /// Gets the environment map effect.
        /// </summary>
        /// <returns></returns>
        public EnvironmentMapEffect GetEnvironmentMapEffect()
        {
            return new EnvironmentMapEffect(device);
        }

        /// <summary>
        /// Gets the dual texture effect.
        /// </summary>
        /// <returns></returns>
        public DualTextureEffect GetDualTextureEffect()
        {
            return new DualTextureEffect(device);
        }

        /// <summary>
        /// Gets a user created effect.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="clone">if set to <c>true</c> [clone].</param>
        /// <param name="isInternal">if set to <c>true</c> [is internal].</param>
        /// <returns></returns>
        public Effect GetEffect(String name, bool clone = false, bool isInternal = false)
        {
            Effect effect = contentManager.GetAsset<Effect>(name, isInternal);
            if (clone)
                return effect.Clone();
            return effect;
        }

        /// <summary>
        /// Gets the shared sprite batch instance.
        /// </summary>
        /// <returns></returns>
        public SpriteBatch GetSpriteBatch(bool createOneFresh = false)
        {
            if (createOneFresh)
            {
                return new SpriteBatch(device);
            }
            else
            {
                return SpriteBatch;
            }
        }

        /// <summary>
        /// Gets a model
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="isinternal">if set to <c>true</c> [isinternal].</param>
        /// <returns></returns>
        public Model GetModel(String name,bool isinternal = false)
        {
            return contentManager.GetAsset<Model>(name, isinternal);
        }

        /// <summary>
        /// Gets an animated model.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="isinternal">if set to <c>true</c> [isinternal].</param>
        /// <returns></returns>
        public SkinnedModel GetAnimatedModel(String name, bool isinternal = false)
        {
            return contentManager.GetAsset<SkinnedModel>(name, isinternal);
        }

        /// <summary>
        /// Creates the render target.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="SurfaceFormat">The surface format.</param>
        /// <param name="mipmap">if set to <c>true</c> [mipmap].</param>
        /// <param name="DepthFormat">The depth format.</param>
        /// <param name="preferedMultisampleCount">The prefered multisample count.</param>
        /// <param name="RenderTargetUsage">The render target usage.</param>
        /// <returns></returns>
        public RenderTarget2D CreateRenderTarget(int width, int height, SurfaceFormat SurfaceFormat = SurfaceFormat.Color, bool mipmap = false, DepthFormat DepthFormat = DepthFormat.Depth24Stencil8, int preferedMultisampleCount = 0, RenderTargetUsage RenderTargetUsage = RenderTargetUsage.DiscardContents)
        {
            return new RenderTarget2D(device, width, height, mipmap, SurfaceFormat, DepthFormat, preferedMultisampleCount, RenderTargetUsage);
        }

        /// <summary>
        /// Gets the render target from pool.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="SurfaceFormat">The surface format.</param>
        /// <param name="mipmap">if set to <c>true</c> [mipmap].</param>
        /// <param name="DepthFormat">The depth format.</param>
        /// <param name="preferedMultisampleCount">The prefered multisample count.</param>
        /// <param name="RenderTargetUsage">The render target usage.</param>
        /// <returns></returns>
        public IntermediateRenderTarget GetRenderTargetFromPool(int width, int height, SurfaceFormat SurfaceFormat = SurfaceFormat.Color, bool mipmap = false, DepthFormat DepthFormat = DepthFormat.Depth24Stencil8, int preferedMultisampleCount = 0, RenderTargetUsage RenderTargetUsage = RenderTargetUsage.DiscardContents)
        {
            return RenderTargetPool.GetIntermediateTexture(width, height, mipmap, SurfaceFormat, DepthFormat, preferedMultisampleCount, RenderTargetUsage);
        }


        /// <summary>
        /// Gets the sprite font.
        /// </summary>
        /// <param name="fontName">Name of the font.</param>
        /// <param name="isInternal">if set to <c>true</c> [is internal].</param>
        /// <returns></returns>
        public SpriteFont GetSpriteFont(String fontName, bool isInternal = false)
        {

            return contentManager.GetAsset<SpriteFont>(fontName, isInternal);
        }

        /// <summary>
        /// Gets the texture2D from file
        /// </summary>
        /// <param name="textureName">Name of the texture.</param>
        /// <param name="isInternal">if set to <c>true</c> [is internal].</param>
        /// <returns></returns>
        public Texture2D GetTexture2D(String textureName,bool isInternal = false)
        {
            Texture2D Texture2D = contentManager.GetAsset<Texture2D>(textureName, isInternal);
            Texture2D.Tag = textureName;
            return Texture2D;
        }



        /// <summary>
        /// Gets the texture cube from file
        /// </summary>
        /// <param name="textureName">Name of the texture.</param>
        /// <param name="isInternal">if set to <c>true</c> [is internal].</param>
        /// <returns></returns>
        public TextureCube GetTextureCube(String textureName, bool isInternal = false)
        {
            TextureCube TextureCube = contentManager.GetAsset<TextureCube>(textureName, isInternal);
            TextureCube.Tag = textureName;
            return TextureCube;
        }


        /// <summary>
        /// Creates the texture2D.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="mipmap">if set to <c>true</c> [mipmap].</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        public Texture2D CreateTexture2D(int width, int height,bool mipmap = false,SurfaceFormat format = SurfaceFormat.Color)
        {
            Texture2D Texture2D =new Texture2D(device, width, height, mipmap, format);
            Texture2D.Tag = "CREATED";
            return Texture2D;
        }
        /// <summary>
        /// Creates the color of the texture2D.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="color">The color.</param>
        /// <param name="mipmap">if set to <c>true</c> [mipmap].</param>
        /// <returns></returns>
        public Texture2D CreateTexture2DColor(int width, int height, Color color,bool mipmap = false)
        {
            Texture2D Texture2D = texCreator.CreateColorTexture(width, height, color, mipmap);
            Texture2D.Tag = "CREATED";
            return Texture2D;
        }
        /// <summary>
        /// Creates the texture2D random color
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="mipmap">if set to <c>true</c> [mipmap].</param>
        /// <returns></returns>
        public Texture2D CreateTexture2DRandom(int width, int height, bool mipmap = false)
        {
            Texture2D Texture2D = texCreator.CreateCompleteRandomColorTexture(width, height, mipmap);
            Texture2D.Tag = "CREATED";
            return Texture2D;
        }

        /// <summary>
        /// Creates the texture2D black and white.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="mipmap">if set to <c>true</c> [mipmap].</param>
        /// <returns></returns>
        public Texture2D CreateTexture2DBlackAndWhite(int width, int height, bool mipmap = false)
        {
            Texture2D Texture2D = texCreator.CreateBlackAndWhiteRandomTexture(width, height, mipmap);
            Texture2D.Tag = "CREATED";
            return Texture2D;
        }

        /// <summary>
        /// Creates the texture2D with perlin noise.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="frequencia">The frequencia.</param>
        /// <param name="amplitude">The amplitude.</param>
        /// <param name="persistence">The persistence.</param>
        /// <param name="octave">The octave.</param>
        /// <param name="mipmap">if set to <c>true</c> [mipmap].</param>
        /// <returns></returns>
        public Texture2D CreateTexture2DPerlinNoise(int width, int height, float frequencia, float amplitude, float persistence, int octave, bool mipmap = false)
        {
            Texture2D Texture2D = texCreator.CreatePerlinNoiseTexture(width, height, frequencia, amplitude, persistence, octave, mipmap);
            Texture2D.Tag = "CREATED";
            return Texture2D;
        }

        /// <summary>
        /// Creates the index buffer.
        /// </summary>
        /// <param name="elementSize">Size of the element.</param>
        /// <param name="indexCount">The index count.</param>
        /// <param name="usage">The usage.</param>
        /// <returns></returns>
        public IndexBuffer CreateIndexBuffer(IndexElementSize elementSize, int indexCount,BufferUsage usage)
        {
            return new IndexBuffer(device, elementSize, indexCount, usage);
        }

        /// <summary>
        /// Creates the dynamic index buffer.
        /// </summary>
        /// <param name="elementSize">Size of the element.</param>
        /// <param name="indexCount">The index count.</param>
        /// <param name="usage">The usage.</param>
        /// <returns></returns>
        public DynamicIndexBuffer CreateDynamicIndexBuffer(IndexElementSize elementSize, int indexCount, BufferUsage usage)
        {
            return new DynamicIndexBuffer(device, elementSize, indexCount, usage);
        }

        /// <summary>
        /// Creates the vertex buffer.
        /// </summary>
        /// <param name="vertexDeclaration">The vertex declaration.</param>
        /// <param name="vertexCount">The vertex count.</param>
        /// <param name="usage">The usage.</param>
        /// <returns></returns>
        public VertexBuffer CreateVertexBuffer(VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage)
        {
            return new VertexBuffer(device, vertexDeclaration, vertexCount, usage);
        }

        /// <summary>
        /// Creates the dynamic vertex buffer.
        /// </summary>
        /// <param name="vertexDeclaration">The vertex declaration.</param>
        /// <param name="vertexCount">The vertex count.</param>
        /// <param name="usage">The usage.</param>
        /// <returns></returns>
        public DynamicVertexBuffer CreateDynamicVertexBuffer(VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage)
        {
            return new DynamicVertexBuffer(device, vertexDeclaration, vertexCount, usage);
        }

        /// <summary>
        /// Gets an arbitrary asset.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName">Name of the asset.</param>
        /// <param name="isInternal">if set to <c>true</c> [is internal].</param>
        /// <returns></returns>
        public T GetAsset<T>(String assetName, bool isInternal = false)
        {
            return contentManager.GetAsset<T>(assetName, isInternal);
        }

        /// <summary>
        /// Gets the scaled texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="Scale">The scale.</param>
        /// <returns></returns>
        public Texture2D GetScaledTexture(Texture2D texture,Vector2 Scale)
        {   
            int width = (int) (texture.Width * Scale.X);
            int height = (int)(texture.Height * Scale.Y);
            return GetScaledTexture(texture, width, height);
        }

        /// <summary>
        /// Gets the scaled texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        public Texture2D GetScaledTexture(Texture2D texture, int width, int height)
        {
            RenderTarget2D cNewRenderTarget = CreateRenderTarget(width, height, SurfaceFormat.Color, ginfo.UseMipMap, DepthFormat.None, ginfo.MultiSample);
            render.PushRenderTarget(cNewRenderTarget);
            render.Clear(Color.Transparent, ClearOptions.Target);
            render.RenderTextureComplete(texture,Color.White,new Rectangle(0,0,width, height),Matrix.Identity,null, true,SpriteSortMode.Deferred,SamplerState.AnisotropicClamp);
            render.ResyncStates();
            return render.PopRenderTargetAsSingleRenderTarget2D();
        }

        public void MipMapTexture(ref Texture2D texture)
        {
            RenderTarget2D cNewRenderTarget = CreateRenderTarget(texture.Width, texture.Height, SurfaceFormat.Color, true, DepthFormat.None, ginfo.MultiSample);
            render.Clear(Color.Black, ClearOptions.Target);
            render.PushRenderTarget(cNewRenderTarget);

            SpriteBatch.Begin();
            SpriteBatch.Draw(texture, Vector2.Zero, Color.White);
            SpriteBatch.End();

            render.PopRenderTarget();
            render.ResyncStates();
            texture = cNewRenderTarget;
        }

        /// <summary>
        /// Gets a texture part from a bigger texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns></returns>
        public Texture2D GetTexturePart(Texture2D texture, Rectangle rectangle)
        {
            RenderTarget2D cNewRenderTarget = CreateRenderTarget(rectangle.Width, rectangle.Height, SurfaceFormat.Color, ginfo.UseMipMap, DepthFormat.None, ginfo.MultiSample);
            render.PushRenderTarget(cNewRenderTarget);
            render.Clear(Color.Transparent, ClearOptions.Target);
            render.RenderTextureComplete(texture, Color.White, new Rectangle(0, 0, rectangle.Width, rectangle.Height), Matrix.Identity, rectangle, true, SpriteSortMode.Deferred, SamplerState.AnisotropicClamp);
            return render.PopRenderTargetAsSingleRenderTarget2D();
        }

        /// <summary>
        /// Releases the asset.
        /// </summary>
        /// <param name="assetName">Name of the asset.</param>
        public void ReleaseAsset(String assetName)
        {
            contentManager.ReleaseAsset(assetName);
        }


    }


}
