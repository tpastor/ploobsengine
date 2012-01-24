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
            BoundingSphere bs = BoundingSphere.CreateFromBoundingBox(obj.PhysicObject.BoundingBox.Value);
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
