using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Helper class to render full screen quads
    /// VertexPositionTexture
    /// </summary>
    public sealed class QuadRender
    {
        private VertexPositionTexture[] verts;                 
        private GraphicsDevice myDevice;

        /// <summary>
        /// Loads the quad.
        /// </summary>
        /// <param name="engine">The engine.</param>
        public QuadRender(GraphicsDevice device) 
        {
            myDevice = device;

            verts = new[] {
                new VertexPositionTexture(new Vector3(-1, -1, 0), new Vector2(0, 1)), 
                new VertexPositionTexture(new Vector3(-1, 1, 0), new Vector2(0, 0)), 
                new VertexPositionTexture(new Vector3(1, -1, 0), new Vector2(1, 1)), 
                new VertexPositionTexture(new Vector3(1, 1, 0), new Vector2(1, 0))
            };            
            
        }

        /// <summary>
        /// Draws the quad.
        /// </summary>
        /// <param name="effect">The effect.</param>
        public void DrawQuad(Effect effect)
        {
            effect.CurrentTechnique.Passes[0].Apply();
            myDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, verts, 0, 2); 
        }   
    
    }
}
