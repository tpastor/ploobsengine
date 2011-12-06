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
using PloobsEngine.MessageSystem;
using PloobsEngine.Physic2D;
using Microsoft.Xna.Framework;
using PloobsEngine.Material2D;
using PloobsEngine.Modelo2D;

namespace PloobsEngine.SceneControl._2DScene
{
    /// <summary>
    /// Called when the object is being removed
    /// </summary>
    /// <param name="obj">The obj.</param>
    public delegate void BeingRemoved(I2DObject obj);

    /// <summary>
    /// Called on every update
    /// </summary>
    /// <param name="obj">The obj.</param>
    public delegate void OnUpdate(I2DObject obj, GameTime gt);

    /// <summary>
    /// called when the object recieves a message
    /// </summary>
    /// <param name="Reciever">The reciever.</param>
    /// <param name="mes">The mes.</param>
    public delegate void OnRecieveMessage(I2DObject Reciever, Message mes);

    /// <summary>
    /// Called when the object moves
    /// </summary>
    /// <param name="Reciever">The reciever.</param>
    public delegate void OnHasMoved(I2DObject Reciever);


    public class I2DObject : IRecieveMessageEntity
    {
        public I2DObject(I2DPhysicObject physicObject, I2DMaterial material,IModelo2D Modelo)
        {
            this.Material = material;
            this.PhysicObject = physicObject;
            this.Modelo = Modelo;
            I2DObjectAtachtment = new List<IObject2DAtachtment>();
        }

        I2DPhysicObject physicObject;

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
        public I2DPhysicObject PhysicObject
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

        public I2DMaterial Material
        {
            get;
            set;
        }
        public IModelo2D Modelo
        {
            set;
            get;
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
        /// The I object atachtment.
        /// </value>
        public List<IObject2DAtachtment> I2DObjectAtachtment
        {
            set;
            get;

        }

        /// <summary>
        /// Updates the object.
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="cam">The cam.</param>
        /// <param name="luzes">The luzes.</param>
        protected virtual void UpdateObject(GameTime gt) { }
        internal void iUpdateObject(GameTime gt)
        {
            UpdateObject(gt);

            this.Material.Update(gt, this);
            this.Modelo.Update(gt);
            
            if (physicObject.HasMoved())
            {
                if (OnHasMoved != null)
                    OnHasMoved(this);                
            }

            foreach (var item in I2DObjectAtachtment)
            {
                item.IUpdate(this, gt);
            }

            if (OnUpdate != null)
                OnUpdate(this, gt);
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
        public int GetId()
        {
            return id;
        }

        /// <summary>
        /// sets the id
        /// </summary>
        /// <param name="id"></param>
        public void SetId(int id)
        {
            this.id = id;
        }

        #endregion


    }
}
