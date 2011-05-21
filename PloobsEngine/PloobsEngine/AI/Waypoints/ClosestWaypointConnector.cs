using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.SceneControl;
using PloobsEngine.Physics;


namespace PloobsEngine.IA
{
    public class ClosestWaypointConnector : IWaypointConnector
    {
        private IWorld world;

        private float altura = 15;

        public float Altura
        {
            get { return altura; }
            set { altura = value; }
        }
        private float maxDistance = 50;

        public float MaxDistance
        {
            get { return maxDistance; }
            set { maxDistance = value; }
        }

        public ClosestWaypointConnector(IWorld world)
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
            }

            foreach (Waypoint item in col.GetWaypointsList())
            {             
                float dist = float.MaxValue;
                float dist2 = float.MaxValue;
                Waypoint candidate = null;
                Waypoint candidate2 = null;
                foreach (Waypoint item2 in col.GetWaypointsList())
                {
                    Vector3 dir = item2.WorldPos - item.WorldPos;
                    dir.Normalize();
                    Ray raio = new Ray(item.WorldPos + new Vector3(0, altura, 0), dir);
                    SegmentInterceptInfo ri = world.PhysicWorld.SegmentIntersect(raio, (a) => true, maxDistance);
                    if (ri != null)
                    {
                        continue;
                    }
                    else
                    {
                        float distance = Vector3.Distance(item.WorldPos, item2.WorldPos);
                        if (distance < dist && item.Id!= item2.Id )
                        {
                            if (item2.NeightBorWaypointsId.Count > 0)
                            {
                                if (item2.NeightBorWaypointsId[0] != item.Id)
                                {
                                    dist = distance;
                                    candidate = item2;
                                }
                            }
                            else
                            {
                                dist = distance;
                                candidate = item2;
                            }
                        }
                        else if (distance < dist2 && dist2 > dist && item.Id != item2.Id )
                        {
                            if (item2.NeightBorWaypointsId.Count > 0)
                            {
                                if (item2.NeightBorWaypointsId[0] != item.Id)
                                {
                                    dist2 = distance;
                                    candidate2 = item2;
                                }
                            }
                            else
                            {
                                dist2 = distance;
                                candidate2 = item2;
                            }
                            
                        }
                    }
                }
                if (candidate != null)
                {
                    item.NeightBorWaypointsId.Add(candidate.Id);
                }
                else
                {
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
