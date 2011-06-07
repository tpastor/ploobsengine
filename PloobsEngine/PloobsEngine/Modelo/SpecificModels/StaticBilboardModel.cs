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
        public StaticBilboardModel(GraphicFactory factory, String BilboardsName, String diffuseTextuteName, List<Vector3> positions)
            : base(factory, BilboardsName, diffuseTextuteName, null, null, null, false)
        {
            System.Diagnostics.Debug.Assert(positions != null && positions.Count != 0);
            this.positions = positions;
            LoadModelo(factory);            
        }                
     
        private float modelRadius;
        List<Vector3> positions;    

        protected override void LoadBatchInfo(GraphicFactory factory, out BatchInformation[][] BatchInformations)
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
        }
        

        public override int MeshNumber
        {
            get { return 1; }
        }

        public override float GetModelRadius()
        {
            return modelRadius;
        }

        public override BatchInformation[] GetBatchInformation(int meshNumber)
        {
            System.Diagnostics.Debug.Assert(meshNumber == 0);
            return BatchInformations[0];   
        }
    }
    
}
