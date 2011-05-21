using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.IA
{
    public class IANodeComparer : IComparer<Waypoint>
    {
        public int Compare(Waypoint nodeA, Waypoint nodeB)
        {
            if (nodeA.Node.F > nodeB.Node.F)
                return 1;
            else if (nodeA.Node.F < nodeB.Node.F)
                return -1;
            return 0;
        }
    }
}
