using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tutorial1.Terrain
{
    internal class BufferManager
    {
        int _active = 0;
        internal VertexBuffer VertexBuffer;
        IndexBuffer[] _IndexBuffers;
        GraphicsDevice _device;

        internal BufferManager(VertexPositionNormalTexture[] vertices, GraphicsDevice device)
        {
            _device = device;
            VertexBuffer = new VertexBuffer(device, VertexPositionNormalTexture.VertexDeclaration, vertices.Length, BufferUsage.WriteOnly);
            VertexBuffer.SetData(vertices);

            _IndexBuffers = new IndexBuffer[]
            {
#if !WINDOWS_PHONE && !REACH
                new IndexBuffer(_device, IndexElementSize.ThirtyTwoBits, 100000, BufferUsage.WriteOnly),
                new IndexBuffer(_device, IndexElementSize.ThirtyTwoBits, 100000, BufferUsage.WriteOnly)
#else
                new IndexBuffer(_device, IndexElementSize.SixteenBits, 63000, BufferUsage.WriteOnly),
                new IndexBuffer(_device, IndexElementSize.SixteenBits, 63000, BufferUsage.WriteOnly)
#endif
            };
        }

        internal IndexBuffer IndexBuffer
        {
            get { return _IndexBuffers[_active]; }
        }

        internal void UpdateIndexBuffer(int[] indices, int indexCount)
        {
            int inactive = _active == 0 ? 1 : 0;

			if(_IndexBuffers[inactive] != _device.Indices)
				_IndexBuffers[inactive].SetData(indices, 0, indexCount);
        }

        internal void SwapBuffer()
        {
            _active = _active == 0 ? 1 : 0;
        }
    }
}
