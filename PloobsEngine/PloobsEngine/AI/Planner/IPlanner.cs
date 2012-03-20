using System;
using System.Collections.Generic;

namespace PloobsEngine.IA
{
    public abstract class IPlanner
    {
        public List<Action> Actions = new List<Action>();
        
        public abstract PlanSet CreatePlan(WorldState actual, Goal destiny);
    }
}
