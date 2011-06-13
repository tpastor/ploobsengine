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
        Texture2D tex;

        /// <summary>
        /// Creates a new box.
        /// Visible by default
        /// </summary>
        /// <param name="BoundingBox">The bounding box.</param>
        /// <param name="color">The box's color.</param>
        public DebugSphere(Vector3 position, float radius, Color color)
        {            
            this.Color = color;
            this.Radius = radius;
            this.Position = position;
            Visible = true;
        }
         
        public void  Initialize(Engine.GraphicFactory factory, Engine.GraphicInfo ginfo)
        {
            tex = factory.CreateTexture2DColor(1, 1, Color);
            sphereModel = new SimpleModel(factory, "Dsphere", true);
            if (effect == null)
            {
                effect = factory.GetBasicEffect();
                effect.TextureEnabled = true;
            }
        }

        public Vector3 Position;
        public float Radius;
        private Color Color;        

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
                effect.World = Matrix.CreateTranslation(Position) * Matrix.CreateScale(Radius);
                effect.Texture = tex;
                render.RenderBatch(sphereModel.GetBatchInformation(0)[0], effect);                
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
