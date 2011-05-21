using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.IA
{
    public interface IWaypointConnector
    {
        WaypointsCollection ConnectWaypoints(WaypointsCollection col);
        ConnectorType ConnectorType { get; }
    }
    public enum ConnectorType
    { 
        BETWEEN_COLLECTIONS_CONNECTEC , BETWEEN_WAYPOINTS_UNCONNECTED
    }
}
