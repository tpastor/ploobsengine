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
using PloobsEngine.Modelo;
using PloobsEngine.Components;
using PloobsEngine.Engine.Logger;
using System.Diagnostics;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Renderer
    /// </summary>
    public class RenderHelper
    {
        internal GraphicsDevice device;
        private QuadRender qrender;
        private BasicEffect effect;
        private SpriteBatch spriteBatch;
        SpriteFont defaultFont;
        private Stack<RenderTargetBinding[]> RenderStatesStack = new Stack<RenderTargetBinding[]>();
        private Stack<RasterizerState> RasterizerStateStack = new Stack<RasterizerState>();
        private Stack<BlendState> BlendStateStack = new Stack<BlendState>();
        private Stack<DepthStencilState> DepthStencilStateStack = new Stack<DepthStencilState>();        
        private Dictionary<String, Texture2D> Scenes = new Dictionary<string, Texture2D>();
        private ComponentManager componentManager;


        /// <summary>
        /// Initializes a new instance of the <see cref="RenderHelper"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="componentManager">The component manager.</param>
        /// <param name="cmanager">The cmanager.</param>
        internal RenderHelper(GraphicsDevice device, ComponentManager componentManager, IContentManager cmanager)
        {
            this.device = device;
            spriteBatch = new SpriteBatch(device);
            effect = new BasicEffect(device);
            qrender = new QuadRender(device);
            this.componentManager = componentManager;
            defaultFont = cmanager.GetAsset<Microsoft.Xna.Framework.Graphics.SpriteFont>("ConsoleFont", true);
        }

        /// <summary>
        /// Renders the pre components.
        /// </summary>
        /// <param name="gametime">The gametime.</param>
        /// <param name="view">The view.</param>
        /// <param name="projection">The projection.</param>
        /// <returns></returns>
        public int RenderPreComponents(GameTime gametime, ref Matrix view, ref Matrix projection)
        {
            return componentManager.PreDraw(this,gametime, ref view, ref projection);
        }
        /// <summary>
        /// Renders the pos components.
        /// </summary>
        /// <param name="gametime">The gametime.</param>
        /// <param name="view">The view.</param>
        /// <param name="projection">The projection.</param>
        /// <returns></returns>
        public int RenderPosComponents(GameTime gametime, ref Matrix view, ref Matrix projection)
        {
            return componentManager.AfterDraw(this,gametime, ref view, ref projection);
        }

        /// <summary>
        /// Renders the pos with depth components.
        /// </summary>
        /// <param name="gametime">The gametime.</param>
        /// <param name="view">The view.</param>
        /// <param name="projection">The projection.</param>
        /// <returns></returns>
        public int RenderPosWithDepthComponents(GameTime gametime, ref Matrix view, ref Matrix projection)
        {
            return componentManager.PosWithDepthDraw(this, gametime, ref view,ref projection);
        }

        /// <summary>
        /// Pushes the state of the rasterizer.
        /// </summary>
        /// <param name="state">The state.</param>
        public void PushRasterizerState(RasterizerState state)
        {
            device.RasterizerState = state;
            RasterizerStateStack.Push(state);
        }

        /// <summary>
        /// Peeks the state of the rasterizer.
        /// </summary>
        /// <returns></returns>
        public RasterizerState PeekRasterizerState()
        {
            System.Diagnostics.Debug.Assert(device.RasterizerState== RasterizerStateStack.Peek());
            return RasterizerStateStack.Peek();
        }

        /// <summary>
        /// Pops the state of the rasterizer.
        /// </summary>
        /// <returns></returns>
        public RasterizerState PopRasterizerState()
        {
            RasterizerState rt = RasterizerStateStack.Peek();
            RasterizerStateStack.Pop();
            RasterizerState rt2 = RasterizerStateStack.Peek();
            device.RasterizerState = rt2;
            return rt;                        
        }

        /// <summary>
        /// Pushes one blendstate.
        /// </summary>
        /// <param name="state">The state.</param>
        public void PushBlendState(BlendState state)
        {
            device.BlendState = state;
            BlendStateStack.Push(state);
        }
        /// <summary>
        /// Peeks the blendstate..
        /// </summary>
        /// <returns></returns>
        public BlendState PeekBlendState()
        {
            System.Diagnostics.Debug.Assert(device.BlendState == BlendStateStack.Peek());
            return BlendStateStack.Peek();
        }

        /// <summary>
        /// Pops one blendstate.
        /// </summary>
        /// <returns></returns>
        public BlendState PopBlendState()
        {
            BlendState rt = BlendStateStack.Peek();
            BlendStateStack.Pop();
            BlendState rt2 = BlendStateStack.Peek();
            device.BlendState = rt2;
            return rt;                        
        }

        /// <summary>
        /// Sets the state of the sampler.
        /// </summary>
        /// <param name="SamplerState">State of the sampler.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public SamplerState SetSamplerState(SamplerState SamplerState, int index)
        {
            System.Diagnostics.Debug.Assert(index >= 0);
            System.Diagnostics.Debug.Assert(device.SamplerStates[index] != null);
            SamplerState ss = device.SamplerStates[index] ;
            device.SamplerStates[index] = SamplerState;   
            return ss;                
        }

        /// <summary>
        /// Sets the sampler states for all slots
        /// </summary>
        /// <param name="SamplerState">State of the sampler.</param>
        /// <param name="registers">The registers to change.</param>
#if REACH || WINDOWS_PHONE
       public void SetSamplerStates(SamplerState SamplerState, int registers = 2)
#else
       public void SetSamplerStates(SamplerState SamplerState, int registers = 16)
#endif
        {
            for (int i = 0; i < registers; i++)
            {
                device.SamplerStates[i] = SamplerState;
            }            
        }

        [Conditional("DEBUG")]
        public void ValidateSamplerStates()
        {
#if REACH || WINDOWS_PHONE
            for (int i = 0; i < 2; i++)          
#else
       for (int i = 0; i < 16; i++)
#endif

            {
                System.Diagnostics.Debug.Assert(device.SamplerStates[i] != null,"Invalid SamplerStates found");
                //if (device.SamplerStates[i] == null)
                //{
                //    throw new Exception("Invalid State Found");
                //}
            }
        }
        
        /// <summary>
        /// Sets the vertex sampler states.
        /// </summary>
        /// <param name="SamplerState">State of the sampler.</param>
        /// <param name="index">The index.</param>
        public void SetVertexSamplerStates(SamplerState SamplerState, int index)
        {
            System.Diagnostics.Debug.Assert(index >= 0);
            device.VertexSamplerStates[index] = SamplerState;
        }

        /// <summary>
        /// Gets the state of the sampler.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public SamplerState GetSamplerState(int index)
        {
            return device.SamplerStates[index];
        }

        /// <summary>
        /// Pushes the DepthState.
        /// </summary>
        /// <param name="state">The state.</param>
        public void PushDepthStencilState(DepthStencilState state)
        {
            device.DepthStencilState = state;
            DepthStencilStateStack.Push(state);
        }

        /// <summary>
        /// Peeks the DepthState.
        /// </summary>
        /// <returns></returns>
        public DepthStencilState PeekDepthState()
        {
            System.Diagnostics.Debug.Assert(device.DepthStencilState == DepthStencilStateStack.Peek());
            return DepthStencilStateStack.Peek();
        }

        /// <summary>
        /// Pops the DepthState.
        /// </summary>
        /// <returns></returns>
        public DepthStencilState PopDepthStencilState()
        {
            DepthStencilState rt = DepthStencilStateStack.Peek();
            DepthStencilStateStack.Pop();
            DepthStencilState rt2 = DepthStencilStateStack.Peek();
            device.DepthStencilState = rt2;
            return rt;
        }

        /// <summary>
        /// Pushed a Render Target Bindings
        /// </summary>
        /// <param name="binding"></param>
        public void PushRenderTargetBinding(RenderTargetBinding[] binding)
        {
            if (binding == null)
            {
                RenderStatesStack.Push(null);
                device.SetRenderTargets(null);
            }
            else
            {
                RenderStatesStack.Push(binding);
                device.SetRenderTargets(binding);
            }
        }

        public void SetCubeRenderTarget(RenderTargetCube RenderTargetCube,CubeMapFace face)
        {
            device.SetRenderTarget(RenderTargetCube,face);
        }

        public RenderTargetBinding[] RestorePreviousRenderTarget()
        {
            RenderTargetBinding[] rt = RenderStatesStack.Peek();                        
            device.SetRenderTargets(rt);
            return rt;            
        }

        /// <summary>
        /// Pushes the render target.
        /// </summary>
        /// <param name="renderTarget">The render target.</param>
        public void PushRenderTarget(params RenderTarget2D[] renderTarget)
        {
            if (renderTarget == null)
            {
                RenderStatesStack.Push(null);
                device.SetRenderTargets(null);
            }
            else
            {
                RenderTargetBinding[] bindings = new RenderTargetBinding[renderTarget.Count()];
                for (int i = 0; i < renderTarget.Count(); i++)
                {
                    bindings[i] = renderTarget[i];
                }

                RenderStatesStack.Push(bindings);
                device.SetRenderTargets(bindings);
            }
        }

        /// <summary>
        /// Pops the render target.
        /// </summary>
        /// <returns></returns>
        public RenderTargetBinding[] PopRenderTarget()
        {
            RenderTargetBinding[] rt = RenderStatesStack.Peek();
            RenderStatesStack.Pop();
            RenderTargetBinding[] rt2 = RenderStatesStack.Peek();
            device.SetRenderTargets(rt2);
            return rt;            
        }

        /// <summary>
        /// Pops the render target.
        /// </summary>
        /// <returns></returns>
        public RenderTarget2D PopRenderTargetAsSingleRenderTarget2D()
        {
            return PopRenderTarget()[0].RenderTarget as RenderTarget2D;
        }

        /// <summary>
        /// Sets the view port.
        /// </summary>
        /// <param name="viewPort">The view port.</param>
        public void SetViewPort(Viewport viewPort)
        {
            device.Viewport = viewPort;
        }

        /// <summary>
        /// Gets the view port.
        /// </summary>
        /// <returns></returns>
        public Viewport GetViewPort()
        {
            return device.Viewport ;
        }

        /// <summary>
        /// Clears actual target.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="options">The options.</param>
        /// <param name="depth">The depth.</param>
        /// <param name="stencil">The stencil.</param>
        public void Clear(Color color, ClearOptions options = ClearOptions.Target | ClearOptions.DepthBuffer, float depth = 1, int stencil = 0)
        {
            device.Clear(options, color, depth, stencil);
        }

        /// <summary>
        /// Gets the textures binded to the device
        /// </summary>
        public TextureCollection Textures
        {
            get
            {
                return device.Textures;
            }
        }
        
        /// <summary>
        /// Renders the batch.
        /// </summary>
        /// <param name="bi">The BatchInformation</param>
        /// <param name="effect">The effect.</param>
        public void RenderBatch(BatchInformation bi, Effect effect)
        {
            effect.CurrentTechnique.Passes[0].Apply();
            RenderBatch(bi);
        }

        /// <summary>
        /// Renders user primitive.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="effect">The effect.</param>
        /// <param name="PrimitiveType">Type of the primitive.</param>
        /// <param name="verts">The verts.</param>
        /// <param name="vertexOffset">The vertex offset.</param>
        /// <param name="primitiveCount">The primitive count.</param>
        public void RenderUserPrimitive<T>(Effect effect,PrimitiveType PrimitiveType, T[] verts, int vertexOffset, int primitiveCount) where T : struct, IVertexType
        {
            effect.CurrentTechnique.Passes[0].Apply(); 
            device.DrawUserPrimitives<T>(PrimitiveType, verts, vertexOffset, primitiveCount);
        }

        /// <summary>
        /// Renders the user primitive.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="PrimitiveType">Type of the primitive.</param>
        /// <param name="verts">The verts.</param>
        /// <param name="vertexOffset">The vertex offset.</param>
        /// <param name="primitiveCount">The primitive count.</param>
        public void RenderUserPrimitive<T>(PrimitiveType PrimitiveType, T[] verts, int vertexOffset, int primitiveCount) where T : struct, IVertexType
        {            
            device.DrawUserPrimitives<T>(PrimitiveType, verts, vertexOffset, primitiveCount);
        }

        /// <summary>
        /// Renders user indexed primitive. 16 bits indices
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="effect">The effect.</param>
        /// <param name="PrimitiveType">Type of the primitive.</param>
        /// <param name="verts">The verts.</param>
        /// <param name="vertexOffset">The vertex offset.</param>
        /// <param name="vertesCount">The vertes count.</param>
        /// <param name="indices">The indices.</param>
        /// <param name="indexOffset">The index offset.</param>
        /// <param name="primitiveCount">The primitive count.</param>
        public void RenderUserIndexedPrimitive<T>(Effect effect, PrimitiveType PrimitiveType, T[] verts, int vertexOffset, int vertesCount, short[] indices, int indexOffset, int primitiveCount) where T : struct, IVertexType
        {
            effect.CurrentTechnique.Passes[0].Apply(); 
            device.DrawUserIndexedPrimitives<T>(PrimitiveType, verts, vertexOffset, vertesCount, indices, indexOffset, primitiveCount);
        }

        /// <summary>
        /// Renders the user indexed primitive.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="PrimitiveType">Type of the primitive.</param>
        /// <param name="verts">The verts.</param>
        /// <param name="vertexOffset">The vertex offset.</param>
        /// <param name="vertesCount">The vertes count.</param>
        /// <param name="indices">The indices.</param>
        /// <param name="indexOffset">The index offset.</param>
        /// <param name="primitiveCount">The primitive count.</param>
        public void RenderUserIndexedPrimitive<T>(PrimitiveType PrimitiveType, T[] verts, int vertexOffset, int vertesCount, short[] indices, int indexOffset, int primitiveCount) where T : struct, IVertexType
        {         
            device.DrawUserIndexedPrimitives<T>(PrimitiveType, verts, vertexOffset, vertesCount, indices, indexOffset, primitiveCount);
        }

        /// <summary>
        /// Renders user indexed primitive. 32 bits indices
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="effect">The effect.</param>
        /// <param name="PrimitiveType">Type of the primitive.</param>
        /// <param name="verts">The verts.</param>
        /// <param name="vertexOffset">The vertex offset.</param>
        /// <param name="vertesCount">The vertes count.</param>
        /// <param name="indices">The indices.</param>
        /// <param name="indexOffset">The index offset.</param>
        /// <param name="primitiveCount">The primitive count.</param>
        public void RenderUserIndexedPrimitive<T>(Effect effect,PrimitiveType PrimitiveType, T[] verts, int vertexOffset, int vertesCount, int[] indices, int indexOffset, int primitiveCount) where T : struct, IVertexType
        {
            effect.CurrentTechnique.Passes[0].Apply(); 
            device.DrawUserIndexedPrimitives<T>(PrimitiveType, verts, vertexOffset, vertesCount, indices, indexOffset, primitiveCount);
        }

        /// <summary>
        /// Renders the user indexed primitive.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="PrimitiveType">Type of the primitive.</param>
        /// <param name="verts">The verts.</param>
        /// <param name="vertexOffset">The vertex offset.</param>
        /// <param name="vertesCount">The vertes count.</param>
        /// <param name="indices">The indices.</param>
        /// <param name="indexOffset">The index offset.</param>
        /// <param name="primitiveCount">The primitive count.</param>
        public void RenderUserIndexedPrimitive<T>(PrimitiveType PrimitiveType, T[] verts, int vertexOffset, int vertesCount, int[] indices, int indexOffset, int primitiveCount) where T : struct, IVertexType
        {            
            device.DrawUserIndexedPrimitives<T>(PrimitiveType, verts, vertexOffset, vertesCount, indices, indexOffset, primitiveCount);
        }

        
        /// <summary>
        /// Renders the batch.
        /// </summary>
        /// <param name="bi">The BatchInformation .</param>
        private void RenderBatch(BatchInformation bi)
        {
            if(bi.PrimitiveCount ==0)
                return;

            switch ( bi.BatchType)
	            {
		            case BatchType.INDEXED:
                        device.SetVertexBuffer(bi.VertexBuffer);
                        device.Indices = bi.IndexBuffer;
                        device.DrawIndexedPrimitives(bi.PrimitiveType, bi.BaseVertex, 0, bi.NumVertices, bi.StartIndex, bi.PrimitiveCount);
                        break;
                    case BatchType.NORMAL:
                        device.SetVertexBuffer(bi.VertexBuffer);
                        device.DrawPrimitives(bi.PrimitiveType, bi.StartVertex, bi.PrimitiveCount);                                            
                        break;
                    case BatchType.INSTANCED:
                        System.Diagnostics.Debug.Assert(bi.InstancedVertexBuffer != null);
                        System.Diagnostics.Debug.Assert(bi.InstanceCount > 0);
                        device.SetVertexBuffers(bi.VertexBuffer, new VertexBufferBinding(bi.InstancedVertexBuffer, 0, 1));                        
                        device.Indices = bi.IndexBuffer;
                        device.DrawInstancedPrimitives(PrimitiveType.TriangleList, bi.BaseVertex, 0, bi.NumVertices, bi.StartIndex, bi.PrimitiveCount, bi.InstanceCount);
                        break;
                    default:
                        ActiveLogger.LogMessage("Batch Type not supported ", LogLevel.RecoverableError);
                        break;
	            }
        }

        /// <summary>
        /// Gets the size of the current render target.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public Rectangle GetCurrentRenderTargetSize(int index = 0)
        {
            return (this.RenderStatesStack.Peek()[index].RenderTarget as Texture2D).Bounds;
        }

        /// <summary>
        /// Renders the text complete. (THIS FUNCTION ALREADY CALLS BEGIN AND END)
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        /// <param name="transform">The transform.</param>
        /// <param name="font">The font.</param>
        /// <param name="resyncState">if set to <c>true</c> [resync state].</param>
        /// <param name="SpriteSortMode">The sprite sort mode.</param>
        /// <param name="samplerState">State of the sampler.</param>
        /// <param name="blenderState">State of the blender.</param>
        /// <param name="rasterizerState">State of the rasterizer.</param>
        /// <param name="depthState">State of the depth.</param>
        /// <param name="effect">The effect.</param>
        public void RenderTextComplete(String text, Vector2 position, Color color, Matrix transform, SpriteFont font = null,bool resyncState = true, SpriteSortMode SpriteSortMode = SpriteSortMode.Deferred, SamplerState samplerState = null, BlendState blenderState = null, RasterizerState rasterizerState = null, DepthStencilState depthState = null, Effect effect = null)
        {            
            spriteBatch.Begin(SpriteSortMode, blenderState, samplerState, depthState, rasterizerState, effect, transform);
            if (font == null)
                font = defaultFont;
            spriteBatch.DrawString(font, text, position, color);
            spriteBatch.End();
            if (resyncState)
                ResyncStates();
        }

        /// <summary>
        /// Renders the texture.
        /// CALL THIS ONLY AFTER RenderBegin
        /// AFTER DRAWING ALL THE TEXTURES CALL RenderEnd
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="position">The position.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="color">The color.</param>
        /// <param name="font">The font.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="effects">The effects.</param>
        /// <param name="layerDepth">The layer depth.</param>
        public void RenderText(StringBuilder text, Vector2 position, Vector2 scale, Color color, SpriteFont font = null, float rotation = 0, SpriteEffects effects = SpriteEffects.None, float layerDepth = 0)
        {
            if (font == null)
                font = defaultFont;
            spriteBatch.DrawString(font, text, position, color, rotation, Vector2.Zero, scale, effects, layerDepth);
        }

        /// <summary>
        /// Renders the text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="position">The position.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="color">The color.</param>
        /// <param name="font">The font.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="effects">The effects.</param>
        /// <param name="layerDepth">The layer depth.</param>
        public void RenderText(string text, Vector2 position, Vector2 scale, Color color, SpriteFont font = null, float rotation = 0, SpriteEffects effects = SpriteEffects.None, float layerDepth = 0)
        {
            if (font == null)
                font = defaultFont;
            spriteBatch.DrawString(font, text, position, color, rotation, Vector2.Zero, scale, effects, layerDepth);
        }

        /// <summary>
        /// Begin render Texture.
        /// DO NOT USE THIS WITH RenderTextureComplete (RenderTextureComplete does all the job inside)
        /// </summary>
        /// <param name="transform">The transform.</param>
        /// <param name="source">The source.</param>
        /// <param name="SpriteSortMode">The sprite sort mode.</param>
        /// <param name="samplerState">State of the sampler.</param>
        /// <param name="blenderState">State of the blender.</param>
        /// <param name="rasterizerState">State of the rasterizer.</param>
        /// <param name="depthState">State of the depth.</param>
        /// <param name="effect">The effect.</param>
        public void RenderBegin(Matrix transform, Rectangle? source = null, SpriteSortMode SpriteSortMode = SpriteSortMode.Deferred, SamplerState samplerState = null, BlendState blenderState = null, RasterizerState rasterizerState = null, DepthStencilState depthState = null, Effect effect = null)
        {
            spriteBatch.Begin(SpriteSortMode, blenderState, samplerState, depthState, rasterizerState, effect, transform);
        }

        /// <summary>
        /// Renders the texture.
        /// CALL THIS ONLY AFTER RenderBegin
        /// AFTER DRAWING ALL THE TEXTURES CALL RenderEnd
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="color">The color.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="source">The source.</param>
        public void RenderTexture(Texture2D texture,Color color, Rectangle destination, Rectangle? source = null)
        {
            spriteBatch.Draw(texture, destination, source, color);
        }

        /// <summary>
        /// Renders the texture.
        /// CALL THIS ONLY AFTER RenderBegin
        /// AFTER DRAWING ALL THE TEXTURES CALL RenderEnd
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="origin">The origin.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="SpriteEffects">The sprite effects.</param>
        /// <param name="layerdepth">The layerdepth.</param>
        public void RenderTexture(Texture2D texture,Vector2 position,Color color, float rotation, Vector2
              origin, float scale = 1,SpriteEffects SpriteEffects = SpriteEffects.None,float layerdepth = 0)
        {
            spriteBatch.Draw(texture, position, null, color, rotation, origin, scale, SpriteEffects, layerdepth);
        }

        /// <summary>
        /// Renders the texture.
        /// CALL THIS ONLY AFTER RenderBegin
        /// AFTER DRAWING ALL THE TEXTURES CALL RenderEnd
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="position">The position.</param>
        /// <param name="sourceRectangle">The source rectangle.</param>
        /// <param name="color">The color.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="origin">The origin.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="SpriteEffects">The sprite effects.</param>
        /// <param name="layerdepth">The layerdepth.</param>
        public void RenderTexture(Texture2D texture, Vector2 position,Rectangle? sourceRectangle, Color color, float rotation, Vector2
              origin, float scale = 1, SpriteEffects SpriteEffects = SpriteEffects.None, float layerdepth = 0)
        {
            spriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, SpriteEffects, layerdepth);
        }

        /// <summary>
        /// End Rendering the texture
        /// ONLY USE THIS if you call RenderBegin BEFORE
        /// </summary>
        public void RenderEnd(bool resyncState = true)
        {
            spriteBatch.End();
            if(resyncState)
                ResyncStates();
        }


        /// <summary>
        /// Renders the texture (Begin, Texture,End).
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="resyncState">if set to <c>true</c> [resync state].</param>
        public void RenderTextureComplete(Texture2D texture,bool resyncState = true)
        {            
            spriteBatch.Begin();
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);
            spriteBatch.End();
            if (resyncState)
                ResyncStates();
        }

        /// <summary>
        /// Renders the texture (Begin, render , End)
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="color">The color.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="transform">The transform.</param>
        /// <param name="source">The source.</param>
        /// <param name="resyncState">if set to <c>true</c> [resync state].</param>
        /// <param name="SpriteSortMode">The sprite sort mode.</param>
        /// <param name="samplerState">State of the sampler.</param>
        /// <param name="blenderState">State of the blender.</param>
        /// <param name="rasterizerState">State of the rasterizer.</param>
        /// <param name="depthState">State of the depth.</param>
        /// <param name="effect">The effect.</param>
        public void RenderTextureComplete(Texture2D texture, Color color, Rectangle destination, Matrix transform, Rectangle? source = null, bool resyncState = true, SpriteSortMode SpriteSortMode = SpriteSortMode.Deferred, SamplerState samplerState = null, BlendState blenderState = null, RasterizerState rasterizerState = null, DepthStencilState depthState = null, Effect effect = null)
        {
            spriteBatch.Begin(SpriteSortMode, blenderState, samplerState, depthState, rasterizerState, effect, transform);
            spriteBatch.Draw(texture, destination,source, color);
            spriteBatch.End();
            if (resyncState)
                ResyncStates();
        }

        /// <summary>
        /// Renders the texture to the render target
        /// Shortcut for Down/Up sampling
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="targetSize">Size of the target.</param>
        /// <param name="resyncState">if set to <c>true</c> [resync state].</param>
        /// <param name="SpriteSortMode">The sprite sort mode.</param>
        /// <param name="samplerState">State of the sampler.</param>
        /// <param name="blenderState">State of the blender.</param>
        /// <param name="rasterizerState">State of the rasterizer.</param>
        /// <param name="depthState">State of the depth.</param>
        /// <param name="effect">The effect.</param>
        public void RenderTextureComplete(Texture2D texture, Rectangle targetSize , bool resyncState = true, SpriteSortMode SpriteSortMode = SpriteSortMode.Deferred, SamplerState samplerState = null, BlendState blenderState = null, RasterizerState rasterizerState = null, DepthStencilState depthState = null, Effect effect = null)
        {
            spriteBatch.Begin(SpriteSortMode, blenderState, samplerState, depthState, rasterizerState, effect, Matrix.Identity);
            spriteBatch.Draw(texture, targetSize, texture.Bounds, Color.White);
            spriteBatch.End();
            if (resyncState)
                ResyncStates();
        }

        /// <summary>
        /// Resyncs the Device States
        /// THIS IS BECAUSE THE SPRITEBATCH KILLS THE RENDER STATES, NO SAVE STATE. XNA DOCUMENTATION FAILS !!!!
        /// </summary>
        public void ResyncStates()
        {
            device.BlendState = BlendStateStack.Peek();
            device.DepthStencilState = DepthStencilStateStack.Peek();
            device.RasterizerState = RasterizerStateStack.Peek();            
            device.SetRenderTargets(RenderStatesStack.Peek());                        
        }

#if WINDOWS_PHONE || REACH
        public void DettachBindedTextures(int numberofTextures = 2)
        {
        if(numberofTextures > 2)
            numberofTextures = 2;
#else
        public void DettachBindedTextures(int numberofTextures = 16)
        {
#endif
        
            for (int i = 0; i < numberofTextures; i++)
            {
                device.Textures[i] = null;
            }
        }

        /// <summary>
        /// Gets or sets a scene with the specified name.
        /// </summary>
        public Texture2D this[String scene]
        {
            get
            {
                return Scenes[scene];
            }
            set
            {
                Scenes[scene] = value;
            }
        }

#if !WINDOWS_PHONE && !REACH
        /// <summary>
        /// Renders the texture to full screen using vertex and pixel shader .
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="samplerStates">The sampler states.</param>
        public void RenderFullScreenQuadVertexPixel(Effect effect,params SamplerState[] samplerStates)
        {

                if (samplerStates != null)
            {
                for (int i = 0; i < samplerStates.Count(); i++)
                {
                    SetSamplerState(samplerStates[i], i);
                }
            }           
            qrender.DrawQuad(effect);
        }

        /// <summary>
        /// Renders the full screen quad vertex pixel.
        /// </summary>
        /// <param name="effect">The effect.</param>
        public void RenderFullScreenQuadVertexPixel(Effect effect)
        {
            qrender.DrawQuad(effect);
        }
#endif
        /// <summary>
        /// Renders the texture to full screen using sprite batch.
        /// </summary>
        /// <param name="scene">The scene.</param>
        /// <param name="effect">The effect.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="samplerState">State of the sampler.</param>
        /// <param name="blenderState">State of the blender.</param>
        /// <param name="sync">if set to <c>true</c> [sync].</param>
        public void RenderTextureToFullScreenSpriteBatch(Texture2D scene, Effect effect, Rectangle rectangle, SamplerState samplerState = null, BlendState blenderState = null, bool sync = true)
        {
            effect.CurrentTechnique.Passes[0].Apply();
            spriteBatch.Begin(SpriteSortMode.Immediate, blenderState, samplerState, null, null, effect, Matrix.Identity);

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                spriteBatch.Draw(scene, rectangle, Color.White);                
            }
            spriteBatch.End();

            if (sync)
                ResyncStates();
        }

#if !WINDOWS_PHONE && !REACH
        /// <summary>
        /// Gets the recomended sampler for the actual render target.
        /// </summary>
        /// <returns></returns>
        public SamplerState GetRecomendedSamplerForTheActualRenderTarget()
        {
            SurfaceFormat format = RenderStatesStack.Peek()[0].RenderTarget.Format;
            if (format == SurfaceFormat.HdrBlendable || format == SurfaceFormat.Vector4 || format == SurfaceFormat.Single)
            {
                return SamplerState.PointClamp;
            }
            return SamplerState.AnisotropicClamp;
        }
#endif
        /// <summary>
        /// Renders the texture to full screen using sprite batch.
        /// </summary>
        /// <param name="texture">The texture name (already in this class).</param>
        /// <param name="effect">The effect.</param>
        /// <param name="samplerState">State of the sampler.</param>
        /// <param name="blenderState">State of the blender.</param>
        /// <param name="sync">if set to <c>true</c> [sync].</param>
        public void RenderTextureToFullScreenSpriteBatch(String texture, Effect effect, SamplerState samplerState = null, BlendState blenderState = null,bool sync = true)
        {
            System.Diagnostics.Debug.Assert(Scenes.ContainsKey(texture),"Texture not found in Render, make sure it exists");
            effect.CurrentTechnique.Passes[0].Apply();           
            
            spriteBatch.Begin(SpriteSortMode.Immediate, blenderState, samplerState, null, null, effect, Matrix.Identity);

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                spriteBatch.Draw(this[texture], Vector2.Zero, Color.White);

            }
            spriteBatch.End();

            if (sync)
                ResyncStates();
        }

        /// <summary>
        /// Renders the scene to a texture cube.
        /// Usefull for high quality reflections
        /// </summary>
        /// <param name="renderTargetCube">The render target cube.</param>
        /// <param name="backGroundColor">Color of the back ground.</param>
        /// <param name="world">The world.</param>
        /// <param name="objPos">The obj pos.</param>
        /// <param name="gt">The gt.</param>
        /// <param name="drawComponentsPreDraw">if set to <c>true</c> [draw components pre draw].</param>
        /// <param name="useCuller">if set to <c>true</c> [use culler].</param>
        /// <param name="objListException">The obj list exception.</param>
        public void RenderSceneToTextureCube(RenderTargetCube renderTargetCube, Color backGroundColor, IWorld world,
            ref Vector3 objPos, GameTime gt,bool drawComponentsPreDraw = true,bool useCuller = false
            , List<IObject> objListException = null, float nearPlane = 1, float farPlane = 1000
            )
        {
            System.Diagnostics.Debug.Assert(renderTargetCube != null);
            
            Matrix proj = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2, 1, nearPlane, farPlane);
            proj.M33 *= -1;
            proj.M34 *= -1;
            Matrix viewMatrix = new Matrix();
            // Render our cube map, once for each cube face( 6 times ).
            for (int i = 0; i < 6; i++)
            {
                // render the scene to all cubemap faces
                CubeMapFace cubeMapFace = (CubeMapFace)i;

                switch (cubeMapFace)
                {
                    case CubeMapFace.NegativeX:
                        {
                            viewMatrix = Matrix.CreateLookAt(objPos, objPos -  Vector3.Left, Vector3.Up);
                            break;
                        }
                    case CubeMapFace.NegativeY:
                        {
                            viewMatrix = Matrix.CreateLookAt(objPos, objPos - Vector3.Down,-Vector3.Forward);
                            break;
                        }
                    case CubeMapFace.NegativeZ:
                        {
                            viewMatrix = Matrix.CreateLookAt(objPos, objPos + Vector3.Backward, Vector3.Up);
                            break;
                        }
                    case CubeMapFace.PositiveX:
                        {
                            viewMatrix = Matrix.CreateLookAt(objPos, objPos - Vector3.Right, Vector3.Up);
                            break;
                        }
                    case CubeMapFace.PositiveY:
                        {
                            viewMatrix = Matrix.CreateLookAt(objPos, objPos - Vector3.Up, -Vector3.Backward);
                            break;
                        }
                    case CubeMapFace.PositiveZ:
                        {
                            viewMatrix = Matrix.CreateLookAt(objPos, objPos + Vector3.Forward, Vector3.Up);
                            break;
                        }
                }                
                
                // Set the cubemap render target, using the selected face
                SetCubeRenderTarget(renderTargetCube, cubeMapFace);
                Clear(backGroundColor);
                RenderSceneWithBasicMaterial(world, gt, objListException, ref viewMatrix, ref proj, drawComponentsPreDraw, useCuller);
                RestorePreviousRenderTarget();                
            }
                        
        }

        /// <summary>
        /// Renders the scene without material.
        /// Uses XNA Basic Effect
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="gt">The gt.</param>
        /// <param name="objListException">The obj list exception.(objects in this list wont be rendered) - can be null</param>
        /// <param name="view">The view.</param>
        /// <param name="projection">The projection.</param>
        /// <param name="drawComponentsPreDraw">if set to <c>true</c> [draw components with pre draw setting also].</param>
        /// <param name="useCuller">if set to <c>true</c> [use culler].</param>
        public void RenderSceneWithBasicMaterial(IWorld world, GameTime gt, List<IObject> objListException, ref Matrix view, ref Matrix projection, bool drawComponentsPreDraw = true,bool useCuller = false)
        {
            if (drawComponentsPreDraw)
            {
                 componentManager.PreDraw(this,gt, ref view, ref projection);
        
            }
            
            effect.Projection = projection;
            effect.View = view;
            effect.EnableDefaultLighting();

            IEnumerable<IObject> objs;
            if (useCuller)
            {
                world.Culler.StartFrame(ref view, ref projection, new BoundingFrustum(view * projection));
                objs = world.Culler.GetNotCulledObjectsList(null);                
            }
            else
            {
                objs = world.Objects;
            }

            foreach (var obj in objs)
            {
                if (objListException != null && objListException.Contains(obj))
                    continue;

                if (!obj.Material.IsVisible)
                    continue;

                if (obj.Modelo == null)
                    continue;

                for (int i = 0; i < obj.Modelo.MeshNumber; i++)
                {
                    BatchInformation[] bi = obj.Modelo.GetBatchInformation(i);
                    for (int j = 0; j < bi.Count(); j++)
                    {
                        effect.Texture = obj.Modelo.GetTextureInformation(i)[j].getTexture(TextureType.DIFFUSE);
                        effect.World = bi[j].ModelLocalTransformation * obj.WorldMatrix;
                        RenderBatch(bi[j], effect);
                    }
                }

            }
        }

#if !WINDOWS_PHONE && !REACH
        /// <summary>
        /// Renders the scene with custom material.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="setupShaderCallback">The setup shader callback.</param>
        /// <param name="world">The world.</param>
        /// <param name="gt">The gt.</param>
        /// <param name="objListException">The obj list exception.</param>
        /// <param name="view">The view.</param>
        /// <param name="projection">The projection.</param>
        /// <param name="drawComponentsPreDraw">if set to <c>true</c> [draw components pre draw].</param>
        /// <param name="useCuller">if set to <c>true</c> [use culler].</param>
        public void RenderSceneWithCustomMaterial(Effect effect,OnDrawingSceneCustomMaterial setupShaderCallback, IWorld world, GameTime gt, List<IObject> objListException, ref Matrix view, ref Matrix projection, bool drawComponentsPreDraw = true, bool useCuller = false)
        {
            if (drawComponentsPreDraw)
            {
                componentManager.PreDraw(this,gt, ref view, ref projection);             
            }
            
            IEnumerable<IObject> objs;
            if (useCuller)
            {
                world.Culler.StartFrame(ref view, ref projection, new BoundingFrustum(view * projection));
                objs = world.Culler.GetNotCulledObjectsList(null);
            }
            else
            {
                objs = world.Objects;
            }

            foreach (var obj in objs)
            {
                if (objListException != null && objListException.Contains(obj))
                    continue;

                Matrix vp = view * projection;                

                for (int i = 0; i < obj.Modelo.MeshNumber; i++)
                {
                    BatchInformation[] bi = obj.Modelo.GetBatchInformation(i);
                    TextureInformation[] ti = obj.Modelo.GetTextureInformation(i);

                    for (int j = 0; j < bi.Count(); j++)
                    {
                        setupShaderCallback(effect, obj, bi[j], ti[j], view, projection, vp);
                        this.RenderBatch(bi[j], effect);
                        
                    }
                }
            }
        }

        /// <summary>
        /// Renders the scene depth.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="gt">The gt.</param>
        /// <param name="objListException">The obj list exception.</param>
        /// <param name="view">The view.</param>
        /// <param name="projection">The projection.</param>
        /// <param name="useCuller">if set to <c>true</c> [use culler].</param>
        public void RenderSceneDepth(IWorld world, GameTime gt, List<IObject> objListException, ref Matrix view, ref Matrix projection, bool useCuller = false)
        {            
            IEnumerable<IObject> objs;
            if (useCuller)
            {
                world.Culler.StartFrame(ref view, ref projection, new BoundingFrustum(view * projection));
                objs = world.Culler.GetNotCulledObjectsList(null);
            }
            else
            {
                objs = world.Objects;
            }

            foreach (var obj in objs)
            {
                if (objListException != null && objListException.Contains(obj))
                    continue;
                             
                if(obj.Material.CanCreateShadow)
                    obj.Material.Shader.DepthExtractor(gt, obj, ref view,ref  projection, this);                
            }
        }

        /// <summary>
        /// Renders the scene depth.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="gt">The gt.</param>
        /// <param name="view">The view.</param>
        /// <param name="projection">The projection.</param>
        /// <param name="useCuller">if set to <c>true</c> [use culler].</param>
        public void RenderSceneDepth(IWorld world, GameTime gt, ref Matrix view, ref Matrix projection, bool useCuller = false)
        {
            IEnumerable<IObject> objs;
            if (useCuller)
            {
                world.Culler.StartFrame(ref view, ref projection, new BoundingFrustum(view * projection));
                objs = world.Culler.GetNotCulledObjectsList(null);
            }
            else
            {
                objs = world.Objects;
            }

            foreach (var obj in objs)
            {
                if (obj.Material.CanCreateShadow && obj.Material.IsVisible)
                    obj.Material.Shader.DepthExtractor(gt, obj, ref view, ref projection, this);
            }
        }

        /// <summary>
        /// Renders the scene reflection refration.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="gt">The gt.</param>
        /// <param name="objListException">The obj list exception.</param>
        /// <param name="view">The view.</param>
        /// <param name="projection">The projection.</param>
        /// <param name="drawComponentsPreDraw">if set to <c>true</c> [draw components pre draw].</param>
        /// <param name="useCuller">if set to <c>true</c> [use culler].</param>
        /// <param name="clippingPlane">The clipping plane.</param>
        /// <param name="useAlphaBlend">if set to <c>true</c> [use alpha blend].</param>
        public void RenderSceneReflectionRefration(IWorld world, GameTime gt, List<IObject> objListException, ref Matrix view, ref Matrix projection,bool drawComponentsPreDraw = true ,bool useCuller = false,Plane? clippingPlane = null,bool useAlphaBlend = false)
        {
            if (drawComponentsPreDraw)
            {
                componentManager.PreDraw(this, gt, ref view, ref projection);
            }

            IEnumerable<IObject> objs;
            if (useCuller)
            {
                world.Culler.StartFrame(ref view, ref projection, new BoundingFrustum(view * projection));
                objs = world.Culler.GetNotCulledObjectsList(null);
            }
            else
            {
                objs = world.Objects;
            }

            foreach (var obj in objs)
            {
                if (objListException != null && objListException.Contains(obj))
                    continue;

                if(obj.Material.CanAppearOfReflectionRefraction && obj.Material.IsVisible)
                    obj.Material.Shader.BasicDraw(gt, obj, ref  view,ref projection, world.Lights, this, clippingPlane, useAlphaBlend);
            }
        }

#endif
    }

    /// <summary>
    /// OnDrawingScene with CustomMaterial
    /// </summary>
    /// <param name="effect">The effect.</param>
    /// <param name="obj">The obj.</param>
    /// <param name="bi">The bi.</param>
    /// <param name="ti">The ti.</param>
    /// <param name="view">The view.</param>
    /// <param name="projection">The projection.</param>
    /// <param name="vp">The vp.</param>
    public delegate void OnDrawingSceneCustomMaterial(Effect effect,IObject obj, BatchInformation bi, TextureInformation ti,Matrix view,Matrix projection, Matrix vp );




}
