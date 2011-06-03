#if !WINDOWS_PHONE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using BEPUphysics;
using Microsoft.Xna.Framework;
using PloobsEngine.Physics;
using PloobsEngine.SceneControl;
using BEPUphysics.Collidables;
using PloobsEngine.Engine.Logger;
using PloobsEngine.Engine;

namespace PloobsEngine.Physics.Bepu
{
    public class TerrainObject : IPhysicObject    
    {
        String image;
        Terrain terrain;        
        float heightMultipler = 10f;
        float maxHeight = float.MaxValue;        
        Matrix rotation;

        /// <summary>
        /// Gets the height of the max.
        /// </summary>
        /// <value>
        /// The height of the max.
        /// </value>
        public float MaxHeight
        {
            get { return maxHeight; }            
        }
        float minHeight = float.MinValue;

        /// <summary>
        /// Gets the height of the min.
        /// </summary>
        /// <value>
        /// The height of the min.
        /// </value>
        public float MinHeight
        {
            get { return minHeight; }            
        }

        /// <summary>
        /// Gets the terrain.
        /// </summary>
        public Terrain Terrain
        {
            get { return terrain; }
        }

        Texture2D sourceImage;


        /// <summary>
        /// Gets the height map.
        /// </summary>
        public Texture2D HeightMap
        {
            get { return sourceImage; }
        }

        /// <summary>
        /// Sets the material description.
        /// </summary>
        /// <param name="materialDescription">The material description.</param>
        public void SetMaterialDescription(MaterialDescription materialDescription)
        {
            this.materialDecription = materialDescription;
            terrain.Material = new BEPUphysics.Materials.Material(materialDescription.StaticFriction, materialDescription.DinamicFriction, materialDescription.Bounciness);
        }

        /// <summary>
        /// Gets the material description.
        /// </summary>
        /// <returns></returns>
        public MaterialDescription GetMaterialDescription()
        {
            return materialDecription;
        }

        MaterialDescription materialDecription;

        /// <summary>
        /// Create a Terrain Physic Object
        /// </summary>
        /// <param name="gfactory">The gfactory.</param>
        /// <param name="heighmapName">Name of the heighmap texture</param>
        /// <param name="translation">The translation.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="XSpacing">The X spacing.</param>
        /// <param name="ZSpacing">The Z spacing.</param>
        /// <param name="heightMultipler">Default 10 - controla a altura, menor mais alto</param>
        public TerrainObject(GraphicFactory gfactory, String heighmapName, Vector3 translation, Matrix rotation, MaterialDescription materialDesc, float XSpacing = 1, float ZSpacing = 1, float heightMultipler = 10)
        {            
            

            this.heightMultipler = heightMultipler;
            this.image = heighmapName;

            sourceImage = gfactory.GetTexture2D(image);
            int xLength = sourceImage.Width;
            int yLength = sourceImage.Height;
            this.rotation = rotation;
            //this.scale = scale;

            Color[] colorData = new Color[xLength * yLength];
            sourceImage.GetData<Color>(colorData);            

            var heights = new float[xLength, yLength];
            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    Color color = colorData[j * xLength + i];                    
                    heights[i, j] = (color.R) / heightMultipler;

                    if (heights[i, j] > maxHeight)
                    {
                        maxHeight = heights[i, j];
                    }
                    if (heights[i, j] < minHeight)
                    {
                        minHeight = heights[i, j];
                    }

                }
            }
            //Create the terrain.
            BEPUphysics.CollisionShapes.TerrainShape shape = new BEPUphysics.CollisionShapes.TerrainShape(heights,BEPUphysics.CollisionShapes.QuadTriangleOrganization.BottomLeftUpperRight);
            
            terrain = new Terrain(shape, new BEPUphysics.MathExtensions.AffineTransform(new Vector3(XSpacing, 1, ZSpacing),Quaternion.CreateFromRotationMatrix(rotation), new Vector3(-xLength * XSpacing / 2, 0, -yLength * ZSpacing / 2) + translation));
            terrain.ImproveBoundaryBehavior = true;

            SetMaterialDescription(materialDesc);
                        
        }

        #region IPhysicObject Members

        public override  Vector3 Position
        {
            get
            {
                return terrain.WorldTransform.Translation;
            }
            set
            {
                ActiveLogger.LogMessage("Cant Set Terrain Position", LogLevel.RecoverableError);
            }
        }

        public override Vector3 Scale
        {
            get
            {
                return Vector3.One;
            }
            set
            {
                ActiveLogger.LogMessage("Cant Set Terrain Scale", LogLevel.RecoverableError);
            }
        }

        public override  Matrix Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                ActiveLogger.LogMessage("Cant Set Terrain Rotation", LogLevel.RecoverableError);
            }
        }

        public override  Vector3 FaceVector
        {
            get {               
                
                return Vector3.TransformNormal(Vector3.Forward,terrain.WorldTransform.Matrix);
            }
        }

        public override Matrix WorldMatrix
        {
            get { return Matrix.Identity; }
        }



        public override Vector3 AngularVelocity
        {
            get { return Vector3.Zero; }
            set
            {
                ActiveLogger.LogMessage("Cant Set Terrain Velocity", LogLevel.RecoverableError);
            }
        }
       
        public override Vector3 Velocity
        {
            get
            {
                return Vector3.Zero;
            }
            set
            {
                ActiveLogger.LogMessage("Cant Set Terrain Velocity", LogLevel.RecoverableError);
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
                ActiveLogger.LogMessage("Cant Set Terrain Motion property", LogLevel.RecoverableError);
            }
        }

        IObject owner;
        public override  IObject ObjectOwner
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

        public override  PhysicObjectTypes PhysicObjectTypes
        {
            get { return PhysicObjectTypes.TERRAIN; }
        }

        public override void Enable()
        {            
            ActiveLogger.LogMessage("Terrain is always enabled", LogLevel.RecoverableError);
        }

        public override void Disable()
        {
            ActiveLogger.LogMessage("Terrain is always enabled", LogLevel.RecoverableError);
        }

        public override  BoundingBox BoundingBox
        {
            get { return terrain.BoundingBox; }
        }

        #endregion

        public override void ApplyImpulse(Vector3 position, Vector3 force)
        {
            ActiveLogger.LogMessage("Cant apply force in terrain", LogLevel.RecoverableError);
        }


        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            ActiveLogger.LogMessage("Serialization not implemented yet", LogLevel.RecoverableError);
        }
    }
}
#endif