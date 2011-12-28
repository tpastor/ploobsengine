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

        internal GraphicFactory(GraphicInfo ginfo, GraphicsDevice device,IContentManager contentManager)
        {
            this.device = device;
            this.ginfo = ginfo;
            this.contentManager = contentManager;
            SpriteBatch = new SpriteBatch(device);
            RenderTargetPool = new RenderTargetPool(device);
            texCreator = new TextureCreator(ginfo, this);            
        }

        public BasicEffect GetBasicEffect()
        {
            return new BasicEffect(device);
        }

        public AlphaTestEffect GetAlphaTestEffect()
        {
            return new AlphaTestEffect(device);
        }

        public SkinnedEffect GetSkinnedEffect()
        {
            return new SkinnedEffect(device);
        }

        public EnvironmentMapEffect GetEnvironmentMapEffect()
        {
            return new EnvironmentMapEffect(device);
        }

        public DualTextureEffect GetDualTextureEffect()
        {
            return new DualTextureEffect(device);
        }

        public Effect GetEffect(String name, bool clone = false, bool isInternal = false)
        {
            Effect effect = contentManager.GetAsset<Effect>(name, isInternal);
            if (clone)
                return effect.Clone();
            return effect;
        }

        public SpriteBatch GetSpriteBatch()
        {
            return SpriteBatch;
        }
        public Model GetModel(String name,bool isinternal = false)
        {
            return contentManager.GetAsset<Model>(name, isinternal);
        }
        
        public SkinnedModel GetAnimatedModel(String name, bool isinternal = false)
        {
            return contentManager.GetAsset<SkinnedModel>(name, isinternal);
        }       

        public RenderTarget2D CreateRenderTarget(int width, int height, SurfaceFormat SurfaceFormat = SurfaceFormat.Color, bool mipmap = false, DepthFormat DepthFormat = DepthFormat.Depth24Stencil8, int preferedMultisampleCount = 0, RenderTargetUsage RenderTargetUsage = RenderTargetUsage.DiscardContents)
        {
            return new RenderTarget2D(device, width, height, mipmap, SurfaceFormat, DepthFormat, preferedMultisampleCount, RenderTargetUsage);
        }

        public IntermediateRenderTarget GetRenderTargetFromPool(int width, int height, SurfaceFormat SurfaceFormat = SurfaceFormat.Color, bool mipmap = false, DepthFormat DepthFormat = DepthFormat.Depth24Stencil8, int preferedMultisampleCount = 0, RenderTargetUsage RenderTargetUsage = RenderTargetUsage.DiscardContents)
        {
            return RenderTargetPool.GetIntermediateTexture(width, height, mipmap, SurfaceFormat, DepthFormat, preferedMultisampleCount, RenderTargetUsage);
        }


        public SpriteFont GetSpriteFont(String fontName, bool isInternal = false)
        {

            return contentManager.GetAsset<SpriteFont>(fontName, isInternal);
        }

        public Texture2D GetTexture2D(String textureName,bool isInternal = false)
        {
            return contentManager.GetAsset<Texture2D>(textureName, isInternal);
        }



        public TextureCube GetTextureCube(String textureName, bool isInternal = false)
        {
            return contentManager.GetAsset<TextureCube>(textureName, isInternal);
        }


        public Texture2D CreateTexture2D(int width, int height,bool mipmap = false,SurfaceFormat format = SurfaceFormat.Color)
        {
            return new Texture2D(device, width, height, mipmap, format);
        }
        public Texture2D CreateTexture2DColor(int width, int height, Color color,bool mipmap = false)
        {
            return texCreator.CreateColorTexture(width, height, color, mipmap);
        }
        public Texture2D CreateTexture2DRandom(int width, int height, bool mipmap = false)
        {
            return texCreator.CreateCompleteRandomColorTexture(width, height, mipmap);
        }

        public Texture2D CreateTexture2DBlackAndWhite(int width, int height, bool mipmap = false)
        {            
            return texCreator.CreateBlackAndWhiteRandomTexture(width, height, mipmap);
        }

        public Texture2D CreateTexture2DPerlinNoise(int width, int height, float frequencia, float amplitude, float persistence, int octave, bool mipmap = false)
        {
            return texCreator.CreatePerlinNoiseTexture(width, height, frequencia,amplitude,persistence,octave,mipmap);
        }

        public IndexBuffer CreateIndexBuffer(IndexElementSize elementSize, int indexCount,BufferUsage usage)
        {
            return new IndexBuffer(device, elementSize, indexCount, usage);
        }

        public DynamicIndexBuffer CreateDynamicIndexBuffer(IndexElementSize elementSize, int indexCount, BufferUsage usage)
        {
            return new DynamicIndexBuffer(device, elementSize, indexCount, usage);
        }

        public VertexBuffer CreateVertexBuffer(VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage)
        {
            return new VertexBuffer(device, vertexDeclaration, vertexCount, usage);
        }

        public DynamicVertexBuffer CreateDynamicVertexBuffer(VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage)
        {
            return new DynamicVertexBuffer(device, vertexDeclaration, vertexCount, usage);
        }

        public T GetAsset<T>(String assetName, bool isInternal = false)
        {
            return contentManager.GetAsset<T>(assetName, isInternal);
        }

        public Texture2D GetScaledTexture(Texture2D texture,Vector2 Scale)
        {   
            int width = (int) (texture.Width * Scale.X);
            int height = (int)(texture.Height * Scale.Y);
            return GetScaledTexture(texture, width, height);
        }

        public Texture2D GetScaledTexture(Texture2D texture, int width, int height)
        {
            RenderTarget2D cNewRenderTarget = CreateRenderTarget(width, height, SurfaceFormat.Color, ginfo.UseMipMap, DepthFormat.None, ginfo.MultiSample);
            render.PushRenderTarget(cNewRenderTarget);
            render.Clear(Color.Transparent, ClearOptions.Target);
            render.RenderTextureComplete(texture,Color.White,new Rectangle(0,0,width, height),Matrix.Identity,null, true,SpriteSortMode.Deferred,SamplerState.AnisotropicClamp);
            return render.PopRenderTargetAsSingleRenderTarget2D();
        }

        public Texture2D GetTexturePart(Texture2D texture, Rectangle rectangle)
        {
            RenderTarget2D cNewRenderTarget = CreateRenderTarget(rectangle.Width, rectangle.Height, SurfaceFormat.Color, ginfo.UseMipMap, DepthFormat.None, ginfo.MultiSample);
            render.PushRenderTarget(cNewRenderTarget);
            render.Clear(Color.Transparent, ClearOptions.Target);
            render.RenderTextureComplete(texture, Color.White, new Rectangle(0, 0, rectangle.Width, rectangle.Height), Matrix.Identity, rectangle, true, SpriteSortMode.Deferred, SamplerState.AnisotropicClamp);
            return render.PopRenderTargetAsSingleRenderTarget2D();
        }


    }


}
