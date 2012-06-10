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
    public class DebugSphere : IDebugDrawShape
    {
        SimpleModel sphereModel;
        /// <summary>
        /// The basic effect used to draw boxes.
        /// </summary>
        private static BasicEffect effect = null;        

        /// <summary>
        /// Creates a new box.
        /// Visible by default
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="color">The box's color.</param>
        public DebugSphere(Vector3 position, float radius, Color color)
        {            
            this.Color = color;
            this.Radius = radius;
            this.Position = position;
            Visible = true;
        }
        
        /// <summary>
        /// Initializes
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="ginfo">The ginfo.</param>
        public void  Initialize(Engine.GraphicFactory factory, Engine.GraphicInfo ginfo)
        {            
            sphereModel = new SimpleModel(factory, "Dsphere", true);
            if (effect == null)
            {
                effect = factory.GetBasicEffect();
                effect.TextureEnabled = false;
                effect.VertexColorEnabled = false;
                effect.LightingEnabled = lightingEnabled;
                WireFrameEnabled = true;
            }
        }

        static RasterizerState RasterizerState = null;
        static bool lightingEnabled = false;
        public static bool LightingEnabled 
        {
            set
            {
                if (effect != null)
                {
                    effect.LightingEnabled = value;
                    lightingEnabled = value;
                }
                else
                {
                    lightingEnabled = value;
                }
            }
        }

        public static bool WireFrameEnabled
        {
            get
            {
                return RasterizerState == null;
            }
            set
            {
                if (value == true)
                {
                    RasterizerState = new RasterizerState();
                    RasterizerState.FillMode = FillMode.WireFrame;
                }
                else
                {
                    RasterizerState = null;
                }
            }

        }

        /// <summary>
        /// Position
        /// </summary>
        public Vector3 Position;
        /// <summary>
        /// Radius
        /// </summary>
        public float Radius;
        private Color Color;

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
                effect.World = Matrix.CreateScale(Radius) * Matrix.CreateTranslation(Position);                
                effect.DiffuseColor = Color.ToVector3();

                if(RasterizerState != null)
                    render.PushRasterizerState(RasterizerState);
                
                render.RenderBatch(sphereModel.GetBatchInformation(0)[0], effect);
                
                if (RasterizerState != null)
                    render.PopRasterizerState();
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
