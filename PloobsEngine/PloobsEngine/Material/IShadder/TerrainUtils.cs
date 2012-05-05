using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.Material
{
    public static class TerrainUtils
    {
        public static void SmoothTerrain(int Passes, float[,] HeightData)
        {
            int MapWidth = HeightData.GetLength(0);
            int MapHeight = HeightData.GetLength(1);
            float[,] newHeightData;

            while (Passes > 0)
            {
                Passes--;

                // Note: MapWidth and MapHeight should be equal and power-of-two values 
                newHeightData = new float[MapWidth, MapHeight];

                for (int x = 0; x < MapWidth; x++)
                {
                    for (int y = 0; y < MapHeight; y++)
                    {
                        int adjacentSections = 0;
                        float sectionsTotal = 0.0f;

                        if ((x - 1) > 0) // Check to left
                        {
                            sectionsTotal += HeightData[x - 1, y];
                            adjacentSections++;

                            if ((y - 1) > 0) // Check up and to the left
                            {
                                sectionsTotal += HeightData[x - 1, y - 1];
                                adjacentSections++;
                            }

                            if ((y + 1) < MapHeight) // Check down and to the left
                            {
                                sectionsTotal += HeightData[x - 1, y + 1];
                                adjacentSections++;
                            }
                        }

                        if ((x + 1) < MapWidth) // Check to right
                        {
                            sectionsTotal += HeightData[x + 1, y];
                            adjacentSections++;

                            if ((y - 1) > 0) // Check up and to the right
                            {
                                sectionsTotal += HeightData[x + 1, y - 1];
                                adjacentSections++;
                            }

                            if ((y + 1) < MapHeight) // Check down and to the right
                            {
                                sectionsTotal += HeightData[x + 1, y + 1];
                                adjacentSections++;
                            }
                        }

                        if ((y - 1) > 0) // Check above
                        {
                            sectionsTotal += HeightData[x, y - 1];
                            adjacentSections++;
                        }

                        if ((y + 1) < MapHeight) // Check below
                        {
                            sectionsTotal += HeightData[x, y + 1];
                            adjacentSections++;
                        }

                        newHeightData[x, y] = (HeightData[x, y] + (sectionsTotal / adjacentSections)) * 0.5f;
                    }
                }

                // Overwrite the HeightData info with our new smoothed info
                for (int x = 0; x < MapWidth; x++)
                {
                    for (int y = 0; y < MapHeight; y++)
                    {
                        HeightData[x, y] = newHeightData[x, y];
                    }
                }
            }
        }
    }
}
