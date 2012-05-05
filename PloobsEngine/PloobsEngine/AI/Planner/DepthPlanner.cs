using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.IA
{
    /// <summary>
    /// not thead safe
    /// </summary>
    public class DepthPlanner : IPlanner
    {
        int iter = 0;
        private bool depthSearch(WorldState WorldState)
        {
            if (destiny.Evaluate(WorldState) == true)
                return true;
            
            List<Action> acts = new List<Action>();
            foreach (var item in Actions)
            {
                if (item.GetPreConditions(WorldState).isCompatibleSource(WorldState))
                {
                    if (item.ProceduralPreConditions(WorldState))
                    {
                        acts.Add(item);
                    }
                }
            }

            if (acts.Count == 0)
                return false;
                        
            foreach (var item in acts)
            {
                 WorldState ws = WorldState.Clone();
                 foreach (var item2 in item.GetEffects(WorldState).GetSymbols())
                 {
                        ws.SetSymbol(item2.Clone());
                 }
                 item.ApplyEffects(ws);
                 
                 iter++;
                 if (iter > MaxIteration)
                     return false;

                 if (depthSearch(ws) == true)
                 {
                     act.AddFirst(item);
                     return true;
                 }
            }            

            return false;
        }

        Goal destiny;
        LinkedList<Action> act = new LinkedList<Action>();

        public override PlanSet CreatePlan(WorldState actual, Goal destiny)
        {
            this.destiny = destiny;
            act.Clear();
            iter = 0;
            PlanSet PlanSet =null;                        
            
            if (depthSearch(actual) == true)
            {
                PlanSet = new PlanSet();
                foreach (var item in act)
                {
                    PlanSet.Actions.Add(item);
                    System.Diagnostics.Debug.WriteLine(item.Name);
                }
                
            }
            return PlanSet;
        }

    }
}
