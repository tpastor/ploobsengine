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

namespace PloobsEngine.IA
{
    #if !WINDOWS_PHONE
    [Serializable]
#endif
    public class Waypoint
    {
        public Waypoint()
        {
        }

        private int id;
        private Vector3 worldPos;
        private WAYPOINTTYPE wayType;
        private List<int> neightBorWaypointsId;
        private object aditionalInfo;
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

        public object AditionalInfo
        {
            get { return aditionalInfo; }
            set { aditionalInfo = value; }
        }

    }
    /// <summary>
    /// EXEMPLOS DE TIPOS DE WAYPONIT
    /// </summary>
    #if !WINDOWS_PHONE
    [Serializable]
    #endif
    public enum WAYPOINTTYPE
    {
        NORMAL, NOTWALKABLE, HARDTOWALK, SPECIAL_1, SPECIAL_2, SPECIAL_3, SPECIAL_4, SPECIAL_5
    }

}
