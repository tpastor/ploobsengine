#if !WINDOWS_PHONE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LTreesLibrary.Trees;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Utils;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine;

namespace PloobsEngine.Modelo
{
    /// <summary>
    /// Tree Model
    /// Interacts with the LTREE API
    /// </summary>
    public class TreeModel : IModelo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeModel"/> class.
        /// </summary>
        /// <param name="profileName">Name of the profile (LTREE content pipeline processed file).</param>
        /// <param name="trunktextureName">Name of the trunktexture.</param>
        /// <param name="LeaftextureName">Name of the leaftexture.</param>
        public TreeModel(GraphicFactory factory, String profileName,String trunktextureName, String LeaftextureName) :base(factory,profileName,false)
        {
            this.trunktextureName = trunktextureName;
            this.LeaftextureName = LeaftextureName;
            this.profileName = profileName;
            LoadModel(factory,out BatchInformations,out TextureInformations);

        }

        protected override void LoadModel(GraphicFactory factory, out BatchInformation[][] BatchInformations, out TextureInformation[][] TextureInformations)
        {
            TreeProfile t = factory.GetAsset<TreeProfile>(profileName);
            if (!String.IsNullOrEmpty(LeaftextureName))
            {
                t.LeafTexture = factory.GetAsset<Texture2D>(LeaftextureName);
            }
            if (!String.IsNullOrEmpty(trunktextureName))
            {
                t.TrunkTexture = factory.GetAsset<Texture2D>(trunktextureName);
            }
            tree = t.GenerateSimpleTree(StaticRandom.RandomInstance);

            BatchInformations = new BatchInformation[2][];
            vertexBufferS = new VertexBuffer[2];
            indexBufferS = new IndexBuffer[2];
            indexBufferS[0] = tree.TrunkMesh.IndexBuffer;
            vertexBufferS[0] = tree.TrunkMesh.VertexBuffer;
            BatchInformation bi0 = new BatchInformation(0, tree.TrunkMesh.NumberOfVertices, tree.TrunkMesh.NumberOfTriangles, 0, 0, tree.TrunkMesh.Vdeclaration, TreeVertex.SizeInBytes);
            BatchInformations[0] = new BatchInformation[1];
            BatchInformations[0][0] = bi0;

            indexBufferS[1] = tree.LeafCloud.Ibuffer;
            vertexBufferS[1] = tree.LeafCloud.Vbuffer;
            BatchInformation bi1 = new BatchInformation(0, tree.LeafCloud.Numleaves * 4, tree.LeafCloud.Numleaves * 2, 0, 0, tree.LeafCloud.Vdeclaration, LeafVertex.SizeInBytes);
            BatchInformations[1] = new BatchInformation[1];
            BatchInformations[1][0] = bi1;

            TextureInformations = new TextureInformation[1][];
            TextureInformations[0] = new TextureInformation[2];
            TextureInformations[0][0] = new TextureInformation(isInternal, factory, null, null, null, null);
            TextureInformations[0][1] = new TextureInformation(isInternal, factory, null, null, null, null);
            TextureInformations[0][0].LoadTexture();
            TextureInformations[0][1].LoadTexture();
            TextureInformations[0][0].SetTexture(tree.TrunkTexture, TextureType.DIFFUSE);
            TextureInformations[0][1].SetTexture(tree.LeafTexture, TextureType.DIFFUSE);

        }
        
        VertexBuffer[] vertexBufferS = null;
        IndexBuffer[] indexBufferS = null;
        String trunktextureName;
        String LeaftextureName;
        String profileName;
        SimpleTree tree;

        /// <summary>
        /// Gets or sets the tree.
        /// </summary>
        /// <value>
        /// The tree.
        /// </value>
        public SimpleTree Tree
        {
            get { return tree; }
            set { tree = value; }
        }

        public override float GetModelRadius()
        {
            return tree.TrunkMesh.BoundingSphere.Radius;
        }

        #region IModelo Members

        /// <summary>
        /// Gets the mesh number.
        /// </summary>
        public override int MeshNumber
        {
            get { return 2; }
        }


        /// <summary>
        /// Gets the name.
        /// CANNOT SET
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get
            {
                return profileName;
            }
            set
            {
                throw new InvalidOperationException("cant change the name");
            }
        }

        #endregion
    }
}
#endif