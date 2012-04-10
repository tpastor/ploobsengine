using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agent.Test;
using PloobsEngine.IA;
using PloobsEngine.TestSuite;

namespace Agent
{
    [TesteAlgorithmClass]
    public class PlannerTest
    {
           [TesteAlgorithmMethod]
        public void Teste()
        {
                AstarPlanner Planner = new AstarPlanner();

                Planner.Actions.Add(new KillAction());
                Planner.Actions.Add(new BeProtectedAction());
                Planner.Actions.Add(new BeArmedAction());
                Planner.Actions.Add(new GoToAction("lugar0", "lugar1",2));
                Planner.Actions.Add(new GoToAction("lugar1", "lugar2", 2));
                Planner.Actions.Add(new GoToAction("lugar2", "lugar1", 2));
                Planner.Actions.Add(new GoToAction("lugar1", "lugar0", 2));


                WorldState TestWorldState = new WorldState();
                TestWorldState.SetSymbol(new WorldSymbol("Armed", false));
                TestWorldState.SetSymbol(new WorldSymbol("Place", "lugar0"));
                TestWorldState.SetSymbol(new WorldSymbol("Protected", false));
                TestWorldState.SetSymbol(new WorldSymbol("EnemyKilled", false));

                PlanSet PlanSet = Planner.CreatePlan(
                    TestWorldState,
                    new TesteGoal()
                    );

                System.Diagnostics.Debug.Assert(PlanSet != null);
            

        }
    }
}
