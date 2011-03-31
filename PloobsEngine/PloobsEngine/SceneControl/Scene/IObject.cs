using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.MessageSystem;
using PloobsEngine.Light;
using PloobsEngine.Material;
using PloobsEngine.Modelo;
using PloobsEngine.Physics;
using PloobsEngine.IA;
using PloobsEngine.Cameras;
using System.Runtime.Serialization;


namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Called when the object is being removed
    /// </summary>
    /// <param name="obj">The obj.</param>
    public delegate void BeingRemoved(IObject obj);
    /// <summary>
    /// Called on every update
    /// </summary>
    /// <param name="obj">The obj.</param>
    public delegate void OnUpdate(IObject obj);

    /// <summary>
    /// called when the object recieves a message
    /// </summary>
    /// <param name="Reciever">The reciever.</param>
    /// <param name="mes">The mes.</param>
    public delegate void OnRecieveMessage(IObject Reciever , Message mes);

    /// <summary>
    /// Called when the object moves
    /// LastPosition != actual position
    /// ROTATION AND SCALE ARE NOT CONSIDERED
    /// if you needto considere rotation, use the ONUPDATE event
    /// </summary>
    /// <param name="Reciever">The reciever.</param>
    public delegate void OnHasMoved(IObject Reciever);


    /// <summary>
    /// Object Specification
    /// </summary>
    public interface IObject : IRecieveMessageEntity, ISerializable
    {
        /// <summary>
        /// Occurs when [on recieve message].
        /// </summary>
        event OnRecieveMessage OnRecieveMessage;        
        /// <summary>
        /// Gets or sets the physic object.
        /// </summary>
        /// <value>
        /// The physic object.
        /// </value>
        IPhysicObject PhysicObject { set; get; }
        IModelo Modelo { set; get; }
        
        /// <summary>
        /// Gets or sets the material.
        /// </summary>
        /// <value>
        /// The material.
        /// </value>
        IMaterial Material { set; get; }

        /// <summary>
        /// Gets a value indicating whether this instance has changed its POSITION, 
        /// ROTATION and SCALE sont change this variavle
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has changed; otherwise, <c>false</c>.
        /// </value>
        bool hasChanged {get;}
        /// <summary>
        /// Pre drawn.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="gt">The gt.</param>
        /// <param name="cam">The cam.</param>
        /// <param name="lights">The lights.</param>
        /// <param name="render">The render.</param>
        void PreDrawn(IWorld world, GameTime gt, ICamera cam, IList<ILight> lights, IRenderHelper render);
        /// <summary>
        /// Drawns .
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="cam">The cam.</param>
        /// <param name="lights">The lights.</param>
        /// <param name="render">The render.</param>
        void Drawn(GameTime gt ,ICamera cam ,IList<ILight> lights,IRenderHelper render);
        /// <summary>
        /// Post Drawn.
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="cam">The cam.</param>
        /// <param name="lights">The lights.</param>
        /// <param name="render">The render.</param>
        void PosDrawn(GameTime gt, ICamera cam, IList<ILight> lights, IRenderHelper render);
        /// <summary>
        /// Updates the object.
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="cam">The cam.</param>
        /// <param name="luzes">The luzes.</param>
        void UpdateObject(GameTime gt, ICamera cam, IList<ILight> luzes);
        /// <summary>
        /// Gets the world matrix.
        /// </summary>
        /// <returns></returns>
        Matrix getWorldMatrix();
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        Vector3 Position { set; get; }
        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        /// <value>
        /// The rotation.
        /// </value>
        Matrix Rotation { set; get; }
        /// <summary>
        /// Gets or sets the scale.
        /// </summary>
        /// <value>
        /// The scale.
        /// </value>
        Vector3 Scale { set; get; }
        /// <summary>
        /// Gets or sets the name of the object.
        /// The IWorld use this name in his GetNameEntities queries
        /// Can be NULL, so it is not added to the NameEntities (You cant make queries, just this)
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        String Name { set; get; }
        /// <summary>
        /// Adds an atachment.
        /// </summary>
        /// <param name="obj">The obj.</param>
        void AddAtachment(IObjectAtachtment obj);
        /// <summary>
        /// Removes the atachment.
        /// </summary>
        /// <param name="obj">The obj.</param>
        void RemoveAtachment(IObjectAtachtment obj);
        /// <summary>
        /// Called when this object is removed from the world
        /// </summary>
        event BeingRemoved OnBeingRemoved;

        /// <summary>
        /// Raised when objects moves
        /// </summary>
        event OnHasMoved OnHasMoved;
        /// <summary>
        /// Called all the time the object is updated
        /// </summary>
        event OnUpdate OnUpdate;
        /// <summary>
        /// IWorld will remove this object as soon as he can
        /// </summary>
        bool ToBeRemoved { set; get; }
        /// <summary>
        /// Gets or sets the agente.
        /// If the object dont have an agent null is returned
        /// </summary>
        /// <value>
        /// The agente.
        /// </value>
        IAgent Agente
        {
            set;
            get;
        }
        /// <summary>
        /// calculated automaticaly by the IWorld
        /// </summary>
        float CameraDistance
        {
            get;
            set;
        }
        
        
    }
}
