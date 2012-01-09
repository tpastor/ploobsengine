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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.SceneControl;
using PloobsEngine.Modelo;

namespace PloobsEngine.Features.DebugDraw
{
    /// <summary>
    /// Debig Line
    /// </summary>
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
        /// <param name="StartPoint">The start point.</param>
        /// <param name="EndPoint">The end point.</param>
        /// <param name="color">The box's color.</param>
        public DebugLine(Vector3 StartPoint, Vector3 EndPoint, Color color)
        {            
            this.Color = color;
            this.StartPoint = StartPoint;
            this.EndPoint = EndPoint;
            Visible = true;
        }

        /// <summary>
        /// Initializes
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="ginfo">The ginfo.</param>
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

        /// <summary>
        /// Startpoint
        /// </summary>
        public Vector3 StartPoint;
        /// <summary>
        /// Endpoint
        /// </summary>
        public Vector3 EndPoint;
        /// <summary>
        /// Color
        /// </summary>
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
