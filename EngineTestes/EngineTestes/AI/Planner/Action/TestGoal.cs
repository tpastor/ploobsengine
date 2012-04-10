using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.IA;

namespace Agent.Test
{
    public class TesteGoal : Goal
    {
        public TesteGoal()
        {
            Name = "Goal: killed = true";
            //this.WorldState = new WorldState();
            //this.WorldState.SetSymbol(new WorldSymbol("EnemyKilled", true));
        }

        public override int GetHeuristic(WorldState WorldState)
        {
            if (WorldState.GetSymbol("EnemyKilled").GetSymbol<bool>() == false)
                return 1;
            return 0;
        }

        public override bool Evaluate(WorldState state)
        {
            if (state.GetSymbolValue<Boolean>("EnemyKilled") == true)
                return true;
            return false;
        }
    }
}
