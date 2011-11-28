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

