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
