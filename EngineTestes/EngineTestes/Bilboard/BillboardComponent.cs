using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EngineTestes.Bilboard
{
    public class BillboardComponent : IComponent
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
                
        public List<Bilboard3D> Billboards = new List<Bilboard3D>();
        BasicEffect basicEffect;
        SpriteBatch spriteBatch;

        protected override void PosWithDepthDraw(PloobsEngine.SceneControl.RenderHelper render, Microsoft.Xna.Framework.GameTime gt, Microsoft.Xna.Framework.Matrix activeView, Microsoft.Xna.Framework.Matrix activeProjection)
        {
            Matrix invertY = Matrix.CreateScale(1, -1, 1);

            basicEffect.World = invertY;
            basicEffect.View = activeView;
            basicEffect.Projection = activeProjection;

            spriteBatch.Begin(0, null, null, DepthStencilState.DepthRead, RasterizerState.CullNone, basicEffect);

            foreach (var item in Billboards)
            {
                Vector3 textPosition = new Vector3(0, 45, 0);

                Vector3 viewSpaceTextPosition = Vector3.Transform(item.Position, activeView * invertY);                                
                spriteBatch.Draw(item.Texture,new Vector2(viewSpaceTextPosition.X, viewSpaceTextPosition.Y),item.Texture.Bounds,Color.White,0,Vector2.Zero,item.Scale,SpriteEffects.None,viewSpaceTextPosition.Z);                
            }

            spriteBatch.End();
            render.ResyncStates();
        }

        public static readonly String MyName = "Billboard";
        public override string getMyName()
        {
            return MyName;
        }
    }
}
