using System;
using System.Collections.Generic;

namespace PloobsEngine.IA
{
    public abstract class IPlanner
    {
        public IPlanner()
        {
            MaxIteration = 250;
        }

        public List<Action> Actions = new List<Action>();
        
        public abstract PlanSet CreatePlan(WorldState actual, Goal destiny);

        /// <summary>
        /// Default 250
        /// </summary>
        public int MaxIteration
        {
            get;
            set;
        }
    }
}
