using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Engine.IA
{
    [Serializable]
    public struct Waypoint
    {
        public int Id;
        public Vector3 gridPOs;
        public WAYPOINTTYPE waytype;
        public Vector3 WordPos;        
        public List<int> Vizinhos;        

    }
    /// <summary>
    /// EXEMPLOS DE TIPOS DE WAYPONIT
    /// </summary>
    public enum WAYPOINTTYPE
    {
        NORMAL, NOTWALKABLE, HARDTOWALK
    }

}
