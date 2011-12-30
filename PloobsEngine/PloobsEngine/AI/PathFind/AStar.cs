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
using PloobsEngine.DataStructure;

namespace PloobsEngine.IA
{
    public delegate float CostToCross<T>(T type);

    /// <summary>
    /// Ploobs Astar Pathfinding implementation
    /// </summary>
    public class AStar 
    {
        private PriorityQueueB<Waypoint> openQueue;
        private List<Waypoint> closeList;
        private Waypoint parentNode;
        private IMap map;
        private float heuristicEstimateValue = 1;

        /// <summary>
        /// Delegate called to evaluate the cost to cross a determined waypoint type
        /// </summary>
        public CostToCross<WAYPOINTTYPE> CostToCross = null;
        private bool ended = false;

        /// <summary>
        /// Gets a value indicating whether Astar <see cref="AStar"/> is ended.
        /// </summary>
        /// <value>
        ///   <c>true</c> if ended; otherwise, <c>false</c>.
        /// </value>
        public bool Ended
        {
            get { return ended; }            
        }


        /// <summary>
        /// Gets or sets the heuristic estimate value.
        /// </summary>
        /// <value>
        /// The heuristic estimate value.
        /// </value>
        public float HeuristicEstimateValue
        {
            get { return heuristicEstimateValue; }
            set { heuristicEstimateValue = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AStar"/> class.
        /// </summary>
        /// <param name="map">The map.</param>
        public AStar(IMap map)
        {            
            this.map = map;
            foreach (Waypoint way in map.Waypoints.GetWaypointsList())
            {
                way.Node = new IAPathfinderNode();
            }
            openQueue = new PriorityQueueB<Waypoint>( new IANodeComparer());
            closeList = new List<Waypoint>();
        }

        #region IPathFinder Members

        /// <summary>
        /// Costs to cross implementation. (used if no delegate costtowalk is provided)
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        protected float CostToCrossImplementation(WAYPOINTTYPE type)
        {
            switch (type)
            {
                case WAYPOINTTYPE.NORMAL:
                    return 10;
                case WAYPOINTTYPE.NOTWALKABLE:
                    return 100000;
                case WAYPOINTTYPE.HARDTOWALK:
                    return 50;
                default:
                    return 10;
            }
        }

        /// <summary>
        /// Compute and return the path
        /// </summary>
        /// <param name="Start">The start.</param>
        /// <param name="End">The end.</param>
        /// <param name="iterations">The iterations.</param>
        /// <returns></returns>
        public LinkedList<Waypoint> GetPath(Vector3 Start, Vector3 End, float iterations = float.MaxValue)
        {
            return GetPath(map.GetClosestWaypoint(Start), map.GetClosestWaypoint(End), iterations);
        }

        /// <summary>
        /// Compute and return the path
        /// </summary>
        /// <param name="Start">The start.</param>
        /// <param name="End">The end.</param>
        /// <param name="iterations">The iterations.</param>
        /// <returns></returns>
        public LinkedList<Waypoint> GetPath(Waypoint Start, Waypoint End,float iterations = float.MaxValue)
        {

            parentNode = Start;

            parentNode.Node.parentId = parentNode.Id;
            parentNode.Node.G = 0;
            parentNode.Node.H = (int)(heuristicEstimateValue * (
                            Math.Abs(Start.WorldPos.X - End.WorldPos.X) +
                            Math.Abs(Start.WorldPos.Y - End.WorldPos.Y) +
                            Math.Abs(Start.WorldPos.Z - End.WorldPos.Z))
                            );
            parentNode.Node.F = parentNode.Node.G + parentNode.Node.H;
            parentNode.Node.PX = parentNode.WorldPos.X;
            parentNode.Node.PY = parentNode.WorldPos.Y;
            parentNode.Node.PZ = parentNode.WorldPos.Z;
                        
            openQueue.Clear();
            closeList.Clear();

            openQueue.Push(parentNode);
            bool found = false;
            int iter = 0;
            while (openQueue.Count > 0 || iter < iterations)
            {
                iter++;
                parentNode = openQueue.Pop();

                if (parentNode.Id == End.Id)
                {
                    closeList.Add(parentNode);
                    found = true;
                    break;
                }

                foreach (int var in parentNode.NeightBorWaypointsId)
                {
                    Waypoint way = map.Waypoints.IdWaypoint[var];

                    if (closeList.Contains(way))
                        continue;
                           
                    float costToCross;
                    if (CostToCross != null)
                        costToCross = CostToCross(way.WayType);
                    else
                        costToCross = CostToCrossImplementation(way.WayType);
                    float newG = parentNode.Node.G + Vector3.Distance(way.WorldPos, parentNode.WorldPos) * costToCross;
                    

                    if (newG == parentNode.Node.G)
                    {
                        continue;
                    }

                    int foundInOpenIndex = -1;

                    for (int j = 0; j < openQueue.Count; j++)
                    {
                        ///ve se ele ta na open list
                        if (openQueue[j].WorldPos.Equals(way.WorldPos))
                        {
                            foundInOpenIndex = j;

                            break;
                        }
                    }
                    ///se tiver na openlist e o custo pelo atual caminho for maior entao descarta
                    if (foundInOpenIndex != -1 &&   openQueue[foundInOpenIndex].Node.G <= newG)
                    {
                        continue;
                    }

                    way.Node.PX = parentNode.WorldPos.X;
                    way.Node.PY = parentNode.WorldPos.Y;
                    way.Node.PZ = parentNode.WorldPos.Z;
                    way.Node.G = newG;
                    way.Node.parentId = parentNode.Id;

                    way.Node.H = heuristicEstimateValue * (
                        Math.Abs(way.WorldPos.X - End.WorldPos.X) +
                        Math.Abs(way.WorldPos.Y - End.WorldPos.Y) +
                        Math.Abs(way.WorldPos.Z - End.WorldPos.Z)
                        );

                    way.Node.F = way.Node.G + way.Node.H;

                    openQueue.Push(way);
                }
                closeList.Add(parentNode);
                continue;
                //break;
            }



            if (found)
            {
                ended = true;
                Waypoint fNode = closeList[closeList.Count - 1]; //objetivo

                //limpa a lista
                for (int i = closeList.Count - 1; i >= 0; i--)
                {
                    if ((fNode.Node.PZ == closeList[i].Node.Z && fNode.Node.PX == closeList[i].Node.X && fNode.Node.PY == closeList[i].Node.Y) || i == closeList.Count - 1)
                    {
                        fNode = closeList[i];
                    }
                    else
                    {
                        closeList.RemoveAt(i);
                    }
                }
                Waypoint ww;

                LinkedList<Waypoint> w = new LinkedList<Waypoint>();
                w.AddFirst(End);
                ww = map.Waypoints.IdWaypoint[End.Node.parentId];
                while (true)
                {
                    if (ww.Equals(Start))
                    {
                        w.AddFirst(ww);
                        break;
                    }
                    else
                    {
                        w.AddFirst(ww);
                        ww = map.Waypoints.IdWaypoint[ww.Node.parentId];
                    }

                }
                return w;

            }
            else if (iterations != float.MaxValue)
            {
                ended = false;
                Waypoint fNode = closeList[closeList.Count - 1]; //objetivo
                Waypoint ww;
                LinkedList<Waypoint> w = new LinkedList<Waypoint>();
                w.AddFirst(End);
                ww = map.Waypoints.IdWaypoint[End.Node.parentId];
                while (true)
                {
                    if (ww.Equals(Start))
                    {
                        w.AddFirst(ww);
                        break;
                    }
                    else
                    {
                        w.AddFirst(ww);
                        ww = map.Waypoints.IdWaypoint[ww.Node.parentId];
                    }

                }
                return w;
            }
            else
            {
                ended = false;
                return null;
            }
        }

        /// <summary>
        /// Gets or sets the map.
        /// </summary>
        /// <value>
        /// The map.
        /// </value>
        public IMap Map
        {
            get
            {
                return map;
            }
            set
            {
                this.map = value;
            }
        }

        #endregion
    }
}
