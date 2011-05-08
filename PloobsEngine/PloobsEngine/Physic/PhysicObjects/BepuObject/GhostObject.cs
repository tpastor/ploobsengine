using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Xml;
using PloobsEngine.Utils;
using PloobsEngine.Modelo;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Physics.Bepu
{    

    /// <summary>
    /// Fake Physic object, not simulated
    /// </summary>
    public class GhostObject : BepuEntityObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GhostObject"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="orientation">The orientation.</param>
        /// <param name="scale">The scale.</param>
     public GhostObject(Vector3 position, Matrix orientation, Vector3 scale) : base(MaterialDescription.DefaultBepuMaterial(), 0)
     {
         this.pos = position;
         this.ori = orientation;
         this.scale = scale;         
     }

     /// <summary>
     /// Initializes a new instance of the <see cref="GhostObject"/> class.
     /// DEfault Object in 0,0,0 identity rotation and 1,1,1 scale
     /// </summary>
     public GhostObject()
         : base(MaterialDescription.DefaultBepuMaterial(), 0)
     {
         this.pos = Vector3.Zero;
         this.ori = Matrix.Identity;
         this.scale = Vector3.One;
     }

     bool internalMatrix = false;
     private Vector3 pos;
     private Matrix ori;
     private Matrix internalWorld;
     private BoundingBox bb = new BoundingBox();

     public override Vector3 Position
     {
         get
         {
             return pos;
         }
         set
         {
             this.pos = value;
         }
     }
     public override Matrix Rotation
     {
         get
         {
             return ori;
         }
         set
         {
             this.ori = value;
         }
     }
     public override BoundingBox BoundingBox
     {
         get
         {
             return bb;
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
             if (value == false)
             {
                 throw new Exception("Eh sempre motion less");
             }
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
             throw new Exception("Sempre imovel");
         }
     }

     public void setInternalWorldMatrix(Matrix mat)
     {
             internalMatrix = true;
             internalWorld = mat;
     }

     public override Matrix WorldMatrix
     {
         get
         {
             if (internalMatrix)
             {
                 return internalWorld;
             }
             else
             {
                 return Matrix.CreateScale(Scale) * Rotation * Matrix.CreateTranslation(Position);
             }
         }         

     }

     public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
     {
         ActiveLogger.LogMessage("Serialization not implemented yet", LogLevel.RecoverableError);
     }
        
        public override PhysicObjectTypes PhysicObjectTypes
        {
            get { return PhysicObjectTypes.GHOST; }
        }    
    }
}
