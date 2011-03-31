using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Cameras;
using PloobsEngine.SceneControl;
using System.Runtime.Serialization;

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
        bool isDebugDraw { get; set; }

        /// <summary>
        /// Gets the physic objects.
        /// </summary>
        List<IPhysicObject> PhysicObjects { get; }

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
        void AddObject(IPhysicObject obj);

        /// <summary>
        /// Removes the object.
        /// </summary>
        /// <param name="obj">The obj.</param>
        void RemoveObject(IPhysicObject obj);        
        
        /// <summary>
        /// Raycast
        /// </summary>
        /// <param name="raio">The raio.</param>
        /// <param name="method">The method.</param>
        /// <param name="maxDistance">The max distance.</param>
        /// <returns></returns>
        SegmentInterceptInfo SegmentIntersect(Ray raio,SegmentInterceptMethod method,float maxDistance);

        /// <summary>
        /// Detects the collisions of a physic object
        /// </summary>
        /// <param name="po">The po.</param>
        /// <returns></returns>
        List<IPhysicObject> DetectCollisions(IPhysicObject po);

        /// <summary>
        /// Get the objects near the object passed as parameter
        /// </summary>
        /// <param name="po">The po.</param>
        /// <param name="distance">The distance.</param>
        /// <param name="CullerAvaliator">The culler avaliator.</param>
        /// <returns></returns>
        List<IPhysicObject> GetPhysicsObjectsInRange(IPhysicObject po, float distance,CullerConditionAvaliator<IPhysicObject, IObject> CullerAvaliator);
        
    }

    /// <summary>
    /// RayCAst interceptor method
    /// its a simple filter
    /// </summary>
    public enum SegmentInterceptMethod
    {
        /// <summary>
        /// intercept will all objects
        /// </summary>
        ALL,
        /// <summary>
        /// Dont intercept with triangle meshes
        /// </summary>
        NO_TRINAGLEMESH,
        /// <summary>
        /// Intercepts only with mobiles objects
        /// </summary>
        ONLY_MOBILES,
        /// <summary>
        /// Only with not mobiles objects
        /// </summary>
        ONLY_STOPPEDS,
        /// <summary>
        /// Only with triangle mesh
        /// </summary>
        ONLY_TRIANGLEMESH
    }
}
