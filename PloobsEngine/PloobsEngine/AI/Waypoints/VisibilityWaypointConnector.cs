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
