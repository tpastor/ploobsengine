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
using PloobsEngine.Components;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Features
{
    public delegate void FpsEvent(float fps);

    /// <summary>
    /// Component to count fps
    /// </summary>
    public class FPSCounter : IComponent
    {
        private float _TotalTime = 0f, _TotalTimeX = 0f,
        _Fps = 0f,
        _FpsX = 0f;
        private  float _DisplayFPS;
        private float _UpdateFPS;

        public event FpsEvent DrawFps = null;
        public event FpsEvent UpdateFps = null;
        public event FpsEvent CombinedFps = null;
        
        public override ComponentType ComponentType
        {
            get { return ComponentType.POS_DRAWABLE_AND_UPDATEABLE; }
        }

        public static readonly String MyName = "FPSCounter";
        public override string getMyName()
        {
            return MyName;
        }

        protected override void AfterDraw(SceneControl.RenderHelper render, GameTime gt, ref Matrix activeView, ref Matrix activeProjection)
        {
            base.AfterDraw(render, gt, ref activeView, ref  activeProjection);
        
            _TotalTime += (float)gt.ElapsedGameTime.TotalSeconds;
            _Fps += 1;

            if (_TotalTime >= 1.0f)
            {
                _TotalTime = _TotalTime - (float)(_TotalTime);
                _DisplayFPS = _Fps;
                _Fps = 0;                
            }          
            
        }

        protected override void  Update(GameTime gt)
        {
 	        base.Update(gt);

            _TotalTimeX += (float)gt.ElapsedGameTime.TotalSeconds;
            _FpsX += 1;

            if (_TotalTimeX >= 1.0f)
            {
                _TotalTimeX = _TotalTimeX - (float)(_TotalTimeX);
                _UpdateFPS = _FpsX;
                _FpsX = 0;
            }

            if (DrawFps != null)
                DrawFps(_DisplayFPS);
            if (UpdateFps != null)
                UpdateFps(_UpdateFPS);
            if (CombinedFps != null)
                CombinedFps(_DisplayFPS + _UpdateFPS);            
            
        }
        
    }
}
