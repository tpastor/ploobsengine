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
    public class UserObject<T> : IObject 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IObject"/> class.
        /// </summary>
        /// <param name="Material">The material.</param>
        /// <param name="Modelo">The modelo.</param>
        /// <param name="PhysicObject">The physic object.</param>
        /// <param name="UserData">The user data.</param>
        public UserObject (IMaterial Material, IModelo Modelo, IPhysicObject PhysicObject, T UserData)
            : base(Material,Modelo,PhysicObject)
        {
            this.UserData = UserData;
        }
        public event Action<UserObject<T>> OnUserUpdate;

        protected override void UpdateObject(GameTime gt, ICamera cam, IWorld world)
        {
            if (OnUserUpdate != null)
                OnUserUpdate(this);
            base.UpdateObject(gt, cam, world);
        }

        /// <summary>
        /// Gets or sets the user data.
        /// </summary>
        /// <value>
        /// The user data.
        /// </value>
        public T UserData
        {
            get;
            set;
        }
    }
}
