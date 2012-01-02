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
using BEPUphysics.Collidables;
using BEPUphysics.CollisionShapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Engine;
using PloobsEngine.Physic.PhysicObjects.BepuObject;
using PloobsEngine.Physics.Bepu;

namespace PloobsEngine.Modelo
{
    public class TerrainModel : IModelo
    {
        public TerrainModel(GraphicFactory factory, TerrainObject terrainObject, String TerrainName, String BaseTexture, String NivelBaixo = null, String NivelMedio = null, String NivelAlto = null)
            : base(factory, TerrainName,false)
        {   
            this.terrainObject = terrainObject;

            LoadModelo(factory);

            TextureInformations[0][0].SetTexture(BaseTexture, TextureType.MULTITEX1);
            TextureInformations[0][0].SetTexture(NivelBaixo, TextureType.MULTITEX2);
            TextureInformations[0][0].SetTexture(NivelMedio, TextureType.MULTITEX3);
            TextureInformations[0][0].SetTexture(NivelAlto, TextureType.MULTITEX4);
            TextureInformations[0][0].SetTexture(terrainObject.HeightMap, TextureType.HEIGHTMAP);                        
        }

        TerrainObject terrainObject;
        private float modelRadius;

        protected override void LoadModel(GraphicFactory factory, out BatchInformation[][] BatchInformations, out TextureInformation[][] TextureInformations)
        {
            List<VertexPositionNormalTexture> vertexList = new List<VertexPositionNormalTexture>();
#if WINDOWS_PHONE || REACH
            ////gambi shortcut
            List<int> indexList2 = new List<int>();
            GetVertexData(vertexList, indexList2, terrainObject);
            List<short> indexList = new List<short>();
            foreach (var item in indexList2)
            {
                indexList.Add( (short) item);
            }
            indexList2.Clear();
#else
            List<int> indexList = new List<int>();
            GetVertexData(vertexList, indexList, terrainObject);
#endif
            modelRadius = (terrainObject.BoundingBox.Max - terrainObject.BoundingBox.Max).Length() / 2;

            var newVertices = new VertexPositionNormalTexture[vertexList.Count];
            vertexList.CopyTo(newVertices);

#if WINDOWS_PHONE|| REACH
            var newIndices = new short[indexList.Count];
#else
            var newIndices = new int[indexList.Count];
#endif
            
            indexList.CopyTo(newIndices);

            VertexBuffer vertexBufferS = factory.CreateVertexBuffer(VertexPositionNormalTexture.VertexDeclaration, newVertices.Count(), BufferUsage.WriteOnly);
            vertexBufferS.SetData(newVertices);
#if WINDOWS_PHONE || REACH
            IndexBuffer indexBufferS = factory.CreateIndexBuffer(IndexElementSize.SixteenBits,newIndices.Count(),BufferUsage.WriteOnly);
#else
            IndexBuffer indexBufferS = factory.CreateIndexBuffer(IndexElementSize.ThirtyTwoBits, newIndices.Count(), BufferUsage.WriteOnly);
#endif
            indexBufferS.SetData(newIndices);
            
            BatchInformations = new BatchInformation[1][];
            BatchInformation[] b = new BatchInformation[1];
            b[0] = new BatchInformation(0, newVertices.Count(), newIndices.Count() / 3, 0, 0, VertexPositionNormalTexture.VertexDeclaration,VertexPositionNormalTexture.VertexDeclaration.VertexStride);
            b[0].VertexBuffer = vertexBufferS;
            b[0].IndexBuffer = indexBufferS;
            b[0].ModelLocalTransformation = Matrix.Identity;
            BatchInformations[0] = b;

            TextureInformations = new TextureInformation[1][];
            TextureInformations[0] = new TextureInformation[1];
            TextureInformations[0][0] = new TextureInformation(isInternal, factory, null, null, null, null);
            TextureInformations[0][0].LoadTexture();
        }

        private void GetVertexData(List<VertexPositionNormalTexture> vertices, List<int> indices, TerrainObject to)
        {
            Terrain DisplayedObject = to.Terrain;
            int numColumns = DisplayedObject.Shape.Heights.GetLength(0);
            int numRows = DisplayedObject.Shape.Heights.GetLength(1);
            TerrainShape shape = DisplayedObject.Shape;

            //The terrain can be transformed arbitrarily.  However, the collision against the triangles is always oriented such that the transformed local
            //up vector points in the same direction as the collidable surfaces.
            //To make sure the graphics match the terrain collision, see if a triangle normal faces in the same direction as the local up vector.
            //If not, construct the graphics with reversed winding.
            Vector3 a, b, c;
            DisplayedObject.GetPosition(0, 0, out a);
            DisplayedObject.GetPosition(1, 0, out b);
            DisplayedObject.GetPosition(0, 1, out c);
            Vector3 normal = Vector3.Cross(c - a, b - a);
            Vector3 terrainUp = new Vector3(DisplayedObject.WorldTransform.LinearTransform.M21, DisplayedObject.WorldTransform.LinearTransform.M22, DisplayedObject.WorldTransform.LinearTransform.M23);
            float dot;
            Vector3.Dot(ref normal, ref terrainUp, out dot);
            bool reverseWinding = dot < 0;

            for (int j = 0; j < numRows; j++)
            {
                for (int i = 0; i < numColumns; i++)
                {
                    VertexPositionNormalTexture v;
                    DisplayedObject.GetPosition(i, j, out v.Position);
                    DisplayedObject.GetNormal(i, j, out v.Normal);
                    if (reverseWinding)
                        Vector3.Negate(ref v.Normal, out v.Normal);
                    v.TextureCoordinate = new Vector2(i, j);

                    vertices.Add(v);

                    if (i < numColumns - 1 && j < numRows - 1)
                        if (shape.QuadTriangleOrganization == QuadTriangleOrganization.BottomLeftUpperRight)
                        {
                            //v3 v4
                            //v1 v2

                            //v1 v2 v3
                            indices.Add((int)(numColumns * j + i));
                            if (reverseWinding)
                            {
                                indices.Add((int)(numColumns * (j + 1) + i));
                                indices.Add((int)(numColumns * j + i + 1));
                            }
                            else
                            {
                                indices.Add((int)(numColumns * j + i + 1));
                                indices.Add((int)(numColumns * (j + 1) + i));
                            }

                            //v2 v4 v3
                            indices.Add((int)(numColumns * j + i + 1));
                            if (reverseWinding)
                            {
                                indices.Add((int)(numColumns * (j + 1) + i));
                                indices.Add((int)(numColumns * (j + 1) + i + 1));
                            }
                            else
                            {
                                indices.Add((int)(numColumns * (j + 1) + i + 1));
                                indices.Add((int)(numColumns * (j + 1) + i));
                            }
                        }
                        else if (shape.QuadTriangleOrganization == QuadTriangleOrganization.BottomRightUpperLeft)
                        {
                            //v1 v2 v4
                            indices.Add((int)(numColumns * j + i));
                            if (reverseWinding)
                            {
                                indices.Add((int)(numColumns * j + i + 1));
                                indices.Add((int)(numColumns * (j + 1) + i + 1));
                            }
                            else
                            {
                                indices.Add((int)(numColumns * (j + 1) + i + 1));
                                indices.Add((int)(numColumns * j + i + 1));
                            }

                            //v1 v4 v3
                            indices.Add((int)(numColumns * j + i));
                            if (reverseWinding)
                            {
                                indices.Add((int)(numColumns * (j + 1) + i));
                                indices.Add((int)(numColumns * (j + 1) + i + 1));
                            }
                            else
                            {
                                indices.Add((int)(numColumns * (j + 1) + i + 1));
                                indices.Add((int)(numColumns * (j + 1) + i));
                            }
                        }
                }

            }
        }

        public override void CleanUp(GraphicFactory factory)
        {
            base.CleanUp(factory);
            BatchInformations[0][0].VertexBuffer.Dispose();
            BatchInformations[0][0].IndexBuffer.Dispose();            
            TextureInformations[0][0].ClenaUp(factory);
        }

        public override int MeshNumber
        {
            get { return 1; }
        }

        public override float GetModelRadius()
        {
            return modelRadius;
        }
    }   
}
