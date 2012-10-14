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
using Microsoft.Xna.Framework.Graphics;

namespace PloobsEngine.Modelo2D
{
    /// <summary>
    /// Sprite keyframe animation information
    /// </summary>
    public class AnimationClass
    {
        internal AnimationClass(String Name, bool isLooping, float FramesPerSecond)
        {
            timeToUpdate = (1f / FramesPerSecond);
            this.Name = Name;
            IsLooping = isLooping;
        }

        public String Name
        {
            get;
            internal set;
        }

        internal float timeToUpdate = 0.05f;
        public float FramesPerSecond
        {            
            get { return 1f / timeToUpdate; }
            set
            {
                timeToUpdate = 1f/value;
            }
        }


        public Rectangle[] Rectangles
        {
            internal set;
            get;
        }
        /// <summary>
        /// Default = true
        /// </summary>
        public bool IsLooping
        {
            set;
            get;
        }

        /// <summary>
        /// Total Frames
        /// </summary>
        public int Frames
        {
            internal set;
            get;
        }
        /// <summary>
        /// Animation Rotation
        /// </summary>
        public float Rotation
        {
            set;
            get;
        }
    }
}
