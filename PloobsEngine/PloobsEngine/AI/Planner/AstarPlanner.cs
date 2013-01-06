using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.DataStructure;
using System;
using System.Diagnostics;

namespace PloobsEngine.IA
{
    class pathrecnode 
    {
        public float f, g, h;
        public List<Action> act = new List<Action>();
        public WorldState WorldState;

        public override string ToString()
        {
            StringBuilder resp = new StringBuilder();
            resp.AppendLine("Current State");
            resp.AppendLine("f " + f );
            resp.AppendLine("Actions:");
            foreach (var item in act)
            {
                resp.AppendLine(item.ToString());
            }
            resp.AppendLine("WorldState:");
            resp.AppendLine(WorldState.ToString());
            return resp.ToString();
        }
    }

    class comparer : IComparer<pathrecnode>
    {
        public int Compare(pathrecnode x, pathrecnode y)
        {
            if (x.f > y.f)
                return 1;
            if (x.f < y.f)
                return -1;
            return 0;
        }
    }

    public class AstarPlanner : IPlanner
    {
        public override PlanSet CreatePlan(WorldState actual, Goal destiny)
        {
            int iter = 0;
            PlanSet PlanSet = new PlanSet();
            PriorityQueueB<pathrecnode> processing = new PriorityQueueB<pathrecnode>( new comparer());
            List<WorldState> close = new List<WorldState>();

            processing.Push(
                new pathrecnode()
                {
                    act = new List<Action>(),
                    WorldState = actual,
                    f = 0,
                    g = 0,
                    h = 0,                    
                }
                );

            pathrecnode current = null;
            while (processing.Count != 0)
            {
                current = processing.Pop(); 

                if (destiny.Evaluate(current.WorldState) == true)
                    break;

                if (close.Contains(current.WorldState))
                {
                    continue;
                }
                else
                {
                    close.Add(current.WorldState);
                }

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
                    
                    pathrecnode pathrec = new pathrecnode();
                    pathrec.WorldState = ws;
                    pathrec.act = new List<Action>(current.act.ToArray());
                    pathrec.act.Add(item);
                    pathrec.g += 1 + item.Cost;
                    pathrec.h = destiny.GetHeuristic(pathrec.WorldState);
                    //pathrec.WorldState.GetHeuristic(destiny.WorldState);
                    pathrec.f = pathrec.g + pathrec.h; 
                    processing.Push(pathrec);                   

                }

              
                iter++;
                if (iter > MaxIteration)
                    return null;
#if !WINRT
                Debug(processing, iter);
#endif
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

#if !WINRT
        [Conditional("DEBUG")]
        void Debug(PriorityQueueB<pathrecnode> processing, int iter)
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Iteration " + iter);
            for (int i = 0; i < processing.Count; i++)
            {
                Console.WriteLine(processing[i].ToString());
            }
        }
#endif

    }
}
