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
using FarseerPhysics.Common;
using FarseerPhysics.Collision;
using FarseerPhysics.Common.Decomposition;
using PloobsEngine.Physics2D.Farseer;
using PloobsEngine.Engine;

namespace PloobsEngine.Physics2D.Farseer
{
    public class FarseerAsset2DCreator
    {
        public FarseerAsset2DCreator(GraphicFactory GraphicFactory)
        {
            _device = GraphicFactory.device;
            _effect = new BasicEffect(_device);
        }

        private GraphicsDevice _device;
        private BasicEffect _effect;

        public Texture2D CreateTextureFromVertices(Vertices vertices, Color color, float materialScale)
        {
            return TextureFromVertices(vertices, color, materialScale, null);
        }

        public Texture2D CreateTextureFromVertices(Vertices vertices, float materialScale, Texture2D tex)
        {
            return TextureFromVertices(vertices, Color.White, materialScale, tex);
        }

        public Texture2D CreateCircleTexture(float radius, float materialScale, Texture2D texture)
        {
            return CircleTexture(radius, Color.White, materialScale, texture);
        }

        public Texture2D CreateCircleTexture(float radius, Color color, float materialScale)
        {
            return CircleTexture(radius, color, materialScale, null);
        }


        public Texture2D CreateEllipseTexture(float radiusX, float radiusY, Color color,
                                        float materialScale)
        {
            return EllipseTexture(radiusX, radiusY, color, materialScale, null);
        }

        public Texture2D CreateEllipseTexture(float radiusX, float radiusY, Texture2D texture,
                                        float materialScale)
        {
            return EllipseTexture(radiusX, radiusY, Color.White, materialScale, texture);
        }


        private const int CircleSegments = 32;

        internal Texture2D TextureFromVertices(Vertices vertices, Color color, float materialScale,Texture2D tex = null)
        {
            // copy vertices
            Vertices verts = new Vertices(vertices);

            // scale to display units (i.e. pixels) for rendering to texture
            Vector2 scale = ConvertUnits.ToDisplayUnits(Vector2.One);
            verts.Scale(ref scale);

            // translate the boundingbox center to the texture center
            // because we use an orthographic projection for rendering later
            AABB vertsBounds = verts.GetCollisionBox();
            verts.Translate(-vertsBounds.Center);

            List<Vertices> decomposedVerts;
            if (!verts.IsConvex())
            {
                decomposedVerts = EarclipDecomposer.ConvexPartition(verts);
            }
            else
            {
                decomposedVerts = new List<Vertices>();
                decomposedVerts.Add(verts);
            }
            List<VertexPositionColorTexture[]> verticesFill =
                new List<VertexPositionColorTexture[]>(decomposedVerts.Count);

            if(tex != null)
                materialScale /= tex.Width;

            for (int i = 0; i < decomposedVerts.Count; ++i)
            {
                verticesFill.Add(new VertexPositionColorTexture[3 * (decomposedVerts[i].Count - 2)]);
                for (int j = 0; j < decomposedVerts[i].Count - 2; ++j)
                {
                    // fill vertices
                    verticesFill[i][3 * j].Position = new Vector3(decomposedVerts[i][0], 0f);
                    verticesFill[i][3 * j + 1].Position = new Vector3(decomposedVerts[i].NextVertex(j), 0f);
                    verticesFill[i][3 * j + 2].Position = new Vector3(decomposedVerts[i].NextVertex(j + 1), 0f);
                    verticesFill[i][3 * j].TextureCoordinate = decomposedVerts[i][0] * materialScale;
                    verticesFill[i][3 * j + 1].TextureCoordinate = decomposedVerts[i].NextVertex(j) * materialScale;
                    verticesFill[i][3 * j + 2].TextureCoordinate = decomposedVerts[i].NextVertex(j + 1) * materialScale;
                    verticesFill[i][3 * j].Color =
                        verticesFill[i][3 * j + 1].Color = verticesFill[i][3 * j + 2].Color = color;
                }
            }

            // calculate outline
            VertexPositionColor[] verticesOutline = new VertexPositionColor[2 * verts.Count];
            for (int i = 0; i < verts.Count; ++i)
            {
                verticesOutline[2 * i].Position = new Vector3(verts[i], 0f);
                verticesOutline[2 * i + 1].Position = new Vector3(verts.NextVertex(i), 0f);
                verticesOutline[2 * i].Color = verticesOutline[2 * i + 1].Color = Color.Black;
            }

            Vector2 vertsSize = new Vector2(vertsBounds.UpperBound.X - vertsBounds.LowerBound.X,
                                            vertsBounds.UpperBound.Y - vertsBounds.LowerBound.Y);

            return RenderTexture((int)vertsSize.X, (int)vertsSize.Y,
                                 tex, verticesFill, verticesOutline, tex == null);
        }

        internal Texture2D CircleTexture(float radius, Color color, float materialScale,Texture2D texture = null)
        {
            return EllipseTexture(radius, radius, color, materialScale, texture);
        }

        internal Texture2D EllipseTexture(float radiusX, float radiusY, Color color,
                                        float materialScale,Texture2D texture = null)
        {
            VertexPositionColorTexture[] verticesFill = new VertexPositionColorTexture[3 * (CircleSegments - 2)];
            VertexPositionColor[] verticesOutline = new VertexPositionColor[2 * CircleSegments];
            const float segmentSize = MathHelper.TwoPi / CircleSegments;
            float theta = segmentSize;

            radiusX = ConvertUnits.ToDisplayUnits(radiusX);
            radiusY = ConvertUnits.ToDisplayUnits(radiusY);
            if(texture != null)
                materialScale /= texture.Width;

            Vector2 start = new Vector2(radiusX, 0f);

            for (int i = 0; i < CircleSegments - 2; ++i)
            {
                Vector2 p1 = new Vector2(radiusX * (float)Math.Cos(theta), radiusY * (float)Math.Sin(theta));
                Vector2 p2 = new Vector2(radiusX * (float)Math.Cos(theta + segmentSize),
                                         radiusY * (float)Math.Sin(theta + segmentSize));
                // fill vertices
                verticesFill[3 * i].Position = new Vector3(start, 0f);
                verticesFill[3 * i + 1].Position = new Vector3(p1, 0f);
                verticesFill[3 * i + 2].Position = new Vector3(p2, 0f);
                verticesFill[3 * i].TextureCoordinate = start * materialScale;
                verticesFill[3 * i + 1].TextureCoordinate = p1 * materialScale;
                verticesFill[3 * i + 2].TextureCoordinate = p2 * materialScale;
                verticesFill[3 * i].Color = verticesFill[3 * i + 1].Color = verticesFill[3 * i + 2].Color = color;

                // outline vertices
                if (i == 0)
                {
                    verticesOutline[0].Position = new Vector3(start, 0f);
                    verticesOutline[1].Position = new Vector3(p1, 0f);
                    verticesOutline[0].Color = verticesOutline[1].Color = Color.Black;
                }
                if (i == CircleSegments - 3)
                {
                    verticesOutline[2 * CircleSegments - 2].Position = new Vector3(p2, 0f);
                    verticesOutline[2 * CircleSegments - 1].Position = new Vector3(start, 0f);
                    verticesOutline[2 * CircleSegments - 2].Color =
                        verticesOutline[2 * CircleSegments - 1].Color = Color.Black;
                }
                verticesOutline[2 * i + 2].Position = new Vector3(p1, 0f);
                verticesOutline[2 * i + 3].Position = new Vector3(p2, 0f);
                verticesOutline[2 * i + 2].Color = verticesOutline[2 * i + 3].Color = Color.Black;

                theta += segmentSize;
            }

            return RenderTexture((int)(radiusX * 2f), (int)(radiusY * 2f),
                                 texture, verticesFill, verticesOutline, texture == null);
        }

        private Texture2D RenderTexture(int width, int height, Texture2D material,
                                        VertexPositionColorTexture[] verticesFill,
                                        VertexPositionColor[] verticesOutline, bool useVertexColor)
        {
            List<VertexPositionColorTexture[]> fill = new List<VertexPositionColorTexture[]>(1);
            fill.Add(verticesFill);
            return RenderTexture(width, height, material, fill, verticesOutline, useVertexColor);
        }

        private Texture2D RenderTexture(int width, int height, Texture2D material,
                                        List<VertexPositionColorTexture[]> verticesFill,
                                        VertexPositionColor[] verticesOutline,bool useVertexColor)
        {
            Matrix halfPixelOffset = Matrix.CreateTranslation(-0.5f, -0.5f, 0f);
            PresentationParameters pp = _device.PresentationParameters;
            RenderTarget2D texture = new RenderTarget2D(_device, width + 2, height + 2, false, SurfaceFormat.Color,
                                                        DepthFormat.None, pp.MultiSampleCount,
                                                        RenderTargetUsage.DiscardContents);
            _device.RasterizerState = RasterizerState.CullNone;
            _device.SamplerStates[0] = SamplerState.LinearWrap;

            _device.SetRenderTarget(texture);
            _device.Clear(Color.Transparent);
            _effect.Projection = Matrix.CreateOrthographic(width + 2f, -height - 2f, 0f, 1f);
            _effect.View = halfPixelOffset;
            // render shape;
            if (useVertexColor)
            {
                _effect.VertexColorEnabled = true;
                _effect.TextureEnabled = false;
            }
            else
            {
                _effect.VertexColorEnabled = false;
                _effect.TextureEnabled = true;
                _effect.Texture = material;
            }
            _effect.Techniques[0].Passes[0].Apply();
            for (int i = 0; i < verticesFill.Count; ++i)
            {
                _device.DrawUserPrimitives(PrimitiveType.TriangleList, verticesFill[i], 0, verticesFill[i].Length / 3);
            }
            // render outline;
            _effect.TextureEnabled = false;
            _effect.Techniques[0].Passes[0].Apply();
            _device.DrawUserPrimitives(PrimitiveType.LineList, verticesOutline, 0, verticesOutline.Length / 2);
            _device.SetRenderTarget(null);
            return texture;
        }
    }
}
