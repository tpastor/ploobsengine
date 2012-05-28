using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.IA;

namespace Settlers.BlackBoardControl.GOAP
{
    public class GoalPlanning
    {
        public GoalPlanning(BlackBoard BlackBoard)
        {
            this.BlackBoard = BlackBoard;
        }

        BlackBoard BlackBoard;

        public PlanSet GetPlan(Goal goal, List<PloobsEngine.IA.Action> Actions)
        {
                AstarPlanner Planner = new AstarPlanner();
                Planner.MaxIteration = int.MaxValue;
                Planner.Actions = Actions;
                WorldState WorldState = BlackBoard.GetEntry<WorldState>("WorldState");
                                                           
                PlanSet PlanSet = Planner.CreatePlan(
                    WorldState,
                    goal
                    );

                return PlanSet;
        }


    }
}
