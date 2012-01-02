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
using Microsoft.Xna.Framework;
using PloobsEngine.Engine;

namespace PloobsEngine.Audio
{
    /// <summary>
    /// Static 2D sound
    /// </summary>
    public class Static2DSound : ISoundEmitter2D
    {
        public Static2DSound(GraphicFactory cmanager, string audioname, Vector2 Position)
            : base(cmanager,audioname)
        {
            EmiterPosition = Position;
        }

        public Vector2 EmiterPosition
        {
            get
            {
                return new Vector2(emiter.Position.X,emiter.Position.Y);
            }
            set
            {
                emiter.Position = new Vector3(value, 0) ;
            }
        }

 
        protected override void Update(GameTime gt, SceneControl._2DScene.ICamera2D camera)
        {
            Listener.Position = new Vector3(camera.Position,0);
            SoundEngineInstance.Apply3D(Listener, Emiter);   
        } 

    }
}
