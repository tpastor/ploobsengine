using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tutorial1.Terrain.Culling;
using PloobsEngine.Modelo;
using PloobsEngine.Cameras;

namespace Tutorial1.Terrain
{
    public class QuadTree
    {
        private QuadNode _rootNode;
        private QuadNode _activeNode;
        private TreeVertexCollection _vertices;
        private BufferManager _buffers;
        private Vector3 _position;
        private int _topNodeSize;

        public ViewClipShape ClipShape;
        internal Vector3[] VFCorners = new Vector3[8];

        private Vector3 _cameraPosition;
        private Vector3 _lastCameraPosition;
        public int[] Indices;

        internal GraphicsDevice Device;        

        public int MinimumDepth;

        public int TopNodeSize { get { return _topNodeSize; } }
        public QuadNode RootNode { get { return _rootNode; } }
        public TreeVertexCollection Vertices { get { return _vertices; } }
        internal Vector3 CameraPosition
        {
            get { return _cameraPosition; }
            set { _cameraPosition = value; }
        }
        public BoundingFrustum ViewFrustrum { get; private set; }
        public int IndexCount { get; private set; }

        public bool Cull { get; set; }


        internal QuadTree(Vector3 position, List<VertexPositionNormalTexture> positions, Texture2D heightMap, Matrix viewMatrix, Matrix projectionMatrix, GraphicsDevice device)
        {
            Device = device;
            _position = position;
            _topNodeSize = heightMap.Width - 1;

            _vertices = new TreeVertexCollection(positions);

            _buffers = new BufferManager(_vertices.Vertices, device);
            _rootNode = new QuadNode(NodeType.FullNode, _topNodeSize, 1, null, this, 0);

            //Initialize the bounding frustrum to be used for culling later.
            ViewFrustrum = new BoundingFrustum(viewMatrix * projectionMatrix);

            //Construct an array large enough to hold all of the indices we'll need
            Indices = new int[((heightMap.Width + 1) * (heightMap.Height + 1)) * 3];

            Cull = false;
        }

        internal void Update(GameTime gameTime, ICamera Camera)
        {
            ViewFrustrum.Matrix = Camera.ViewProjection;
            CameraPosition = Camera.Position;

            //Corners 0-3 = near plane clockwise, Corners 4-7 = far plane clockwise
            ViewFrustrum.GetCorners(VFCorners);

            var clip = ClippingFrustrum.FromFrustrumCorners(VFCorners, CameraPosition);
            ClipShape = clip.ProjectToTargetY(_position.Y);

            _lastCameraPosition = _cameraPosition;
            IndexCount = 0;

            _rootNode.Merge();
            _rootNode.EnforceMinimumDepth();

            _activeNode = _rootNode.DeepestNodeWithPoint(ClipShape.ViewPoint);


            if (_activeNode != null)
            {
                _activeNode.Split();
            }

            _rootNode.SetActiveVertices();

            _buffers.UpdateIndexBuffer(Indices, IndexCount);
            _buffers.SwapBuffer();
        }


        internal void UpdateBatchInformation(BatchInformation BatchInformation)
        {
            BatchInformation.VertexBuffer = _buffers.VertexBuffer;
            BatchInformation.IndexBuffer = _buffers.IndexBuffer;
            BatchInformation.PrimitiveCount = IndexCount / 3;
            BatchInformation.NumVertices = _vertices.Vertices.Length;
        }


        internal void UpdateBuffer(int vIndex)
        {
            Indices[IndexCount] = vIndex;
            IndexCount++;
        }
    }
}
