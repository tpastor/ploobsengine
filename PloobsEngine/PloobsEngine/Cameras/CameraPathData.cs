using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Utils;

namespace PloobsEngine.Cameras
{
    [Serializable]
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
