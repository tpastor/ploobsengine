using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Utils
{
    public class Primitives
    {
        public static void Sphere(float radius, int n, bool invertFaces,
                          out VertexPositionTexture[] vertices, out int[] indices)
        {
            // Sphere Basis:
            //  r = radius
            //  theta = lines of latitude (horizontal division)
            //  phi = lines of longitude (vertical division)
            //  x = r * cos(theta) * cos(phi)
            //  y = r * cos(theta) * sin(phi)
            //  z = r * sin(theta)

            if (radius < 0)
                radius *= -1;

            n = Math.Max(4, n);
            n += (n % 2);

            int vertexCount = 0,
                indexCount = 0;

            vertices = new VertexPositionTexture[((n / 2) * (n - 1)) + 1];
            indices = new int[(((n / 2) - 2) * (n + 1) * 6) + (6 * (n + 1))];

            for (int j = 0; j <= n / 2; j++)
            {
                float theta = j * MathHelper.TwoPi / n - MathHelper.PiOver2;

                for (int i = 0; i <= n; i++)
                {
                    float phi = i * MathHelper.TwoPi / n;

                    vertices[vertexCount++] = new VertexPositionTexture()
                    {
                        Position = new Vector3()
                        {
                            X = radius * (float)(Math.Cos(theta) * Math.Cos(phi)),
                            Y = radius * (float)(Math.Sin(theta)),
                            Z = radius * (float)(Math.Cos(theta) * Math.Sin(phi))
                        },
                        TextureCoordinate = new Vector2()
                        {
                            X = i / n,
                            Y = 2 * j / n
                        }
                    };

                    if (j == 0)
                    {   // bottom cap
                        for (i = 0; i <= n; i++)
                        {
                            int i0 = 0,
                                i1 = (i % n) + 1,
                                i2 = i;

                            indices[indexCount++] = i0;
                            indices[indexCount++] = invertFaces ? i2 : i1;
                            indices[indexCount++] = invertFaces ? i1 : i2;
                        }
                    }
                    else if (j < n / 2 - 1)
                    {   // middle area
                        int i0 = vertexCount - 1,
                            i1 = vertexCount,
                            i2 = vertexCount + n,
                            i3 = vertexCount + n + 1;

                        indices[indexCount++] = i0;
                        indices[indexCount++] = invertFaces ? i2 : i1;
                        indices[indexCount++] = invertFaces ? i1 : i2;

                        indices[indexCount++] = i1;
                        indices[indexCount++] = invertFaces ? i2 : i3;
                        indices[indexCount++] = invertFaces ? i3 : i2;
                    }
                    else if (j == n / 2)
                    {   // top cap
                        for (i = 0; i <= n; i++)
                        {
                            int i0 = (vertexCount - 1),
                                i1 = (vertexCount - 1) - ((i % n) + 1),
                                i2 = (vertexCount - 1) - i;

                            indices[indexCount++] = i0;
                            indices[indexCount++] = invertFaces ? i2 : i1;
                            indices[indexCount++] = invertFaces ? i1 : i2;
                        }
                    }
                }
            }
        }

        public static void Capsole(Vector3 p0, Vector3 p1, float r0, float r1,
                           int segments, int slices, bool invertFaces,
                           out VertexPositionTexture[] vertices, out int[] indices)
        {
            segments = Math.Max(1, segments);
            slices = Math.Max(3, slices);
            slices += slices % 2;

            Vector3 normal = p1 - p0;
            float length = normal.Length();

            // this vector should not be between p0 and p1
            Vector3 p = new Vector3();
            p.X = (float) StaticRandom.RandomInstance.NextDouble();
            p.Y = (float)StaticRandom.RandomInstance.NextDouble();
            p.Z = (float)StaticRandom.RandomInstance.NextDouble();

            // derive two points on the plane formed by [p1 - p0]
            Vector3 r = Vector3.Cross(p - p0, normal);
            Vector3 s = Vector3.Cross(r, normal);
            r.Normalize();
            s.Normalize();
            normal.Normalize();

            float invSegments = 1f / segments,
                  invSlices = 1f / slices,
                  capRadialSlice = MathHelper.Pi / slices,
                  cylRadialSlice = MathHelper.TwoPi / slices;

            Vector2 inc = new Vector2(1f / slices, 1f / segments);

            int vertexCount = 0, indexCount = 0;
            vertices = new VertexPositionTexture[((segments + 1) * (slices * 2 + 1)) + 2];
            indices = new int[((segments * slices * 2) + slices) * 6];

            #region Start Cap
            for (int j = 0; j < (slices / 2); j++)
            {
                float phi = j * capRadialSlice - MathHelper.PiOver2;
                float cosPhi = (float)Math.Cos(phi), sinPhi = (float)Math.Sin(phi);

                for (int i = 0; i <= slices; i++)
                {
                    int i0, i1, i2, i3;
                    if (j == 0)
                    {
                        if (i == 0)
                        {
                            vertices[vertexCount++] = new VertexPositionTexture()
                            {
                                Position = p0 + (normal * -r0),
                                TextureCoordinate = new Vector2(0.5f, 0)
                            };
                            i++;
                        }

                        i0 = 0;
                        i1 = i + 1;
                        i2 = i;

                        indices[indexCount++] = i0;
                        indices[indexCount++] = invertFaces ? i2 : i1;
                        indices[indexCount++] = invertFaces ? i1 : i2;

                        continue;
                    }

                    float theta = i * cylRadialSlice;
                    float cosTheta = (float)Math.Cos(theta), sinTheta = (float)Math.Sin(theta);

                    Vector3 sphere = new Vector3()
                    {
                        X = r0 * cosPhi * cosTheta,
                        Y = r0 * sinPhi,
                        Z = r0 * cosPhi * sinTheta
                    };

                    Vector3 center = p0 + (normal * sphere.Y);
                    float radius = Vector3.Distance(Vector3.Up * sphere.Y, sphere);

                    vertices[vertexCount++] = new VertexPositionTexture()
                    {
                        Position = center + radius * cosTheta * r + radius * sinTheta * s,
                        TextureCoordinate = inc * new Vector2(i, 0)
                    };

                    if (i < slices)
                    {
                        i0 = vertexCount - 1;
                        i1 = vertexCount;
                        i2 = vertexCount + slices + 1;
                        i3 = vertexCount + slices;

                        indices[indexCount++] = i0;
                        indices[indexCount++] = invertFaces ? i2 : i1;
                        indices[indexCount++] = invertFaces ? i1 : i2;

                        indices[indexCount++] = i0;
                        indices[indexCount++] = invertFaces ? i3 : i2;
                        indices[indexCount++] = invertFaces ? i2 : i3;
                    }
                }
            }
            #endregion

            #region Cylinder
            for (int j = 0; j <= segments; j++)
            {
                Vector3 center = Vector3.Lerp(p0, p1, j * invSegments);
                float radius = MathHelper.Lerp(r0, r1, j * invSegments);

                for (int i = 0; i <= slices; i++)
                {
                    float theta = i * cylRadialSlice;
                    float cosTheta = (float)Math.Cos(theta),
                          sinTheta = (float)Math.Sin(theta);

                    vertices[vertexCount++] = new VertexPositionTexture()
                    {
                        Position = center + radius * cosTheta * r + radius * sinTheta * s,
                        TextureCoordinate = inc * new Vector2(i, j)
                    };

                    if (i < slices && j < segments)
                    {   // middle area
                        int i0 = vertexCount - 1,
                            i1 = vertexCount,
                            i2 = vertexCount + slices + 1,
                            i3 = vertexCount + slices;

                        indices[indexCount++] = i0;
                        indices[indexCount++] = invertFaces ? i2 : i1;
                        indices[indexCount++] = invertFaces ? i1 : i2;

                        indices[indexCount++] = i0;
                        indices[indexCount++] = invertFaces ? i3 : i2;
                        indices[indexCount++] = invertFaces ? i2 : i3;
                    }
                }
            }
            #endregion

            #region End Cap
            for (int j = 0; j <= (slices / 2); j++)
            {
                float phi = j * capRadialSlice;
                float cosPhi = (float)Math.Cos(phi), sinPhi = (float)Math.Sin(phi);

                for (int i = 0; i <= slices; i++)
                {
                    int vRef;
                    if (j > 0)
                    {
                        float theta = i * cylRadialSlice;
                        float cosTheta = (float)Math.Cos(theta), sinTheta = (float)Math.Sin(theta);

                        Vector3 sphere = new Vector3()
                        {
                            X = r1 * cosPhi * cosTheta,
                            Y = r1 * sinPhi,
                            Z = r1 * cosPhi * sinTheta
                        };

                        Vector3 center = p1 + (normal * sphere.Y);
                        float radius = Vector3.Distance(Vector3.Up * sphere.Y, sphere);

                        vertices[vertexCount++] = new VertexPositionTexture()
                        {
                            Position = center + radius * cosTheta * r + radius * sinTheta * s,
                            TextureCoordinate = inc * new Vector2(i, segments)
                        };

                        vRef = vertexCount - 1;
                    }
                    else // during the first iteration, offset the vertices 1 row down
                        vRef = vertexCount - slices - 1 + i;

                    if (i < slices)
                    {
                        int i0, i1, i2, i3;
                        if (j < (slices / 2))
                        {
                            i0 = vRef;
                            i1 = vRef + 1;
                            i2 = vRef + slices + 2;
                            i3 = vRef + slices + 1;

                            indices[indexCount++] = i0;
                            indices[indexCount++] = invertFaces ? i2 : i1;
                            indices[indexCount++] = invertFaces ? i1 : i2;

                            indices[indexCount++] = i0;
                            indices[indexCount++] = invertFaces ? i3 : i2;
                            indices[indexCount++] = invertFaces ? i2 : i3;
                        }
                        else
                        {
                            i0 = (vRef + slices + 2) - (vRef % (slices + 1));
                            i1 = vRef;
                            i2 = vRef + 1;

                            indices[indexCount++] = i0;
                            indices[indexCount++] = invertFaces ? i2 : i1;
                            indices[indexCount++] = invertFaces ? i1 : i2;
                        }
                    }
                }

                if (j == (slices / 2))
                {
                    vertices[vertexCount++] = new VertexPositionTexture()
                    {
                        Position = p1 + (normal * r0),
                        TextureCoordinate = new Vector2(0.5f, 1)
                    };
                }
            }
            #endregion
        }

        static float Eval_SuperShape2D(float m, float n1, float n2, float n3, float phi)
        {
            float t1 = (float)Math.Pow(Math.Abs(Math.Cos(m * phi / 4)), n2);
            float t2 = (float)Math.Pow(Math.Abs(Math.Sin(m * phi / 4)), n3);
            return (float)Math.Sqrt(Math.Pow(t1 + t2, 1f / n1));
        }

        public static void SuperCylinder(Vector3 p0, Vector3 p1, float r0, float r1,
                                         float m, float n1, float n2, float n3,
                                         int segments, int slices, bool invertFaces,
                                         out VertexPositionTexture[] vertices, out int[] indices)
        {
            segments = Math.Max(1, segments);
            slices = Math.Max(3, slices);

            // this vector should not be between start and end
            Vector3 p = new Vector3();
            p.X = (float)StaticRandom.RandomInstance.NextDouble();
            p.Y = (float)StaticRandom.RandomInstance.NextDouble();
            p.Z = (float)StaticRandom.RandomInstance.NextDouble();

            // derive two points on the plane formed by [end - start]
            Vector3 r = Vector3.Cross(p - p0, p1 - p0);
            Vector3 s = Vector3.Cross(r, p1 - p0);
            r.Normalize();
            s.Normalize();

            int vertexCount = 0, indexCount = 0;
            float invSegments = 1f / segments, invSlices = 1f / slices;

            vertices = new VertexPositionTexture[((segments + 1) * (slices + 1)) + 2];
            indices = new int[(slices + (slices * segments)) * 6];

            for (int j = 0; j <= segments; j++)
            {
                Vector3 center = Vector3.Lerp(p0, p1, j * invSegments);
                float radius = MathHelper.Lerp(r0, r1, j * invSegments);

                if (j == 0)
                {
                    vertices[vertexCount++] = new VertexPositionTexture()
                    {
                        Position = center,
                        TextureCoordinate = new Vector2(0.5f, j * invSegments)
                    };
                }

                for (int i = 0; i <= slices; i++)
                {
                    float theta = i * MathHelper.TwoPi * invSlices;
                    float sRadius = Eval_SuperShape2D(m, n1, n2, n3, theta);
                    sRadius = (float)Math.Sqrt(sRadius);

                    float rCosTheta = sRadius * radius * (float)Math.Cos(theta),
                          rSinTheta = sRadius * radius * (float)Math.Sin(theta);

                    vertices[vertexCount++] = new VertexPositionTexture()
                    {
                        Position = new Vector3()
                        {
                            X = center.X + rCosTheta * r.X + rSinTheta * s.X,
                            Y = center.Y + rCosTheta * r.Y + rSinTheta * s.Y,
                            Z = center.Z + rCosTheta * r.Z + rSinTheta * s.Z
                        },
                        TextureCoordinate = new Vector2()
                        {
                            X = i * invSlices,
                            Y = j * invSegments
                        }
                    };

                    if (i < slices)
                    {
                        // just an alias to assist with think of each vertex that's
                        //  iterated in here as the bottom right corner of a triangle
                        int vRef = vertexCount - 1;

                        if (j == 0)
                        {   // start cap - i0 is always center point on start cap
                            int i0 = 0,
                                i1 = vRef + 1,
                                i2 = vRef;

                            indices[indexCount++] = i0;
                            indices[indexCount++] = invertFaces ? i2 : i1;
                            indices[indexCount++] = invertFaces ? i1 : i2;
                        }
                        if (j == segments)
                        {   // end cap - i0 is always the center point on end cap
                            int i0 = (vRef + slices + 2) - (vRef % (slices + 1)),
                                i1 = vRef,
                                i2 = vRef + 1;

                            indices[indexCount++] = i0;
                            indices[indexCount++] = invertFaces ? i2 : i1;
                            indices[indexCount++] = invertFaces ? i1 : i2;
                        }

                        if (j < segments)
                        {   // middle area
                            int i0 = vRef,
                                i1 = vRef + 1,
                                i2 = vRef + slices + 2,
                                i3 = vRef + slices + 1;

                            indices[indexCount++] = i0;
                            indices[indexCount++] = invertFaces ? i2 : i1;
                            indices[indexCount++] = invertFaces ? i1 : i2;

                            indices[indexCount++] = i0;
                            indices[indexCount++] = invertFaces ? i3 : i2;
                            indices[indexCount++] = invertFaces ? i2 : i3;
                        }
                    }
                }

                if (j == segments)
                {
                    vertices[vertexCount++] = new VertexPositionTexture()
                    {
                        Position = center,
                        TextureCoordinate = new Vector2(0.5f, j * invSegments)
                    };
                }
            }
        }
       
        public static void Cylinder(Vector3 p0, Vector3 p1, float r0, float r1,
                            int segments, int slices, bool invertFaces,
                            out VertexPositionTexture[] vertices, out int[] indices)
        {
            segments = Math.Max(1, segments);
            slices = Math.Max(3, slices);

            // this vector should not be between start and end
            Vector3 p = new Vector3();
            p.X = (float)StaticRandom.RandomInstance.NextDouble();
            p.Y = (float)StaticRandom.RandomInstance.NextDouble();
            p.Z = (float)StaticRandom.RandomInstance.NextDouble();

            // derive two points on the plane formed by [end - start]
            Vector3 r = Vector3.Cross(p - p0, p1 - p0);
            Vector3 s = Vector3.Cross(r, p1 - p0);
            r.Normalize();
            s.Normalize();

            int vertexCount = 0, indexCount = 0;
            float invSegments = 1f / segments, invSlices = 1f / slices;

            vertices = new VertexPositionTexture[((segments + 1) * (slices + 1)) + 2];
            indices = new int[(slices + (slices * segments)) * 6];

            for (int j = 0; j <= segments; j++)
            {
                Vector3 center = Vector3.Lerp(p0, p1, j * invSegments);
                float radius = MathHelper.Lerp(r0, r1, j * invSegments);

                if (j == 0)
                {
                    vertices[vertexCount++] = new VertexPositionTexture()
                    {
                        Position = center,
                        TextureCoordinate = new Vector2(0.5f, j * invSegments)
                    };
                }

                for (int i = 0; i <= slices; i++)
                {
                    float theta = i * MathHelper.TwoPi * invSlices;
                    float rCosTheta = radius * (float)Math.Cos(theta),
                          rSinTheta = radius * (float)Math.Sin(theta);

                    vertices[vertexCount++] = new VertexPositionTexture()
                    {
                        Position = new Vector3()
                        {
                            X = center.X + rCosTheta * r.X + rSinTheta * s.X,
                            Y = center.Y + rCosTheta * r.Y + rSinTheta * s.Y,
                            Z = center.Z + rCosTheta * r.Z + rSinTheta * s.Z
                        },
                        TextureCoordinate = new Vector2()
                        {
                            X = i * invSlices,
                            Y = j * invSegments
                        }
                    };

                    if (i < slices)
                    {
                        // just an alias to assist with think of each vertex that's
                        //  iterated in here as the bottom right corner of a triangle
                        int vRef = vertexCount - 1;

                        if (j == 0)
                        {   // start cap - i0 is always center point on start cap
                            int i0 = 0,
                                i1 = vRef + 1,
                                i2 = vRef;

                            indices[indexCount++] = i0;
                            indices[indexCount++] = invertFaces ? i2 : i1;
                            indices[indexCount++] = invertFaces ? i1 : i2;
                        }
                        if (j == segments)
                        {   // end cap - i0 is always the center point on end cap
                            int i0 = (vRef + slices + 2) - (vRef % (slices + 1)),
                                i1 = vRef,
                                i2 = vRef + 1;

                            indices[indexCount++] = i0;
                            indices[indexCount++] = invertFaces ? i2 : i1;
                            indices[indexCount++] = invertFaces ? i1 : i2;
                        }

                        if (j < segments)
                        {   // middle area
                            int i0 = vRef,
                                i1 = vRef + 1,
                                i2 = vRef + slices + 2,
                                i3 = vRef + slices + 1;

                            indices[indexCount++] = i0;
                            indices[indexCount++] = invertFaces ? i2 : i1;
                            indices[indexCount++] = invertFaces ? i1 : i2;

                            indices[indexCount++] = i0;
                            indices[indexCount++] = invertFaces ? i3 : i2;
                            indices[indexCount++] = invertFaces ? i2 : i3;
                        }
                    }
                }

                if (j == segments)
                {
                    vertices[vertexCount++] = new VertexPositionTexture()
                    {
                        Position = center,
                        TextureCoordinate = new Vector2(0.5f, j * invSegments)
                    };
                }
            }
        }

        public static void FullscreenQuad(int width, int height,
                                  out VertexPositionTexture[] vertices, out int[] indices)
        {
            width = Math.Max(1, width);
            height = Math.Max(1, height);

            float cellWidth = 1f / width++,
                  cellHeight = 1f / height++;

            vertices = new VertexPositionTexture[width * height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    vertices[x + y * width] = new VertexPositionTexture()
                    {
                        Position = new Vector3()
                        {
                            X = (2f * (x * cellWidth)) - 1f,
                            Y = ((2f * (y * cellHeight)) - 1f) * -1f,
                            Z = 0f
                        },
                        TextureCoordinate = new Vector2()
                        {
                            X = x * cellWidth,
                            Y = y * cellHeight
                        }
                    };
                }
            }

            indices = new int[(width - 1) * (height - 1) * 6];
            for (int y = 0, counter = 0; y < height - 1; y++)
            {
                for (int x = 0; x < width - 1; x++)
                {
                    int topLeft = x + (y + 1) * width;
                    int topRight = (x + 1) + (y + 1) * width;
                    int bottomLeft = x + y * width;
                    int bottomRight = (x + 1) + y * width;

                    // left triangle
                    indices[counter++] = bottomLeft;
                    indices[counter++] = topRight;
                    indices[counter++] = topLeft;

                    // right triangle
                    indices[counter++] = bottomRight;
                    indices[counter++] = topRight;
                    indices[counter++] = bottomLeft;
                }
            }
        }
    }
}
