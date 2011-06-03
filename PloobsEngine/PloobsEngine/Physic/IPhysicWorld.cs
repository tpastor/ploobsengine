using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Cameras;
using PloobsEngine.SceneControl;
using System.Runtime.Serialization;
using PloobsEngine.Physic.Constraints;

namespace PloobsEngine.Physics
{
    public delegate bool CullerConditionAvaliator<T, V>(T param1, V param2);

    /// <summary>
    /// Physic Object Specification
    /// </summary>
    public abstract class IPhysicWorld : ISerializable
    {        
        /// <summary>
        /// Gets or sets a value indicating whether this instance is debug draw.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is debug draw; otherwise, <c>false</c>.
        /// </value>
        public bool isDebugDraw { get; set; }

        /// <summary>
        /// Gets the physic objects.
        /// </summary>
        public abstract List<IPhysicObject> PhysicObjects { get; }

        /// <summary>
        /// Gets the physic constraints.
        /// </summary>
        public abstract List<IPhysicConstraint> PhysicConstraints { get; }

        /// <summary>
        /// Draw the physic world in debug mode.
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="cam">The cam.</param>
        protected abstract void DebugDrawn(GameTime gt, ICamera cam);
        internal void iDebugDrawn(GameTime gt, ICamera cam)
        {
            DebugDrawn(gt,cam);
        }


        /// <summary>
        /// Updates 
        /// </summary>
        /// <param name="gt">The gt.</param>
        protected abstract void Update(GameTime gt);
        internal void iUpdate(GameTime gt)
        {
            Update(gt);
        }

        /// <summary>
        /// Adds the object.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public abstract void AddObject(IPhysicObject obj);

        /// <summary>
        /// Removes the object.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public abstract void RemoveObject(IPhysicObject obj);


        /// <summary>
        ///  Adds the constraint
        /// </summary>
        /// <param name="ctn"></param>
        public abstract void AddConstraint(IPhysicConstraint ctn);

        /// <summary>
        /// Removes the constraints
        /// </summary>
        /// <param name="ctn"></param>
        public abstract void RemoveConstraint(IPhysicConstraint ctn);



        
        /// <summary>
        /// Raycast
        /// </summary>
        /// <param name="raio">The raio.</param>
        /// <param name="method">The method.</param>
        /// <param name="maxDistance">The max distance.</param>
        /// <returns></returns>
        public abstract SegmentInterceptInfo SegmentIntersect(Ray raio, Func<IPhysicObject,bool> filter, float maxDistance);

        /// <summary>
        /// Detects the collisions of a physic object
        /// </summary>
        /// <param name="po">The po.</param>
        /// <param name="resp">The resp.</param>
        public abstract void DetectCollisions(IPhysicObject po,List<IPhysicObject> resp);

        /// <summary>
        /// Get the objects near the object passed as parameter
        /// </summary>
        /// <param name="po">The po.</param>
        /// <param name="distance">The distance.</param>
        /// <param name="CullerAvaliator">The culler avaliator.</param>
        /// <param name="resp">The resp.</param>
        public abstract void GetPhysicsObjectsInRange(IPhysicObject po, float distance, CullerConditionAvaliator<IPhysicObject, IObject> CullerAvaliator,List<IPhysicObject> resp);


        #region ISerializable Members

        public abstract void GetObjectData(SerializationInfo info, StreamingContext context);
        
        #endregion
    }
    
}
