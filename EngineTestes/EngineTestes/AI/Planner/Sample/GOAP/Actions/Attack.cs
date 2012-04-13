using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.IA;

namespace Settlers
{
    public class Attack : PloobsEngine.IA.Action
    {
        Dictionary<String, int> unitsNeeded;
        public Attack(Dictionary<String,int> unitsNeeded)
        {
            this.unitsNeeded = unitsNeeded;
            this.Cost = 1;
            Name = "attack";
            this.preWorldState.SetSymbol(new WorldSymbol("attack", false));
            this.effectWorldState.SetSymbol(new WorldSymbol("attack", true));
        }

        public override bool ProceduralPreConditions(WorldState WorldState)
        {
            foreach (var item in unitsNeeded)
            {
                WorldSymbol WorldSymbol =  WorldState.GetSymbol(item.Key);
                if(WorldSymbol == null || WorldSymbol.GetSymbol<int>() < item.Value)
                    return false;
            }
            return true;
        }

    }
}
