using System;
using System.Collections.Generic;
using System.Linq;
using StillDesign.PhysX.MathPrimitives;

namespace EngineTestes
{
    public class VertexGrid
    {
        private VertexGrid(Vector3[] points, int[] indices, Vector2[] tex)
        {
            this.TextCoords = tex;
            this.Points = points;
            this.Indices = indices;
        }
        
        public static VertexGrid CreateGrid(int rows, int columns, float scale  = 1,float texXScale = 30f, float texYScale = 30f)
        {
            int numVertsX = rows + 1;
            int numVertsZ = columns + 1;

            VertexGrid grid = new VertexGrid(new Vector3[numVertsX * numVertsZ], new int[rows * columns * 2 * 3], new Vector2[numVertsX * numVertsZ]);

            {
                for (int r = 0; r < numVertsX; r++)
                {
                    for (int c = 0; c < numVertsZ; c++)
                    {
                        grid.Points[r * numVertsZ + c] = new Vector3(r * scale, 0, c * scale);
                        //grid.TextCoords[r * numVertsZ + c] = new Vector2((float)r, (float)c);
                        grid.TextCoords[r * numVertsZ + c] = new Vector2((float)r / texXScale, (float)c / texYScale);                                                
                    }
                }
            }

            {
                int count = 0;
                int vi = 0;
                for (int z = 0; z < columns; z++)
                {
                    for (int x = 0; x < rows; x++)
                    {
                        // First triangle
                        grid.Indices[count++] = vi;
                        grid.Indices[count++] = vi + 1;
                        grid.Indices[count++] = vi + numVertsX;

                        // Second triangle
                        grid.Indices[count++] = vi + numVertsX;
                        grid.Indices[count++] = vi + 1;
                        grid.Indices[count++] = vi + numVertsX + 1;

                        vi++;
                    }
                    vi++;
                }
            }

            return grid;
        }

        public Vector3[] Points { get; private set; }
        public Vector2[] TextCoords { get; private set; }
        public int[] Indices { get; private set; }
    }
}