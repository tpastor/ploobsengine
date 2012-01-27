using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Components;
using PloobsEngine.Features.DebugDraw;

namespace PloobsEngine.Features.DebugDraw
{
    /// <summary>
    /// Component to draw 2D primitives on the top of the screen
    /// </summary>
    public class Primitive2DDraw : IComponent
    {
        List<I2DPrimitive> primitives = new List<I2DPrimitive>();

        /// <summary>
        /// Add the 2D primitive.
        /// </summary>
        /// <param name="I2DPrimitive">The i2 D primitive.</param>
        public void Add2DPrimitive(I2DPrimitive I2DPrimitive)
        {
            System.Diagnostics.Debug.Assert(GraphicInfo != null);
            System.Diagnostics.Debug.Assert(factory != null);
            I2DPrimitive.iInit(GraphicInfo, factory);
            primitives.Add(I2DPrimitive);
        }

        /// <summary>
        /// Remove the 2D primitive.
        /// </summary>
        /// <param name="I2DPrimitive">The i2 D primitive.</param>
        public void Remove2DPrimitive(I2DPrimitive I2DPrimitive)
        {
            primitives.Remove(I2DPrimitive);
        }

        PrimitiveBatch batch;
        public override ComponentType ComponentType
        {
            get { return PloobsEngine.Components.ComponentType.POS_DRAWABLE; }
        }

        PloobsEngine.Engine.GraphicInfo GraphicInfo;
        PloobsEngine.Engine.GraphicFactory factory;
        protected override void LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory)
        {
            this.GraphicInfo = GraphicInfo;
            this.factory = factory;
            base.LoadContent(GraphicInfo, factory);
            batch = new PrimitiveBatch(factory);
        }


        public static readonly String myName = "Primitive2DDraw";
        public override string getMyName()
        {
            return myName;
        }

        protected override void AfterDraw(PloobsEngine.SceneControl.RenderHelper render, Microsoft.Xna.Framework.GameTime gt, ref Microsoft.Xna.Framework.Matrix activeView, ref Microsoft.Xna.Framework.Matrix activeProjection)
        {
            foreach (var item in primitives)
            {
                item.iDraw(activeView, activeProjection, render, batch);
            }

            base.AfterDraw(render, gt, ref activeView, ref activeProjection);
        }
    }
}
