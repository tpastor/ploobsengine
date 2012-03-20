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
                
            WorldState goal = destiny.WorldState.Clone();

            WorldState current = actual.Clone();            

            while (!destiny.WorldState.isCompatibleSource(current))
            {
                Action act = null;
                foreach (var item in Actions)
                {
                    if (item.GetPreConditions().isCompatibleSource(current))
                    {
                        if (item.ProceduralPreConditions())
                        {
                            act = item;
                            break;
                        }
                    }
                }

                if (act != null)
                {
                    foreach (var item in act.GetEffects().GetSymbols())
                    {
                        current.SetSymbol(item.Clone());
                    }
                    act.ApplyEffects();
                }
                else
                {
                    return null;
                }

                
                System.Diagnostics.Debug.WriteLine(act.Name);
            }
            System.Diagnostics.Debug.WriteLine(destiny.Name);
           return PlanSet;
        }
    }
}
