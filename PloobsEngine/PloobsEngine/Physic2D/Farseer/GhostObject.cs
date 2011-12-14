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
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Physic2D.Farseer
{
    public class GhostObject : I2DPhysicObject
    {
        public GhostObject(Vector2 Position, float rotation = 0)
        {
            this.Enabled = false;
            this.Position = Position;
            this.Rotation = rotation;
            this.Origin = Vector2.Zero;
        }

        public override Physic2DType Physic2DType
        {
            get { return Physic2D.Physic2DType.Ghost; }
        }

        public override bool isDynamic
        {
            get
            {
                return false;
            }
            set
            {
                if(value == true)
                    ActiveLogger.LogMessage("ghost is never dynamic", LogLevel.RecoverableError);
            }
        }

        public override void ApplyForce(Vector2 force, Vector2? point = null)
        {
            ActiveLogger.LogMessage("Cant apply force on ghost obj", LogLevel.RecoverableError);
        }
    }
}
