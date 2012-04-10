using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Modelo;
using Microsoft.Xna.Framework;
using BEPUphysics.Collidables;
using BEPUphysics.CollisionShapes;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.SceneControl;
using PloobsEngine.Engine.Logger;
using BEPUphysics.CollisionShapes.ConvexShapes;

namespace PloobsEngine.Physics.Bepu
{
    /// <summary>
    /// Instanced Triangle Mesh
    /// </summary>
    public class InstancedTriangleMeshObject : IPhysicObject
    {
        InstancedMesh instancedMesh;

        public InstancedMesh InstancedMesh
        {
            get
            {
                return instancedMesh;
            }
        }
        /// <summary>
        /// Creates and instance of InstancedTriangleMesh
        /// Call the Static Method GetInstacedMesh to get the InstancedMeshShape obj
        /// </summary>
        /// <param name="InstancedMeshShape"></param>
        /// <param name="pos"></param>
        /// <param name="rotation"></param>
        /// <param name="scale"></param>
        /// <param name="materialDescription"></param>
        public InstancedTriangleMeshObject(InstancedMeshShape InstancedMeshShape, Vector3 pos, Matrix rotation, Vector3 scale, MaterialDescription materialDescription, TriangleSidedness TriangleSidedness = TriangleSidedness.Counterclockwise)
        {
            instancedMesh = new InstancedMesh(InstancedMeshShape,new BEPUphysics.MathExtensions.AffineTransform(scale,Quaternion.CreateFromRotationMatrix(rotation),pos));
            instancedMesh.Material = new BEPUphysics.Materials.Material(materialDescription.StaticFriction, materialDescription.DynamicFriction, materialDescription.Bounciness);
            instancedMesh.Sidedness = TriangleSidedness;
            this.rotation = rotation;
            this.scale = scale;
        }

        /// <summary>
        /// Return the Instanced MEsh shape from a IModelo
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static InstancedMeshShape GetInstacedMesh(IModelo model)
        {
            Vector3[] vertices = null;
            int[] indices = null;
            ExtractData(ref vertices, ref indices, model);
            return new InstancedMeshShape(vertices, indices);
        }

        private static void ExtractData(ref Vector3[] vert, ref int[] ind, IModelo model)
        {
            List<Vector3> vertices = new List<Vector3>();
            List<int> indices = new List<int>();

            for (int i = 0; i < model.MeshNumber; i++)
            {

                BatchInformation[] bi = model.GetBatchInformation(i);
                for (int j = 0; j < bi.Length; j++)
                {
                    BatchInformation info = bi[j];
                    int offset = vertices.Count;
                    Vector3[] a = new Vector3[info.NumVertices];

                    // Read the format of the vertex buffer  
                    VertexDeclaration declaration = bi[j].VertexBuffer.VertexDeclaration;
                    VertexElement[] vertexElements = declaration.GetVertexElements();
                    // Find the element that holds the position  
                    VertexElement vertexPosition = new VertexElement();
                    foreach (VertexElement elem in vertexElements)
                    {
                        if (elem.VertexElementUsage == VertexElementUsage.Position &&
                            elem.VertexElementFormat == VertexElementFormat.Vector3)
                        {
                            vertexPosition = elem;
                            // There should only be one  
                            break;
                        }
                    }
                    // Check the position element found is valid  
                    if (vertexPosition == null ||
                        vertexPosition.VertexElementUsage != VertexElementUsage.Position ||
                        vertexPosition.VertexElementFormat != VertexElementFormat.Vector3)
                    {
                        throw new Exception("Model uses unsupported vertex format!");
                    }
                    // This where we store the vertices until transformed                      
                    // Read the vertices from the buffer in to the array  
                    bi[j].VertexBuffer.GetData<Vector3>(
                        bi[j].BaseVertex * declaration.VertexStride + vertexPosition.Offset,
                        a,
                        0,
                        bi[j].NumVertices,
                        declaration.VertexStride);

                    for (int k = 0; k != a.Length; ++k)
                    {
                        Vector3.Transform(ref a[k], ref info.ModelLocalTransformation, out a[k]);
                    }
                    vertices.AddRange(a);

                    if (info.IndexBuffer.IndexElementSize != IndexElementSize.SixteenBits)
                    {
                        int[] s = new int[info.PrimitiveCount * 3];
                        info.IndexBuffer.GetData<int>(info.StartIndex * 2, s, 0, info.PrimitiveCount * 3);
                        for (int k = 0; k != info.PrimitiveCount; ++k)
                        {
                            indices.Add(s[k * 3 + 2] + offset);
                            indices.Add(s[k * 3 + 1] + offset);
                            indices.Add(s[k * 3 + 0] + offset);
                        }
                    }
                    else
                    {
                        short[] s = new short[info.PrimitiveCount * 3];
                        info.IndexBuffer.GetData<short>(info.StartIndex * 2, s, 0, info.PrimitiveCount * 3);
                        for (int k = 0; k != info.PrimitiveCount; ++k)
                        {
                            indices.Add(s[k * 3 + 2] + offset);
                            indices.Add(s[k * 3 + 1] + offset);
                            indices.Add(s[k * 3 + 0] + offset);
                        }
                    }
                }
            }

            ind = indices.ToArray();
            vert = vertices.ToArray();
        }


        /// <summary>
        /// Gets the physic object type.
        /// </summary>
        public override PhysicObjectTypes PhysicObjectTypes
        {
            get { return PhysicObjectTypes.InstancedTriangleMeshObject; }
        }

        #region IPhysicObject Members

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public override Vector3 Position
        {
            get
            {
                return instancedMesh.WorldTransform.Translation;
            }
            set
            {
                ActiveLogger.LogMessage("Position in triangle mesh should not be called", LogLevel.Warning);
            }
        }

        private Vector3 scale;
        /// <summary>
        /// Gets or sets the scale.
        /// </summary>
        /// <value>
        /// The scale.
        /// </value>
        public override Vector3 Scale
        {
            get
            {
                return scale;
            }
            set
            {
                ActiveLogger.LogMessage("scale in triangle mesh should not be called", LogLevel.Warning);
            }
        }

        private Matrix rotation = Matrix.Identity;
        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        /// <value>
        /// The rotation.
        /// </value>
        public override Matrix Rotation
        {
            get
            {
                return rotation;   
            }
            set
            {
                ActiveLogger.LogMessage("Rotation in triangle mesh should not be called", LogLevel.Warning);
            }
        }

        private Vector3 faceVector;
        /// <summary>
        /// Vector pointing to the front
        /// </summary>
        public override Vector3 FaceVector
        {
            get { return faceVector; }
        }

        /// <summary>
        /// Gets the world matrix.
        /// </summary>
        public override Matrix WorldMatrix
        {
            get { return instancedMesh.WorldTransform.Matrix; }
        }


        /// <summary>
        /// Gets or sets the angular velocity.
        /// </summary>
        /// <value>
        /// The angular velocity.
        /// </value>
        public override Vector3 AngularVelocity
        {
            get { return Vector3.Zero; }
            set
            {
                ActiveLogger.LogMessage("Cant Set Terrain Velocity", LogLevel.RecoverableError);
            }
        }

        /// <summary>
        /// Gets velocity. Always Zero cause this object is static
        /// </summary>
        /// <value>
        /// The velocity.
        /// </value>
        public override Vector3 Velocity
        {
            get
            {
                return Vector3.Zero;
            }
            set
            {
                ActiveLogger.LogMessage("velocity in triangle mesh should not be called", LogLevel.Warning);
            }
        }

        /// <summary>
        /// This object is always motion less
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is motion less; otherwise, <c>false</c>.
        /// </value>
        public override bool isMotionLess
        {
            get
            {
                return true;
            }
            set
            {
                ActiveLogger.LogMessage("isMotionLess in triangle mesh should not be called", LogLevel.Warning);
            }
        }

        IObject owner;
        /// <summary>
        /// Gets or sets the IObject owner.
        /// </summary>
        /// <value>
        /// The IObject owner.
        /// </value>
        public override IObject ObjectOwner
        {
            get
            {
                return owner;
            }
            set
            {
                this.owner = value;
            }
        }


        /// <summary>
        /// Cant Aply Impulse on Static Object
        /// </summary>
        /// <param name="position"></param>
        /// <param name="force">The force.</param>
        public override void ApplyImpulse(Vector3 position, Vector3 force)
        {
            ActiveLogger.LogMessage("triangle mesh cant move - no forces/torques", LogLevel.Warning);
        }

        /// <summary>
        /// Gets the bounding box IN WORLD COORDINATES
        /// </summary>
        public override BoundingBox? BoundingBox
        {
            get { return instancedMesh.BoundingBox; }
        }

#if WINDOWS
        /// <summary>
        /// Serialization
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            ActiveLogger.LogMessage("Serialization not implemented yet", LogLevel.RecoverableError);
        }
#endif

        #endregion

    }
}
