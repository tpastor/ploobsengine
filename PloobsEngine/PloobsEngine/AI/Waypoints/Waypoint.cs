using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PloobsEngine.IA
{
    [Serializable]
    public class Waypoint
    {
        public Waypoint()
        {
        }

        private int id;
        private Vector3 worldPos;
        private WAYPOINTTYPE wayType;
        private List<int> neightBorWaypointsId;
        private float aditionalInfo;
        private IAPathfinderNode node;

        public IAPathfinderNode Node
        {
            get { return node; }
            set { node = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        

        public Vector3 WorldPos
        {
            get { return worldPos; }
            set { worldPos = value; }
        }
        

        public WAYPOINTTYPE WayType
        {
            get { return wayType; }
            set { wayType = value; }
        }
        

        public List<int> NeightBorWaypointsId
        {
            get { return neightBorWaypointsId; }
            set { neightBorWaypointsId = value; }
        }

        public float AditionalInfo
        {
            get { return aditionalInfo; }
            set { aditionalInfo = value; }
        }

    }
    /// <summary>
    /// EXEMPLOS DE TIPOS DE WAYPONIT
    /// </summary>
    [Serializable]
    public enum WAYPOINTTYPE
    {
        NORMAL, NOTWALKABLE, HARDTOWALK, SPECIAL_1, SPECIAL_2, SPECIAL_3, SPECIAL_4, SPECIAL_5
    }

}
