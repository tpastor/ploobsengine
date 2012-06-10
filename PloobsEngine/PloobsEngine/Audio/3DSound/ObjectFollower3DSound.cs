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
using Microsoft.Xna.Framework.Audio;
using PloobsEngine.SceneControl;
using PloobsEngine.Engine;

namespace PloobsEngine.Audio
{
    /// <summary>
    /// 3D sound that follows a 3D object
    /// </summary>
    public class ObjectFollower3DSound : ISoundEmitter3D
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectFollower3DSound"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="audioname">The audioname.</param>
        /// <param name="follow">The follow.</param>
        public ObjectFollower3DSound(GraphicFactory factory, string audioname, IObject follow)
            : base(factory, audioname)
        {
            System.Diagnostics.Debug.Assert(follow != null);
            this.follower = follow;
        }

        IObject follower;

        /// <summary>
        /// Updates !!!
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="camera">The camera.</param>
        protected override void Update(Microsoft.Xna.Framework.GameTime gt, Cameras.ICamera camera)
        {
            Emiter.Position = follower.PhysicObject.Position;
            Emiter.Velocity = follower.PhysicObject.Velocity;
            Emiter.Forward = follower.PhysicObject.FaceVector;
            
            Listener.Position = camera.Position;
            Listener.Up = camera.Up;            

            SoundEngineInstance.Apply3D(Listener, Emiter);   
        }
    }
}
