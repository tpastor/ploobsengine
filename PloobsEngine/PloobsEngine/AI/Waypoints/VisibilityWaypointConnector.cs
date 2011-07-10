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
using PloobsEngine.SceneControl;
using PloobsEngine.Physics;


namespace PloobsEngine.IA
{
    public class VisibilityWaypointConnector : IWaypointConnector
    {
        private IWorld world;

        private float altura = 15;

        public float Altura
        {
            get { return altura; }
            set { altura = value; }
        }
        private float maxDistance = 200;

        public float MaxDistance
        {
            get { return maxDistance; }
            set { maxDistance = value; }
        }

        public VisibilityWaypointConnector(IWorld world)
        {
            this.world = world;
        }

        #region IWaypointConnector Members

        public WaypointsCollection ConnectWaypoints(WaypointsCollection col)
        {
            WaypointsCollection resp = new WaypointsCollection(col.MapName);
            foreach (Waypoint item in col.GetWaypointsList())
            {
                item.NeightBorWaypointsId = new List<int>();
                foreach (Waypoint item2 in col.GetWaypointsList())
                {
                    if (item.Id != item2.Id)
                    {
                        Vector3 dir = item2.WorldPos - item.WorldPos;
                        if (dir.Length() < maxDistance)
                        {
                            float dist = dir.Length() * 1.2f;
                            dir.Normalize();
                            Ray raio = new Ray(item.WorldPos + new Vector3(0, altura, 0), dir);
                            SegmentInterceptInfo ri = world.PhysicWorld.SegmentIntersect(raio, (a) => true, dist);
                            if (ri != null)
                            {
                                continue;
                            }
                            else
                            {
                                
                                item.NeightBorWaypointsId.Add(item2.Id);
                            }
                        }
                    }
                }
            }
            resp.State = WaypointsState.Connected;
            resp.IdWaypoint = col.IdWaypoint;
            return resp;
        }

        public ConnectorType ConnectorType
        {
            get { return ConnectorType.BETWEEN_WAYPOINTS_UNCONNECTED; }
        }

        #endregion
    }
}
