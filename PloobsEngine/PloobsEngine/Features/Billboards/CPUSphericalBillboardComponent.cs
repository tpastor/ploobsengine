using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PloobsEngine.Features.Billboard
{
    public class CPUSphericalBillboardComponent : IComponent
    {
        public override ComponentType ComponentType
        {
            get { return PloobsEngine.Components.ComponentType.POS_WITHDEPTH_DRAWABLE; }
        }

        protected override void LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory)
        {
            basicEffect = factory.GetBasicEffect();
            basicEffect.TextureEnabled = true;
            spriteBatch = factory.GetSpriteBatch();
            base.LoadContent(GraphicInfo, factory);
        }
                
        public List<Billboard3D> Billboards = new List<Billboard3D>();
        BasicEffect basicEffect;
        SpriteBatch spriteBatch;

        protected override void PosWithDepthDraw(PloobsEngine.SceneControl.RenderHelper render, Microsoft.Xna.Framework.GameTime gt, Microsoft.Xna.Framework.Matrix activeView, Microsoft.Xna.Framework.Matrix activeProjection)
        {
            Matrix invertY = Matrix.CreateScale(1, -1, 1);

            basicEffect.World = invertY;
            basicEffect.View = Matrix.Identity;
            basicEffect.Projection = activeProjection;

            spriteBatch.Begin(0, null, null, DepthStencilState.DepthRead, RasterizerState.CullNone, basicEffect);

            foreach (var item in Billboards)
            {
                Vector3 viewSpaceTextPosition = Vector3.Transform(item.Position, activeView * invertY);
                spriteBatch.Draw(item.Texture, new Vector2(viewSpaceTextPosition.X, viewSpaceTextPosition.Y), item.Texture.Bounds, Color.White, 0, new Vector2(item.Texture.Bounds.Center.X,item.Texture.Bounds.Center.Y), item.Scale, SpriteEffects.None, viewSpaceTextPosition.Z);                
            }            
    

            spriteBatch.End();
            render.ResyncStates();
        }




        public static readonly String MyName = "SphericalBillboard";
        public override string getMyName()
        {
            return MyName;
        }
    }
}
