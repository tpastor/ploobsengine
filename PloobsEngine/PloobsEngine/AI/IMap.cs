using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PloobsEngine.IA
{
    public interface IMap
    {
        Waypoint GetClosestWaypoint(Vector3 Position);
        
        /// <summary>
        /// Waypoints do Mapa
        /// </summary>
        WaypointsCollection Waypoints
        {
            get;
            set;
        }

    }
}
