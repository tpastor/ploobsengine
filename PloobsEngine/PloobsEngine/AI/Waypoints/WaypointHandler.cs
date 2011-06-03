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
        string FileName = null;

        /// <summary>
        /// ReInicia a Instancia do handler
        /// </summary>
        /// <param name="MapName"></param>
        /// <param name="FileName"></param>
        public void Init(String MapName, String FileName)
        {
            col = new WaypointsCollection();
            col.MapName = MapName;
            this.FileName = FileName;
        }

        public WaypointHandler()
        {
            col = new WaypointsCollection();
            col.MapName = null;
        }

        public WaypointHandler(String mapName)
        {
            col = new WaypointsCollection();
            col.MapName = mapName;
        }


        private int getId()
        {
            return id++;
        }


        public void AddWaypoint(Vector3 worldPos,WAYPOINTTYPE type)
        {
            Waypoint w = new Waypoint();
            w.Id = getId();
            w.WorldPos = worldPos;
            w.WayType = type;
            w.NeightBorWaypointsId = null;
            col.IdWaypoint.Add(w.Id, w);
            col.State = WaypointsState.UnConnected;
        }

        public void RemoveWaypoint(int id)
        {
            col.IdWaypoint.Remove(id);
        }

        public void SaveUnconnectedWaypoints()
        {
            if (FileName == null)
            {
                throw new Exception("MapName cannot be null");
            }
            this.SaveUnconnectedWaypoints(FileName);
        }

        public void SaveConnectedWaypoints()
        {
            this.SaveConnectedWaypoints("_"+FileName );
        }

        public void SaveConnectedWaypoints(String fileName)
        {
            if (col.State != WaypointsState.Connected)
            {
                throw new Exception("Waypoints are already Connected");
            }
            else if (string.IsNullOrEmpty(col.MapName))
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
            else if (string.IsNullOrEmpty(col.MapName))
            {
                throw new Exception("MapName cannot be null or empty");
            }
            XmlContentLoader.SaveXmlContent(col, col.GetType(), fileName);
        }

        public void LoadUnconnectedWaypoints()
        {
            if (FileName == null)
            {
                throw new Exception("MapName cannot be null");
            }                        
            this.LoadUnconnectedWaypoints(FileName);
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

        }

        /// <summary>
        /// Carrega waypoints e os Desconecta (se estiverem conectados)
        /// </summary>
        /// <param name="FileName"></param>
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

        
        /// <summary>
        /// Carreaga diversos Wapoints Conectados e os Connecta entre si.
        /// </summary>
        /// <param name="waypoints"></param>
        /// <param name="connector"></param>
        public void LoadConnectedWaypoints(String[] waypoints , IWaypointConnector connector)
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

        /// <summary>
        /// Desconecta todos os Waypoints
        /// </summary>
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

        /// <summary>
        /// Conecta Waypoints Desconectados
        /// </summary>
        /// <param name="connector"></param>
        public void ConnectWaypoints(IWaypointConnector connector)
        {
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

        public WaypointsCollection GetWaypoints()
        {
            return col;
        }

        //public void DebugDrawWaypointsVector()
        //{
        //    foreach (Waypoint item in col.GetWaypointsList())
        //    {
        //        //Drawing.Draw3dLine(item.WorldPos, item.WorldPos + Vector3.Up * 30, Color.Yellow);                
        //        if (item.NeightBorWaypointsId != null)
        //        {
        //            foreach (int viz in item.NeightBorWaypointsId)
        //            {

        //                Vector3 dir = col.IdWaypoint[viz].WorldPos - item.WorldPos;                        
        //                //Drawing.Draw3dLine(item.WorldPos, item.WorldPos + dir, Color.Red);                        
        //            }
        //        }

        //    }
        //}

    }

    [Serializable]
    public enum WaypointsState
    {
        Connected,UnConnected,Empty
    }
}
