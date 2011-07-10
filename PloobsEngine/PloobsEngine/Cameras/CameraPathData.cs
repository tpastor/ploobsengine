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
using PloobsEngine.Utils;
using System.Xml.Serialization;

namespace PloobsEngine.Cameras
{
#if !WINDOWS_PHONE
    [Serializable]
#endif
    public class CameraPathData
    {
        public CameraPathData(CurveLoopType CurveLoopType)
        {
            heads = new Curve3D(CurveLoopType);
            targets = new Curve3D(CurveLoopType);
            ups = new Curve3D(CurveLoopType);
        }
        public CameraPathData()
        {
            heads = new Curve3D();
            targets = new Curve3D();
            ups = new Curve3D();
        }

        private double maxTime = 0;
        private Curve3D heads;
        private Curve3D targets;
        private Curve3D ups ;

        
        public void FinishWithCicle(float Spacetime)
        {
            float time = (float)maxTime + Spacetime;
            heads.AddPoint(heads.GetPointOnCurve(0), time);
            targets.AddPoint(targets.GetPointOnCurve(0), time);
            ups.AddPoint(ups.GetPointOnCurve(0), time);
            maxTime = time;
        }

        public void AddPoint(Microsoft.Xna.Framework.Vector3 pos, Microsoft.Xna.Framework.Vector3 target, Microsoft.Xna.Framework.Vector3 up, double time)
        {
            if (time > maxTime)
                maxTime = time;
            heads.AddPoint(pos, (float)time);
            targets.AddPoint(target, (float)time);
            ups.AddPoint(up, (float)time);
        }
        public void SetTangents()
        {
            heads.SetTangents();
            targets.SetTangents();
            ups.SetTangents();
        }

        public Microsoft.Xna.Framework.Vector3 GetHead(float time)
        {
            return heads.GetPointOnCurve(time);
        }
        public Microsoft.Xna.Framework.Vector3 GetTarget(float time)
        {
            return targets.GetPointOnCurve(time);
        }
        public Microsoft.Xna.Framework.Vector3 GetUp(float time)
        {
            return ups.GetPointOnCurve(time);
        }
        public float getMaxTime()
        {
            return (float)maxTime;
        }

    }

}
