using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.IA
{
    public abstract class Goal
    {
        public abstract int GetHeuristic(WorldState WorldState );
        public abstract bool Evaluate(WorldState state);
        public String Name
        {
            get;
            set;
        }
    }
}
