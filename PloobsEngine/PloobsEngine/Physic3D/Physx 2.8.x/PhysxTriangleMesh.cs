#if WINDOWS
using  System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Modelo;
using PloobsEngine.Engine.Logger;
using PloobsEngine.Physics;
using StillDesign.PhysX;
using StillDesign.PhysX.MathPrimitives;
 
namespace PloobsEngine.Physics
{
    public class PhysxTriangleMesh : PhysxPhysicObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhysxTriangleMesh"/> class.
        /// Cooks the Model on the fly
        /// </summary>
        /// <param name="PhysxPhysicWorld">The physx physic world.</param>
        /// <param name="model">The model.</param>
        /// <param name="worldTransformation">The world transformation.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="density">The density.</param>
        public PhysxTriangleMesh(PhysxPhysicWorld PhysxPhysicWorld, IModelo model, Microsoft.Xna.Framework.Matrix worldTransformation, Microsoft.Xna.Framework.Vector3 scale, float density = 1)
        {
            Microsoft.Xna.Framework.Vector3[] vertices = null;
            int[] indices = null;
            ExtractData(ref vertices, ref indices, model);


            TriangleMeshDescription meshDesc = new TriangleMeshDescription();
            meshDesc.AllocateVertices<Microsoft.Xna.Framework.Vector3>(vertices.Count());            
            meshDesc.VerticesStream.SetData<Microsoft.Xna.Framework.Vector3>(vertices);
            meshDesc.AllocateTriangles<int>(indices.Count());
            meshDesc.TriangleStream.SetData<int>(indices);
            meshDesc.Flags = 0;
            meshDesc.VertexCount = vertices.Count();
            meshDesc.TriangleCount = indices.Count();

            MemoryStream ms = new MemoryStream();
            Cooking.InitializeCooking();            
            if (Cooking.CookTriangleMesh(meshDesc, ms) == false)
            {
                PloobsEngine.Engine.Logger.ActiveLogger.LogMessage("Cant Cook Model",Engine.Logger.LogLevel.FatalError);
            }
            Cooking.CloseCooking();

            ms.Position = 0;
            TriangleMesh triangleMesh = PhysxPhysicWorld.Core.CreateTriangleMesh(ms);
            TriangleMeshShapeDescription bunnyShapeDesc = new TriangleMeshShapeDescription();
            bunnyShapeDesc.TriangleMesh = triangleMesh;                
            ActorDesc = new ActorDescription();
            ActorDesc.Shapes.Add(bunnyShapeDesc);            
            ActorDesc.BodyDescription= null;
            ActorDesc.GlobalPose = worldTransformation.AsPhysX();
            this.Scale = scale;
        }

        public static void CookTriangleMesh(IModelo model, FileStream FileStream)
        {
            Microsoft.Xna.Framework.Vector3[] vertices = null;
            int[] indices = null;
            ExtractData(ref vertices, ref indices, model);

            TriangleMeshDescription meshDesc = new TriangleMeshDescription();
            meshDesc.AllocateVertices<Microsoft.Xna.Framework.Vector3>(vertices.Count());
            meshDesc.VerticesStream.SetData<Microsoft.Xna.Framework.Vector3>(vertices);
            meshDesc.AllocateTriangles<int>(indices.Count());
            meshDesc.TriangleStream.SetData<int>(indices);
            meshDesc.Flags = 0;
            meshDesc.VertexCount = vertices.Count();
            meshDesc.TriangleCount = indices.Count();

            Cooking.InitializeCooking();
            if (Cooking.CookTriangleMesh(meshDesc, FileStream) == false)
            {
                PloobsEngine.Engine.Logger.ActiveLogger.LogMessage("Cant Cook Model", Engine.Logger.LogLevel.FatalError);
            }
            Cooking.CloseCooking();
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="PhysxTriangleMesh"/> class.
        /// For cooked Models
        /// </summary>
        /// <param name="PhysxPhysicWorld">The physx physic world.</param>
        /// <param name="FileStream">The file stream.</param>
        /// <param name="localTransformation">The local transformation.</param>
        /// <param name="worldTransformation">The world transformation.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="MaterialDescription">The material description.</param>
        public PhysxTriangleMesh(PhysxPhysicWorld PhysxPhysicWorld, FileStream FileStream, Microsoft.Xna.Framework.Matrix localTransformation, Microsoft.Xna.Framework.Matrix worldTransformation, Microsoft.Xna.Framework.Vector3 scale, MaterialDescription MaterialDescription)
        {            
            TriangleMesh triangleMesh = PhysxPhysicWorld.Core.CreateTriangleMesh(FileStream);
            TriangleMeshShapeDescription bunnyShapeDesc = new TriangleMeshShapeDescription();
            bunnyShapeDesc.TriangleMesh = triangleMesh;
            BodyDescription bodyDesc = new BodyDescription();
            ActorDesc.Shapes.Add(bunnyShapeDesc);
            ActorDesc.BodyDescription = bodyDesc;
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

        public override void ApplyImpulse(Microsoft.Xna.Framework.Vector3 position, Microsoft.Xna.Framework.Vector3 force)
        {
            ActiveLogger.LogMessage("Cant apply impulse on Triangle Meshes", LogLevel.RecoverableError);
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