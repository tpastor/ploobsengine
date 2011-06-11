using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Bnoerj.AI.Steering;
using PloobsEngine.SceneControl;

namespace Bnoerj.AI.Steering
{
    public class Path
    {
        public Path(List<Vector3> pathPoints, float pathRadius)
        {
            testPath = new PolylinePathway(pathPoints.Count,
                                                 pathPoints.ToArray(),
                                                 pathRadius,
                                                 false);
        }

        private PolylinePathway testPath = null;

        public PolylinePathway PolyPath
        {
            get { return testPath; }            
        }

        public void AddCircularObstacle(Vector3 center, float radius)
        {
            SphericalObstacle so = new SphericalObstacle(radius, center);
            obstacles.Add(so);
        }

        public void AddCircularObstacle(IObject obj)
        {
            BoundingSphere bs = BoundingSphere.CreateFromBoundingBox(obj.PhysicObject.BoundingBox);
            SphericalObstacle so = new SphericalObstacle(bs.Radius, obj.PhysicObject.Position);
            obstacles.Add(so);
        }

        private List<IObstacle> obstacles = new List<IObstacle>();

        public List<IObstacle> Obstacles
        {
            get { return obstacles; }            
        }

    }
}
