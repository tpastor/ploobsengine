using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.IA;

namespace Settlers
{
    public class GoalAttack : Goal
    {
        public override bool Evaluate(WorldState state)
        {
            return state.GetSymbolValue<bool>("attack") == true;
            //return state.GetSymbolValue<int>("gold") > 6 && state.GetSymbolValue<int>("wood") > 6;
            //return state.GetSymbolValue<int>("warrior") == 4;
        }

        public override int GetHeuristic(WorldState WorldState)
        {
            //if (WorldState.GetSymbol("attack").GetSymbol<bool>() == false)
                return 1;
            //return 0;
        }

    }
}
