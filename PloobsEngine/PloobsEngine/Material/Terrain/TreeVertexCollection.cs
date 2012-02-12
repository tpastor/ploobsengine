using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
 
namespace Tutorial1.Terrain
{
	public class TreeVertexCollection
	{
		public VertexPositionNormalTexture[] Vertices;

		public VertexPositionNormalTexture this[int index]
		{
			get { return Vertices[index]; }
			set { Vertices[index] = value; }
		}


		public TreeVertexCollection(List<VertexPositionNormalTexture> positions)
		{
            //Initialize our array to hold the vertices
			Vertices = positions.ToArray();

		}
	}
}