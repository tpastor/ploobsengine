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
        private GraphicsDevice device;
        private QuadRender qrender;
        private BasicEffect effect;
        private SpriteBatch spriteBatch;
        private static Stack<RenderTargetBinding[]> RenderStatesStack = new Stack<RenderTargetBinding[]>();
        private static Dictionary<String, Texture2D> Scenes = new Dictionary<string, Texture2D>();


        public RenderHelper(GraphicsDevice device)
        {
            this.device = device;
            spriteBatch = new SpriteBatch(device);
            effect = new BasicEffect(device);
            qrender = new QuadRender(device);
        }        

        public void PushRenderTarget(params RenderTarget2D[] renderTarget)
        {
            RenderTargetBinding[] bindings = new RenderTargetBinding[renderTarget.Count()];
            for (int i = 0; i < renderTarget.Count(); i++)
            {
                bindings[i] = renderTarget[i];
            }

            RenderStatesStack.Push(bindings);
            device.SetRenderTargets(bindings);
        }

        public void Clear(Color color,ClearOptions options = ClearOptions.Target | ClearOptions.DepthBuffer,float depth = 1, int stencil = 0)
        {
            device.Clear(options, color, depth, stencil);
        }

        public RenderTargetBinding[] PopRenderTarget()
        {
            RenderTargetBinding[] rt = RenderStatesStack.Peek();
            RenderStatesStack.Pop();
            RenderTargetBinding[] rt2 = RenderStatesStack.Peek();
            device.SetRenderTargets(rt2);
            return rt;            
        }

        public void RenderBatch(ref BatchInformation bi)
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
                    default:
                        ActiveLogger.LogMessage("Batch Type not supported ", LogLevel.RecoverableError);
                        break;
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


        /// <summary>
        /// Renders the texture to full screen using vertex and pixel shader .
        /// </summary>
        /// <param name="effect">The effect.</param>
        public void RenderTextureToFullScreenVertexPixel(Effect effect)
        {
            qrender.DrawQuad(effect);
        }

        /// <summary>
        /// Renders the texture to full screen using sprite batch.
        /// </summary>
        /// <param name="scene">The scene.</param>
        /// <param name="effect">The effect.</param>
        public void RenderTextureToFullScreenSpriteBatch(Texture2D scene, Effect effect, Rectangle rectangle)
        {
            effect.CurrentTechnique.Passes[0].Apply();
            spriteBatch.Begin(SpriteSortMode.Immediate,null,null,null,null,effect,Matrix.Identity);

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                spriteBatch.Draw(scene, rectangle, Color.White);
                
            }
            spriteBatch.End();            
        }

        /// <summary>
        /// Renders the texture to full screen using sprite batch.
        /// </summary>
        /// <param name="texture">The texture name (already in this class).</param>
        /// <param name="effect">The effect.</param>
        public void RenderTextureToFullScreenSpriteBatch(String texture, Effect effect)
        {
            System.Diagnostics.Debug.Assert(Scenes.ContainsKey(texture));
            effect.CurrentTechnique.Passes[0].Apply();
            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, effect, Matrix.Identity);

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                spriteBatch.Draw(this[texture], Vector2.Zero, Color.White);

            }
            spriteBatch.End();            
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
        /// <param name="componentManager">The component manager, can be NULL, if drawComponentsPreDraw is false.</param>
        /// <param name="drawComponentsPreDraw">if set to <c>true</c> [draw components with pre draw setting also].</param>
        public void RenderSceneWithoutMaterial(IWorld world, GameTime gt, List<IObject> objListException, Matrix view, Matrix projection, ComponentManager componentManager, bool drawComponentsPreDraw = true)
        {
            if (drawComponentsPreDraw)
            {
                if (componentManager == null)
                {
                    ActiveLogger.LogMessage("ComponentManager is null but, drawComponentsPreDraw is true -> inconsistency", LogLevel.RecoverableError);
                }
                else
                {
                    componentManager.PreDraw(gt, view, projection);
                }
            }
            
            effect.Projection = projection;
            effect.View = view;
            effect.EnableDefaultLighting();                        

            foreach (var obj in world.Objects)
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
       
    }

}
