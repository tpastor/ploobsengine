using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.Modelo;
using PloobsEngine.Components;
using PloobsEngine.Engine.Logger;

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
        internal RenderHelper(GraphicsDevice device,ComponentManager componentManager,IContentManager cmanager)
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
        public void RenderPreComponents(GameTime gametime, Matrix view, Matrix projection)
        {
            componentManager.PreDraw(this,gametime, view, projection);
        }
        /// <summary>
        /// Renders the pos components.
        /// </summary>
        /// <param name="gametime">The gametime.</param>
        /// <param name="view">The view.</param>
        /// <param name="projection">The projection.</param>
        public void RenderPosComponents(GameTime gametime, Matrix view, Matrix projection)
        {
            componentManager.AfterDraw(this,gametime, view, projection);
        }

        /// <summary>
        /// Renders the pos with depth components.
        /// </summary>
        /// <param name="gametime">The gametime.</param>
        /// <param name="view">The view.</param>
        /// <param name="projection">The projection.</param>
        public void RenderPosWithDepthComponents(GameTime gametime, Matrix view, Matrix projection)
        {
            componentManager.PosWithDepthDraw(this, gametime, view, projection);
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
        public void SetSamplerState(SamplerState SamplerState, int index)
        {
            device.SamplerStates[index] = SamplerState;                    
        }

        /// <summary>
        /// Sets the vertex sampler states.
        /// </summary>
        /// <param name="SamplerState">State of the sampler.</param>
        /// <param name="index">The index.</param>
        public void SetVertexSamplerStates(SamplerState SamplerState, int index)
        {
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

        public void SetViewPort(Viewport viewPort)
        {
            device.Viewport = viewPort;
        }

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
        /// Renders the batch.
        /// </summary>
        /// <param name="bi">The BatchInformation .</param>
        private void RenderBatch(BatchInformation bi)
        {
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
        /// Resyncs the Device States
        /// THIS IS BECAUSE THE SPRITEBATCH KILL THE RENDER STATES, NO SAVE STATE. XNA DOCUMENTATION FAILS !!!!
        /// </summary>
        public void ResyncStates()
        {
            device.BlendState = BlendStateStack.Peek();
            device.DepthStencilState = DepthStencilStateStack.Peek();
            device.RasterizerState = RasterizerStateStack.Peek();
            device.SetRenderTargets(RenderStatesStack.Peek());            
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


        /// <summary>
        /// Renders the texture to full screen using vertex and pixel shader .
        /// </summary>
        /// <param name="effect">The effect.</param>
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

        /// <summary>
        /// Renders the texture to full screen using sprite batch.
        /// </summary>
        /// <param name="scene">The scene.</param>
        /// <param name="effect">The effect.</param>
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

        public SamplerState GetRecomendedSamplerForTheActualRenderTarget()
        {
            SurfaceFormat format = RenderStatesStack.Peek()[0].RenderTarget.Format;
            if (format == SurfaceFormat.HdrBlendable || format == SurfaceFormat.Vector4 || format == SurfaceFormat.Single)
            {
                return SamplerState.PointClamp;
            }
            return SamplerState.AnisotropicClamp;
        }

        /// <summary>
        /// Renders the texture to full screen using sprite batch.
        /// </summary>
        /// <param name="texture">The texture name (already in this class).</param>
        /// <param name="effect">The effect.</param>
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
        public void RenderSceneWithBasicMaterial(IWorld world, GameTime gt, List<IObject> objListException, Matrix view, Matrix projection, bool drawComponentsPreDraw = true,bool useCuller = false)
        {
            if (drawComponentsPreDraw)
            {
                 componentManager.PreDraw(this,gt, view, projection);
        
            }
            
            effect.Projection = projection;
            effect.View = view;
            effect.EnableDefaultLighting();

            IEnumerable<IObject> objs;
            if (useCuller)
            {
                world.Culler.StartFrame(view, projection, new BoundingFrustum(view * projection));
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
                
                Matrix wld = obj.WorldMatrix;
                for (int i = 0; i < obj.Modelo.MeshNumber; i++)
                {
                    BatchInformation[] bi = obj.Modelo.GetBatchInformation(i);                    

                    for (int j = 0; j < bi.Count(); j++)
                    {                        
                        Matrix w1 = Matrix.Multiply(wld, bi[j].ModelLocalTransformation);
                        effect.World = w1;
                        effect.CurrentTechnique.Passes[0].Apply();                        
                        device.SetVertexBuffer(bi[j].VertexBuffer, bi[j].StreamOffset);
                        device.Indices = bi[j].IndexBuffer;
                        device.DrawIndexedPrimitives(PrimitiveType.TriangleList, bi[j].BaseVertex, 0, bi[j].NumVertices, bi[j].StartIndex, bi[j].PrimitiveCount);                                                
                    }
                }
            }
        }

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
        public void RenderSceneWithCustomMaterial(Effect effect,OnDrawingSceneCustomMaterial setupShaderCallback, IWorld world, GameTime gt, List<IObject> objListException, Matrix view, Matrix projection, bool drawComponentsPreDraw = true, bool useCuller = false)
        {
            if (drawComponentsPreDraw)
            {
                componentManager.PreDraw(this,gt, view, projection);             
            }
            
            IEnumerable<IObject> objs;
            if (useCuller)
            {
                world.Culler.StartFrame(view, projection, new BoundingFrustum(view * projection));
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

                Matrix wld = obj.WorldMatrix;
                for (int i = 0; i < obj.Modelo.MeshNumber; i++)
                {
                    BatchInformation[] bi = obj.Modelo.GetBatchInformation(i);

                    for (int j = 0; j < bi.Count(); j++)
                    {
                        setupShaderCallback(effect,obj, bi[j], ref view, ref projection);
                        this.RenderBatch(bi[j], effect);
                        
                    }
                }
            }
        }

        public void RenderSceneDepth(IWorld world, GameTime gt, List<IObject> objListException, Matrix view, Matrix projection, bool useCuller = false)
        {            
            IEnumerable<IObject> objs;
            if (useCuller)
            {
                world.Culler.StartFrame(view, projection, new BoundingFrustum(view * projection));
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
                    obj.Material.Shadder.DepthExtractor(gt, obj, view,projection, this);                
            }
        }

        public void RenderSceneDepth(IWorld world, GameTime gt, Matrix view, Matrix projection, bool useCuller = false)
        {
            IEnumerable<IObject> objs;
            if (useCuller)
            {
                world.Culler.StartFrame(view, projection, new BoundingFrustum(view * projection));
                objs = world.Culler.GetNotCulledObjectsList(null);
            }
            else
            {
                objs = world.Objects;
            }

            foreach (var obj in objs)
            {

                if (obj.Material.CanCreateShadow)
                    obj.Material.Shadder.DepthExtractor(gt, obj, view, projection, this);
            }
        }

        public void RenderSceneReflectionRefration(IWorld world, GameTime gt, List<IObject> objListException, Matrix view, Matrix projection,bool drawComponentsPreDraw = true ,bool useCuller = false,Plane? clippingPlane = null,bool useAlphaBlend = false)
        {
            if (drawComponentsPreDraw)
            {
                componentManager.PreDraw(this, gt, view, projection);
            }

            IEnumerable<IObject> objs;
            if (useCuller)
            {
                world.Culler.StartFrame(view, projection, new BoundingFrustum(view * projection));
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

                if(obj.Material.CanAppearOfReflectionRefraction)
                    obj.Material.Shadder.BasicDraw(gt, obj, view,projection, world.Lights, this, clippingPlane, useAlphaBlend);
            }
        }
       
    }
    public delegate void OnDrawingSceneCustomMaterial(Effect effect,IObject obj, BatchInformation bi,ref Matrix view,ref Matrix projection);

}
