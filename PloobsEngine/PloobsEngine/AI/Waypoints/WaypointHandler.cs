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
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using PloobsEngine.DataStructure;
using PloobsEngine.Utils;

namespace PloobsEngine.IA
{
    public class WaypointHandler
    {
        static int id = 0;
        WaypointsCollection col;       

        /// <summary>
        /// ReInicia a Instancia do handler
        /// </summary>
        public void Clear()
        {
            col = new WaypointsCollection();        
        }

        public WaypointHandler(String mapName = null)
        {
            col = new WaypointsCollection();
            col.MapName = mapName;
        }        

        private int getId()
        {
            return id++;
        }

        public void AddDefaultWaypointUnconnected(Vector3 worldPos,WAYPOINTTYPE type)
        {
            Waypoint w = new Waypoint();
            w.Id = getId();
            w.WorldPos = worldPos;
            w.WayType = type;
            w.NeightBorWaypointsId = null;
            col.IdWaypoint.Add(w.Id, w);
            col.State = WaypointsState.UnConnected;
        }

        public void AddWaypointUnconnected(Waypoint waypoint)
        {
            if (waypoint == null)
                throw new NullReferenceException("waypoint cannot be null");
            waypoint.Id = getId();
            waypoint.NeightBorWaypointsId = null;
            col.IdWaypoint.Add(waypoint.Id, waypoint);
            col.State = WaypointsState.UnConnected;
        }

        public void RemoveWaypoint(int id)
        {
            col.IdWaypoint.Remove(id);
        }        
        
        public void SaveConnectedWaypoints(String fileName)
        {
            if (col.State != WaypointsState.Connected)
            {
                throw new Exception("Waypoints are already Connected");
            }
            else if (string.IsNullOrEmpty(fileName))
            {
                throw new Exception("MapName cannot be null or empty");
            }
            XmlContentLoader.SaveXmlContent(col, col.GetType(), fileName);
        }

        public void SaveUnconnectedWaypoints(String fileName)
        {
            if (col.State == WaypointsState.Connected)
            {
                throw new Exception("Waypoints are already Connected");
            }
            else if (string.IsNullOrEmpty(fileName))
            {
                throw new Exception("MapName cannot be null or empty");
            }

            XmlContentLoader.SaveXmlContent(col, col.GetType(), fileName);
        }


        public void LoadConnectedWaypoints(String FileName)        
        {
            if (FileName == null)
            {
                throw new Exception("MapName cannot be null");
            }            
            
            this.col = XmlContentLoader.LoadXmlContent(FileName, col.GetType()) as WaypointsCollection;
            foreach (Waypoint item in col.GetWaypointsList())
            {             
                if (item.Id > id)
                {
                    id = item.Id + 1;
                }
            }
            col.State = WaypointsState.Connected;

        }

        /// <summary>
        /// Loads the unconnected waypoints.
        /// If it is connected, it will be unconnected
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        public void LoadUnconnectedWaypoints(String FileName)
        {
            if (FileName == null)
            {
                throw new Exception("MapName cannot be null");
            }            
            this.col =  XmlContentLoader.LoadXmlContent(FileName,col.GetType()) as WaypointsCollection;
            if(col.State == WaypointsState.Connected)
            Unconnect();

        }        
        
        public void LoadConnectedWaypointsIslands(String[] waypoints , IWaypointConnector connector)
        {
            if (waypoints==null || waypoints.Length == 0 )
            {
                throw new Exception("MapName cannot be null");
            }
            else if (connector.ConnectorType != ConnectorType.BETWEEN_COLLECTIONS_CONNECTEC)
            {
                throw new Exception("Wrong Type of Connector");
            }
            int toadd = 0;
            foreach (var item in waypoints)
            {
                WaypointsCollection w = XmlContentLoader.LoadXmlContent(item, col.GetType()) as WaypointsCollection;
                foreach (Waypoint way in w.IdWaypoint.Values)
                {
                    way.Id = way.Id + toadd;
                    way.NeightBorWaypointsId =  way.NeightBorWaypointsId.Select( (p1 , p2) => p1 + toadd).ToList<int>();                                                            
                }

                IDictionary<int,Waypoint> xx = w.IdWaypoint.ToDictionary(t => t.Key + toadd, u => u.Value);
                w.IdWaypoint = new SerializableDictionary<int, Waypoint>(xx);                   
                toadd+= w.IdWaypoint.Keys.Max();
                this.col.IdWaypoint.Concate(w.IdWaypoint);
            }

            this.col = connector.ConnectWaypoints(col);

        }

        public void Unconnect()
        {
            col.State = WaypointsState.UnConnected;
            id = 0;
            foreach (Waypoint item in col.GetWaypointsList())
            {
                item.NeightBorWaypointsId = null;
                if (item.Id > id)
                {
                    id = item.Id + 1;
                }
            }
        }
        
        public void ConnectWaypoints(IWaypointConnector connector)
        {
            if (connector == null)
                throw new NullReferenceException("connector cannot be null");

            if (connector.ConnectorType != ConnectorType.BETWEEN_WAYPOINTS_UNCONNECTED)
            {
                throw new Exception("Wrong Type of Connector");
            }

            if (col.State == WaypointsState.UnConnected)
            {
                col = connector.ConnectWaypoints(col);
                col.State = WaypointsState.Connected;
            }            
        }

        public WaypointsCollection CurrentWaypointsCollection
        {
            get
            {
                return col;
            }
        }
    }

    #if !WINDOWS_PHONE
    [Serializable]
    #endif

    public enum WaypointsState
    {
        Connected,UnConnected,Empty
    }
}
