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
#if !WINDOWS_PHONE && !REACH && !XBOX360 && !MONO && !MONODX
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
        /// <param name="factory">The factory.</param>
        /// <param name="profileName">Name of the profile (LTREE content pipeline processed file).</param>
        /// <param name="trunktextureName">Name of the trunktexture.</param>
        /// <param name="LeaftextureName">Name of the leaftexture.</param>
        public TreeModel(GraphicFactory factory, String profileName,String trunktextureName, String LeaftextureName) :base(factory,profileName,false)
        {
            this.trunktextureName = trunktextureName;
            this.LeaftextureName = LeaftextureName;
            this.profileName = profileName;
            LoadModel(factory,out BatchInformations,out TextureInformations);
            this.modelName = profileName;
        }

        /// <summary>
        /// Loads the model.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="BatchInformations">The batch informations.</param>
        /// <param name="TextureInformations">The texture informations.</param>
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

        /// <summary>
        /// Gets the model radius.
        /// </summary>
        /// <returns></returns>
        public override float GetModelRadius()
        {
            return tree.TrunkMesh.BoundingSphere.Radius;
        }

#region IModelo Members

        /// <summary>
        /// Gets the Total mesh number.
        /// </summary>
        public override int MeshNumber
        {
            get { return 2; }
        }       

#endregion
    }
}
#endif