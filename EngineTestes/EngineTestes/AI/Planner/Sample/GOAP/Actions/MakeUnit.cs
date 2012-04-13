using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.IA;

namespace Settlers
{
    public class MakeUnit : PloobsEngine.IA.Action
    {
        Dictionary<String, int> resourcesNeeded;
        String unitName;
        String HouseNeeded;
        public MakeUnit(String unitName,String HouseNeeded,Dictionary<String,int> resourcesNeeded)
        {
            this.HouseNeeded = HouseNeeded;
            this.unitName = unitName;
            this.Cost = 1;
            Name = "MakeUnit";
            this.resourcesNeeded = resourcesNeeded;
        }

        public override bool ProceduralPreConditions(WorldState WorldState)
        {
            foreach (var item in resourcesNeeded)
            {
                if (WorldState.GetSymbolValue<int>(item.Key) < item.Value)
                    return false;
            }

            if (WorldState.GetSymbol(HouseNeeded) == null || WorldState.GetSymbolValue<int>(HouseNeeded) == 0)
            {
                return false;
            }

            return true;
        }

        public override void ApplyEffects(WorldState WorldState)
        {
            ///add one unit
            WorldSymbol WorldSymbol = WorldState.GetSymbol(unitName);
            if (WorldSymbol == null)
                WorldSymbol = new WorldSymbol(unitName, 0);

            WorldSymbol.SetSymbol<int>(WorldSymbol.GetSymbol<int>() + 1);
            WorldState.SetSymbol(WorldSymbol);

            ////remove the resources
            foreach (var item in resourcesNeeded)
            {
                WorldSymbol WorldSymbol2 = WorldState.GetSymbol(item.Key);
                WorldSymbol2.SetSymbol<int>(WorldSymbol2.GetSymbol<int>() - item.Value);
                WorldState.SetSymbol(WorldSymbol2);                        
            }
           
        }

    }
}
