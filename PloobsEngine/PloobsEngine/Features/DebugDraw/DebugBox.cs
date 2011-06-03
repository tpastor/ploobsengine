using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.SceneControl;
using PloobsEngine.Modelo;

namespace PloobsEngine.Features.DebugDraw
{
    public class DebugBox : IDebugDrawShape
    {
        static VertexPositionColor[] verts = new VertexPositionColor[8];
        static short[] indices = new short[]
        {
            0, 1,
            1, 2,
            2, 3,
            3, 0,
            0, 4,
            1, 5,
            2, 6,
            3, 7,
            4, 5,
            5, 6,
            6, 7,
            7, 4,
        };

        /// <summary>
        /// The basic effect used to draw boxes.
        /// </summary>
        private static BasicEffect effect = null;

        /// <summary>
        /// Creates a new box.
        /// Visible by default
        /// </summary>
        /// <param name="BoundingBox">The bounding box.</param>
        /// <param name="color">The box's color.</param>
        public DebugBox(BoundingBox BoundingBox, Color color)
        {            
            this.Color = color;
            this.BoundingBox = BoundingBox;
            Visible = true;
        }
         
        public void  Initialize(Engine.GraphicFactory factory, Engine.GraphicInfo ginfo)
        {
            if (DebugBox.effect == null)
            {
                DebugBox.effect = factory.GetBasicEffect();
                DebugBox.effect.VertexColorEnabled = true;
                DebugBox.effect.LightingEnabled = false;
                DebugBox.effect.TextureEnabled = false;                
            }        
        }

        public BoundingBox BoundingBox;
        public Color Color;

        /// <summary>
        /// Draws the box.
        /// </summary>
        /// <param name="render">The render.</param>
        /// <param name="view">The viewing matrix.</param>
        /// <param name="projection">The projection matrix.</param>
        public void Draw(RenderHelper render, Matrix view, Matrix projection)
        {
            if (Visible)
            {                
                //// Setup the effect.                
                DebugBox.effect.View = view;
                DebugBox.effect.Projection = projection;

                Vector3[] corners = BoundingBox.GetCorners();
                for (int i = 0; i < 8; i++)
                {
                    verts[i].Position = corners[i];
                    verts[i].Color = Color;
                }
                                                               
                render.RenderUserIndexedPrimitive<VertexPositionColor>(effect,PrimitiveType.LineList, verts, 0, verts.Count(), indices, 0, indices.Count() / 2);
                
            }
        }

        #region IDebugDrawShape Members

        public bool Visible
        {
            get;
            set;
        }

        #endregion
    }    
}
