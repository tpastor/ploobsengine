using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Engine;

namespace EngineTestes.Bilboard
{
    public class BillboardComponent : IComponent
    {
        public override ComponentType ComponentType
        {
            get { return PloobsEngine.Components.ComponentType.POS_WITHDEPTH_DRAWABLE; }
        }

        GraphicInfo GraphicInfo;
        protected override void LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory)
        {
            this.GraphicInfo = GraphicInfo;
            basicEffect = factory.GetBasicEffect();
            basicEffect.TextureEnabled = true;
            spriteBatch = factory.GetSpriteBatch();
            base.LoadContent(GraphicInfo, factory);
        }
                
        public List<SphericalBillboard3D> Billboards = new List<SphericalBillboard3D>();
        BasicEffect basicEffect;
        SpriteBatch spriteBatch;

        protected override void PosWithDepthDraw(PloobsEngine.SceneControl.RenderHelper render, Microsoft.Xna.Framework.GameTime gt, Microsoft.Xna.Framework.Matrix activeView, Microsoft.Xna.Framework.Matrix activeProjection)
        {        
            Matrix viewIT = Matrix.Invert(Matrix.Transpose(activeView));
            Vector3 position = new Vector3(viewIT.M14, viewIT.M24, viewIT.M34);

            foreach (var item in Billboards)
            {
                basicEffect.World = Matrix.CreateConstrainedBillboard(item.Position, position, Vector3.Up, null, null);

                basicEffect.View = activeView;
                basicEffect.Projection = activeProjection;

                spriteBatch.Begin(0, null, null, DepthStencilState.DepthRead, RasterizerState.CullNone, basicEffect);
                spriteBatch.Draw(item.Texture, Vector2.Zero, item.Texture.Bounds, Color.White, 0, new Vector2(item.Texture.Bounds.Center.X, item.Texture.Bounds.Center.Y), item.Scale, SpriteEffects.None, 1);
                spriteBatch.End();
            }
            render.ResyncStates();
        }

        public static readonly String MyName = "Billboard";
        public override string getMyName()
        {
            return MyName;
        }
    }
}
