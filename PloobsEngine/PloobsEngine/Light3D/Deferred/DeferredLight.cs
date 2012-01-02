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
using PloobsEngine.Entity;
using PloobsEngine.MessageSystem;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Light
{
    /// <summary>
    /// Deferred Light Specification
    /// </summary>
    public abstract class DeferredLight : ILight
    {
        public DeferredLight()
        {
            Enabled = true;
        }

        #region ILight Members


        /// <summary>
        /// Gets the type of the light.
        /// </summary>
        /// <value>
        /// The type of the light.
        /// </value>
        public abstract LightType LightType
        {
            get;
        }


        #endregion        

        protected Matrix viewMatrix;
        protected Matrix projMatrix;
        protected float _bias;


        /// <summary>
        /// Gets or sets the SHADOWBIAS.
        /// </summary>
        /// <value>
        /// The SHADOWBIAS.
        /// </value>
        public float SHADOWBIAS
        {
            get { return _bias; }
            set { _bias = value; }
        }

        protected Color color;

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                this.color = value;
            }
        }



        /// <summary>
        /// Gets or sets the view matrix.
        /// </summary>
        /// <value>
        /// The view matrix.
        /// </value>
        public virtual Matrix ViewMatrix
        {
            set
            {
                this.viewMatrix = value;
            }
            get
            {
                return this.viewMatrix;
            }
        }

        /// <summary>
        /// Gets or sets the proj matrix.
        /// </summary>
        /// <value>
        /// The proj matrix.
        /// </value>
        public virtual Matrix ProjMatrix
        {
            set
            {
                this.projMatrix = value;
            }
            get
            {
                return this.projMatrix;
            }
        }


        #region ILight Members


        /// <summary>
        /// Gets or sets the name of the light.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get;
            set;
        }

        #endregion

        #region ILight Members

        private bool castShadow = false;
        /// <summary>
        /// Gets or sets a value indicating whether [cast shadown].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [cast shadown]; otherwise, <c>false</c>.
        /// </value>
        public bool CastShadown
        {
            get
            {
                return castShadow;
            }
            set
            {
                this.castShadow = value;
            }
        }

        #endregion

        #region ISerializable Members
        #if !WINDOWS_PHONE
        public virtual void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            ActiveLogger.LogMessage("Serialization not implemented yet", LogLevel.RecoverableError);
        }
        #endif
        #endregion

        #region ILight Members


        public bool Enabled
        {
            get;
            set;
        }

        #endregion
    }
}
