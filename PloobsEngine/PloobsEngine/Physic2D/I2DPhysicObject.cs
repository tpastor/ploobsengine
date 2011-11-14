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
using PloobsEngine.SceneControl._2DScene;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Physic2D
{
    public abstract class I2DPhysicObject
    {       
        Vector2 origin;

        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }
        

        public I2DObject Owner
        {
            internal set;
            get;
        }

        public virtual bool isDynamic
        {
            get;
            set;
        }

        public virtual Vector2 Position
        {
            set;
            get;
        }

        public virtual Vector2 LinearVelocity
        {
            set;
            get;
        }
        public virtual float AngularVelocity
        {
            set;
            get;

        }
        public virtual float Rotation
        {
            set;
            get;
        }

        public virtual bool Enabled
        {
            internal set;
            get;
        }
        
        private Vector2 pos;
        private float rot;
        internal bool HasMoved()
        {
            if (!isDynamic)
                return false;

            if (Position != pos || Rotation != rot)
            {
                pos = Position;
                rot = Rotation;
                return true;
            }
            return false;
        }
    
    }
}
