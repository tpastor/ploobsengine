using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Components;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Features
{
    public delegate void FpsEvent(float fps);

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

        protected override void AfterDraw(SceneControl.RenderHelper render, GameTime gt, Matrix activeView, Matrix activeProjection)
        {
            base.AfterDraw(render, gt, activeView, activeProjection);
        
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
