using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.IA
{
    class pathrec 
    {
        public List<Action> act = new List<Action>();
        public WorldState WorldState;
    }

    public class WidthPlanner : IPlanner
    {
        public override PlanSet CreatePlan(WorldState actual, Goal destiny)
        {

            PlanSet PlanSet = new PlanSet();
            LinkedList<pathrec> processing = new LinkedList<pathrec>();

            processing.AddLast(
                new pathrec()
                {
                    act = new List<Action>(),
                    WorldState = actual
                }
                );

            int iter = 0;
            pathrec current = null;
            while (processing.Count != 0)
            {
                current = processing.First.Value;
                processing.RemoveFirst();

                if (destiny.Evaluate(current.WorldState) == true)
                    break;

                List<Action> acts = new List<Action>();
                foreach (var item in Actions)
                {
                    if (item.GetPreConditions(current.WorldState).isCompatibleSource(current.WorldState))
                    {
                        if (item.ProceduralPreConditions(current.WorldState))
                        {
                            acts.Add(item);
                        }
                    }
                }
                
                foreach (var item in acts)
                {
                    WorldState ws = current.WorldState.Clone();
                    foreach (var item2 in item.GetEffects(current.WorldState).GetSymbols())
                    {
                        ws.SetSymbol(item2.Clone());
                    }
                    item.ApplyEffects(ws);

                    System.Diagnostics.Debug.WriteLine(item.Name);

                    pathrec pathrec = new pathrec();
                    pathrec.WorldState = ws;
                    pathrec.act = new List<Action>(current.act.ToArray());
                    pathrec.act.Add(item);
                    processing.AddLast(pathrec);                    
                }

                System.Diagnostics.Debug.WriteLine("---START");                
                foreach (var item in processing)
                {
                    System.Diagnostics.Debug.WriteLine("-<>-");
                    foreach (var item2 in item.act)
                    {
                        System.Diagnostics.Debug.WriteLine(item2.Name);    
                    }
                    
                }
                System.Diagnostics.Debug.WriteLine("---END");

                iter++;
                if (iter > MaxIteration)
                    return null;

            }

            if (current != null)
            {
                foreach (var item in current.act)
                {
                    PlanSet.Actions.Add(item);
                    System.Diagnostics.Debug.WriteLine(item.Name);
                }
            }

            
            return PlanSet;
        }

    }
}
