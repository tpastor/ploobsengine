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
using PloobsEngine.Cameras;
using System.Runtime.Serialization;
using PloobsEngine.Engine.Logger;


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
    /// <param name="gt">The gt.</param>
    /// <param name="cam">The cam.</param>
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
    /// Basic Object 
    /// </summary>
    #if !WINDOWS_PHONE
    public class IObject : IRecieveMessageEntity, ISerializable
#else
    public class IObject : IRecieveMessageEntity
#endif
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IObject"/> class.
        /// </summary>
        /// <param name="Material">The material.</param>
        /// <param name="Modelo">The modelo.</param>
        /// <param name="PhysicObject">The physic object.</param>
        public IObject(IMaterial Material, IModelo Modelo, IPhysicObject PhysicObject)
        {
            System.Diagnostics.Debug.Assert(Modelo != null);
            System.Diagnostics.Debug.Assert(Material != null);
            System.Diagnostics.Debug.Assert(PhysicObject != null);            

            this.Material = Material;
            this.Modelo = Modelo;
            this.PhysicObject = PhysicObject;            
            IObjectAttachment = new List<IObjectAttachment>();
            Name = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IObject"/> class.
        /// Let the user free to implement the IObject the way they want
        /// </summary>
        protected IObject()
        {
        }

        /// <summary>
        /// Last Frame Matrix
        /// </summary>
        protected Matrix lastFrameWorld;
        private IPhysicObject physicObject;
        private IModelo modelo;
        private IMaterial material;
        /// <summary>
        /// World Matrix
        /// </summary>
        protected Matrix worldMatrix;

        private object materialLock = new object();        
             

        /// <summary>
        /// Occurs when [on recieve message].
        /// </summary>
        public event OnRecieveMessage OnRecieveMessage = null;

        /// <summary>
        /// Called when this object is removed from the world
        /// </summary>
        public event BeingRemoved OnBeingRemoved = null;

        public event Action<IObject, IWorld> OnBeingAdd = null;

        /// <summary>
        /// Raised when objects moves
        /// </summary>
        public event OnHasMoved OnHasMoved = null;
        /// <summary>
        /// Called all the time the object is updated
        /// </summary>
        public event OnUpdate OnUpdate = null;        
        
        internal void FireOnBeingAdd(IWorld world)
        {
            if(OnBeingAdd!=null)
                OnBeingAdd(this,world);
        }

        /// <summary>
        /// Gets or sets the physic object.
        /// </summary>
        /// <value>
        /// The physic object.
        /// </value>
        public IPhysicObject PhysicObject 
        {
            set
            {
                this.physicObject = value;
            }
            get
            {
                return physicObject;
            }
        }

        /// <summary>
        /// Gets or sets the modelo.
        /// </summary>
        /// <value>
        /// The modelo.
        /// </value>
        public IModelo Modelo 
        {
            set
            {
                this.modelo = value;
            }
            get
            {
                return modelo;
            }
        }
        
        /// <summary>
        /// Gets or sets the material.
        /// </summary>
        /// <value>
        /// The material.
        /// </value>
        public IMaterial Material 
        {
            set
            {
                lock (materialLock) 
                    this.material = value;
            }
            get
            {
                lock (materialLock) 
                    return material;
            }
        }        
        
        /// <summary>
        /// Updates the object.
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="cam">The cam.</param>
        /// <param name="luzes">The luzes.</param>
        protected virtual void UpdateObject(GameTime gt, ICamera cam, IWorld world) { }
        internal void iUpdateObject(GameTime gt, ICamera cam, IWorld world)
        {

            lock (materialLock)
            {
                if (material != null)
                    material.Update(gt, this, world);
            }
            
            worldMatrix = physicObject.WorldMatrix;

            if (lastFrameWorld != WorldMatrix)
            {
                if(OnHasMoved!=null)
                    OnHasMoved(this);

                lastFrameWorld = WorldMatrix;
            }

                UpdateObject(gt, cam, world);

                foreach (var item in IObjectAttachment)
                {
                    item.IUpdate(this, gt);
                }

                if (OnUpdate != null)
                    OnUpdate(this, gt, cam);
            
        }       


        /// <summary>
        /// Gets the world matrix.
        /// </summary>
        public virtual Matrix WorldMatrix {
            get
            {
                return worldMatrix;
            }            
        }        
        /// <summary>
        /// Gets or sets the name of the object.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public String Name { set; get; }

        /// <summary>
        /// Gets or sets the Iobject atachtment.
        /// </summary>
        /// <value>
        /// The Iobject atachtment.
        /// </value>
        public List<IObjectAttachment> IObjectAttachment
        {
            set;
            get;

        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public virtual void CleanUp(PloobsEngine.Engine.GraphicFactory factory)
        {
            Modelo.CleanUp(factory);
            Material.CleanUp(factory);
            Modelo = null;
            Material = null;
            PhysicObject = null;
            IObjectAttachment.Clear();
            IObjectAttachment = null;
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
        private long id;
        /// <summary>
        /// return the entity id
        /// </summary>
        /// <returns>
        /// the id
        /// </returns>
        public long GetId()
        {
            return id;
        }

        /// <summary>
        /// sets the id
        /// </summary>
        /// <param name="id"></param>
        public void SetId(long id)
        {
            this.id = id;
        }

        #endregion

        #region ISerializable Members
        #if !WINDOWS_PHONE
        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext"/>) for this serialization.</param>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ActiveLogger.LogMessage("Serialization is not implemented yet", LogLevel.RecoverableError);
        }
        #endif
        #endregion
    }
}
