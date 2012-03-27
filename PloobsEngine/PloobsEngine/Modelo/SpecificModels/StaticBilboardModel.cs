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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.SceneControl;
using PloobsEngine.Engine;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Modelo
{
    public class StaticBilboardModel : IModelo
    {
        public StaticBilboardModel(GraphicFactory factory, String BilboardsName, String diffuseTextureName, List<Vector3> positions)
            : base(factory, BilboardsName,false)
        {
            System.Diagnostics.Debug.Assert(positions != null && positions.Count != 0);
            this.positions = positions;
            this.diffuseTextureName = diffuseTextureName;
            LoadModelo(factory);     
        }

        string diffuseTextureName;
        private float modelRadius = 0;
        List<Vector3> positions;

        protected override void LoadModel(GraphicFactory factory, out BatchInformation[][] BatchInformations, out TextureInformation[][] TextureInformations)
        {
            VertexPositionTexture[] billboardVertices = new VertexPositionTexture[positions.Count * 6];
            int i = 0;

            foreach (var position in positions)
            {
                billboardVertices[i++] = new VertexPositionTexture(position, new Vector2(0, 0));
                billboardVertices[i++] = new VertexPositionTexture(position, new Vector2(1, 0));
                billboardVertices[i++] = new VertexPositionTexture(position, new Vector2(1, 1));

                billboardVertices[i++] = new VertexPositionTexture(position, new Vector2(0, 0));
                billboardVertices[i++] = new VertexPositionTexture(position, new Vector2(1, 1));
                billboardVertices[i++] = new VertexPositionTexture(position, new Vector2(0, 1));
            }


            VertexBuffer vertexBufferS = factory.CreateVertexBuffer(VertexPositionTexture.VertexDeclaration, billboardVertices.Count(), BufferUsage.WriteOnly);
            vertexBufferS.SetData(billboardVertices);
            int noVertices = billboardVertices.Count();
            int noTriangles = noVertices / 3;

            BatchInformations = new BatchInformation[1][];
            BatchInformation[] b = new BatchInformation[1];
            b[0] = new BatchInformation(0, 0, noTriangles, 0, 0, VertexPositionTexture.VertexDeclaration, VertexPositionTexture.VertexDeclaration.VertexStride, BatchType.NORMAL);
            b[0].VertexBuffer = vertexBufferS;
            b[0].IndexBuffer = null;
            BatchInformations[0] = b;

            TextureInformations = new TextureInformation[1][];
            TextureInformations[0] = new TextureInformation[1];
            TextureInformations[0][0] = new TextureInformation(isInternal, factory, diffuseTextureName, null, null, null);
            TextureInformations[0][0].LoadTexture();
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
