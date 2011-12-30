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
    /// <summary>
    /// SImple implementation of a map
    /// Brutal force one. Do not scale for HUGE number of waypoints
    /// </summary>
    public class SimpleMap : IMap
    {
        public SimpleMap(WaypointsCollection col)
        {          
            this.ways = col;
        }

        
        private WaypointsCollection ways;

        #region IMap Members

        /// <summary>
        /// Gets the closest waypoint to a position
        /// </summary>
        /// <param name="Position">The position.</param>
        /// <returns></returns>
        public Waypoint GetClosestWaypoint(Microsoft.Xna.Framework.Vector3 Position)
        {
            List<Waypoint> list = ways.GetWaypointsList();
            Waypoint w = list[0];
            float  dist = Vector3.DistanceSquared(w.WorldPos,Position);
            for (int i = 1; i < list.Count; i++)
			{
                float d = Vector3.DistanceSquared(list[i].WorldPos, Position);
                if (d < dist)
                {
                    w = list[i];
                    dist = d;
                }
			}
            return w;
        }


        public WaypointsCollection Waypoints
        {
            get
            {
                return ways;
            }
            set
            {
                this.ways = value;
            }
        }

        #endregion
    }
}
