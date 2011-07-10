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
#if !WINDOWS_PHONE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Utils;
using Microsoft.Xna.Framework;
using PloobsEngine.SceneControl;

namespace PloobsEngine.Cameras
{
    public class CameraRecordPath : IScreenUpdateable
    {
        private ICamera cam;
        private CameraPathData data;
        private double time = 0;
        private float spaceBetweenPointsInMillisenconds = 1000;
        private bool finishInTheStartPosition = false;

        public bool FinishInTheStartPosition
        {
            get { return finishInTheStartPosition; }
            set { finishInTheStartPosition = value; }
        }

        public float SpaceBetweenPointsInMillisenconds
        {
            get { return spaceBetweenPointsInMillisenconds; }
            set { spaceBetweenPointsInMillisenconds = value; }
        }



        public CameraRecordPath(IScreen screen, ICamera cam)
            : base(screen)
        {
            this.cam = cam;            
        }
        
        /// <summary>
        /// CameraRecordPath
        /// </summary>
        /// <param name="CurveLoopType">Attenuation in the path curves</param>
        public CameraRecordPath(IScreen screen, ICamera cam, CurveLoopType CurveLoopType)
            : base(screen)
        {
            this.cam = cam;
            data = new CameraPathData(CurveLoopType);
        }



        public void StartRecord()
        {
            this.Start();
            data = new CameraPathData();
        }

        public void StopRecord()
        {            
            this.Stop();            
        }


        public void SaveCurveToFile(string name)
        {
            if (FinishInTheStartPosition)
                data.FinishWithCicle(SpaceBetweenPointsInMillisenconds);
            data.SetTangents();                        
            BinaryContentLoader.SaveBinaryContent(data, typeof(CameraPathData), name);            
        }

        public CameraPathData LoadCurveFile(string name)
        {
            return (CameraPathData) BinaryContentLoader.LoadBynaryContent(name, typeof(CameraPathData));
        }               

        protected override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {            
            time += gameTime.ElapsedGameTime.TotalMilliseconds;
            data.AddPoint(cam.Position, cam.Target,cam.Up, time);
         }

        public void SavePoint()
        {
            data.AddPoint(cam.Position, cam.Target, cam.Up, time);
            time += SpaceBetweenPointsInMillisenconds;
        }
    }
    
}
#endif