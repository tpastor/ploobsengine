using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Physic2D.Farseer;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Common;
using FarseerPhysics.Factories;
using PloobsEngine.Engine;
using PloobsEngine.Physic2D;
using PloobsEngine.SceneControl;
using PloobsEngine.SceneControl._2DScene;

namespace EngineTestes
{
        public class Border
        {
            private Body _anchor;
            private BasicEffect _basicEffect;
            private VertexPositionColorTexture[] _borderVerts;            
            private World _world;

            public Border(FarseerWorld world, GraphicFactory factory, GraphicInfo ginfo, Texture2D texture)
            {
                _world = world.World;

                float halfWidth = ConvertUnits.ToSimUnits(ginfo.Viewport.Width) / 2f - 0.75f;
                float halfHeight = ConvertUnits.ToSimUnits(ginfo.Viewport.Height) / 2f - 0.75f;

                Vertices borders = new Vertices(4);
                borders.Add(new Vector2(-halfWidth, halfHeight));
                borders.Add(new Vector2(halfWidth, halfHeight));
                borders.Add(new Vector2(halfWidth, -halfHeight));
                borders.Add(new Vector2(-halfWidth, -halfHeight));

                _anchor = BodyFactory.CreateLoopShape(_world, borders);
                _anchor.CollisionCategories = Category.All;
                _anchor.CollidesWith = Category.All;
                 

                _basicEffect = factory.GetBasicEffect();
                _basicEffect.VertexColorEnabled = true;
                _basicEffect.TextureEnabled = true;
                _basicEffect.Texture = texture;

                VertexPositionColorTexture[] vertice = new VertexPositionColorTexture[8];
                vertice[0] = new VertexPositionColorTexture(new Vector3(-halfWidth, -halfHeight, 0f),
                                                            Color.LightGray, new Vector2(-halfWidth, -halfHeight) / 5.25f);
                vertice[1] = new VertexPositionColorTexture(new Vector3(halfWidth, -halfHeight, 0f),
                                                            Color.LightGray, new Vector2(halfWidth, -halfHeight) / 5.25f);
                vertice[2] = new VertexPositionColorTexture(new Vector3(halfWidth, halfHeight, 0f),
                                                            Color.LightGray, new Vector2(halfWidth, halfHeight) / 5.25f);
                vertice[3] = new VertexPositionColorTexture(new Vector3(-halfWidth, halfHeight, 0f),
                                                            Color.LightGray, new Vector2(-halfWidth, halfHeight) / 5.25f);
                vertice[4] = new VertexPositionColorTexture(new Vector3(-halfWidth - 2f, -halfHeight - 2f, 0f),
                                                            Color.LightGray,
                                                            new Vector2(-halfWidth - 2f, -halfHeight - 2f) / 5.25f);
                vertice[5] = new VertexPositionColorTexture(new Vector3(halfWidth + 2f, -halfHeight - 2f, 0f),
                                                            Color.LightGray,
                                                            new Vector2(halfWidth + 2f, -halfHeight - 2f) / 5.25f);
                vertice[6] = new VertexPositionColorTexture(new Vector3(halfWidth + 2f, halfHeight + 2f, 0f),
                                                            Color.LightGray,
                                                            new Vector2(halfWidth + 2f, halfHeight + 2f) / 5.25f);
                vertice[7] = new VertexPositionColorTexture(new Vector3(-halfWidth - 2f, halfHeight + 2f, 0f),
                                                            Color.LightGray,
                                                            new Vector2(-halfWidth - 2f, halfHeight + 2f) / 5.25f);

                _borderVerts = new VertexPositionColorTexture[24];
                _borderVerts[0] = vertice[0];
                _borderVerts[1] = vertice[5];
                _borderVerts[2] = vertice[4];
                _borderVerts[3] = vertice[0];
                _borderVerts[4] = vertice[1];
                _borderVerts[5] = vertice[5];
                _borderVerts[6] = vertice[1];
                _borderVerts[7] = vertice[6];
                _borderVerts[8] = vertice[5];
                _borderVerts[9] = vertice[1];
                _borderVerts[10] = vertice[2];
                _borderVerts[11] = vertice[6];
                _borderVerts[12] = vertice[2];
                _borderVerts[13] = vertice[7];
                _borderVerts[14] = vertice[6];
                _borderVerts[15] = vertice[2];
                _borderVerts[16] = vertice[3];
                _borderVerts[17] = vertice[7];
                _borderVerts[18] = vertice[3];
                _borderVerts[19] = vertice[4];
                _borderVerts[20] = vertice[7];
                _borderVerts[21] = vertice[3];
                _borderVerts[22] = vertice[0];
                _borderVerts[23] = vertice[4];
            }

            public void Draw(RenderHelper render,ICamera2D cam)
            {
                render.SetSamplerState(SamplerState.AnisotropicWrap,0);
                render.PushRasterizerState(RasterizerState.CullNone);
                _basicEffect.Projection = cam.SimProjection;
                _basicEffect.View = cam.SimView;
                render.RenderUserPrimitive(_basicEffect,PrimitiveType.TriangleList, _borderVerts, 0, 8);

                render.PopRasterizerState();
                
            }        
    }
}
