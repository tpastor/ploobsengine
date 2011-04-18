using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Helper To Restore Depth in deferred Pass
    /// In XNA its needed cause when we change Render Targets, the depth buffer is erased
    /// </summary>
    internal class RestoreDepth
    {
        private bool usefloatBuffer;
        /// <summary>
        /// Initializes a new instance of the <see cref="RestoreDepth"/> class.
        /// </summary>
        /// <param name="useFloatBuffer">if set to <c>true</c> [use float buffer].</param>
        /// <param name="manager">The manager.</param>
        /// <param name="factory">The factory.</param>
        /// <param name="ginfo">The ginfo.</param>
        public RestoreDepth(bool useFloatBuffer,IContentManager manager,GraphicFactory factory, GraphicInfo ginfo)
        {
            this.usefloatBuffer = useFloatBuffer;
            this.restore = manager.GetAsset<Effect>("RestoreDepth",true);
            this.restore.Parameters["halfPixel"].SetValue(ginfo.HalfPixel);                
            if (useFloatBuffer)
                target = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.HdrBlendable, false, DepthFormat.Depth24Stencil8, 0, RenderTargetUsage.DiscardContents);
            else
                target = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color, false, DepthFormat.Depth24Stencil8, 0, RenderTargetUsage.DiscardContents);
        }        

        private RenderTarget2D target;
        private Effect restore;        

        /// <summary>
        /// Performs the forward pass.
        /// </summary>
        /// <param name="combined">The combined.</param>
        /// <param name="depth">The depth.</param>
        /// <param name="render">The render.</param>
        public void PerformForwardPass(Texture2D combined, Texture2D depth, RenderHelper render)
        {
            render.PushRenderTarget(target);                           
            restore.Parameters["DepthTexture"].SetValue(depth);                
            restore.Parameters["ColorTexture"].SetValue(combined);
            if (usefloatBuffer)
            {
                render.SetSamplerState(SamplerState.PointClamp, 0);
            }
            else
            {
                render.SetSamplerState(SamplerState.LinearClamp, 0);
            }
            render.RenderFullScreenQuadVertexPixel(restore);

        }

        /// <summary>
        /// Ends the forward pass.
        /// Clean Up
        /// </summary>
        /// <returns></returns>
        public Texture2D EndForwardPass(RenderHelper render)
        {
            render.PopRenderTarget();
            return target;
        }
    }
}
