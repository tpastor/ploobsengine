using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PloobsEngine.Features.Billboard
{
    public class TextCPUSphericalBillboardComponent : IComponent
    {
        public override ComponentType ComponentType
        {
            get { return PloobsEngine.Components.ComponentType.POS_WITHDEPTH_DRAWABLE; }
        }

        protected override void LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory)
        {
            SpriteFont = factory.GetAsset<SpriteFont>("ConsoleFont", true);
            basicEffect = factory.GetBasicEffect();
            basicEffect.TextureEnabled = true;
            basicEffect.VertexColorEnabled = true;
            spriteBatch = factory.GetSpriteBatch();
            base.LoadContent(GraphicInfo, factory);
        }

        public List<TextBillboard3D> Billboards = new List<TextBillboard3D>();
        BasicEffect basicEffect;
        SpriteBatch spriteBatch;
        SpriteFont SpriteFont;

        protected override void PosWithDepthDraw(PloobsEngine.SceneControl.RenderHelper render, Microsoft.Xna.Framework.GameTime gt, ref Microsoft.Xna.Framework.Matrix activeView, ref Microsoft.Xna.Framework.Matrix activeProjection)
        {
            Matrix invertY = Matrix.CreateScale(1, -1, 1);

            basicEffect.World = invertY;
            basicEffect.View = Matrix.Identity;
            basicEffect.Projection = activeProjection;

            spriteBatch.Begin(0, null, null, DepthStencilState.DepthRead, RasterizerState.CullNone, basicEffect);

            foreach (var item in Billboards)
            {
                SpriteFont font = item.SpriteFont == null ? SpriteFont : item.SpriteFont;
                Vector3 viewSpaceTextPosition = Vector3.Transform(item.Position, activeView * invertY);
                Vector2 textOrigin = font.MeasureString(item.Message) / 2;                
                spriteBatch.DrawString(font, item.Message, new Vector2(viewSpaceTextPosition.X, viewSpaceTextPosition.Y), item.Color, 0, textOrigin, item.Scale, 0, viewSpaceTextPosition.Z);                    
            }                

            spriteBatch.End();
            render.ResyncStates();
        }




        public static readonly String MyName = "TextSphericalBillboard";
        public override string getMyName()
        {
            return MyName;
        }
    }
}
