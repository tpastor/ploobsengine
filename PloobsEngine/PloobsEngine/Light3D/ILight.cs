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
using PloobsEngine.SceneControl;
using System.Runtime.Serialization;

namespace PloobsEngine.Light
{
    /// <summary>
    /// Specification of a Light
    /// </summary>
#if WINDOWS
    public interface ILight : ISerializable
#else
    public interface ILight 
    #endif
    {
        /// <summary>
        /// Gets or sets a value indicating whether [cast shadown].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [cast shadown]; otherwise, <c>false</c>.
        /// </value>
        bool CastShadown
        {
            set;
            get;
        }        
        /// <summary>
        /// Gets or sets the name of the light.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        String Name
        {
            set;
            get;
        }

        bool Enabled
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the type of the light.
        /// </summary>
        /// <value>
        /// The type of the light.
        /// </value>
        LightType LightType
        {
            get;
        }
    }

    /// <summary>
    /// Light Types
    /// </summary>
    public enum LightType
    {
        /// <summary>
        /// Deferred Directional Light
        /// </summary>
        Deferred_Directional,
        /// <summary>
        /// Deferred Point Light 
        /// </summary>
        Deferred_Point,
        /// <summary>
        /// Deferred Spot Light
        /// </summary>
        Deferred_Spot,
        /// <summary>
        /// None of these
        /// </summary>
        OTHER
    }
}
