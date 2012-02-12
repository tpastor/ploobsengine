using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Engine;

namespace PloobsEngine.Features.Billboard
{
    public class TextCPUCylindricBillboardComponent : IComponent
    {
        public override ComponentType ComponentType
        {
            get { return PloobsEngine.Components.ComponentType.POS_WITHDEPTH_DRAWABLE; }
        }

        SpriteFont SpriteFont;
        GraphicInfo GraphicInfo;
        protected override void LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory)
        {
            this.GraphicInfo = GraphicInfo;
            basicEffect = factory.GetBasicEffect();
            basicEffect.TextureEnabled = true;
            basicEffect.VertexColorEnabled = true;
            spriteBatch = factory.GetSpriteBatch();
            SpriteFont = factory.GetAsset<SpriteFont>("ConsoleFont", true);
            base.LoadContent(GraphicInfo, factory);
        }

        public List<TextBillboard3D> Billboards = new List<TextBillboard3D>();
        BasicEffect basicEffect;
        SpriteBatch spriteBatch;

        protected override void PosWithDepthDraw(PloobsEngine.SceneControl.RenderHelper render, Microsoft.Xna.Framework.GameTime gt, ref Microsoft.Xna.Framework.Matrix activeView, ref Microsoft.Xna.Framework.Matrix activeProjection)
        {        
            Matrix viewIT = Matrix.Invert(Matrix.Transpose(activeView));
            Vector3 position = new Vector3(viewIT.M14, viewIT.M24, viewIT.M34);

            foreach (var item in Billboards)
            {
                SpriteFont font = item.SpriteFont == null ? SpriteFont : item.SpriteFont;

                basicEffect.World = Matrix.CreateConstrainedBillboard(item.Position, position, Vector3.Down, null, null);

                basicEffect.View = activeView;
                basicEffect.Projection = activeProjection;

                spriteBatch.Begin(0, null, null, DepthStencilState.DepthRead, RasterizerState.CullNone, basicEffect);

                Vector2 textOrigin = font.MeasureString(item.Message) / 2;
                
                spriteBatch.DrawString(font, item.Message, Vector2.Zero, item.Color, 0, textOrigin, item.Scale, 0, 0);

                spriteBatch.End();
            }
            render.ResyncStates();
        }

        public static readonly String MyName = "TextCylindricBillboard";
        public override string getMyName()
        {
            return MyName;
        }
    }
}
