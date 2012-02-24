#if WINDOWS
using  System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhysX;
using PhysX.Math;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Modelo;
using PloobsEngine.Engine.Logger;
using PloobsEngine.Physics;

namespace PloobsEngine.Physics3x
{
    public class PhysxTriangleMesh : IPhysicObject
    {
        protected PhysX.Material material;
        RigidStatic staticActor;
        Shape aTriMeshShape;

        public Shape TriMeshShape
        {
            get { return aTriMeshShape; }
            set { aTriMeshShape = value; }
        }

        public RigidStatic StaticActor
        {
            get { return staticActor; }
        }

        public static void CookTriangleMesh(PhysxPhysicWorld PhysxPhysicWorld, IModelo model, FileStream FileStream)
        {
            Microsoft.Xna.Framework.Vector3[] vertices = null;
            int[] indices = null;
            ExtractData(ref vertices, ref indices, model);

            TriangleMeshDesc meshDesc = new TriangleMeshDesc();
            Vector3[] points = new Vector3[vertices.Count()];
            for (int i = 0; i < vertices.Count(); i++)
            {
                points[i] = vertices[i].AsPhysX();
            }
            meshDesc.Points = points;
            meshDesc.SetTriangles<int>(indices);

            if (PhysxPhysicWorld.Cooking.CookTriangleMesh(meshDesc, FileStream) == false)
            {
                PloobsEngine.Engine.Logger.ActiveLogger.LogMessage("Cant Cook Model", Engine.Logger.LogLevel.FatalError);
            }

        }

        public PhysxTriangleMesh(PhysxPhysicWorld PhysxPhysicWorld, IModelo model,Microsoft.Xna.Framework.Matrix localTransformation, Microsoft.Xna.Framework.Matrix worldTransformation, Microsoft.Xna.Framework.Vector3 scale, MaterialDescription MaterialDescription)
        {
            Microsoft.Xna.Framework.Vector3[] vertices = null;
            int[] indices = null;
            ExtractData(ref vertices, ref indices, model);                                    

            
            TriangleMeshDesc meshDesc = new TriangleMeshDesc();            
            Vector3[] points = new Vector3[vertices.Count()];
            for (int i = 0; i < vertices.Count(); i++)
			{
			    points[i] = vertices[i].AsPhysX();
			}            
            meshDesc.Points = points;            
            meshDesc.SetTriangles<int>(indices);
            //meshDesc.Triangles = indices;
            
            MemoryStream ms = new MemoryStream();
            if(PhysxPhysicWorld.Cooking.CookTriangleMesh(meshDesc,ms)==false)
            {
                PloobsEngine.Engine.Logger.ActiveLogger.LogMessage("Cant Cook Model",Engine.Logger.LogLevel.FatalError);
            }
            
            ms.Position = 0;
            TriangleMesh triangleMesh = PhysxPhysicWorld.Physix.CreateTriangleMesh(ms);
            
            staticActor = PhysxPhysicWorld.Physix.CreateRigidStatic(worldTransformation.AsPhysX());
            TriangleMeshGeometry TriangleMeshGeometry = new TriangleMeshGeometry(triangleMesh,new MeshScale(scale.AsPhysX(),Quaternion.Identity));

            material = PhysxPhysicWorld.Physix.CreateMaterial(MaterialDescription.StaticFriction, MaterialDescription.DynamicFriction, MaterialDescription.Bounciness);
            aTriMeshShape = staticActor.CreateShape(TriangleMeshGeometry, material, localTransformation.AsPhysX());

            this.Scale = scale;
        }


        public PhysxTriangleMesh(PhysxPhysicWorld PhysxPhysicWorld, FileStream FileStream, Microsoft.Xna.Framework.Matrix localTransformation, Microsoft.Xna.Framework.Matrix worldTransformation, Microsoft.Xna.Framework.Vector3 scale, MaterialDescription MaterialDescription)
        {
            TriangleMesh triangleMesh = PhysxPhysicWorld.Physix.CreateTriangleMesh(FileStream);

            staticActor = PhysxPhysicWorld.Physix.CreateRigidStatic(worldTransformation.AsPhysX());
            TriangleMeshGeometry TriangleMeshGeometry = new TriangleMeshGeometry(triangleMesh, new MeshScale(scale.AsPhysX(), Quaternion.Identity));

            material = PhysxPhysicWorld.Physix.CreateMaterial(MaterialDescription.StaticFriction, MaterialDescription.DynamicFriction, MaterialDescription.Bounciness);
            aTriMeshShape = staticActor.CreateShape(TriangleMeshGeometry, material, localTransformation.AsPhysX());

            this.Scale = scale;
        }


        private static void ExtractData(ref Microsoft.Xna.Framework.Vector3[] vert, ref int[] ind, IModelo model)
        {
            List<Microsoft.Xna.Framework.Vector3> vertices = new List<Microsoft.Xna.Framework.Vector3>();
            List<int> indices = new List<int>();

            for (int i = 0; i < model.MeshNumber; i++)
            {

                BatchInformation[] bi = model.GetBatchInformation(i);
                for (int j = 0; j < bi.Length; j++)
                {
                    BatchInformation info = bi[j];
                    int offset = vertices.Count;
                    Microsoft.Xna.Framework.Vector3[] a = new Microsoft.Xna.Framework.Vector3[info.NumVertices];

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
                    bi[j].VertexBuffer.GetData<Microsoft.Xna.Framework.Vector3>(
                        bi[j].BaseVertex * declaration.VertexStride + vertexPosition.Offset,
                        a,
                        0,
                        bi[j].NumVertices,
                        declaration.VertexStride);

                    for (int k = 0; k != a.Length; ++k)
                    {
                        Microsoft.Xna.Framework.Vector3.Transform(ref a[k], ref info.ModelLocalTransformation, out a[k]);
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

        public override Microsoft.Xna.Framework.Vector3 Position
        {
            get
            {
                return staticActor.GlobalPose.AsXNA().Translation;
            }
            set
            {
                PhysX.Math.Matrix m = staticActor.GlobalPose;
                m.M41 = value.X;
                m.M42 = value.Y;
                m.M43 = value.Z;
                staticActor.GlobalPose = m;
            }
        }

        public override Microsoft.Xna.Framework.Vector3 Scale
        {
            get;
            set;
        }

        public override Microsoft.Xna.Framework.Matrix Rotation
        {
            get
            {
                Microsoft.Xna.Framework.Matrix rot = staticActor.GlobalPose.AsXNA();
                rot.M41 = 0;
                rot.M42 = 0;
                rot.M43 = 0;
                rot.M44 = 1;
                return rot;
            }
            set
            {
                PhysX.Math.Matrix m = value.AsPhysX();
                m.M41 = staticActor.GlobalPose.M41;
                m.M42 = staticActor.GlobalPose.M42;
                m.M43 = staticActor.GlobalPose.M43;
                staticActor.GlobalPose = m;
            }
        }

        public override Microsoft.Xna.Framework.Vector3 FaceVector
        {
            get { return new Microsoft.Xna.Framework.Vector3(staticActor.GlobalPose.M31, staticActor.GlobalPose.M32, staticActor.GlobalPose.M33); }
        }

        public override Microsoft.Xna.Framework.Matrix WorldMatrix
        {
            get { return Microsoft.Xna.Framework.Matrix.CreateScale(Scale) * staticActor.GlobalPose.AsXNA(); }
        }

        public override Microsoft.Xna.Framework.Vector3 Velocity
        {
            get
            {
                ActiveLogger.LogMessage("Cant set/get velocity on Triangle Meshes", LogLevel.RecoverableError);
                return Microsoft.Xna.Framework.Vector3.Zero;
            }
            set
            {
                ActiveLogger.LogMessage("Cant set/get velocity on Triangle Meshes", LogLevel.RecoverableError);                
            }
        }

        public override bool isMotionLess
        {
            get
            {
                return true;
            }
            set
            {
                ActiveLogger.LogMessage("Triangle Meshes are always MotionLess", LogLevel.Warning);                
            }
        }

        public override SceneControl.IObject ObjectOwner
        {
            get;
            set;
        }

        public override PhysicObjectTypes PhysicObjectTypes
        {
            get { return Physics.PhysicObjectTypes.TRIANGLEMESHOBJECT; }
        }

        public override void ApplyImpulse(Microsoft.Xna.Framework.Vector3 position, Microsoft.Xna.Framework.Vector3 force)
        {
            ActiveLogger.LogMessage("Cant apply impulse on Triangle Meshes", LogLevel.RecoverableError);
        }

        public override Microsoft.Xna.Framework.BoundingBox? BoundingBox
        {
            get { return new Microsoft.Xna.Framework.BoundingBox(staticActor.WorldBounds.Min.AsXNA(), staticActor.WorldBounds.Max.AsXNA()); }
        }

        public override Microsoft.Xna.Framework.Vector3 AngularVelocity
        {
            get
            {
                ActiveLogger.LogMessage("Cant set/get velocity on Triangle Meshes", LogLevel.RecoverableError);
                return Microsoft.Xna.Framework.Vector3.Zero;
            }
            set
            {
                ActiveLogger.LogMessage("Cant set/get velocity on Triangle Meshes", LogLevel.RecoverableError);
            }
        }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
#endif