using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Xml;
using PloobsEngine.Particle3D;
using PloobsEngine.Light;
using PloobsEngine.Cameras;
using PloobsEngine.Trigger;
using PloobsEngine.Physics;
using PloobsEngine.Audio;
using System.Runtime.Serialization;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Specification of a world
    /// </summary>
    public interface IWorld : ISerializable
    {
        /// <summary>
        /// Gets the particle manager instance.
        /// </summary>
        ParticleManager ParticleManager
        {
            get;            
        }
        /// <summary>
        /// Adds an object to the world.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        int  AddObject(IObject obj);
        /// <summary>
        /// Removes an object from the world.
        /// </summary>
        /// <param name="obj">The obj.</param>
        void RemoveObject(IObject obj);
        /// <summary>
        /// Updates the world.
        /// </summary>
        /// <param name="gt">The gt.</param>
        void UpdateWorld(GameTime gt);
        /// <summary>
        /// Drawns the world.
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="render">The render.</param>
        void DrawnWorld(GameTime gt ,IRenderHelper render);
        /// <summary>
        /// Contains the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        bool Contain(IObject obj);
        /// <summary>
        /// Contains the named entity.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        bool ContainNamedEntity(String name);
        /// <summary>
        /// Adds the light.
        /// </summary>
        /// <param name="light">The light.</param>
        /// <returns></returns>
        int AddLight(ILight light);
        /// <summary>
        /// Removes the light.
        /// </summary>
        /// <param name="light">The light.</param>
        void RemoveLight(ILight light);
        /// <summary>
        /// Camera Managment
        /// </summary>
        CameraManager CameraManager { get; }
        /// <summary>
        /// Adds the trigger.
        /// </summary>
        /// <param name="trigger">The trigger.</param>
        void AddTrigger(ITrigger trigger);
        /// <summary>
        /// Removes the trigger.
        /// </summary>
        /// <param name="trigger">The trigger.</param>
        void RemoveTrigger(ITrigger trigger);
        /// <summary>
        /// Gets or sets the physic world.
        /// </summary>
        /// <value>
        /// The physic world.
        /// </value>
        IPhysicWorld PhysicWorld { set; get; }        

        /// <summary>
        /// Get a collection of entities by name
        /// Find all Entity that CONTAINS the NAME
        /// Entity is EVERYTHING added to the world that has a name
        /// Light,Objects ,dummies...
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Name">The name.</param>
        /// <returns></returns>
        List<T> GetWorldGroupEntityByName<T>(String Name);

        /// <summary>
        /// Gets the world entity by name
        /// Entity is EVERYTHING added to the world that has a name
        /// Light,Objects, dummies ...
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Name">The name.</param>
        /// <returns></returns>
        T GetWorldEntityByName<T>(String Name);

        /// <summary>
        /// Changes the name of an entity.
        /// Just the world copy of the name will be changed, the true object name wont
        /// Entity is EVERYTHING added to the world that has a name
        /// Light,Objects ,dummies...
        /// </summary>
        /// <param name="oldName">The old name.</param>
        /// <param name="newName">The new name.</param>
        void ChangeEntityName(String oldName, String newName);

        /// <summary>
        /// Add a Dummy to the world
        /// Its like a position, 
        /// usefull to serializable position from a world editor
        /// </summary>
        /// <param name="dummy">The dummy.</param>
        void AddDummy(IDummy dummy);
        /// <summary>
        /// Removes the dummy.
        /// </summary>
        /// <param name="dummy">The dummy.</param>
        void RemoveDummy(IDummy dummy);
        /// <summary>
        /// Gets all the dummyes.
        /// </summary>
        /// <returns></returns>
        IList<IDummy> GetDummyes();
        
        /// <summary>
        /// Gets or sets the culler.
        /// </summary>
        /// <value>
        /// The culler.
        /// </value>
        ICuller Culler
        {
            set;
            get;
        }

        /// <summary>
        /// Gets the active camera.
        /// </summary>
        ICamera ActiveCamera
        {
            get;
        }

        /// <summary>
        /// Ray test
        /// </summary>
        /// <param name="raio">Ray do Xna</param>
        /// <param name="method">considerar ou nao objetos imoveis</param>
        /// <param name="maxDistance">0 para qualquer distancia</param>
        /// <returns></returns>
        RayTestInfo SegmentIntersect(Ray raio, SegmentInterceptMethod method, float maxDistance);
        /// <summary>
        /// Gets the lights.
        /// </summary>
        IList<ILight> Lights
        {
            get;
        }
        /// <summary>
        /// Gets the objects.
        /// </summary>
        IList<IObject> Objects
        {
            get;
        }
        
        /// <summary>
        /// Adds the sound emitter.
        /// </summary>
        /// <param name="em">The em.</param>
        /// Sound
        void AddSoundEmitter(ISoundEmitter3D em);        
        /// <summary>
        /// Removes the sound emitter.
        /// </summary>
        /// <param name="e">The e.</param>
        void RemoveSoundEmitter(ISoundEmitter3D e);
    }
}
