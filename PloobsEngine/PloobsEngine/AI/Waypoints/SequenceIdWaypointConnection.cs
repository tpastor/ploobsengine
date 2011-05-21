using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.IA
{
    public class SequenceIdWaypointConnection : IWaypointConnector
    {
        #region IWaypointConnector Members

        public WaypointsCollection ConnectWaypoints(WaypointsCollection col)
        {
            int maxid = int.MinValue;
            int minid=int.MaxValue;
            foreach (int item in  col.IdWaypoint.Keys)
	        {
        		 if(item < minid)
                     minid = item;
                if(item>maxid)
                    maxid = item;
	        }

            WaypointsCollection resp = new WaypointsCollection(col.MapName);
            foreach (Waypoint item in col.GetWaypointsList())
            {
                item.NeightBorWaypointsId = new List<int>();
                if (item.Id == maxid)
                {
                    item.NeightBorWaypointsId.Add(minid);
                }
                else
                {                    
                    item.NeightBorWaypointsId.Add(item.Id + 1);
                }
                resp.IdWaypoint.Add(item.Id, item);
            }
            return resp;
        }

        #endregion

        #region IWaypointConnector Members


        public ConnectorType ConnectorType
        {
            get { return ConnectorType.BETWEEN_WAYPOINTS_UNCONNECTED; }
        }

        #endregion
    }
}

