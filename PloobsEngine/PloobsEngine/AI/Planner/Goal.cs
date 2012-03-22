using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.IA
{
    public abstract class Goal
    {
        public WorldState WorldState
        {
            set;
            get;
        }

        public abstract bool Evaluate(WorldState state);
        public String Name
        {
            get;
            set;
        }
    }
}
