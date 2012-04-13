using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.IA;

namespace Settlers.BlackBoardControl.GOAP.Actions
{
        public class Build : PloobsEngine.IA.Action
        {
            public Build(Dictionary<String,int> resourcesNeeded, String houseName, int cost)
            {
                this.Cost = cost;
                Name = "Build";                
                this.houseName = houseName;
                this.resourcesNeeded = resourcesNeeded;
            }
            Dictionary<String, int> resourcesNeeded;
            String houseName;

            public override bool ProceduralPreConditions(WorldState WorldState)
            {
                foreach (var item in resourcesNeeded)
                {
                    if (WorldState.GetSymbolValue<int>(item.Key) < item.Value)
                        return false;
                }
                return true;
            }

            public override void ApplyEffects(PloobsEngine.IA.WorldState WorldState)
            {
                ////remove the resources
                foreach (var item in resourcesNeeded)
                {
                    WorldSymbol WorldSymbol2 = WorldState.GetSymbol(item.Key);
                    WorldSymbol2.SetSymbol<int>(WorldSymbol2.GetSymbol<int>() - item.Value);
                    WorldState.SetSymbol(WorldSymbol2);
                }


                ///add one house
                WorldSymbol WorldSymbol = WorldState.GetSymbol(houseName);
                if(WorldSymbol == null)
                {
                    WorldSymbol = new WorldSymbol(houseName, 1);
                    WorldState.SetSymbol(WorldSymbol);
                }
                else
                {
                    int val = WorldSymbol.GetSymbol<int>();
                    WorldSymbol.SetSymbol(val + 1);                    
                    WorldState.SetSymbol(WorldSymbol);
                }                

                base.ApplyEffects(WorldState);
            }

        }
    }
