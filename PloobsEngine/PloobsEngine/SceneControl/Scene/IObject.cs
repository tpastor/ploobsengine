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
    public delegate void OnUpdate(IObject obj,GameTime gt, ICamera cam);

    /// <summary>
    /// called when the object recieves a message
    /// </summary>
    /// <param name="Reciever">The reciever.</param>
    /// <param name="mes">The mes.</param>
    public delegate void OnRecieveMessage(IObject Reciever , Message mes);

    /// <summary>
    /// Called when the object moves
    /// </summary>
    /// <param name="Reciever">The reciever.</param>
    public delegate void OnHasMoved(IObject Reciever);

    /// <summary>
    /// Object Specification
    /// </summary>
    public abstract class IObject : IRecieveMessageEntity, ISerializable
    {
        public IObject(IMaterial Material, IModelo Modelo, IPhysicObject PhysicObject)
        {
            this.Material = Material;
            this.Modelo = Modelo;
            this.PhysicObject = PhysicObject;
            Scale = Vector3.One;
            Rotation = Matrix.Identity;
            Position = Vector3.Zero;
            WorldMatrix = Matrix.Identity;            
            Agente = null;
            Name = null;
        }

        private Matrix lastFrameWorld;

        /// <summary>
        /// Occurs when [on recieve message].
        /// </summary>
        public event OnRecieveMessage OnRecieveMessage = null;

        /// <summary>
        /// Called when this object is removed from the world
        /// </summary>
        public event BeingRemoved OnBeingRemoved = null;

        /// <summary>
        /// Raised when objects moves
        /// </summary>
        public event OnHasMoved OnHasMoved = null;
        /// <summary>
        /// Called all the time the object is updated
        /// </summary>
        public event OnUpdate OnUpdate = null;        
        
        /// <summary>
        /// Gets or sets the physic object.
        /// </summary>
        /// <value>
        /// The physic object.
        /// </value>
        public abstract IPhysicObject PhysicObject { set; get; }

        /// <summary>
        /// Gets or sets the modelo.
        /// </summary>
        /// <value>
        /// The modelo.
        /// </value>
        public abstract IModelo Modelo { set; get; }
        
        /// <summary>
        /// Gets or sets the material.
        /// </summary>
        /// <value>
        /// The material.
        /// </value>
        public abstract IMaterial Material { set; get; }
        
        /// <summary>
        /// Pre drawn.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="gt">The gt.</param>
        /// <param name="cam">The cam.</param>
        /// <param name="lights">The lights.</param>
        /// <param name="render">The render.</param>
        protected abstract void PreDrawn(IWorld world, GameTime gt, ICamera cam, IList<ILight> lights, IRenderHelper render);

        internal void iPreDrawn(IWorld world, GameTime gt, ICamera cam, IList<ILight> lights, IRenderHelper render)
        {            
            PreDrawn(world, gt, cam, lights, render);
        }


        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="cam">The cam.</param>
        /// <param name="lights">The lights.</param>
        /// <param name="render">The render.</param>
        protected abstract void Drawn(GameTime gt ,ICamera cam ,IList<ILight> lights,IRenderHelper render);
        internal void iDrawn(GameTime gt, ICamera cam, IList<ILight> lights, IRenderHelper render)
        {
            Drawn(gt, cam, lights, render);
        }

        /// <summary>
        /// Post Drawn.
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="cam">The cam.</param>
        /// <param name="lights">The lights.</param>
        /// <param name="render">The render.</param>        
        protected abstract void PosDrawn(GameTime gt, ICamera cam, IList<ILight> lights, IRenderHelper render);
        internal void iPosDrawn(GameTime gt, ICamera cam, IList<ILight> lights, IRenderHelper render)
        {
            PosDrawn(gt, cam, lights, render);
        }

        /// <summary>
        /// Updates the object.
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="cam">The cam.</param>
        /// <param name="luzes">The luzes.</param>
        protected abstract void UpdateObject(GameTime gt, ICamera cam, IList<ILight> luzes);
        internal void iUpdateObject(GameTime gt, ICamera cam, IList<ILight> luzes)
        {
            UpdateObject(gt, cam, luzes);

            if (lastFrameWorld != WorldMatrix)
            {
                if(OnHasMoved!=null)
                    OnHasMoved(this);

                lastFrameWorld = WorldMatrix;
            }

            if (OnUpdate != null)
                OnUpdate(this, gt,cam);
        }       


        /// <summary>
        /// Gets the world matrix.
        /// </summary>
        public virtual Matrix WorldMatrix { get; protected set; }
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public virtual Vector3 Position { set; get; }
        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        /// <value>
        /// The rotation.
        /// </value>
        public virtual Matrix Rotation { set; get; }
        /// <summary>
        /// Gets or sets the scale.
        /// </summary>
        /// <value>
        /// The scale.
        /// </value>
        public virtual Vector3 Scale { set; get; }
        /// <summary>
        /// Gets or sets the name of the object.
        /// The IWorld use this name in his GetNameEntities queries
        /// Can be NULL, so it is not added to the NameEntities (You cant make queries, just this)
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public String Name { set; get; }

        /// <summary>
        /// Adds an atachment.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public abstract void AddAtachment(IObjectAtachtment obj);
        /// <summary>
        /// Removes the atachment.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public abstract void RemoveAtachment(IObjectAtachtment obj);
        
        /// <summary>
        /// Gets or sets the agente.
        /// If the object dont have an agent null is returned
        /// </summary>
        /// <value>
        /// The agente.
        /// </value>
        public IAgent Agente
        {
            set;
            get;
        }

        #region IRecieveMessageEntity Members

        /// <summary>
        /// Handles a message from determined sender type.
        /// </summary>
        /// <param name="type">Sender type.</param>
        /// <returns></returns>
        public virtual bool HandleThisMessageType(SenderType type)
        {
            return true;        
        }

        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="mes">The mes.</param>
        public virtual void HandleMessage(Message mes)
        {
            if (OnRecieveMessage != null)
                OnRecieveMessage(this, mes);
        }

        /// <summary>
        /// Removes this object.
        /// Called internally
        /// </summary>
        internal void RemoveThisObject()
        {
            if (OnBeingRemoved != null)
                OnBeingRemoved(this);
        }

        #endregion

        #region IEntity Members
        private int id;
        /// <summary>
        /// return the entity id
        /// </summary>
        /// <returns>
        /// the id
        /// </returns>
        public int getId()
        {
            return id;
        }

        /// <summary>
        /// sets the id
        /// </summary>
        /// <param name="id"></param>
        public void setId(int id)
        {
            this.id = id;
        }

        #endregion

        #region ISerializable Members

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext"/>) for this serialization.</param>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        public abstract void GetObjectData(SerializationInfo info, StreamingContext context);
        
        #endregion
    }
}
