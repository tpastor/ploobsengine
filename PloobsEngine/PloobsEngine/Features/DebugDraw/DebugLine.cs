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
    public class DebugLine : IDebugDrawShape
    {
        VertexPositionColor[] verts = new VertexPositionColor[2];        

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
        public DebugLine(Vector3 StartPoint, Vector3 EndPoint, Color color)
        {            
            this.Color = color;
            this.StartPoint = StartPoint;
            this.EndPoint = EndPoint;
            Visible = true;
        }
         
        public void  Initialize(Engine.GraphicFactory factory, Engine.GraphicInfo ginfo)
        {
            if (effect == null)
            {
                effect = factory.GetBasicEffect();
                effect.VertexColorEnabled = true;
                effect.LightingEnabled = false;
                effect.TextureEnabled = false;                
            }        
        }

        public Vector3 StartPoint;
        public Vector3 EndPoint;
        public Color Color;        

        /// <summary>
        /// Draws the box.
        /// </summary>
        /// <param name="view">The viewing matrix.</param>
        /// <param name="projection">The projection matrix.</param>
        public void Draw(RenderHelper render, Matrix view, Matrix projection)
        {
            if (Visible)
            {                
                //// Setup the effect.                
                effect.View = view;
                effect.Projection = projection;
                verts[0].Position = StartPoint;
                verts[0].Color = Color;
                verts[1].Position = EndPoint;
                verts[1].Color = Color;                        
                render.RenderUserPrimitive<VertexPositionColor>(effect,PrimitiveType.LineList, verts, 0, verts.Count() / 2);
                
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
