using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PloobsEngine.IA
{
    public class SimpleMap : IMap
    {
        public SimpleMap(WaypointsCollection col)
        {          
            this.ways = col;
        }

        
        private WaypointsCollection ways;

        #region IMap Members

        public Waypoint GetClosestWaypoint(Microsoft.Xna.Framework.Vector3 Position)
        {
            List<Waypoint> list = ways.GetWaypointsList();
            Waypoint w = list[0];
            float  dist = Vector3.DistanceSquared(w.WorldPos,Position);
            for (int i = 1; i < list.Count; i++)
			{
                float d = Vector3.DistanceSquared(list[i].WorldPos, Position);
                if (d < dist)
                {
                    w = list[i];
                    dist = d;
                }
			}
            return w;
        }


        public WaypointsCollection Waypoints
        {
            get
            {
                return ways;
            }
            set
            {
                this.ways = value;
            }
        }

        #endregion
    }
}
