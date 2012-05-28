using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.IA
{
    public class BasicPlanner : IPlanner
    {

        public override PlanSet CreatePlan(WorldState actual, Goal destiny)
        {
            PlanSet PlanSet = new PlanSet();
                        
            WorldState current = actual.Clone();
            int iter = 0;
            while (!destiny.Evaluate(current))
            {
                Action act = null;
                foreach (var item in Actions)
                {
                    if (item.GetPreConditions(current).isCompatibleSource(current))
                    {
                        if (item.ProceduralPreConditions(current))
                        {
                            act = item;
                            break;
                        }
                    }
                }

                if (act != null)
                {
                    foreach (var item in act.GetEffects(current).GetSymbols())
                    {
                        current.SetSymbol(item.Clone());
                    }
                    act.ApplyEffects(current);
                }
                else
                {
                    return null;
                }
                iter++;
                if (iter > MaxIteration)
                    return null;
                
                System.Diagnostics.Debug.WriteLine(act.Name);
            }
            System.Diagnostics.Debug.WriteLine(destiny.Name);
           return PlanSet;
        }
    }
}
