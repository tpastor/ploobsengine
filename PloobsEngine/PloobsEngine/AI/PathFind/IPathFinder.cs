using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.IA
{
    public interface IPathFinder
    {               

        LinkedList<Waypoint> GetPath(AIWorkType type,Waypoint Start,Waypoint End);

        IMap Map
        {
            get;
            set;
        }

    }
    public enum AIWorkType
    {
        ITERATION,SINGLE_TIME
    }
}
