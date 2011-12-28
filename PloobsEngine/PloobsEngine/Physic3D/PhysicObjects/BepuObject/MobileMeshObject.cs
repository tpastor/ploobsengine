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
using System.Collections.Generic;
using BEPUphysics.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Modelo;
using PloobsEngine.SceneControl;
using BEPUphysics.Collidables;
using BEPUphysics.MathExtensions;
using PloobsEngine.Engine.Logger;
using System;
using BEPUphysics.Entities.Prefabs;
using BEPUphysics.CollisionShapes;

namespace PloobsEngine.Physics.Bepu
{
    /// <summary>
    /// Dynamic Mesh Physic Object
    /// </summary>
    public class MobileMeshObject : IPhysicObject
    {
        MobileMesh triangleGroup;
        IObject owner;                

        /// <summary>
        /// Gets or sets the Mobile mesh.
        /// </summary>
        /// <value>
        /// The MObile mesh.
        /// </value>
        public MobileMesh MobileMesh 
        {
            get { return triangleGroup; }
            set { triangleGroup = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MobileMeshObject "/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="pos">The pos.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="materialDescription">The material description.</param>
        /// <param name="MobileMeshSolidity">The mobile mesh solidity.</param>
        /// <param name="mass">The mass.</param>
        public MobileMeshObject(IModelo model, Vector3 pos, Matrix rotation, Vector3 scale, MaterialDescription materialDescription,MobileMeshSolidity MobileMeshSolidity,float mass)
        {
            System.Diagnostics.Debug.Assert(model != null);
            System.Diagnostics.Debug.Assert(scale != Vector3.Zero);
            
            this.scale = scale;            
            Vector3[] vertices = null;
            int[] indices = null;
            ExtractData(ref vertices, ref indices, model);
            triangleGroup = new MobileMesh(vertices, indices, new AffineTransform(scale, Quaternion.CreateFromRotationMatrix(rotation), pos), MobileMeshSolidity,mass);        
            triangleGroup.Material = new BEPUphysics.Materials.Material(materialDescription.StaticFriction, materialDescription.DinamicFriction, materialDescription.Bounciness);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MobileMeshObject "/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="pos">The pos.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="materialDescription">The material description.</param>
        /// <param name="MobileMeshSolidity">The mobile mesh solidity.</param>
        /// <param name="mass">The mass.</param>
        public MobileMeshObject(Model model, Vector3 pos, Matrix rotation, Vector3 scale, MaterialDescription materialDescription, MobileMeshSolidity MobileMeshSolidity,float mass)
        {
            System.Diagnostics.Debug.Assert(model != null);
            System.Diagnostics.Debug.Assert(scale != Vector3.Zero);
                        
            this.scale = scale;            
            Vector3[] vertices;
            int[] indices;
            TriangleMesh.GetVerticesAndIndicesFromModel(model, out vertices, out indices);
            triangleGroup = new MobileMesh(vertices, indices, new AffineTransform(scale, Quaternion.CreateFromRotationMatrix(rotation), pos), MobileMeshSolidity, mass);            
            triangleGroup.Material = new BEPUphysics.Materials.Material(materialDescription.StaticFriction,materialDescription.DinamicFriction,materialDescription.Bounciness);
        }


        /// <summary>
        /// Helper to get the vertex and index List from the model.
        /// </summary>
        /// <param name="vert">The vert.</param>
        /// <param name="ind">The ind.</param>
        /// <param name="model">The model.</param>
        private void ExtractData(ref Vector3[] vert, ref int[] ind, IModelo model)
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
            get { return PhysicObjectTypes.MobilePhysicObject; }
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
                return triangleGroup.WorldTransform.Translation;
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
                ActiveLogger.LogMessage("scale in mobile mesh should not be called", LogLevel.Warning);
            }
        }
                
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
                return Matrix.CreateFromQuaternion(triangleGroup.Orientation);
            }
            set
            {
                triangleGroup.Orientation = Quaternion.CreateFromRotationMatrix(value);
            }
        }
                
        /// <summary>
        /// Vector pointing to the front
        /// </summary>
        public override Vector3 FaceVector
        {
            get { return triangleGroup.WorldTransform.Forward; }
        }

        /// <summary>
        /// Gets the world matrix.
        /// </summary>
        public override Matrix WorldMatrix
        {
            get { return Matrix.CreateScale(scale) * triangleGroup.WorldTransform; }
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
                ActiveLogger.LogMessage("isMotionLess in triangle mesh should not be called",LogLevel.Warning);
            }
        }

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
        public override BoundingBox BoundingBox
        {
            get { return triangleGroup.CollisionInformation.BoundingBox; }
        }

        #if !WINDOWS_PHONE
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
