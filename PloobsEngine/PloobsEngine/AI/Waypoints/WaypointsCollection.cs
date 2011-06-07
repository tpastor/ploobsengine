using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using PloobsEngine.DataStructure;

namespace PloobsEngine.IA
{
    [XmlRoot("WayPointCollection")]
    public class WaypointsCollection
    {

        public WaypointsCollection()
            : this("")
            {
            }
        public WaypointsCollection(String mapName)
        {
            idWaypoint = new SerializableDictionary<int, Waypoint>();
            state = WaypointsState.Empty;
            this.mapName = mapName;
        }

        private String mapName;                
        private SerializableDictionary<int, Waypoint> idWaypoint;        
        private WaypointsState state;
        
        public List<Waypoint> GetWaypointsList()
        {
            return idWaypoint.Values.ToList<Waypoint>();
        }

        public List<Vector3> GetWorldWayPointsList()
        {
            List<Vector3> resp = new List<Vector3>();
            foreach (Waypoint way in idWaypoint.Values.ToList<Waypoint>())
            {
                resp.Add(way.WorldPos);
            }
            return resp;
        }


        public SerializableDictionary<int, Waypoint> IdWaypoint
        {
            get { return idWaypoint; }
            set { idWaypoint = value; }
        }

        public String MapName
        {
            get { return mapName; }
            set { mapName = value; }
        }

        public WaypointsState State
        {
            get { return state; }
            set { state = value; }
        }


    }
}
